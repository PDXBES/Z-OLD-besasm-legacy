using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VirtualRain
{
    //ExcelHelper is the primary experiment that will lead to Raintools.
    //Any advances in ExcelHelper can be transferred to RainTools, with the 
    //intent of reducing the amount of code per file, and abstracting the
    //code that currently exists.
    class ExcelHelper
    {
        private static void AddText(System.IO.FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        public static void CreateExcelFile(DateTime sdate, DateTime edate, int maxRows, string path1)
        {
            //Excel.Application oXL; 
            SystemsAnalysis.Interop.Excel9.Application oXL;

            /*Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel._Worksheet dataSheet;
            Excel._Chart oChart;
            Excel._Worksheet oChartSheet;
            Excel.Range oRng;
            Excel.Series oSeries;*/
            SystemsAnalysis.Interop.Excel9._Workbook oWB;
            SystemsAnalysis.Interop.Excel9._Worksheet oSheet = null;
            SystemsAnalysis.Interop.Excel9._Worksheet dataSheet = null;
            SystemsAnalysis.Interop.Excel9._Chart oChart;
            SystemsAnalysis.Interop.Excel9._Worksheet oChartSheet;
            SystemsAnalysis.Interop.Excel9.Range oRng;
            SystemsAnalysis.Interop.Excel9.Series oSeries;

            DateTime DateStep;
            int RainRow = 0;
            int DownRow = 0;
            bool DataFound = false;
            System.Data.DataRow theRainRow;
            System.Data.DataRow theDownRow;

            //for each time step
            DateStep = sdate;

            System.TimeSpan diff1 = edate.Subtract(sdate);
            long NumberOfTimeStepsBetweenStartAndEnd = (((long)diff1.TotalMinutes) / 5) + 1;

            try
            {
                //Start Excel and get Application object.
                oXL = new SystemsAnalysis.Interop.Excel9.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = (SystemsAnalysis.Interop.Excel9._Workbook)(oXL.Workbooks.Add("Workbook"));

                try
                {
                    oXL.Workbooks.Open(path1,
                                        Type.Missing,
                                        Type.Missing,
                                        Type.Missing,
                                        Type.Missing,
                                        Type.Missing,
                                        Type.Missing,
                                        Type.Missing,
                                        Type.Missing,
                                        Type.Missing,
                                        Type.Missing,
                                        Type.Missing,
                                        Type.Missing);
                    /*oXL.Workbooks.OpenText(@path1,
                                            SystemsAnalysis.Interop.Excel9.XlPlatform.xlWindows,
                                            1,//starting at row 1
                                            SystemsAnalysis.Interop.Excel9.XlTextParsingType.xlDelimited,
                                            SystemsAnalysis.Interop.Excel9.XlTextQualifier.xlTextQualifierNone,
                                            false,
                                            false,
                                            false,
                                            true,
                                            false,
                                            true,
                                            ",",
                                            Type.Missing,
                                            Type.Missing,//null,
                                            ".",
                                            ",");//'?');*/
                    dataSheet = (SystemsAnalysis.Interop.Excel9._Worksheet)oXL.ActiveSheet;
                    dataSheet.Name = "source_data";
                    oSheet = (SystemsAnalysis.Interop.Excel9._Worksheet)oXL.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing); //oWB.ActiveSheet;
                    oSheet.Name = "parsed_data";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //Add table headers going cell by cell
                maxRows = dataSheet.UsedRange.Count / 2;
                oSheet.Cells[1, 1] = "Date";
                oRng = oSheet.get_Range("A2", "A" + ((Int32)(maxRows + 1)).ToString());
                oRng.Formula = "=" + dataSheet.Name + "!A1";
                oSheet.Cells[1, 2] = "Year";
                oRng = oSheet.get_Range("B2", "B" + ((Int32)(maxRows + 1)).ToString());
                oRng.Formula = "=YEAR(" + dataSheet.Name + "!A1)";
                oSheet.Cells[1, 3] = "Month";
                oRng = oSheet.get_Range("C2", "C" + ((Int32)(maxRows + 1)).ToString());
                oRng.Formula = "=MONTH(" + dataSheet.Name + "!A1)";
                oSheet.Cells[1, 4] = "Day";
                oRng = oSheet.get_Range("D2", "D" + ((Int32)(maxRows + 1)).ToString());
                oRng.Formula = "=DAY(" + dataSheet.Name + "!A1)";
                oSheet.Cells[1, 5] = "Hour";
                oRng = oSheet.get_Range("E2", "E" + ((Int32)(maxRows + 1)).ToString());
                oRng.Formula = "=HOUR(" + dataSheet.Name + "!A1)";
                oSheet.Cells[1, 6] = "Minute";
                oRng = oSheet.get_Range("F2", "F" + ((Int32)(maxRows + 1)).ToString());
                oRng.Formula = "=MINUTE(" + dataSheet.Name + "!A1)";
                oSheet.Cells[1, 7] = "Rainfall(inches/hour)";
                oRng = oSheet.get_Range("G2", "G" + ((Int32)(maxRows + 1)).ToString());
                oRng.Formula = "=" + dataSheet.Name + "!B1";

                //Make the graph
                oChart = (SystemsAnalysis.Interop.Excel9._Chart)oXL.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oChart.Name = "Rain_Chart";
                oChart.ChartType = SystemsAnalysis.Interop.Excel9.XlChartType.xlXYScatterLinesNoMarkers;
                oChart.SetSourceData(oSheet.get_Range("A2:A" + ((Int32)(maxRows + 1)).ToString() + "," +
                                                      "G2:G" + ((Int32)(maxRows + 1)).ToString(), Type.Missing), SystemsAnalysis.Interop.Excel9.XlRowCol.xlColumns);

                //This section is necessary in order for me to be able to grab pieces of the seriescollection
                //so that I can use the NewSeries() method.
                SystemsAnalysis.Interop.Excel9.SeriesCollection seriesCol = (SystemsAnalysis.Interop.Excel9.SeriesCollection)oChart.SeriesCollection(Type.Missing);

                oSeries = (SystemsAnalysis.Interop.Excel9.Series)oChart.SeriesCollection(1);
                oSeries.XValues = oSheet.get_Range("A2", "A" + ((Int32)(maxRows + 1)).ToString());
                oSeries.Values = oSheet.get_Range("G2", "G" + ((Int32)(maxRows + 1)).ToString());

                SystemsAnalysis.Interop.Excel9.Series oSeries2 = seriesCol.NewSeries();
                oSeries2.MarkerStyle = SystemsAnalysis.Interop.Excel9.XlMarkerStyle.xlMarkerStyleNone;
                oSeries2.Border.LineStyle = SystemsAnalysis.Interop.Excel9.XlLineStyle.xlLineStyleNone;

                oSeries2.XValues = oSheet.get_Range("A2", "A" + ((Int32)(maxRows + 1)).ToString());
                oSeries2.Values = oSheet.get_Range("G2", "G" + ((Int32)(maxRows + 1)).ToString());
                oSeries2.AxisGroup = SystemsAnalysis.Interop.Excel9.XlAxisGroup.xlSecondary;
                oSeries2.ErrorBar(SystemsAnalysis.Interop.Excel9.XlErrorBarDirection.xlY, SystemsAnalysis.Interop.Excel9.XlErrorBarInclude.xlErrorBarIncludeMinusValues,
                    SystemsAnalysis.Interop.Excel9.XlErrorBarType.xlErrorBarTypePercent, 100, Type.Missing);


                //reverse the plot order for the Y axis
                SystemsAnalysis.Interop.Excel9.Axis a =
                    (SystemsAnalysis.Interop.Excel9.Axis)oChart.Axes(SystemsAnalysis.Interop.Excel9.XlAxisType.xlValue, SystemsAnalysis.Interop.Excel9.XlAxisGroup.xlSecondary);
                a.ReversePlotOrder = true;

                //oSeries2.ChartType = Excel.XlChartType.

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }
        }

        public static void CreateRainReport(string path1, DateTime sdate, DateTime edate, bool cbReportZeroRainValue, int nudTimeStepValue, DataGridView dataGridView1)
        {
            //Data set and table adapter for the variable minute rain gauges
            NEPTUNEDataSetTableAdapters.DataTable1TableAdapter theRainTableAdapter =
                new VirtualRain.NEPTUNEDataSetTableAdapters.DataTable1TableAdapter();
            NEPTUNEDataSet.DataTable1DataTable theRainTable =
                new NEPTUNEDataSet.DataTable1DataTable();
            int timeStep = (int)nudTimeStepValue;

            sdate = sdate.AddMinutes(-(sdate.Minute % timeStep));

            NEPTUNEDataSetTableAdapters.V_MODEL_RAIN_DOWNTIMETableAdapter theDownTimeTableAdapter =
                new VirtualRain.NEPTUNEDataSetTableAdapters.V_MODEL_RAIN_DOWNTIMETableAdapter();
            NEPTUNEDataSet.V_MODEL_RAIN_DOWNTIMEDataTable theDownTimeTable =
                new NEPTUNEDataSet.V_MODEL_RAIN_DOWNTIMEDataTable();

            theRainTableAdapter.Fill(theRainTable, edate, timeStep, (int)dataGridView1.SelectedRows[0].Cells[0].Value, sdate);
            theDownTimeTableAdapter.FillByStationID(theDownTimeTable, (int)dataGridView1.SelectedRows[0].Cells[0].Value, sdate, edate);

            DateTime DateStep;
            int RainRow = 0;
            int DownRow = 0;
            int maxRows = theRainTable.Rows.Count;
            int DownRows = theDownTimeTable.Rows.Count;
            bool DataFound = false;
            System.Data.DataRow theRainRow;
            System.Data.DataRow theDownRow;

            if (File.Exists(path1))
            {
                File.Delete(path1);
            }

            System.IO.FileStream fs = File.Create(path1);

            //for each time step
            DateStep = sdate;

            System.TimeSpan diff1 = edate.Subtract(sdate);
            long NumberOfTimeStepsBetweenStartAndEnd = (((long)diff1.TotalMinutes) / timeStep) + 1;

            if (maxRows != 0)
            {
                theRainRow = theRainTable.Rows[0];
                for (int j = 0; j < NumberOfTimeStepsBetweenStartAndEnd; j++)
                {
                    DataFound = false;
                    //if the datetime in the rainDS is the same as the datetime in DateStep
                    //then get the rainfall sum and increment DateStep
                    if (RainRow < maxRows)
                    {
                        theRainRow = theRainTable.Rows[RainRow];
                    }
                    //if the current DateStep value is during one of the station's
                    //downtimes, then print "DOWN" as the value of the rainfall
                    //if 'ThereIsDownTime' == true, then print "DOWN"
                    //This means I will need to select the downtime for this
                    //raingage and place it in a new dataset?

                    if (String.Compare((string.Format("{0:MM/dd/yyyy HH:mm}\t", theRainRow["date_time"])), (string.Format("{0:MM/dd/yyyy HH:mm}\t", DateStep))) == 0)
                    {

                        AddText(fs, (string.Format("{0:MM/dd/yyyy HH:mm}\t", DateStep))
                            + theRainRow["sum_inches"].ToString() + "\r\n");

                        RainRow++;
                        DataFound = true;
                    }
                    else if (DownRows > 0)
                    {
                        for (DownRow = 0; DownRow < DownRows; DownRow++)
                        {
                            if (DateTime.Compare(DateStep, (DateTime)theDownTimeTable.Rows[DownRow]["start_date"]) >= 0 &&
                                DateTime.Compare(DateStep, (DateTime)theDownTimeTable.Rows[DownRow]["end_date"]) <= 0 &&
                                DataFound == false)
                            {
                                AddText(fs, (string.Format("{0:MM/dd/yyyy HH:mm}\t", DateStep))
                                    + "DOWN\r\n");
                                DataFound = true;
                            }
                        }
                    }
                    if (DataFound == false)
                    {
                        if (cbReportZeroRainValue == true)
                        {
                            AddText(fs, (string.Format("{0:MM/dd/yyyy HH:mm}\t", DateStep))
                                + "0\r\n");
                        }
                    }
                    DateStep = DateStep.AddMinutes(timeStep);
                }
                fs.Close();

                CreateExcelFile(sdate, edate, maxRows, path1);
            }
            else
            {
                MessageBox.Show("There is no rainfall recorded by that monitor during the specified interval");
            }
        }
    }
}
