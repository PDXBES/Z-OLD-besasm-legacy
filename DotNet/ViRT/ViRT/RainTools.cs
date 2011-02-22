using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace ViRT
{
    //Raintools is intended to contain the methods with which to create the program output.  
    //considering the form itself has grown beyond manageable length, this is the first step
    //in separating and controlling the code.  The intent will be for all parts of code to pass
    //entire object sets to here, as it has been determined to have negligible effect on
    //processing speed.  
    class RainTools
    {

        public void virtmain(DateTime sdate,
            DateTime edate,
            long NumberOfGages,
            long NumberOfTimeStepsBetweenStartAndEnd,
            SortedList QuarterSectionList,
            gageinfo[][] QuarterSectionGageInfo,
            string QSListString,
            System.Data.OleDb.OleDbConnection oleDbConnection2,
            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1,
            System.Data.SqlClient.SqlConnection sqlConnection1,
            System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1,
            RainDSClass rainDS,
            qsDS qsDS1,
            string txtDestination,
            double[][] largeRainArray,
            bool checkBinaryOutput,
            DataTable graphData)
        {
            bool Silent = false;
            //quartersectiongageinfo primary dimension is based upon the number of selected
            //quartersections, the secondary dimension is based upon the total number of
            //raingages.
            //indexKeeper tracks any given index which is accessed indirectly
            int indexKeeper = 0;
            double[] rain = new double[3];
            double[] distance = new double[3];
            long[] h2 = new long[3];
            int count = 0;
            byte[] gagelist;
            long GageIndex; //array index for gages
            float thisResult;

            System.Windows.Forms.DialogResult ReturnCode; //return code

            //frmMapEditing.txtQSBulkInputParse();

            //figure out the number of gages and the last gage
            //the gage id is used as an array index to the Rmem array
            //the max gageid is used to dimension the Rmem array
            //the number of gages is used to dimension the RGinfo array


            string mySelectQuery = "SELECT Count([station_id])AS CountOfRGid FROM STATION";//, Max(rain_sensor_id) AS MaxOfRGid STATION";
            SqlConnection myConnection = new SqlConnection("Data Source=Reportsio;Initial Catalog=NEPTUNE;Integrated Security=SSPI;connection timeout=0;");
            SqlCommand myCommand = new SqlCommand(mySelectQuery, myConnection);

            try
            {
                myConnection.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect to database.  Database is most likely down.");
                return;
            }
            SqlDataReader myReader = myCommand.ExecuteReader();

            try
            {
                if (myReader.HasRows)
                {
                    myReader.Read();
                }
                else
                {
                    MessageBox.Show("No data returned", "My Application", MessageBoxButtons.OK);
                }

                NumberOfGages = myReader.GetInt32(0);
            }
            finally
            {
                // always call Close when done reading.
                myReader.Close();
                // always call Close when done reading.
                myConnection.Close();
            }

            System.TimeSpan diff1 = edate.Subtract(sdate);

            NumberOfTimeStepsBetweenStartAndEnd = (((long)diff1.TotalMinutes) / 5) + 1;

            mySelectQuery = "select [station_id],[state_plane_x_ft],[state_plane_y_ft], [start_date], [end_date] , [h2_number] from STATION order by [station_id]";
            myCommand = new SqlCommand(mySelectQuery, myConnection);
            myConnection.Open();
            myReader = myCommand.ExecuteReader();

            string downTimeQuery = "SELECT station_id, start_date, end_date FROM DOWNTIME ORDER BY station_id, start_date";
            SqlConnection myDTConnection = new SqlConnection("Data Source=Reportsio;Initial Catalog=NEPTUNE;connection timeout=0;Integrated Security=SSPI;");
            SqlCommand myDTCommand = new SqlCommand(downTimeQuery, myDTConnection);
            myDTConnection.Open();
            SqlDataReader myDTReader = myDTCommand.ExecuteReader();
            myDTReader.Read();
            myDTReader.Close();
            myDTConnection.Close();

            try
            {
                if (myReader.HasRows)
                {
                    myReader.Read();
                }
                else
                {
                    if (!Silent)
                    {
                        MessageBox.Show("No gage selected", "My Application", MessageBoxButtons.OK);
                    }
                }

                gagelist = new byte[NumberOfGages + 1];
                GageIndex = 0;

                //at this point, we have the max # of gages, and the last gage id

                //now create an array connecting the selected quartersections
                //with the distances to each raingage, and sort this list by
                //the quartersection, then the distance

                //First, what is the number of quartersections that have been selected?
                count = QuarterSectionList.Count;
                //Second, what is the number of raingages in the database?
                //Answer: NumberOfGages
                //Third, make the array
                QuarterSectionGageInfo = new gageinfo[count][];
                for (int i = 0; i < count; i++)
                {
                    QuarterSectionGageInfo[i] = new gageinfo[NumberOfGages];
                    for (int j = 0; j < NumberOfGages; j++)
                    {
                        QuarterSectionGageInfo[i][j] = new gageinfo();
                    }
                }

                // the next section should simply be placing the gageinfo and the
                // distance into the QuartersectionGageInfo array.
                // the primary part of this would be getting the location data
                // of the quartersections.  What table to use here?
                // i just created a table from arcgis.  The new table contains
                // the x and y coordinates of the quartersection centroids.
                // I have to find some way to access this new table from this 
                // program

                //the new table is accessed, and the values can now be queried.
                // the next step is to fill the distances array with the values.
                try
                {
                    string QSQuery = "SELECT QTRNO,Qs_x,Qs_y FROM QS_Data WHERE " + QSListString + " ORDER BY QTRNO";

                    System.Data.OleDb.OleDbCommand QSCommand = new System.Data.OleDb.OleDbCommand(QSQuery, oleDbConnection2);
                    oleDbDataAdapter1.SelectCommand = QSCommand;
                    oleDbConnection2.Open();
                    oleDbDataAdapter1.Fill(qsDS1, "QSList");

                    int maxqsRows = qsDS1.Tables["QSList"].Rows.Count;
                    System.Data.DataRow qsDataRow;
                    int qsRow = 0;

                    while (qsRow < maxqsRows)
                    {
                        qsDataRow = qsDS1.Tables["QSList"].Rows[qsRow];

                        if (myReader.IsClosed)
                        {
                            mySelectQuery = "select [station_id],[state_plane_x_ft],[state_plane_y_ft], [start_date], [end_date], [h2_number] from STATION order by [station_id]";
                            myCommand = new SqlCommand(mySelectQuery, myConnection);
                            myConnection.Open();
                            myReader = myCommand.ExecuteReader();
                            myReader.Read();
                        }

                        // this part requires finding the position in the list QuarterSectionList
                        // that the quartersection in the reader is currently looking at in myreader2.
                        // search quartersectionlist for the position of the quartersection
                        // that is currently in myreader2					
                        while (GageIndex < NumberOfGages)
                        {
                            //record the station id into the gagelist
                            gagelist[GageIndex] = (byte)myReader.GetInt32(0);
                            TimeInterval firstDownTime = new TimeInterval();
                            TimeInterval lastDownTime = new TimeInterval();
                            firstDownTime.IntervalStart = DateTime.MinValue;
                            lastDownTime.IntervalEnd = DateTime.MaxValue;

                            //record the station id into the main data structure
                            indexKeeper = QuarterSectionList.IndexOfValue(qsDataRow[0].ToString());
                            QuarterSectionGageInfo[indexKeeper][GageIndex].ID = myReader.GetInt32(0);
                            if (myDTReader.IsClosed)
                            {
                                downTimeQuery = "SELECT start_date, end_date FROM DOWNTIME WHERE station_id = " + myReader.GetInt32(0).ToString() + " AND data_category = 'Rain' order by start_date";
                                myDTCommand = new SqlCommand(downTimeQuery, myDTConnection);
                                myDTConnection.Open();
                                myDTReader = myDTCommand.ExecuteReader();
                                TimeInterval ThisDownTime;
                                int DTIndex = 0;
                                QuarterSectionGageInfo[indexKeeper][GageIndex].DownTimeList = new SortedList();
                                QuarterSectionGageInfo[indexKeeper][GageIndex].SafeDownTimeList = new SortedList();
                                while (myDTReader.Read())
                                {
                                    ThisDownTime = new TimeInterval();
                                    ThisDownTime.IntervalStart = System.DateTime.Parse(myDTReader.GetSqlDateTime(0).ToString());

                                    ThisDownTime.IntervalEnd = System.DateTime.Parse(myDTReader.GetSqlDateTime(1).ToString());
                                    //if there is already an entry with the same start time, then use the downtime record with the greatest end time
                                    if (QuarterSectionGageInfo[indexKeeper][GageIndex].DownTimeList.ContainsKey(ThisDownTime.IntervalStart))
                                    {
                                        int position = QuarterSectionGageInfo[indexKeeper][GageIndex].DownTimeList.IndexOfKey(ThisDownTime.IntervalStart);
                                        if (((TimeInterval)QuarterSectionGageInfo[indexKeeper][GageIndex].DownTimeList[ThisDownTime.IntervalStart]).IntervalEnd < ThisDownTime.IntervalEnd)
                                        {
                                            //remove the current downtime record for this start time and replace it with the longer one
                                            QuarterSectionGageInfo[indexKeeper][GageIndex].DownTimeList.Remove(ThisDownTime.IntervalStart);
                                            QuarterSectionGageInfo[indexKeeper][GageIndex].SafeDownTimeList.Remove(ThisDownTime.IntervalStart);
                                            QuarterSectionGageInfo[indexKeeper][GageIndex].DownTimeList.Add(ThisDownTime.IntervalStart, ThisDownTime);
                                            QuarterSectionGageInfo[indexKeeper][GageIndex].SafeDownTimeList.Add(ThisDownTime.IntervalStart, ThisDownTime);
                                        }
                                        else
                                        {
                                            //if the current record is the longest, do nothing.
                                        }

                                    }
                                    else
                                    {
                                        QuarterSectionGageInfo[indexKeeper][GageIndex].DownTimeList.Add(ThisDownTime.IntervalStart, ThisDownTime);
                                        QuarterSectionGageInfo[indexKeeper][GageIndex].SafeDownTimeList.Add(ThisDownTime.IntervalStart, ThisDownTime);
                                    }

                                    DTIndex++;
                                }
                            }

                            //check for stations which do not have state_plane_x or y entries.  If these exist, pretend they are very far away.
                            if (myReader.IsDBNull(1) || myReader.IsDBNull(2))
                            {
                                QuarterSectionGageInfo[indexKeeper][GageIndex].Dist = 10000000;
                                QuarterSectionGageInfo[indexKeeper][GageIndex].xPos = 10000000;
                                QuarterSectionGageInfo[indexKeeper][GageIndex].yPos = 0;
                            }
                            //if the station has x AND y coordinates, calculate the distance to the raingage from the quartersection.
                            else
                            {
                                QuarterSectionGageInfo[indexKeeper][GageIndex].Dist = System.Math.Sqrt(System.Math.Pow(myReader.GetInt32(1) - (double)qsDataRow[1], 2.0) + System.Math.Pow(myReader.GetInt32(2) - (double)qsDataRow[2], 2.0));
                                QuarterSectionGageInfo[indexKeeper][GageIndex].xPos = (float)myReader.GetInt32(1);
                                QuarterSectionGageInfo[indexKeeper][GageIndex].yPos = (float)myReader.GetInt32(2);
                            }

                            QuarterSectionGageInfo[indexKeeper][GageIndex].startDate = System.DateTime.Parse(myReader.GetSqlDateTime(3).ToString());
                            firstDownTime.IntervalEnd = QuarterSectionGageInfo[indexKeeper][GageIndex].startDate;

                            if (myReader.IsDBNull(4))
                            {
                                QuarterSectionGageInfo[indexKeeper][GageIndex].endDate = System.DateTime.Parse(System.DateTime.Now.ToLongDateString());
                            }
                            else
                            {
                                QuarterSectionGageInfo[indexKeeper][GageIndex].endDate = System.DateTime.Parse(myReader.GetSqlDateTime(4).ToString());
                            }
                            lastDownTime.IntervalStart = QuarterSectionGageInfo[indexKeeper][GageIndex].endDate;
                            QuarterSectionGageInfo[indexKeeper][GageIndex].h2Number = myReader.GetInt32(5);
                            QuarterSectionGageInfo[indexKeeper][GageIndex].stationID = myReader.GetInt32(0);

                            //add the first and last downtimes to the gage downtime list
                            QuarterSectionGageInfo[indexKeeper][GageIndex].DownTimeList.Add(firstDownTime.IntervalStart, firstDownTime);
                            QuarterSectionGageInfo[indexKeeper][GageIndex].DownTimeList.Add(lastDownTime.IntervalStart, lastDownTime);

                            if (myReader.Read())
                            {
                                GageIndex = GageIndex + 1;
                                myDTReader.Close();
                                myDTConnection.Close();
                            }
                            else
                            {
                                myDTReader.Close();
                                myDTConnection.Close();
                                break;
                            }
                        }
                        //Sort the array of gages based upon the Distance
                        System.Array.Sort(QuarterSectionGageInfo[indexKeeper]);

                        myReader.Close();
                        myDTReader.Close();
                        myConnection.Close();
                        myDTConnection.Close();
                        gagelist[GageIndex] = 0;
                        GageIndex = 0;
                        qsRow++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to connect to first data source");
                }
                finally
                {
                    //conn.Close();
                }
            }
            finally
            {
                // always call Close when done reading.
                myReader.Close();
                myDTReader.Close();
                myConnection.Close();
                myDTConnection.Close();
            }
            //now that this is complete, I have an array with the gage distances,
            //I have a sorted list with the quartersections.  I have a sorted list
            //containing the downtimes of the raingages in question.  Now I need to find 
            //at most 3 working raingages for a time period for each quartersection.
            //To start this task out, I should take the start date, then run through
            //each raingage and find at most 3 raingages that are closest to the
            //quartersection and have a start date that is less than the date requested,
            //as well as search through the list of downtimes for that gage and
            //if there are any start time objects within that list that are less than
            //the requested time, look for an end date that is before the 
            //requested time as well in the same object.  then move to the 
            //next object until the start time of the downtime is later than the
            //requested time or there are no more downtime objects within the list.

            //essentially, the setup goes like this: for each Quartersection,
            //for each 5 minute period after sdate and before edate, for each
            //raingage, find at most 3 of the closest operating raingages.
            //send the distances and rain values of the raingages to the RDS function.
            //Then place the output in a file.

            //the following loop section creates an array of raingages for the time
            // period specified.  Once this array is created, all that is needed now
            // is to locate the closest working raingage for each quartersection.
            GageIndex = 0;

            largeRainArray = new double[NumberOfGages][];
            for (int i = 0; i < NumberOfGages; i++)
            {
                largeRainArray[i] = new double[NumberOfTimeStepsBetweenStartAndEnd];
                for (int j = 0; j < NumberOfTimeStepsBetweenStartAndEnd; j++)
                {
                    largeRainArray[i][j] = 200.0;
                }
            }

            SortedList UsedGages = new SortedList();
            createUsedGagesListForAllQuartersections(count, UsedGages, sdate, edate, QuarterSectionGageInfo, NumberOfGages);

            //for each quartersection (count is the number of quartersections)
            for (int i = 0; i < 1; i++)
            {
                //for each gage, get all of the rain records between sdate and edate
                for (int k = 0; k < UsedGages.Count; k++)
                {
                    //flip through the QuarterSectionGageInfo Array until we find the index of the gage with the current h2 number
                    int h2Index = 0;
                    while (h2Index < NumberOfGages)
                    {
                        if (QuarterSectionGageInfo[i][h2Index].h2Number == (long)UsedGages.GetByIndex(k))
                        {
                            break;
                        }
                        else
                        {
                            h2Index++;
                        }
                    }
                    if (true)
                    {
                        //create variables
                        gageinfo currentGagesInfo;
                        string rainQuery;
                        SqlCommand myRainCommand;
                        int thisRow;
                        DateTime dateTimeIterator;
                        int ourRow;
                        int maxRows;
                        System.Data.DataRow theDataRow;

                        //initialize variables
                        currentGagesInfo = QuarterSectionGageInfo[i][h2Index];
                        rainQuery = "SELECT five_minute_sum_inches, five_minute_date_time FROM V_MODEL_RAIN_FIVE_MINUTE where h2_number = " + ((long)UsedGages.GetByIndex(k)).ToString() + " AND five_minute_date_time >= '" + sdate.ToString() + "' AND five_minute_date_time <= '" + edate.ToString() + "' order by five_minute_date_time";
                        myRainCommand = new SqlCommand(rainQuery, sqlConnection1);
                        thisRow = 0;
                        dateTimeIterator = sdate;
                        ourRow = 0;

                        sqlDataAdapter1.SelectCommand = myRainCommand;
                        sqlConnection1.Open();
                        sqlDataAdapter1.Fill(rainDS, "Rain");
                        maxRows = rainDS.Tables["Rain"].Rows.Count;

                        if (maxRows != 0)
                        {
                            theDataRow = rainDS.Tables["Rain"].Rows[ourRow];
                        }

                        while (ourRow < maxRows)
                        {
                            //flip through the dates until we find one that is in the database.
                            theDataRow = rainDS.Tables["Rain"].Rows[ourRow];
                            while (dateTimeIterator.CompareTo(System.DateTime.Parse((theDataRow[1]).ToString())) != 0)
                            {
                                //check to see if the date is one that occurs while the sensor is down
                                basicDownTimeSearch(ref dateTimeIterator, ref currentGagesInfo, ref h2Index, ref thisRow, ref largeRainArray);

                                dateTimeIterator = dateTimeIterator.AddMinutes(5);
                            }
                            largeRainArray[h2Index][thisRow] = Convert.ToDouble(theDataRow[0]);
                            dateTimeIterator = dateTimeIterator.AddMinutes(5);
                            ourRow++;
                            thisRow++;

                            //if this row is greater than the number of data points we requested, then the
                            //rest of the data must be either down or zero.
                            if (ourRow >= maxRows && dateTimeIterator <= edate)
                            {
                                while (dateTimeIterator.CompareTo(System.DateTime.Parse(edate.ToString())) != 0)
                                {
                                    basicDownTimeSearch(ref dateTimeIterator, ref currentGagesInfo, ref h2Index, ref thisRow, ref largeRainArray);
                                    dateTimeIterator = dateTimeIterator.AddMinutes(5);
                                }
                            }
                        }
                        sqlConnection1.Close();
                        rainDS.Clear();
                    }
                }
            }
            DateTime DateStep = sdate;
            string indexPath = txtDestination;
            //create the text file if the user has not checked the 'binary' checkbox.
            System.IO.FileStream fsIndex = null;

            if (checkBinaryOutput == false)
            {
                if (File.Exists(indexPath))
                {
                    File.Delete(indexPath);
                }
                fsIndex = File.Create(indexPath);
                AddText(fsIndex, "Virtual raingages were created for the following quartersections, for the dates:\n" + sdate.ToString() + " - " + edate.ToString() + "\n");
            }

            //set up the Dataset for the graph
            graphData.Columns.Add("QuarterSection", typeof(System.DateTime));
            DateStep = sdate;
            graphData.Columns.Add("Intensity", typeof(System.Double));

            DataRow rainDataRow;

            //set up the bitmap image for the graph
            double highestValue = 0;

            //create the list of <Date, Quartersection, Virtualrainfall> objects
            //we can use to write the binary file
            DataTable allTheVirtualRainfall = new DataTable("Virtual_rainfall_by_quartersection");
            allTheVirtualRainfall.Columns.Add("Date", System.Type.GetType("System.DateTime"));
            //create new columns for all of the quartersections.  This should be consolidated by
            //a sum/group by query once it is all filled up.
            for (int i = 0; i < count; i++)
            {
                allTheVirtualRainfall.Columns.Add((string)QuarterSectionList.GetByIndex(i), System.Type.GetType("System.Double"));
            }

            object[] allTheVirtualRainfallArray = new object[count + 1];

            //for each quartersection
            for (int i = 0; i < count; i++)
            {
                allTheVirtualRainfallArray.Initialize();
                //this should probably be changed to an array of file pointers, one
                //file for each quatersection.
                string path1;
                System.IO.FileStream fs = null;

                if (checkBinaryOutput == false)
                {
                    path1 = txtDestination.Remove(txtDestination.Length - 4, 4) + QuarterSectionList.GetByIndex(i) + ".txt";

                    AddText(fsIndex, QuarterSectionList.GetByIndex(i) + "\n");
                    if (File.Exists(path1))
                    {
                        File.Delete(path1);
                    }
                    fs = File.Create(path1);
                }

                //for each time step
                DateStep = sdate;
                for (int j = 0; j < NumberOfTimeStepsBetweenStartAndEnd; j++)
                {
                    //find at most 3 of the closest working raingages
                    int workingGages = 0;
                    h2[0] = 0;
                    h2[1] = 0;
                    h2[2] = 0;
                    rain[0] = 0.0;
                    rain[1] = 0.0;
                    rain[2] = 0.0;
                    distance[0] = 0.0;
                    distance[1] = 0.0;
                    distance[2] = 0.0;
                    for (int k = 0; k < NumberOfGages && workingGages < 3; k++)
                    {
                        //look through the list of raingages for one where the largeRainArray value is <199
                        long gageMatch = 0;
                        while (QuarterSectionGageInfo[0][gageMatch].h2Number != QuarterSectionGageInfo[i][k].h2Number)
                        {
                            gageMatch++;
                        }
                        if (largeRainArray[gageMatch][j] < 100)
                        {
                            rain[workingGages] = largeRainArray[gageMatch][j];
                            distance[workingGages] = QuarterSectionGageInfo[i][k].Dist;
                            h2[workingGages] = QuarterSectionGageInfo[i][k].h2Number;
                            workingGages++;
                        }
                    }

                    thisResult = (float)((RDS(workingGages, rain[0], rain[1], rain[2], distance[0], distance[1], distance[2])) * 12.0);

                    if (thisResult > highestValue)
                    {
                        highestValue = thisResult;
                    }

                    rainDataRow = graphData.NewRow();
                    rainDataRow[1] = thisResult;
                    rainDataRow[0] = DateStep;
                    graphData.Rows.Add(rainDataRow);

                    if (thisResult != 0)
                    {
                        if (checkBinaryOutput == false)
                        {
                            AddText(fs, (string.Format("{0:MM/dd/yyyy HH:mm},", DateStep))
                                + thisResult.ToString() + ",\r\n");
                        }

                        allTheVirtualRainfallArray[0] = DateStep;
                        for (int loop = 0; loop < count; loop++)
                        {
                            allTheVirtualRainfallArray[loop + 1] = 0.0;
                        }
                        allTheVirtualRainfallArray[i + 1] = thisResult;
                        allTheVirtualRainfall.Rows.Add(allTheVirtualRainfallArray);
                    }

                    DateStep = DateStep.AddMinutes(5);
                }
                if (checkBinaryOutput == false)
                {
                    fs.Close();
                }
            }
            //try to sort the data by the date, and group it by the date.  All of the
            //columns that are not dates (all of the columns that are rainfall) should
            //be summed.  The sums will simply be a bunch of zeroes and possibly a decimal(double)
            //that represents a rainfall for that time, per each quartersection.  There will
            //be a variable number of quartersections, so this query must be dynamic.
            var consolidateQuery = allTheVirtualRainfall.AsEnumerable();

            var query = from o in consolidateQuery
                        orderby o.Field<System.DateTime>("Date")
                        select o;

            System.DateTime queryDate = System.DateTime.MinValue;
            System.Double[] queryRainfall = new System.Double[count];
            bool bFirstTimeThrough = true;
            string allTheNumbers = "";
            Int32 iJulianYearDate = 0;
            Single singMinuteStep = 0;
            Single singConstantTimeStep = 300;

            string pathx = txtDestination.Remove(txtDestination.Length - 4, 4) + ".rin";
            if (checkBinaryOutput == true)
            {
                System.IO.FileStream fsx = File.Create(pathx);


                AddBinaryInt32(fsx, 8 * (1 + QuarterSectionList.Count));
                AddBinaryInt32(fsx, QuarterSectionList.Count);
                AddBinaryInt32(fsx, Int32.MaxValue);
                for (int thisloop = 0; thisloop < QuarterSectionList.Count; thisloop++)
                {
                    string QSNumber = "";
                    QSNumber = (String)QuarterSectionList.GetByIndex(thisloop);
                    QSNumber = QSNumber.PadLeft(8);
                    AddBinaryString(fsx, QSNumber);
                }
                AddBinaryInt32(fsx, 8 * (1 + QuarterSectionList.Count));

                foreach (var entry in query)
                {
                    if (bFirstTimeThrough == true)
                    {
                        for (int loop = 0; loop < count; loop++)
                        {
                            queryRainfall[loop] = (System.Double)entry.ItemArray.GetValue(loop + 1);
                        }
                        queryDate = (System.DateTime)entry.ItemArray.GetValue(0);
                        bFirstTimeThrough = false;
                    }
                    else
                    {
                        if (queryDate == (System.DateTime)entry.ItemArray.GetValue(0))
                        {
                            //place the new numbers into the saving values
                            for (int loop = 0; loop < count; loop++)
                            {
                                queryRainfall[loop] += (System.Double)entry.ItemArray.GetValue(loop + 1);
                            }
                        }
                        else
                        {
                            //write the saved numbers to the file
                            allTheNumbers = "";
                            for (int loop = 0; loop < count; loop++)
                            {
                                allTheNumbers += queryRainfall[loop].ToString() + ",";
                            }
                            AddBinaryInt32(fsx, 12 + 4 * count);
                            //Translate the Year and Date into a Julian integer amalgamation
                            iJulianYearDate = queryDate.Year * 1000 + queryDate.DayOfYear;
                            AddBinaryInt32(fsx, iJulianYearDate);
                            singMinuteStep = (Single)((queryDate.Hour * 60 + queryDate.Minute) * 60);
                            AddBinarySingle(fsx, singMinuteStep);
                            AddBinarySingle(fsx, singConstantTimeStep);
                            for (int loop = 0; loop < count; loop++)
                            {
                                AddBinarySingle(fsx, (Single)queryRainfall[loop]);
                            }
                            AddBinaryInt32(fsx, 12 + 4 * count);
                            //place the new numbers into the saving values
                            for (int loop = 0; loop < count; loop++)
                            {
                                queryRainfall[loop] = (System.Double)entry.ItemArray.GetValue(loop + 1);
                            }
                            //remember the date
                            queryDate = (System.DateTime)entry.ItemArray.GetValue(0);
                        }
                    }
                }

                fsx.Close();
            }

            if (checkBinaryOutput == false)
            {
                fsIndex.Close();
            }
            MessageBox.Show("Process complete.");
        }

        public double RDS(long GNo,
            double R1,
            double R2,
            double R3,
            double d1,
            double d2,
            double d3)
        {
            //RDS = Reciprocal Distanc  e-Squared method
            //GNo = Number of active gages
            //R1, R2, R3 = Rain at Gages 1, 2, & 3
            //D1, D2, D3 = Distance from QS centroid to Gages 1, 2, & 3

            //Case statement needed to prevent div by 0 if # Gages < 3

            double result = 0;

            switch (GNo)
            {
                case 3:
                    result = ((R1 / Math.Pow(d1, 2)) + (R2 / Math.Pow(d2, 2)) + (R3 / Math.Pow(d3, 2))) / ((1 / Math.Pow(d1, 2)) + (1 / Math.Pow(d2, 2)) + (1 / Math.Pow(d3, 2)));
                    break;
                case 2:
                    result = ((R1 / Math.Pow(d1, 2)) + (R2 / Math.Pow(d2, 2))) / ((1 / Math.Pow(d1, 2)) + (1 / Math.Pow(d2, 2)));
                    break;
                case 1:
                    result = ((R1 / Math.Pow(d1, 2))) / ((1 / Math.Pow(d1, 2)));
                    break;
                default:
                    result = 0;
                    break;
            }

            return result;
        }
        //Test assumptions	:There will be many periods of time that are already in use by another virtual raingage
        //					:Every real raingage that is closest to the virtual raingage will be completely in
        //					 use by another virtual raingage.
        //					:Every virtual raingage will require many (say 10 or more) real raingages just
        //					 to assemble one point.  These assumptions are not expected to ever actually happen,
        //					 they are simply extreme conditions which must be accounted for.
        //Starting with:
        //A raingage list
        //A downtime list for each raingage (see if we can add downtimes to the downtime list as the 
        //  other virtual raingages use them up).
        //An sdate and an edate
        //
        //
        /// <summary>
        /// New summary.  Restructure of algorithm and data structure
        /// </summary>

        //the first call to this function should pass sdate and edate
        //this function should have some way to track the previous virtual gage trees
        //passing the array of the virtual gage trees and the index number of the current
        //virtual gage tree should suffice.  Try to find some way to get past this though

        //VirtualGageListCreator
        //This form will simply create a sorted list for the current virtual gage
        //the list will be sorted on the sdate that and particular real gage will
        //be used.  the list will be composed of objects with the elements
        //-raingage number
        //-start time
        //-end time
        //this doesnt really get filled up linearly, just put the nearest raingages
        //that are available in the list at the proper times.
        //VirtualGageListCreator(current virtual gage times list, sdate, edate)
        //	:loop until the virtual gage has all empty time frames full, each iteration of this loop will check the next farthest raingage
        //		:loop through the empty spots in the time frame list(if there are no empty spots, then the time frame list is full
        //			:check to see if this real gage is up during the empty time
        //				:if the gage is not up during the empty time, go to the next empty time
        //				:if the gage is up during this empty time
        //					:if the gage is up during the entire empty time
        //						:place the empty time start and end times in the downtime and usedtime list for the real gage
        //					:if the gage is up only at the start of the empty time
        //						:place those times in the downtime and usedtime list for the real gage
        //					:if the gage is up only at the end of the empty time
        //						:place those times in the downtime and usedtime list for the real gage
        //					:if the gage is up in between the start and end of the empty time
        //						:place those times in the downtime and usedtime list for the real gage

        //The real gage needs to have an original downtime and uptime list, and also
        //a downtime and uptime list that can be modified and then thrown away for
        //each quartersection in question.

        //I have a function which will take the overall time interval, search for a raingage
        //and a useable time within that interval, and return the start and end times which 
        //that raingage is being used for.

        //this function will call the free time look up function.
        //this function is called before the query for raingage data.
        //this function must be called for each quartersection.
        //on every call of this function, after we have gotten all of the info
        //for one virtual gage, we need to call resetdowntimelist for
        //every raingage.  That doesn't take much time (< 1 sec), but
        //maybe in the future find an algorithm that allows us to not have
        //to iterate through all of the raingages.
        SortedList VirtualGageListCreator(gageinfo[][] QuarterSectionGageInfo, long NumberOfGages, int QuarterSectionIndex, DateTime StartDate, DateTime EndDate)
        {
            //list of h2Numbers
            SortedList UsedGages = new SortedList();

            gageinfo gageTracker = new gageinfo();
            bool TimeFrameComplete = false;
            int RealGageNumber = 0;
            int TimeIndex = 0;

            TimeInterval gageTrackerFirstDown = new TimeInterval();
            TimeInterval gageTrackerLastDown = new TimeInterval();
            gageTrackerFirstDown.IntervalStart = DateTime.MinValue;
            gageTrackerFirstDown.IntervalEnd = StartDate;
            gageTrackerLastDown.IntervalStart = EndDate;
            gageTrackerLastDown.IntervalEnd = DateTime.MaxValue;

            gageTracker.DownTimeList = new SortedList();
            gageTracker.SafeDownTimeList = new SortedList();

            gageTracker.SafeDownTimeList.Add(DateTime.MinValue, gageTrackerFirstDown);
            gageTracker.SafeDownTimeList.Add(EndDate, gageTrackerLastDown);

            gageTracker.DownTimeList.Add(DateTime.MinValue, gageTrackerFirstDown);
            gageTracker.DownTimeList.Add(EndDate, gageTrackerLastDown);

            //as long as we have not finished with the time frame
            //and we have not checked all gages

            //this part should look at the times in the gageTracker usedTime list.
            //any difference between the end time of one 'downtime' and the start time
            //of the next 'downtime' should be sent to the LookForUpTime() function
            //and the result should be placed in the list.  After something is
            //placed in the list, the search should start over from scratch,
            //considering that the index would have been messed with.
            while (TimeFrameComplete == false && RealGageNumber < NumberOfGages)
            {
                //for this real gage, peruse the list of available times in gageTracker's time list
                //because we need to go through the list several times, resetting our counter every time
                //we add a new item to the time list, a for loop with no automatic incrementor will
                //be used

                //TimeFrameComplete will be flagged as true on the beginning of every
                //iteration of the for loop and flagged as false if any difference
                //is found between the end date of one TimeInterval and the start
                //date of another TimeInterval.  The problem with this format is that
                //lets say that we do find a gage that fills all the openings.
                //This isn't really a problem, it just makes us do one more 
                //iteration of this loop with one more gage, which we dont have to put in the list
                //because it will return no timeIntervals.
                //REMEMBER WE HAVE TO CALL RESET OF THE TIME INTERVAL LISTS FOR EACH QUARTERSECTION
                TimeFrameComplete = true;
                for (int i = 0; i < gageTracker.DownTimeList.Count - 1; )
                {
                    if (((TimeInterval)gageTracker.DownTimeList.GetByIndex(i)).IntervalEnd == ((TimeInterval)gageTracker.DownTimeList.GetByIndex(i + 1)).IntervalStart)
                    {
                        //this interval's start is equal to the next interval's end
                        //so we move to the next interval
                        i++;
                    }
                    else
                    {
                        //if there was a difference, that means that the list is not yet complete
                        TimeFrameComplete = false;

                        //if the lookforuptime function returns an uptime that is not our 'false' value
                        //we add it to the gagetracker list
                        //and we add it to the quartersection list
                        DateTime firstDate = new DateTime();
                        DateTime lastDate = new DateTime();
                        DateTime thisLastDate = ((TimeInterval)gageTracker.DownTimeList.GetByIndex(i)).IntervalEnd;
                        DateTime nextFirstDate = ((TimeInterval)gageTracker.DownTimeList.GetByIndex(i + 1)).IntervalStart;
                        if (QuarterSectionGageInfo[QuarterSectionIndex][RealGageNumber].LookForUpTime
                            (thisLastDate,
                            nextFirstDate
                            ).IntervalStart != DateTime.MinValue.AddMilliseconds(1))
                        {
                            gageTracker.DownTimeList.Add(
                                ((TimeInterval)QuarterSectionGageInfo[QuarterSectionIndex][RealGageNumber].LookForUpTime(thisLastDate, nextFirstDate)).IntervalStart,
                                (TimeInterval)QuarterSectionGageInfo[QuarterSectionIndex][RealGageNumber].LookForUpTime(thisLastDate, nextFirstDate));
                            if (QuarterSectionGageInfo[QuarterSectionIndex][RealGageNumber].DownTimeList.ContainsKey(
                                ((TimeInterval)QuarterSectionGageInfo[QuarterSectionIndex][RealGageNumber].LookForUpTime(
                                thisLastDate, nextFirstDate)).IntervalStart
                                )
                                )
                            {
                                //do nothing
                            }
                            else
                            {
                                QuarterSectionGageInfo[QuarterSectionIndex][RealGageNumber].DownTimeList.Add(
                                    ((TimeInterval)QuarterSectionGageInfo[QuarterSectionIndex][RealGageNumber].LookForUpTime(
                                    thisLastDate, nextFirstDate)).IntervalStart,
                                    (TimeInterval)QuarterSectionGageInfo[QuarterSectionIndex][RealGageNumber].LookForUpTime(thisLastDate,
                                    nextFirstDate)
                                    );
                            }
                            if (UsedGages.ContainsKey(QuarterSectionGageInfo[QuarterSectionIndex][RealGageNumber].h2Number))
                            {
                                //do nothing
                            }
                            else
                            {
                                UsedGages.Add(QuarterSectionGageInfo[QuarterSectionIndex][RealGageNumber].h2Number, QuarterSectionGageInfo[QuarterSectionIndex][RealGageNumber].h2Number);
                            }
                            i = 0;
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
                //exiting the for loop means that we have exhausted the available times
                //that this real gage can help us.  Move to the next gage

                RealGageNumber++;
            }
            //try to use the virtual gage structure, fill this structure up with data being returned from the
            //LookForUpTime() function.  Probably create these structures within this function
            //that would be the best way to concentrate on this data.  Still, that list would need to be 
            //passed back to the main program after a while.  Maybe this function would pass an array?
            //or just make a global sorted list which is filled with the raingage numbers that are being
            //used by this function.  That would be the best idea really.  But then how do i figure out
            //how to track if the start and end dates are full?  Create a new function within the 
            //time raingage structure.  That would be the best approach with that aslo.
            return UsedGages;
        }

        void basicDownTimeSearch(ref DateTime dateTimeIterator, ref gageinfo currentGagesInfo, ref int h2Index, ref int thisRow, ref double[][] largeRainArray)
        {
            int DownTimeIndex = 0;
            TimeInterval x;

            x = (TimeInterval)currentGagesInfo.DownTimeList.GetByIndex(DownTimeIndex);
            //if the date is not between the start date and end dates of the gage, then set the reading to 200
            if (dateTimeIterator < currentGagesInfo.startDate || dateTimeIterator > currentGagesInfo.endDate)
            {
                largeRainArray[h2Index][thisRow] = 200.0;
                thisRow++;
            }
            else
            {
                //if the gage cannot be down (no reading, but the downtime index is 
                //greater than the number of downtimes) then the reading must be zero.
                if (DownTimeIndex >= currentGagesInfo.DownTimeList.Count)
                {
                    largeRainArray[h2Index][thisRow] = 0.0;
                    thisRow++;
                }

                //if the gage might be down, check the downtime list for that gage
                //and if the date falls during a downtime, then set the reading to 200.
                //Otherwise, set the reading to zero.
                intenseDownTimeSearch(ref DownTimeIndex, ref currentGagesInfo, ref dateTimeIterator, ref x, ref h2Index, ref thisRow, ref largeRainArray);
            }
        }

        /// <summary>
        /// intenseDownTimeSearch should probably be renamed.  This function does a full search of the
        /// downtimes for the gage in question and applies zeros (for no rain) or 200's (an identifier for downtime)
        /// to the largeRainArray.
        /// </summary>
        /// <param name="DownTimeIndex">This identifies the position of the static raingage array we are looking at</param>
        /// <param name="currentGagesInfo">currentgagesInfo is a gageinfo object which is simply used as a 
        /// memory trick to save access times and to make the code look cleaner</param>
        /// <param name="dateTimeIterator">dateTimeIterator is a dateTime value that represents the current time step.</param>
        /// <param name="x">x is the time inteval, which is a start date and end date, of a period of downtime</param>
        /// <param name="h2Index">h2Index is an integer which identifies which gage we are considering.  This 
        /// value identifies the index of the gage in the h2List query, and not the h2Number itself</param>
        /// <param name="thisRow">thisRow is an integer that identifies the current rainfall record we are looking at</param>
        void intenseDownTimeSearch(ref int DownTimeIndex, ref gageinfo currentGagesInfo, ref DateTime dateTimeIterator, ref TimeInterval x, ref int h2Index, ref int thisRow, ref double[][] largeRainArray)
        {
            while (DownTimeIndex < currentGagesInfo.DownTimeList.Count)
            {
                //if the current time step is during a downtime, enter 200 into the array position
                if (dateTimeIterator >= x.IntervalStart)
                {
                    if (dateTimeIterator <= x.IntervalEnd)
                    {
                        largeRainArray[h2Index][thisRow] = 200.0;
                        thisRow++;
                        break;
                    }
                    // or keep looking if the dateTimeIterator is greater than the downtime
                    else
                    {
                        //if there aren't any other downtimes, then this is a zero rain period
                        if (setZeroRainPeriod(ref DownTimeIndex, ref thisRow, ref x, ref h2Index, ref currentGagesInfo, ref largeRainArray) == true)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    largeRainArray[h2Index][thisRow] = 0.0;
                    DownTimeIndex = 0;
                    thisRow++;
                    break;
                }
            }
        }

        void createUsedGagesListForAllQuartersections(int count, SortedList UsedGages, DateTime sdate, DateTime edate, gageinfo[][] QuarterSectionGageInfo, long NumberOfGages)
        {
            //Count is the number of quartersections that have been selected.
            for (int i = 0; i < count; i++)
            {
                SortedList ThisQuarterSectionsGages = new SortedList();
                for (int m = 0; m < 3; m++)
                {
                    ThisQuarterSectionsGages = VirtualGageListCreator(QuarterSectionGageInfo, NumberOfGages, i, sdate, edate);

                    for (int j = 0; j < ThisQuarterSectionsGages.Count; j++)
                    {
                        if (UsedGages.Contains((long)ThisQuarterSectionsGages.GetByIndex(j)))
                        {
                            //do nothing if the master list contains this gage's h2Number
                        }
                        else
                        {
                            UsedGages.Add((long)ThisQuarterSectionsGages.GetByIndex(j), (long)ThisQuarterSectionsGages.GetByIndex(j));
                        }
                    }
                }
                for (int n = 0; n < NumberOfGages; n++)
                {
                    QuarterSectionGageInfo[i][n].ResetDownTimeList();
                }
            }
        }

        private static void AddText(System.IO.FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private static void AddBinaryInt32(System.IO.FileStream fs, Int32 value)
        {
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(value);
        }

        private static void AddBinaryString(System.IO.FileStream fs, string value)
        {
            BinaryWriter bw = new BinaryWriter(fs);
            foreach (char c in value.ToCharArray())
            {
                bw.Write(c);
            }
        }
        private static void AddBinarySingle(System.IO.FileStream fs, Single value)
        {
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(value);
        }

        /// <summary>
        /// setZeroRainPeriod will place zero values for rainfall during the time indicated
        /// if there is no more downtime associated with a raingage, no matching record 
        /// in the rainfall that has been registered, and the current time is still before
        /// the end time of the raingage
        /// </summary>
        /// <param name="DownTimeIndex">This identifies the position of the static raingage array we are looking at</param>
        /// <param name="thisRow">thisRow is an integer that identifies the current rainfall record we are looking at </param>
        /// <param name="x">x is the time inteval, which is a start date and end date, of a period of downtime</param>
        /// <param name="h2Index">h2Index is an integer which identifies which gage we are considering.  This 
        /// value identifies the index of the gage in the h2List query, and not the h2Number itself</param>
        /// <param name="currentGagesInfo">currentgagesInfo is a gageinfo object which is simply used as a 
        /// memory trick to save access times and to make the code look cleaner</param>
        bool setZeroRainPeriod(ref int DownTimeIndex, ref int thisRow, ref TimeInterval x, ref int h2Index, ref gageinfo currentGagesInfo, ref double[][] largeRainArray)
        {
            bool returnValue = false;

            if (DownTimeIndex + 1 >= currentGagesInfo.DownTimeList.Count)
            {
                largeRainArray[h2Index][thisRow] = 0.0;
                thisRow++;
                DownTimeIndex = 0;
                returnValue = true; ;
            }
            else
            {
                x = (TimeInterval)currentGagesInfo.DownTimeList.GetByIndex(++DownTimeIndex);
            }

            return returnValue;
        }
    }
}
