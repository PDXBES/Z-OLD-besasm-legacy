using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Core.Layers;

namespace PumpCycleDataAnalyzer
{
    public partial class Form1 : Form
    {
        struct CycleDetailInfo
        {
            public string psName;
            public DateTime startDate;
            public DateTime endDate;
            public double wwVolume;
        }

        public Form1()
        {
            InitializeComponent();
            ultraGrid1.DataSource = pumpCycleDS;
            ultraGrid1.DataMember = "Calculated";
            ultraGrid2.DataSource = pumpCycleDS;
            ultraGrid2.DataMember = "Raw";
            ultraGrid2.Height = 480;
            ultraGrid2.Width = 640;
            ultraPopupControlContainer1.PopupControl = ultraGrid2;
            ultraDropDownButton1.PopupItem = ultraPopupControlContainer1;
            txtPumpThreshold.Value = 15;
            try
            {
                ConnectToNeptune();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to Neptune, Database analysis will be unavailable: " + ex.Message);
            }

            AxisItem axisX;
            axisX = ultraChart1.CompositeChart.ChartAreas[0].Axes[0];
            AxisAppearance axisXAppearance;
            axisXAppearance = (AxisAppearance)axisX;
            axisXAppearance.ScrollScale.Visible = true;
        }

        private void btnLoadCycleDetail_Click(object sender, EventArgs e)
        {
            CycleDetailInfo cdi;

            cdi = LoadRawData(txtCycleDetail.Text);
            txtPSName.Value = cdi.psName;
            txtStartDate.Value = cdi.startDate;
            txtEndDate.Value = cdi.endDate;
            txtWWVolume.Value = cdi.wwVolume;
            CalculateFlows(cdi, false);
            UpdateExcludedValues(Convert.ToDouble(txtPumpThreshold.Value) / 100.0);
        }

        private void txtPumpThreshold_ValueChanged(object sender, EventArgs e)
        {
            UpdateExcludedValues(Convert.ToDouble(txtPumpThreshold.Value) / 100.0);

            trackBar1.Value = Convert.ToInt32(txtPumpThreshold.Value);
            ultraGrid1.Refresh();

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            txtPumpThreshold.Value = trackBar1.Value;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            DateTime previousTime;
            DateTime currentTime;

            TimeSpan uniformDelta;
            uniformDelta = new TimeSpan(0, Convert.ToInt32(txtUniformInterval.Value), 0);

            TimeSpan minDelta;
            minDelta = new TimeSpan(long.MaxValue);

            PumpCycleDataSet.CalculatedRow[] rows;
            rows = (PumpCycleDataSet.CalculatedRow[])pumpCycleDS.Calculated.Select("", "TimeAvg ASC");

            previousTime = ((PumpCycleDataSet.CalculatedRow)rows[0]).TimeAvg;// PumpCycleDataSet.Calculated[0].TimeAvg;

            toolStripProgressBar1.Maximum = rows.Length;
            toolStripProgressBar1.Minimum = 1;
            toolStripProgressBar1.Value = 1;
            for (int i = 1; i < rows.Length; i++)
            {
                toolStripProgressBar1.Increment(1);
                PumpCycleDataSet.CalculatedRow calcRow;
                calcRow = (PumpCycleDataSet.CalculatedRow)rows[i];
                if (calcRow.Flagged)
                {
                    continue;
                }
                currentTime = calcRow.TimeAvg;

                if (minDelta.CompareTo(currentTime.Subtract(previousTime)) > 0)
                {
                    minDelta = currentTime.Subtract(previousTime);
                }
                previousTime = currentTime;
            }

            int minutes;
            minutes = 60 / (int)Math.Ceiling((60.0 / minDelta.TotalMinutes));
            minutes = Math.Min(minutes, (int)uniformDelta.TotalMinutes);
            minDelta = new TimeSpan(minDelta.Hours, minutes, 0);

            DateTime startTime, endTime;
            startTime = rows[0].TimeAvg; // PumpCycleDataSet.Calculated[0].TimeAvg;//.Add(PumpCycleDataSet.Calculated[0].TimeAvg.Subtract(minDelta));            
            startTime = RoundUpToInterval(startTime, minutes);
            //startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, minutes * (int)(startTime.Minute / (int)Math.Floor(minutes / (double)startTime.Minute)), 0);
            endTime = rows[rows.Length - 1].TimeAvg; // PumpCycleDataSet.Calculated[PumpCycleDataSet.Calculated.Count - 1].TimeAvg;
            endTime = RoundDownToInterval(endTime, minutes);
            pumpCycleDS.Distributed.Clear();
            pumpCycleDS.Distributed.BeginLoadData();

            toolStripProgressBar1.Maximum = (int)endTime.Subtract(startTime).TotalMinutes;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Value = 0;

            //previousTime = new DateTime(0);
            int lesserIndex = 0;
            int previousLesserIndex = 0;

            for (DateTime j = startTime; j <= endTime; j += minDelta)
            {
                toolStripProgressBar1.Increment((int)minDelta.TotalMinutes);

                //PumpCycleDataSet.CalculatedRow[] foundRows;
                PumpCycleDataSet.CalculatedRow lesserRow, greaterRow;
                DateTime lesserTime, greaterTime;
                double lesserWeight, greaterWeight;
                double lesserFlow, greaterFlow, weightedFlow;

                bool flagged, multi;

                if (j >= rows[lesserIndex + 1].TimeAvg)
                {
                    lesserIndex++;
                }


                //id = (int)PumpCycleDataSet.Calculated.Compute("Max(ID)", "TimeAvg < #" + j.ToString() + "# AND TimeAvg > #" + previousTime.ToString() + "#");
                //id = foundRows[0].ID;
                lesserRow = rows[lesserIndex];// PumpCycleDataSet.Calculated.FindByID(id);                
                lesserTime = lesserRow.TimeAvg;

                greaterRow = rows[lesserIndex + 1]; // PumpCycleDataSet.Calculated.FindByID(id + 1);
                greaterTime = greaterRow.TimeAvg;

                lesserFlow = lesserRow.InflowRate;
                greaterFlow = greaterRow.InflowRate;
                lesserWeight = 1 - (j.Subtract(lesserTime).Ticks) / (double)(greaterTime.Subtract(lesserTime).Ticks);
                greaterWeight = 1 - lesserWeight;

                weightedFlow = lesserFlow * lesserWeight + greaterFlow * greaterWeight;
                flagged = lesserRow.Flagged | greaterRow.Flagged;
                multi = lesserRow.Multi | greaterRow.Multi;
                pumpCycleDS.Distributed.AddDistributedRow(j, lesserTime, greaterTime, lesserWeight, greaterWeight, lesserFlow, greaterFlow, weightedFlow, multi, flagged, 0, 0);
                //previousTime = lesserTime;
                previousLesserIndex = lesserIndex;
            }
            pumpCycleDS.Distributed.EndLoadData();

            StreamWriter stream;
            stream = new StreamWriter(saveFileDialog1.FileName);
            stream.WriteLine("DateTime,Flow,Multi,Flagged");

            for (DateTime j = RoundUpToInterval(startTime, uniformDelta.Minutes); j < endTime; j += uniformDelta)
            {
                double avgFlow;
                string exp;
                bool flagged, multi;
                exp = "DateTime <= #" + j.Add(new TimeSpan(uniformDelta.Ticks / 2)).ToString() + "# AND DateTime > #" + j.Subtract(new TimeSpan(uniformDelta.Ticks / 2)).ToString() + "#";
                avgFlow = (double)pumpCycleDS.Distributed.Compute("Avg(WeightedFlow)", exp);
                flagged = (bool)pumpCycleDS.Distributed.Compute("Max(Flagged)", exp);
                multi = (bool)pumpCycleDS.Distributed.Compute("Max(Multi)", exp);
                stream.WriteLine(j.ToString() + "," + avgFlow + "," + (multi ? "1" : "0") + "," + (flagged ? "1" : "0"));
            }

            stream.Close();

            toolStripProgressBar1.Value = 0;
            return;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCycleDetail.Value = openFileDialog1.FileName;
            }
        }

        private void ultraDropDownButton1_DroppingDown(object sender, CancelEventArgs e)
        {
            ultraGrid2.Enabled = true;
        }

        private void ultraDropDownButton1_ClosedUp(object sender, EventArgs e)
        {
            ultraGrid2.Enabled = false;
        }

        private void btnRecalculate_Click(object sender, EventArgs e)
        {
            RecalculateFlows();
            UpdateExcludedValues(Convert.ToDouble(txtPumpThreshold.Value) / 100.0);
        }

        private DateTime RoundUpToInterval(DateTime dt, int minutes)
        {
            minutes = minutes == 0 ? 60 : 0;

            int actualMinutes;
            int frac;
            actualMinutes = dt.Minute;
            frac = (int)Math.Ceiling(actualMinutes / (double)minutes);
            int hour;
            if (frac == 1)
            {
                hour = dt.Hour + 1;
                minutes = 0;
            }
            else
            {
                hour = dt.Hour;
            }
            return new DateTime(dt.Year, dt.Month, dt.Day, hour, frac * minutes, 0);
        }

        private DateTime RoundDownToInterval(DateTime dt, int minutes)
        {
            minutes = minutes == 0 ? 60 : 0;

            int actualMinutes;
            int frac;
            actualMinutes = dt.Minute;
            frac = (int)Math.Floor(actualMinutes / (double)minutes);
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, frac * minutes, 0);
        }

        private CycleDetailInfo LoadRawData(string cycleDetail)
        {
            string PSName;
            double WWVolume;
            DateTime StartDate;
            DateTime EndDate;


            if (!File.Exists(cycleDetail))
            {
                MessageBox.Show("Cycle detail file not found.");
                return new CycleDetailInfo();
            }

            StreamReader stream;
            stream = new StreamReader(cycleDetail);
            string line;
            string[] tokens;
            line = stream.ReadLine();
            if (line != "\"FLOWS AT PUMP STATION\"")
            {
                MessageBox.Show("File does not appear to contain cycle detail.");
                return new CycleDetailInfo();
            }
            line = stream.ReadLine();
            line = line.Replace("\"Station:", "");
            line = line.Trim(new char[] { ' ', '"' });
            PSName = line;
            line = stream.ReadLine();
            line = stream.ReadLine();
            tokens = line.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            WWVolume = Convert.ToDouble(tokens[0]);

            line = stream.ReadLine();
            line = line.Replace("\"Dates: ", "");
            tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            StartDate = Convert.ToDateTime(tokens[0].Remove(10)).AddHours(Convert.ToDouble(tokens[0].Substring(11)));
            EndDate = Convert.ToDateTime(tokens[1].Remove(10)).AddHours(Convert.ToDouble(tokens[1].Substring(11)));

            //tokens = line.Split(new char[] { ' ', ',', '"', }, StringSplitOptions.RemoveEmptyEntries);
            //StartDate = Convert.ToDateTime(tokens[2]);

            while (!(line = stream.ReadLine()).Contains("Cycle #"))
            {
                if (line == null || line == "")
                {
                    MessageBox.Show("Could not find cycle data.");
                    return new CycleDetailInfo();
                }
            }

            pumpCycleDS.Raw.BeginInit();
            pumpCycleDS.Raw.Clear();
            while ((line = stream.ReadLine()) != "")
            {
                tokens = line.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                pumpCycleDS.Raw.AddRawRow(
                    Convert.ToInt32(tokens[0]),
                    Convert.ToInt32(tokens[1]),
                    Convert.ToInt32(tokens[2]),
                    Convert.ToInt32(tokens[3]),
                    Convert.ToDouble(tokens[4]),
                    Convert.ToDouble(tokens[5]),
                    Convert.ToDouble(tokens[6]),
                    Convert.ToDouble(tokens[7]),
                    Convert.ToDouble(tokens[8]),
                    Convert.ToBoolean(Convert.ToInt32(tokens[9])));
            }
            pumpCycleDS.Raw.EndInit();

            CycleDetailInfo detailInfo;
            detailInfo = new CycleDetailInfo();
            detailInfo.psName = PSName;
            detailInfo.wwVolume = WWVolume;
            detailInfo.startDate = StartDate;
            detailInfo.endDate = EndDate;

            return detailInfo;
        }

        private void CalculateFlows(CycleDetailInfo cdi, bool append)
        {
            if (pumpCycleDS.Raw.Count == 0)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            pumpCycleDS.Calculated.BeginLoadData();
            if (!append)
            {
                pumpCycleDS.Calculated.Clear();
            }

            DateTime timeOn, timeOff, timeAvg;
            DateTime previousTime, startTime;

            double pumpDownTime, fillTime;
            double inflowRate, pumpRate;
            double wetwellVolume;
            int pumpOnDay, pumpOffDay;

            pumpOnDay = 0;
            pumpOffDay = 0;
            startTime = cdi.startDate;// Convert.ToDateTime(txtStartDate.Value);
            wetwellVolume = cdi.wwVolume; //Convert.ToDouble(txtWWVolume.Value);

            PumpCycleDataSet.RawRow previousRow = pumpCycleDS.Raw[0];
            previousTime = startTime.AddDays(previousRow.TimeOff / (60 * 60 * 24.0));

            int[] pumpCount = { 0, 0, 0, 0 };
            double[] pumpRateTally = { 0, 0, 0, 0 };
            int pumpNum = 0;

            toolStripProgressBar1.Maximum = pumpCycleDS.Raw.Count;
            toolStripProgressBar1.Minimum = 1;
            toolStripProgressBar1.Value = 1;

            for (int i = 0; i < pumpCycleDS.Raw.Count; i++)
            {
                toolStripProgressBar1.Increment(1);

                PumpCycleDataSet.RawRow rawRow;
                rawRow = pumpCycleDS.Raw[i];

                if (rawRow.TimeOn < previousRow.TimeOn)
                {
                    pumpOnDay++;
                }
                if (rawRow.TimeOff < previousRow.TimeOff)
                {
                    pumpOffDay++;
                }

                timeOn = startTime.AddDays(pumpOnDay + rawRow.TimeOn / (60 * 60 * 24.0));
                timeOff = startTime.AddDays(pumpOffDay + rawRow.TimeOff / (60 * 60 * 24.0));
                DateTime alternativeTimeOff;
                alternativeTimeOff = timeOn + new TimeSpan(0, 0, rawRow.PumpRun);
                if (timeOff != alternativeTimeOff)
                {
                    int p = 0;
                }
                pumpDownTime = timeOff.Subtract(timeOn).TotalDays * 60 * 24.0;
                fillTime = timeOn.Subtract(previousTime).TotalDays * 60 * 24.0;

                timeAvg = new DateTime((timeOn.Ticks + previousTime.Ticks) / 2);
                inflowRate = wetwellVolume / fillTime;
                pumpRate = (wetwellVolume + inflowRate * pumpDownTime) / pumpDownTime;

                if (rawRow.Pump1 != 0)
                {
                    pumpNum = 1;
                }
                else if (rawRow.Pump2 != 0)
                {
                    pumpNum = 2;
                }
                else if (rawRow.Pump3 != 0)
                {
                    pumpNum = 3;
                }
                else if (rawRow.Pump4 != 0)
                {
                    pumpNum = 4;
                }

                pumpCycleDS.Calculated.AddCalculatedRow(timeOn, timeOff, timeAvg, pumpDownTime, fillTime, inflowRate, pumpRate, rawRow.Multi, false, pumpNum);

                previousTime = timeOff;
                previousRow = rawRow;

            }

            pumpCycleDS.Calculated.EndLoadData();
            txtPumpRate1.Value = pumpCycleDS.getPump1Average();
            txtPumpRate2.Value = pumpCycleDS.getPump2Average();
            txtPumpRate3.Value = pumpCycleDS.getPump3Average();
            txtPumpRate4.Value = pumpCycleDS.getPump4Average();
            ultraGrid1.Refresh();

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;
            this.Cursor = Cursors.Default;
        }

        private void RecalculateFlows()
        {
            if (pumpCycleDS.Calculated.Count == 0)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            pumpCycleDS.Calculated.BeginLoadData();

            DateTime timeOn, timeOff, timeAvg;
            DateTime startTime;

            double pumpDownTime, fillTime;
            double inflowRate, pumpRate;
            double wetwellVolume;
            //int pumpOnDay, pumpOffDay;

            //pumpOnDay = 0;
            //pumpOffDay = 0;
            startTime = Convert.ToDateTime(txtStartDate.Value);
            wetwellVolume = Convert.ToDouble(txtWWVolume.Value);

            PumpCycleDataSet.CalculatedRow[] rows;
            rows = (PumpCycleDataSet.CalculatedRow[])pumpCycleDS.Calculated.Select("", "TimeAvg ASC");

            PumpCycleDataSet.CalculatedRow previousRow = (PumpCycleDataSet.CalculatedRow)rows[0];

            for (int i = 1; i < rows.Length; i++)
            {
                PumpCycleDataSet.CalculatedRow calcRow;
                calcRow = rows[i];

                timeOn = calcRow.TimeOn;// ststartTime.AddDays(pumpOnDay + rawRow.TimeOn / (60 * 60 * 24.0));
                timeOff = calcRow.TimeOff; // startTime.AddDays(pumpOffDay + rawRow.TimeOff / (60 * 60 * 24.0));

                pumpDownTime = timeOff.Subtract(timeOn).TotalDays * 60 * 24.0;
                fillTime = timeOn.Subtract(previousRow.TimeOff).TotalDays * 60 * 24.0;

                timeAvg = calcRow.TimeAvg; // new DateTime((timeOn.Ticks + previousTime.Ticks) / 2);
                inflowRate = wetwellVolume / fillTime;

                pumpRate = (wetwellVolume + inflowRate * pumpDownTime) / pumpDownTime;

                calcRow.BeginEdit();
                calcRow.FillTime = fillTime;
                calcRow.PumpDownTime = pumpDownTime;
                calcRow.InflowRate = inflowRate;
                calcRow.PumpRate = pumpRate;
                calcRow.AcceptChanges();
                calcRow.EndEdit();

                previousRow = calcRow;
                //PumpCycleDataSet.Calculated.AddCalculatedRow(timeOn, timeOff, timeAvg, pumpDownTime, fillTime, inflowRate, pumpRate, rawRow.Multi, false, pumpNum);                
            }

            //PumpCycleDataSet.Calculated.EndLoadData();
            txtPumpRate1.Value = pumpCycleDS.getPump1Average();
            txtPumpRate2.Value = pumpCycleDS.getPump2Average();
            txtPumpRate3.Value = pumpCycleDS.getPump3Average();
            txtPumpRate4.Value = pumpCycleDS.getPump4Average();
            ultraGrid1.Refresh();

            this.Cursor = Cursors.Default;
        }

        private int UpdateExcludedValues(double Threshold)
        {

            double[] avgPumpRate;
            avgPumpRate = new double[4];

            int excludedRows = 0;

            avgPumpRate[0] = Convert.ToDouble(txtPumpRate1.Value);
            avgPumpRate[1] = Convert.ToDouble(txtPumpRate2.Value);
            avgPumpRate[2] = Convert.ToDouble(txtPumpRate3.Value);
            avgPumpRate[3] = Convert.ToDouble(txtPumpRate4.Value);

            for (int i = 0; i < pumpCycleDS.Calculated.Count; i++)
            {
                PumpCycleDataSet.CalculatedRow row;
                row = pumpCycleDS.Calculated[i];
                row.Flagged = false;

                if (row.PumpNum == 1 &&
                    (row.PumpRate >= avgPumpRate[0] * (1 + Threshold) ||
                    row.PumpRate <= avgPumpRate[0] * (1 - Threshold)))
                {
                    row.Flagged = true;
                    excludedRows++;
                    continue;
                }
                else if (row.PumpNum == 2 &&
                    (row.PumpRate >= avgPumpRate[1] * (1 + Threshold) ||
                    row.PumpRate <= avgPumpRate[1] * (1 - Threshold)))
                {
                    row.Flagged = true;
                    excludedRows++;
                    continue;
                }
                else if (row.PumpNum == 3 &&
                    (row.PumpRate >= avgPumpRate[2] * (1 + Threshold) ||
                    row.PumpRate <= avgPumpRate[2] * (1 - Threshold)))
                {
                    row.Flagged = true;
                    excludedRows++;
                    continue;
                }
                else if (row.PumpNum == 4 &&
                    (row.PumpRate >= avgPumpRate[3] * (1 + Threshold) ||
                    row.PumpRate <= avgPumpRate[3] * (1 - Threshold)))
                {
                    row.Flagged = true;
                    excludedRows++;
                    continue;
                }
                else if (row.PumpNum == 0)
                {
                    row.Flagged = true;
                    excludedRows++;
                    continue;
                }
            }
            pumpCycleDS.Calculated.AcceptChanges();
            ultraGrid1.Refresh();

            double excludePercent;

            txtInflow.Value = pumpCycleDS.getInflowAverage();

            excludePercent = excludedRows / (double)pumpCycleDS.Calculated.Count;
            txtExludedRowCount.Value = Math.Round(excludePercent * 100.0, 1) + "% (" + excludedRows + " of " + pumpCycleDS.Calculated.Count + ")";
            return excludedRows;

        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string[] files;
            files = openFileDialog1.FileNames;

            pumpCycleDS.Calculated.Clear();
            txtStartDate.Clear();
            txtEndDate.Clear();
            txtPSName.Clear();
            txtWWVolume.Clear();

            string psName = "";
            double wwVolume = 0;
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();

            for (int i = 0; i < files.Length; i++)
            {
                CycleDetailInfo cdi;
                cdi = LoadRawData(files[i]);
                if (i == 0)
                {
                    psName = cdi.psName;
                    startDate = cdi.startDate;
                    endDate = cdi.endDate;
                    wwVolume = cdi.wwVolume;
                }
                else
                {
                    if (startDate > cdi.startDate)
                    {
                        startDate = cdi.startDate;
                    }
                    if (endDate < cdi.endDate)
                    {
                        endDate = cdi.endDate;
                    }
                    if (psName != cdi.psName)
                    {
                        MessageBox.Show("Files do not match! Primary PS Name: " + psName + "\nMismatch PS Name: " + cdi.psName + "\nIn file: " + files[i]);
                        return;
                    }
                    if (wwVolume != cdi.wwVolume)
                    {
                        MessageBox.Show("Inconsistent Wetwell Volumes!\nPrimary File: " + files[0] + "\nMismatch File: " + files[i]);
                        return;
                    }
                }
                CalculateFlows(cdi, true);
            }

            txtPSName.Value = psName;
            txtStartDate.Value = startDate;
            txtEndDate.Value = endDate;
            txtWWVolume.Value = wwVolume;
            txtCycleDetail.Text = "Multiple Cycle Details Merged...";

            RecalculateFlows();
            UpdateExcludedValues(Convert.ToDouble(txtPumpThreshold.Value) / 100.0);

            openFileDialog1.Multiselect = false;
        }

        private void ConnectToNeptune()
        {
            //PumpCycleDataSet.STATIONDataTable stationTable;
            //stationTable = new PumpCycleDataSet.STATIONDataTable();  
            //lstPumpStations.BeginUpdate();
            grdStationList.BeginUpdate();
            Cursor = Cursors.WaitCursor;
            try
            {
                PumpCycleDataSetTableAdapters.StationTableAdapter stationTA;
                stationTA = new PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.StationTableAdapter();
                pumpCycleDS.Clear();
                stationTA.Fill(pumpCycleDS.Station);

                //PumpCycleDataSet.PUMPDataTable pumpTable;
                //pumpTable = new PumpCycleDataSet.PUMPDataTable();
                PumpCycleDataSetTableAdapters.PumpTableAdapter pumpTA;
                pumpTA = new PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.PumpTableAdapter();
                pumpTA.Fill(pumpCycleDS.Pump);

                PumpCycleDataSetTableAdapters.CycleDataTableAdapter cycleTA;
                cycleTA = new PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.CycleDataTableAdapter();

                List<int> stationIDList = pumpCycleDS.Station.StationIDList();
                List<int> pumpStationIDList = pumpCycleDS.Pump.StationIDList();
                foreach (int stationID in stationIDList)
                {
                    if (!pumpStationIDList.Contains(stationID))
                    {
                        pumpCycleDS.Station.RemoveStationRow(pumpCycleDS.Station.FindByStationID(stationID));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
                grdStationList.EndUpdate();
                //lstPumpStations.EndUpdate();
            }
            return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ConnectToNeptune();
        }

        private void btnProcessCycleData_Click(object sender, EventArgs e)
        {
            int stationID = 0;
            DateTime beginDate = DateTime.Now;
            DateTime endDate = beginDate;
            try
            {
                //stationID = (int)lstPumpStations.SelectedValue;
                stationID = (int)grdStationList.Selected.Rows[0].Cells["StationID"].Value;
                beginDate = (DateTime)cmbBeginDateTime.Value;
                endDate = (DateTime)cmbEndDateTime.Value;
                if (beginDate >= endDate) { throw new Exception("Begin date is after end date."); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Fill DataSets from NEPTUNE data
            PumpCycleDataSetTableAdapters.CycleDataTableAdapter cycleTA;
            cycleTA = new PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.CycleDataTableAdapter();
            cycleTA.Fill(pumpCycleDS.CycleData, stationID, beginDate, endDate);
            if (pumpCycleDS.CycleData.Count == 0)
            {
                MessageBox.Show("No cycle data found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ultraChart1.SuspendLayout();

            pumpCycleDS.OperationsData.Clear();
            pumpCycleDS.WetWell.Clear();
            ClearChart();

            PumpCycleDataSetTableAdapters.OperationsDataTableAdapter operationsTA;
            operationsTA = new PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.OperationsDataTableAdapter();
            operationsTA.Fill(pumpCycleDS.OperationsData, stationID, beginDate, endDate);

            PumpCycleDataSetTableAdapters.WetWellTableAdapter wetWellTA;
            wetWellTA = new PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.WetWellTableAdapter();
            wetWellTA.Fill(pumpCycleDS.WetWell, stationID);

            Dictionary<int, PumpCycleSummary> pumpCycleSummaries = new Dictionary<int, PumpCycleSummary>();
            
            /*An "inconsistent" cycle occurs when a pump-on signal occurs for a pump that is already running,
             * or a pump-off signal occurs for a pump that is not running.  These should be tracked because
             * flow estimation algorithm may provide incorrect results in these cases. */
            int inconsistentCycleCount = 0; 
            double multiPumpDuration = 0;
            int runningPumpCount = 0;

            DateTime previousPumpOffTime = new DateTime();
            DateTime previousPumpOnTime = new DateTime();
            DateTime currentPumpOffTime = new DateTime();
            DateTime currentPumpOnTime = new DateTime();
            //A Stack (first-in first-out) data structure is convenient for storing pump cycles
            Stack<PumpCycleDataSet.CycleDataRow> runningPumpStack = new Stack<PumpCycleDataSet.CycleDataRow>();
            double previousPumpRate = 0;
            double currentPumpRate = 0;

            //For each time period that appears in the cycle records
            foreach (PumpCycleDataSet.CycleDataRow cycleRow in pumpCycleDS.CycleData.Select("", pumpCycleDS.CycleData.CycleChangeTimeColumn.ColumnName))
            {
                int pumpID = cycleRow.PumpID;

                PumpCycleSummary pumpCycleSummary; //Data structure contains summary stats for each pump over the duration of the analysis period
                if (!pumpCycleSummaries.ContainsKey(pumpID))
                {
                    pumpCycleSummaries.Add(pumpID, new PumpCycleSummary(pumpID));
                    pumpCycleSummaries[pumpID].firstPumpCycleTime = cycleRow.CycleChangeTime;
                }
                pumpCycleSummary = pumpCycleSummaries[pumpID];

                //Pump turning on
                if (cycleRow.OnOffState == 1)
                {
                    previousPumpOnTime = currentPumpOnTime;

                    pumpCycleSummary.pumpOnTime = cycleRow.CycleChangeTime;
                    currentPumpOnTime = cycleRow.CycleChangeTime;

                    if (pumpCycleSummary.pumpRunning == true)
                    {
                        //MessageBox.Show("Pump On signal received without Pump Off signal at time " + cycleRow.CycleChangeTime.ToString("g"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //return;                        
                        inconsistentCycleCount++;
                    }
                    else
                    {
                        runningPumpStack.Push(cycleRow);
                        pumpCycleSummary.cycleCount++;
                        WriteToRunningPumpCountChart(cycleRow.CycleChangeTime, runningPumpStack.Count);
                    }
                    pumpCycleSummary.pumpRunning = true;
                    previousPumpOffTime = currentPumpOffTime;
                }
                    //Pump turning off
                else if (cycleRow.OnOffState == 0)
                {
                    pumpCycleSummary.pumpOffTime = cycleRow.CycleChangeTime;
                    currentPumpOffTime = cycleRow.CycleChangeTime;

                    if (runningPumpStack.Count == 0 ||
                        runningPumpStack.Pop().PumpID != cycleRow.PumpID ||
                        pumpCycleSummary.pumpRunning == false)
                    {
                        pumpCycleSummary.pumpRunning = false;
                        inconsistentCycleCount++;
                        continue;
                    }

                    pumpCycleSummary.pumpOnDuration += ((TimeSpan)(pumpCycleSummary.pumpOffTime - pumpCycleSummary.pumpOnTime)).TotalDays;
                    pumpCycleSummary.pumpRunning = false;
                    WriteToRunningPumpCountChart(cycleRow.CycleChangeTime, runningPumpStack.Count);

                    double pumpDownTime;
                    double fillTime;
                    double wetWellVolume;
                    DateTime timeAvg;

                    pumpDownTime = 24 * 60.0 * pumpCycleSummary.pumpOffTime.Subtract(pumpCycleSummary.pumpOnTime).TotalDays;

                    if (chkCustomWetwellVolume.Checked)
                    {
                        wetWellVolume = Convert.ToDouble(txtLeadActiveWWVol.Value);
                    }
                    else
                    {
                        wetWellVolume = this.ReadLeadActiveWetWellVolume(cycleRow.CycleChangeTime);
                    }


                    if (runningPumpStack.Count == 0)
                    {
                        fillTime = 24 * 60.0 * pumpCycleSummary.pumpOnTime.Subtract(previousPumpOffTime).TotalDays;
                        timeAvg = new DateTime((pumpCycleSummary.pumpOffTime.Ticks + previousPumpOffTime.Ticks) / 2);
                    }
                    //HACK - Attempt to estimate lag pump active ww volume
                    else
                    {
                        wetWellVolume = 0.5 * wetWellVolume;
                        fillTime = 24 * 60.0 * pumpCycleSummary.pumpOnTime.Subtract(runningPumpStack.Peek().CycleChangeTime).TotalDays;
                        fillTime = Math.Max(fillTime, .5);
                        timeAvg = new DateTime((pumpCycleSummary.pumpOffTime.Ticks + previousPumpOnTime.Ticks) / 2);
                    }

                    double inflowRate;
                    double pumpRate;

                    if (runningPumpStack.Count == 0)
                    {
                        inflowRate = wetWellVolume / fillTime;
                        pumpRate = (wetWellVolume + inflowRate * pumpDownTime) / pumpDownTime;
                        pumpCycleSummary.cumulativePumpingRate += pumpRate;
                        previousPumpRate = pumpRate;
                    }
                    else
                    {
                        inflowRate = previousPumpRate + wetWellVolume / fillTime;
                        pumpRate = previousPumpRate + (wetWellVolume + inflowRate * pumpDownTime) / pumpDownTime;
                        //currentPumpRate = previousPumpRate;
                        //pumpRate += previousPumpRate;
                    }
                    pumpCycleSummary.maxInflow = Math.Max(pumpCycleSummary.maxInflow, inflowRate);

                    if (timeAvg >= beginDate && timeAvg <= endDate)
                    {
                        WriteToCalculatedFlowChart(timeAvg, inflowRate);
                        WriteToCalculatedPumpingRateChart(timeAvg, pumpRate);
                    }

                }
            }

            if (inconsistentCycleCount != 0)
            {
                if (MessageBox.Show("Inconsistent cycle data found for " + inconsistentCycleCount + " of " + pumpCycleDS.CycleData.Count + " cycles.  Continue reporting results?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    ClearChart();
                    return;
                }

            }

            try
            {
                Cursor = Cursors.WaitCursor;
                pumpCycleDS.DepthData.Clear();

                if (chkRetrieveWetWellLevel.Checked && endDate.Subtract(beginDate).Days <= 3)
                {
                    PumpCycleDataSetTableAdapters.DepthDataTableAdapter depthTA;
                    depthTA = new PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.DepthDataTableAdapter();
                    depthTA.Fill(pumpCycleDS.DepthData, stationID, beginDate, endDate);
                }
                WriteCycleSummaryTable(pumpCycleSummaries);
                UpdateChart(beginDate, endDate, pumpCycleSummaries);
                ultraChart1.ResumeLayout(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not create chart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            return;
        }

        private double ReadLeadActiveWetWellVolume(DateTime cycleChangeTime)
        {
            if (pumpCycleDS.WetWell.Count == 0)
            {
                throw new Exception("Wetwell Volume not found");
            }
            PumpCycleDataSet.WetWellRow wetWellRow;
            wetWellRow = (PumpCycleDataSet.WetWellRow)pumpCycleDS.WetWell.Select("StartDate <= #" + cycleChangeTime + "#", "StartDate desc")[0];
            if (wetWellRow == null)
            {
                throw new Exception("Wetwell Volume not found");
            }
            return wetWellRow.WetWellVolumeGallons;
        }

        private void WriteCycleSummaryTable(Dictionary<int, PumpCycleSummary> pumpCycleSummaries)
        {
            foreach (PumpCycleSummary pumpCycleSummary in pumpCycleSummaries.Values)
            {
                double pumpAnalysisDuration = ((TimeSpan)(pumpCycleSummary.pumpOffTime - pumpCycleSummary.firstPumpCycleTime)).TotalDays;
                double pumpOnDuration = pumpCycleSummary.pumpOnDuration;
                double pumpOffDuration = pumpAnalysisDuration - pumpOnDuration;
                double pumpOnPercent = pumpOnDuration / pumpAnalysisDuration;
                double averageCycleTime = 24.0 * 60.0 * pumpAnalysisDuration / pumpCycleSummary.cycleCount;

                int pumpRateGPM;
                pumpRateGPM = pumpCycleDS.Pump.FindByPumpID(pumpCycleSummary.pumpID).IsPumpRateGPMNull() ? 0 : pumpCycleDS.Pump.FindByPumpID(pumpCycleSummary.pumpID).PumpRateGPM;
                PumpCycleDataSet.CycleSummaryRow summaryRow;
                summaryRow = pumpCycleDS.CycleSummary.FindByPumpID(pumpCycleSummary.pumpID);
                if (summaryRow == null) { continue; }
                summaryRow.CycleCount = pumpCycleSummary.cycleCount;
                summaryRow.PumpOnDuration = pumpOnDuration;
                summaryRow.PumpOffDuration = pumpOffDuration;
                summaryRow.PumpOnPercent = pumpOnPercent;
                summaryRow.AverageCycleTime = averageCycleTime;
                summaryRow.CalculatedPumpingRateGPM = pumpCycleSummary.cumulativePumpingRate / pumpCycleSummary.cycleCount; //Need to calculate averge pumping rate here                                
            }
            return;
        }

        private void UpdateChart(DateTime beginDate, DateTime endDate, Dictionary<int, PumpCycleSummary> pumpCycleSummaries)
        {
            double maxDepth = 0;

            foreach (PumpCycleDataSet.DepthDataRow depthRow in pumpCycleDS.DepthData)
            {
                maxDepth = Math.Max(maxDepth, (double)depthRow.FinalValue);
                this.WriteToWetWellDepthChart(depthRow.AssignedSampleTime, (double)depthRow.FinalValue);
            }

            double maxInflow = 0;
            double maxPumpRate = 0;
            foreach (PumpCycleSummary summary in pumpCycleSummaries.Values)
            {
                maxInflow = Math.Max(maxInflow, summary.maxInflow);
                maxPumpRate = Math.Max(maxPumpRate, summary.calculatedPumpingRate);

            }
            maxInflow = ((int)Math.Ceiling((double)maxInflow * 1.2 / 10.0 + Double.Epsilon)) * 10;
            maxPumpRate = ((int)Math.Ceiling((double)maxPumpRate * 1.2 / 10.0 + Double.Epsilon)) * 10;

            AxisItem axisX, axisY, axisY2, axisPumpCountY, axisPumpCountX;
            axisX = ultraChart1.CompositeChart.ChartAreas[0].Axes[0];
            axisY = ultraChart1.CompositeChart.ChartAreas[0].Axes[1];
            axisY2 = ultraChart1.CompositeChart.ChartAreas[0].Axes[2];
            axisPumpCountX = ultraChart1.CompositeChart.ChartAreas[1].Axes[0];
            axisPumpCountY = ultraChart1.CompositeChart.ChartAreas[1].Axes[1];

            axisX.RangeType = AxisRangeType.Custom;
            axisX.RangeMin = beginDate.Ticks;
            axisX.RangeMax = endDate.Ticks;

            axisY.RangeType = AxisRangeType.Custom;// Automatic;
            axisY.RangeMin = 0;
            axisY.RangeMax = Math.Max(maxInflow, maxPumpRate);

            axisY2.RangeType = AxisRangeType.Automatic;
            axisX.TickmarkStyle = AxisTickStyle.Smart;
            axisY.TickmarkStyle = AxisTickStyle.Smart;
            axisY2.TickmarkStyle = AxisTickStyle.Smart;

            axisPumpCountX.RangeType = AxisRangeType.Custom;
            axisPumpCountX.RangeMin = beginDate.Ticks;
            axisPumpCountX.RangeMax = endDate.Ticks;

            axisX.TickmarkStyle = AxisTickStyle.Percentage;
            axisX.TickmarkPercentage = 10;
            axisPumpCountX.TickmarkStyle = AxisTickStyle.Percentage;
            axisPumpCountX.TickmarkPercentage = 10;

            axisPumpCountY.RangeMax = pumpCycleSummaries.Count;
            axisPumpCountY.RangeMin = 0;
            axisPumpCountY.RangeType = AxisRangeType.Custom;
            axisPumpCountY.TickmarkStyle = AxisTickStyle.DataInterval;
            axisPumpCountY.TickmarkInterval = 1;

            axisY2.RangeMax = maxDepth;
            axisY2.RangeMin = 0;
            axisY2.RangeType = AxisRangeType.Custom;
            axisY2.TickmarkStyle = AxisTickStyle.Percentage;
            axisY2.TickmarkPercentage = 10;
        }
        private void ClearChart()
        {
            NumericTimeSeries series;
            series = (NumericTimeSeries)ultraChart1.Series.FromKey("runningPumpCount");
            series.Points.Clear();
            series = (NumericTimeSeries)ultraChart1.Series.FromKey("calculatedFlow");
            series.Points.Clear();
            series = (NumericTimeSeries)ultraChart1.Series.FromKey("calculatedPumpingRate");
            series.Points.Clear();
            series = (NumericTimeSeries)ultraChart1.Series.FromKey("wetwellLevel");
            series.Points.Clear();
            ultraChart1.CompositeChart.ChartAreas[0].Axes[0].ScrollScale.Scale = 1;
            ultraChart1.CompositeChart.ChartAreas[0].Axes[0].ScrollScale.Scroll = 0;
            ultraChart1.CompositeChart.ChartAreas[1].Axes[0].ScrollScale.Scale = 1;
            ultraChart1.CompositeChart.ChartAreas[1].Axes[0].ScrollScale.Scroll = 0;

        }
        private void WriteToRunningPumpCountChart(DateTime dateTime, int runningPumpCount)
        {
            NumericTimeSeries series;
            series = (NumericTimeSeries)ultraChart1.Series.FromKey("runningPumpCount");
            series.Points.Add(new NumericTimeDataPoint(dateTime, runningPumpCount, "", false));
        }
        private void WriteToCalculatedFlowChart(DateTime dateTime, double calculatedFlow)
        {
            NumericTimeSeries series;
            series = (NumericTimeSeries)ultraChart1.Series.FromKey("calculatedFlow");
            series.Points.Add(new NumericTimeDataPoint(dateTime, calculatedFlow, "", false));
        }
        private void WriteToCalculatedPumpingRateChart(DateTime dateTime, double pumpRate)
        {
            NumericTimeSeries series;
            series = (NumericTimeSeries)ultraChart1.Series.FromKey("calculatedPumpingRate");
            series.Points.Add(new NumericTimeDataPoint(dateTime, pumpRate, "", false));
        }
        private void WriteToWetWellDepthChart(DateTime dateTime, double wetWellDepth)
        {
            NumericTimeSeries series;
            series = (NumericTimeSeries)ultraChart1.Series.FromKey("wetwellLevel");
            series.Points.Add(new NumericTimeDataPoint(dateTime, wetWellDepth, "", false));
        }


        private void cmbBeginDateTime_ValueChanged(object sender, EventArgs e)
        {
            txtAnalysisDuration.Value = ((TimeSpan)((DateTime)cmbEndDateTime.Value - (DateTime)cmbBeginDateTime.Value)).TotalDays.ToString("N1");
        }

        private void cmbEndDateTime_ValueChanged(object sender, EventArgs e)
        {
            txtAnalysisDuration.Value = ((TimeSpan)((DateTime)cmbEndDateTime.Value - (DateTime)cmbBeginDateTime.Value)).TotalDays.ToString("N1");
        }

        private void btnExportChart_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save pump cycle data:";
            saveFileDialog1.Filter = "*.csv|*.csv";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ValidateNames = true;
            StreamWriter stream;
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = saveFileDialog1.FileName;
            stream = new StreamWriter(fileName);
            try
            {
                Cursor = Cursors.WaitCursor;

                if (chkUniformInterval.Checked)
                {
                    int interval;
                    interval = Convert.ToInt32(txtUniformInterval2.Value);
                    ExportUniformCSV(stream, interval);                    
                }
                else
                {
                    ExportRaw(stream);
                }
                MessageBox.Show("Export complete", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Export failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                stream.Close();
            }
        }

        private void ExportRaw(StreamWriter stream)
        {
            NumericTimeSeries flowSeries;
            flowSeries = (NumericTimeSeries)ultraChart1.Series.FromKey("calculatedFlow");            
            NumericTimeSeries pumpRateSeries;
            pumpRateSeries = (NumericTimeSeries)ultraChart1.Series.FromKey("calculatedPumpingRate");
            NumericTimeSeries pumpCountSeries;
            pumpCountSeries = (NumericTimeSeries)ultraChart1.Series.FromKey("runningPumpCount");
            
            int rawCount = flowSeries.Points.Count;
            DateTime time;
            double avgFlow;
            double avgPumpRate;
            int runningPumpCount;
            int inconsistentData;

            //Read through data and calculate minDelta
            for (int i = 1; i < rawCount; i++)
            {
                time = flowSeries.Points[i].TimeValue;
                avgFlow = flowSeries.Points[i].NumericValue;
                avgPumpRate = pumpRateSeries.Points[i].NumericValue;
                runningPumpCount = (int)pumpCountSeries.Points[i].NumericValue;

                stream.WriteLine(time.ToString() + "," + avgFlow + "," + avgPumpRate + "," + runningPumpCount);
            }
        }

        private void ExportUniformCSV(StreamWriter stream, int interval)
        {
            DateTime previousTime;
            DateTime currentTime;

            TimeSpan uniformDelta;
            uniformDelta = new TimeSpan(0, Convert.ToInt32(interval), 0);

            TimeSpan minDelta;
            minDelta = new TimeSpan(long.MaxValue);

            NumericTimeSeries flowSeries;
            flowSeries = (NumericTimeSeries)ultraChart1.Series.FromKey("calculatedFlow");
            List<ExportDataItem> exportDataItems = new List<ExportDataItem>();
            NumericTimeSeries pumpRateSeries;
            pumpRateSeries = (NumericTimeSeries)ultraChart1.Series.FromKey("calculatedPumpingRate");
            NumericTimeSeries pumpCountseries;
            pumpCountseries = (NumericTimeSeries)ultraChart1.Series.FromKey("runningPumpCount");

            previousTime = flowSeries.Points[0].TimeValue;
            int rawCount = flowSeries.Points.Count;
            for (int i = 0; i < rawCount; i++)
            {
                ExportDataItem exportDataItem = new ExportDataItem(flowSeries.Points[i].TimeValue);
                exportDataItem.flow = flowSeries.Points[i].NumericValue;
                exportDataItem.pumpRate = pumpRateSeries.Points[i].NumericValue;
                exportDataItem.runningPumpCount = (int)pumpCountseries.Points[i].NumericValue;
                exportDataItems.Add(exportDataItem);

            }

            toolStripProgressBar1.Maximum = rawCount;
            toolStripProgressBar1.Minimum = 1;
            toolStripProgressBar1.Value = 1;

            //Read through data and calculate minDelta
            for (int i = 1; i < rawCount; i++)
            {
                ExportDataItem currentDataItem = exportDataItems[i];

                toolStripProgressBar1.Increment(1);
                if (currentDataItem.flagged)
                {
                    continue;
                }
                currentTime = currentDataItem.dateTime;

                if (minDelta.CompareTo(currentTime.Subtract(previousTime)) > 0)
                {
                    minDelta = currentTime.Subtract(previousTime);
                }
                previousTime = currentTime;
            }

            //Round minDelta to nearest minute
            int minutes;
            minutes = 60 / (int)Math.Ceiling((60.0 / minDelta.TotalMinutes));
            minutes = Math.Min(minutes, (int)uniformDelta.TotalMinutes);
            minutes = Math.Max(1, minutes);
            minDelta = new TimeSpan(minDelta.Hours, minutes, 0);

            DateTime startTime, endTime;
            startTime = exportDataItems[0].dateTime; // PumpCycleDataSet.Calculated[0].TimeAvg;//.Add(PumpCycleDataSet.Calculated[0].TimeAvg.Subtract(minDelta));            
            startTime = RoundUpToInterval(startTime, minutes);
            //startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, minutes * (int)(startTime.Minute / (int)Math.Floor(minutes / (double)startTime.Minute)), 0);
            endTime = exportDataItems[rawCount - 1].dateTime; // PumpCycleDataSet.Calculated[PumpCycleDataSet.Calculated.Count - 1].TimeAvg;
            endTime = RoundDownToInterval(endTime, minutes);

            toolStripProgressBar1.Maximum = (int)endTime.Subtract(startTime).TotalMinutes;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Value = 0;

            //previousTime = new DateTime(0);
            int lesserIndex = 0;
            int previousLesserIndex = 0;

            pumpCycleDS.Distributed.Clear();

            for (DateTime j = startTime; j <= endTime; j += minDelta)
            {
                toolStripProgressBar1.Increment((int)minDelta.TotalMinutes);

                //PumpCycleDataSet.CalculatedRow[] foundRows;
                ExportDataItem lesserDataItem, greaterDataItem;
                DateTime lesserTime, greaterTime;
                double lesserWeight, greaterWeight;
                double lesserFlow, greaterFlow, weightedFlow;

                bool flagged, multi;

                if (j >= exportDataItems[lesserIndex + 1].dateTime)
                {
                    lesserIndex++;
                }

                //id = (int)PumpCycleDataSet.Calculated.Compute("Max(ID)", "TimeAvg < #" + j.ToString() + "# AND TimeAvg > #" + previousTime.ToString() + "#");
                //id = foundRows[0].ID;
                lesserDataItem = exportDataItems[lesserIndex];// PumpCycleDataSet.Calculated.FindByID(id);                
                lesserTime = lesserDataItem.dateTime;

                greaterDataItem = exportDataItems[lesserIndex + 1]; // PumpCycleDataSet.Calculated.FindByID(id + 1);
                greaterTime = greaterDataItem.dateTime;

                lesserFlow = lesserDataItem.flow;
                greaterFlow = greaterDataItem.flow;
                lesserWeight = 1 - (j.Subtract(lesserTime).Ticks) / (double)(greaterTime.Subtract(lesserTime).Ticks);
                greaterWeight = 1 - lesserWeight;

                weightedFlow = lesserFlow * lesserWeight + greaterFlow * greaterWeight;
                double pumpRate = lesserDataItem.pumpRate * lesserWeight + greaterDataItem.pumpRate * greaterWeight;
                flagged = lesserDataItem.flagged | greaterDataItem.flagged;

                pumpCycleDS.Distributed.AddDistributedRow(j, lesserTime, greaterTime, lesserWeight, greaterWeight, lesserFlow, greaterFlow, weightedFlow, false, flagged, Math.Max(lesserDataItem.runningPumpCount, greaterDataItem.runningPumpCount), pumpRate);
                //previousTime = lesserTime;
                previousLesserIndex = lesserIndex;
            }

            stream.WriteLine("DateTime,Flow,PumpRate,RunningPumpCount");

            for (DateTime j = RoundUpToInterval(startTime, uniformDelta.Minutes); j < endTime; j += uniformDelta)
            {
                double avgFlow;
                double avgPumpingRate;
                string exp;
                int runningPumpCount;
                exp = "DateTime <= #" + j.Add(new TimeSpan(uniformDelta.Ticks / 2)).ToString() + "# AND DateTime > #" + j.Subtract(new TimeSpan(uniformDelta.Ticks / 2)).ToString() + "#";
                avgFlow = (double)pumpCycleDS.Distributed.Compute("Avg(WeightedFlow)", exp);
                avgPumpingRate = (double)pumpCycleDS.Distributed.Compute("Avg(WeightedPumpingRate)", exp);
                runningPumpCount = (int)pumpCycleDS.Distributed.Compute("Max(RunningPumpCount)", exp);
                stream.WriteLine(j.ToString() + "," + avgFlow + "," + avgPumpingRate + "," + runningPumpCount);
            }

            toolStripProgressBar1.Value = 0;
            return;
        }

        private void grdStationList_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdStationList.Selected.Rows.Count != 1)
            {
                return;
            }



            int stationID = (int)grdStationList.Selected.Rows[0].Cells["StationID"].Value;
            int wetWellVolume;

            pumpCycleDS.CycleSummary.Clear();
            try
            {
                int pumpRateNotFound = 0;
                foreach (PumpCycleDataSet.PumpRow pumpRow in pumpCycleDS.Station.FindByStationID(stationID).GetPumpRows())
                {
                    if (pumpRow.IsPumpRateGPMNull())
                    {
                        pumpRateNotFound++;
                        continue;
                    }
                    pumpCycleDS.CycleSummary.AddCycleSummaryRow(pumpRow.PumpID, 0, 0, 0, 0, 0, 0, pumpRow.PumpRateGPM);
                }
                if (pumpRateNotFound != 0)
                {
                    MessageBox.Show("Invalid pump data found for " + pumpRateNotFound + " of " + pumpCycleDS.Station.FindByStationID(stationID).GetPumpRows().Length + " pumps.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No pump data found: " + ex.Message);
            }

            PumpCycleDataSetTableAdapters.WetWellTableAdapter wetWellTA;
            wetWellTA = new PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.WetWellTableAdapter();
            wetWellTA.Fill(pumpCycleDS.WetWell, stationID);

            if (pumpCycleDS.WetWell.Count == 0)
            {
                txtLeadActiveWWVol.Value = 0;
                return;
            }


            wetWellVolume = (int)this.ReadLeadActiveWetWellVolume(Convert.ToDateTime(this.cmbBeginDateTime.Value));
            //PumpCycleDataSet.WetWellRow wetWellRow;
            //wetWellRow = (PumpCycleDataSet.WetWellRow)pumpCycleDS.WetWell.Select("", "StartDate desc")[0];
            ///if (wetWellRow == null)
            //{
            //txtLeadActiveWWVol.Value = 0;
            //return;
            //}
            //wetWellVolume = wetWellRow.WetWellVolumeGallons;
            txtLeadActiveWWVol.Value = wetWellVolume;

        }

        private void ultraChart1_Scrolling(object sender, Infragistics.UltraChart.Shared.Events.ChartScrollScaleEventArgs e)
        {
            if (e.AxisNumber != AxisNumber.X_Axis)
            {
                return;
            }
            AxisItem axisX, axisPumpCountX;
            axisX = ultraChart1.CompositeChart.ChartAreas[0].Axes[0];
            axisPumpCountX = ultraChart1.CompositeChart.ChartAreas[1].Axes[0];

            AxisAppearance axisXAppearance;
            axisXAppearance = (AxisAppearance)axisX;
            AxisAppearance axisPumpCountAppearance;
            axisPumpCountAppearance = (AxisAppearance)axisPumpCountX;
            axisPumpCountAppearance.ScrollScale.Scroll = e.NewValue; // axisXAppearance.ScrollScale.Scroll;                      

            //axisPumpCountX.RangeType = AxisRangeType.Custom;
            //axisPumpCountX.RangeMin = axisX.RangeMin;
            //axisPumpCountX.RangeMax = axisX.RangeMax;
        }

        private void ultraChart1_Scaling(object sender, Infragistics.UltraChart.Shared.Events.ChartScrollScaleEventArgs e)
        {
            if (e.AxisNumber != AxisNumber.X_Axis)
            {
                e.Cancel = true;
                return;
            }
            AxisItem axisX, axisPumpCountX;
            axisX = ultraChart1.CompositeChart.ChartAreas[0].Axes[0];
            axisPumpCountX = ultraChart1.CompositeChart.ChartAreas[1].Axes[0];

            AxisAppearance axisXAppearance;
            axisXAppearance = (AxisAppearance)axisX;
            AxisAppearance axisPumpCountAppearance;
            axisPumpCountAppearance = (AxisAppearance)axisPumpCountX;
            axisPumpCountAppearance.ScrollScale.Scale = e.NewValue;

        }

        private void chkCustomWetwellVolume_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCustomWetwellVolume.Checked)
            {
                txtLeadActiveWWVol.ReadOnly = false;
            }
            else
            {
                txtLeadActiveWWVol.ReadOnly = true;
                txtLeadActiveWWVol.Value = pumpCycleDS.WetWell[0].WetWellVolumeGallons;
            }
        }

    }

    internal class PumpCycleSummary
    {
        public int pumpID;
        public PumpCycleSummary(int pumpID)
        {
            this.pumpID = pumpID;
        }

        public int cycleCount = 0;
        public double pumpOnDuration;
        public DateTime pumpOnTime = new DateTime();
        public DateTime pumpOffTime = new DateTime();

        public DateTime firstPumpCycleTime = new DateTime();
        public DateTime lastPumpCycleTime = new DateTime();

        public double maxInflow = 0;
        public double calculatedPumpingRate
        {
            get { return cumulativePumpingRate / cycleCount; }
        }
        public double cumulativePumpingRate = 0;

        public bool pumpRunning = false;
    }

    internal class ExportDataItem
    {
        public DateTime dateTime;
        public double flow;
        public int runningPumpCount;
        public double pumpRate;
        public bool flagged;

        public ExportDataItem(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }


    }
}
