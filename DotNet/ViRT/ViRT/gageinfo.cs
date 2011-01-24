using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ViRT
{
    class gageinfo : IComparable
    {
        public long ID;
        public double Dist;
        public float xPos;
        public float yPos;
        public DateTime startDate;
        public DateTime endDate;
        public long h2Number;
        public long stationID;
        //DownTimeList holds all of the downtimes and used time periods for a quartersection
        public SortedList DownTimeList;
        //SafeDownTimeList holds only the original downtimes, DownTimeList is reset to the values in
        //SafeDownTimeList when the complete virtual raingage for a quartersection is complete.
        public SortedList SafeDownTimeList;

        public int CompareTo(object o)
        {

            if (!(o is gageinfo))
            {
                throw new ArgumentException("o must be of type 'gageinfo'");
            }

            gageinfo v = (gageinfo)o;
            return (int)(Dist - v.Dist);
        }

        //ResetDownTimeList Empties DownTimeList and places all the values in SafeDownTimeList
        //into DownTimeList
        public void ResetDownTimeList()
        {
            DownTimeList.Clear();
            for (int i = 0; i < SafeDownTimeList.Count; i++)
            {
                DownTimeList.Add(((TimeInterval)SafeDownTimeList.GetByIndex(i)).IntervalStart, SafeDownTimeList.GetByIndex(i));
            }
        }

        //This function should be used to examine whether or not a list of times
        //within the sortedList class is completely full.  The problem with placing
        //this function within the gageinfo class is that it does not cross gage
        //numbers.  Consider the new structure for this instead.  Later on merge
        //the two structures.  Maybe consider some other way to do this, because
        //there seems to be no way to do this in both structures without simply
        //copying code.

        //search the downtime list for any period of time which is available

        //remember that there is a startDate and endDate for each gage.
        //this should be taken into account for in the DownTimeList.
        //I guess the best way to place these objects into the DownTimeList
        //is to put them into the downTimeList when the startDate and endDate
        //are first discovered.  The IntervalStart should be the lowest number
        //that can be represented in DateTime format and the IntervalEnd should
        //be the highest number that can be represented in DateTimeFormat.
        public TimeInterval LookForUpTime(DateTime start, DateTime end)
        {
            TimeInterval ReturnTime = new TimeInterval();
            for (int i = 0; i < DownTimeList.Count; i++)
            {
                //if there is a downtime, and our time interval begins before it
                if (start < ((TimeInterval)DownTimeList.GetByIndex(i)).IntervalStart)
                {
                    //mark our time interval start as the returned start time
                    ReturnTime.IntervalStart = start;
                    //if our time interval ends before the start of the downtime
                    if (end <= ((TimeInterval)DownTimeList.GetByIndex(i)).IntervalStart)
                    {
                        //mark our time interval end as the returned end time
                        ReturnTime.IntervalEnd = end;
                        return ReturnTime;
                    }
                    //if the start time of the downtime occurs before the requested end time
                    else
                    {
                        //mark the downtime start as the returned end time
                        ReturnTime.IntervalEnd = ((TimeInterval)DownTimeList.GetByIndex(i)).IntervalStart;
                        return ReturnTime;
                    }
                }
                //if our interval ends after the downtime ends
                if (end > ((TimeInterval)DownTimeList.GetByIndex(i)).IntervalEnd
                    && start < ((TimeInterval)DownTimeList.GetByIndex(i)).IntervalEnd)
                {
                    //recurse the function and set the important intevervals as those occuring after this downtime period
                    return LookForUpTime(((TimeInterval)DownTimeList.GetByIndex(i)).IntervalEnd, end);
                }
                //if the whole time period is down, return null
                if (start >= ((TimeInterval)DownTimeList.GetByIndex(i)).IntervalStart
                    && end <= ((TimeInterval)DownTimeList.GetByIndex(i)).IntervalEnd)
                {
                    //null is represented by 1 millisecond after the absolute lowest beginning time
                    //that can be represented by the DateTime object.  This allows us
                    //to at some time in the future still use MinValue, and not have to worry
                    //too much about it.  There really should be a way to represent
                    //this as null.  Try to find a more formal version of null for
                    //the DateTime object.
                    ReturnTime.IntervalStart = DateTime.MinValue.AddMilliseconds(1);
                    ReturnTime.IntervalEnd = DateTime.MinValue.AddMilliseconds(1);
                    return ReturnTime;
                }
            }
            //if there is nothing in the list or no matches, return the entire time
            ReturnTime.IntervalStart = start;
            ReturnTime.IntervalEnd = end;
            return ReturnTime;
        }
    }
}
