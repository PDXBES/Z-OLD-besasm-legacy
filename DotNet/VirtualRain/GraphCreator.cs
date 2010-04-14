using System;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace VirtualRain
{
	/// <summary>
	/// Summary description for GraphCreator.
	/// </summary>
	public class GraphCreator
	{
		//GraphCreator should take a handle to the output window
		//GraphCreator should also take a DataSet of some kind,
		//either a text file or a true DataSet.

		double point1x;
		double point1y;
		double point2x;
		double point2y;
		float theFontSize = 9.0F;

		Graphics g;

		public GraphCreator(PictureBox theBox, DataSet theData, System.Windows.Forms.PaintEventArgs e)
		{
			g = e.Graphics;
			Pen blackPen = new Pen(System.Drawing.Color.Black, 1);
			Pen bluePen = new Pen(System.Drawing.Color.Blue, 1);
			
			//draw something on the picturebox

			//these margins are required variables and should be
			//modified later to fit the width and height of the
			//strings describing the graph

			//the margins should be changed to allow for the width of the text
			//which denotes the tick marks on the graph.  This would really need
			//to be done once we have the number of significant digits involved
			//in the difference between the highest and lowest numbers
			int leftMargin = (int)(theFontSize*6);
			int rightMargin = 10;
			int topMargin = 10;
			int bottomMargin = (int)(theFontSize*3);
			//the axis ticks are simply the tick marks on the axes which are 
			//labeled
			int numberOfYAxisTicks = 5;
			int numberOfXAxisTicks = 5;
			//the magnitudes indicate the power of 10 which the distance between
			//the minimum and maximum values are
			System.TimeSpan magnitudeX;
			int magnitudeY = 0;
			//the buffer percents are the relative amounts of empty space on a graph
			//to place between the minimum and maximum points and the extents of 
			//the graph.  Buffer percents are can and should be overridden by
			//ceiling and floor demi-integers.
			double bufferPercentX = 0.05;
			double bufferPercentY = 0.05;

			int count = theData.Tables[0].Rows.Count - 1;

			double staggerX = (theBox.Width-(leftMargin+rightMargin))/((double)count);
			System.Type yType = theData.Tables[0].Columns[1].DataType;

			object normalizePosYObject = theData.Tables[0].Compute("Max("+theData.Tables[0].Columns[1].ToString()+")", "");
			object normalizeNegYObject = theData.Tables[0].Compute("Min("+theData.Tables[0].Columns[1].ToString()+")", "");
			double maxValue = (double) normalizePosYObject;
			double minValue = (double) normalizeNegYObject;
			double normalizeY = maxValue - minValue;
			if(normalizeY == 0)
			{
				normalizeY = 1;
			}

			System.Type xType = theData.Tables[0].Columns[0].DataType;

			System.DateTime normalizePosXObject = (System.DateTime) theData.Tables[0].Compute("Max("+theData.Tables[0].Columns[0].ToString()+")", "");
			System.DateTime normalizeNegXObject = (System.DateTime) theData.Tables[0].Compute("Min("+theData.Tables[0].Columns[0].ToString()+")", "");
			DateTime maxXValue = (System.DateTime) normalizePosXObject;
			DateTime minXValue = (System.DateTime) normalizeNegXObject;
			TimeSpan normalizeX = maxXValue.Subtract(minXValue);
			
			if(normalizeX.CompareTo(System.TimeSpan.Zero) == 0)
			{
				TimeSpan justADay = System.DateTime.Now.AddDays(1).Subtract(System.DateTime.Now);
				normalizeX = normalizeX.Add(justADay.Duration());
			}

			int bottomOfPictureBox = theBox.Height;
			int bottomOfGraph = theBox.Height - bottomMargin;
			int heightOfGraph = theBox.Height - (bottomMargin + topMargin);
			int widthOfGraph = theBox.Width - (rightMargin + leftMargin);

			double DByValue = 0;
			System.Data.DataTable theTable = theData.Tables[0];
		
			for(int i = 0; i < count; i++)
			{

				point1x = (float)leftMargin + staggerX * (float)i;
				point1y = (float)(bottomOfGraph+((minValue-(double)theTable.Rows[i].ItemArray.GetValue(1))/normalizeY)*heightOfGraph);

				point2x = (float)leftMargin + staggerX * (float)(i + 1);
				point2y = (float)(bottomOfGraph+((minValue-(double)theTable.Rows[i+1].ItemArray.GetValue(1))/normalizeY)*heightOfGraph);

				g.DrawLine(bluePen, (float)point1x, (float)point1y, (float)point2x, (float)point2y);
			}
			
			//set up the axes
			PointF Vertex =  new PointF(leftMargin, bottomOfGraph);
			PointF TopOfYAxis =  new PointF(leftMargin, 0);
			PointF RightEndOfXAxis =  new PointF(theBox.Width, bottomOfGraph);

			//draw the axes
			g.DrawLine(blackPen, Vertex, TopOfYAxis);
			g.DrawLine(blackPen, Vertex, RightEndOfXAxis);

			//this is where the extents are reduced to having 2 digits to the
			//left of the decimal point so that the minimum and maximum can be
			//expressed as a ceiling or floor with a minor difference.
			//the reason for this algorithm is that say you have a group of
			//numbers which fluctuate by only 0.0001 but do so at mean of 1023.
			//this results in numbers between 1022.9999 and 1023.0001, which
			//would require something like this to express.
			//On second thought, not even excel cares about that kind of degree
			//of accuracy.  I'll leave it as is until someone asks for more.

			//as of last testing this algorithm worked.  I'm not going to
			//say it will always work, as I mashed the last function in
			//without giving it much thought.

			//BUG: on resizing redraw, these numbers return to multiple digit floats
			//Why does redraw ignore this?  This is fixed by using the string format
			//method.  Should still be tweaked.
			magnitudeY = (int)System.Math.Ceiling(System.Math.Log10((double)(maxValue-minValue)));
			double maxYAxisValueNormal = System.Math.Ceiling(maxValue*System.Math.Pow(10,2-magnitudeY));
			double maxYAxisValue = maxYAxisValueNormal * System.Math.Pow(10, magnitudeY-2);
			
			double Y3 = (double)(maxValue-minValue);
			int magnitudeY2 = (int)Math.Ceiling(Y3/(Math.Pow(10,Math.Floor(Math.Log10(Y3))-1)));
			double YAxisTickDistance = (magnitudeY2 * Math.Pow(10, Math.Floor(Math.Log10(Y3))-1)/(numberOfYAxisTicks-1));

			//do this same thing only now for the X values
			magnitudeX = (maxXValue.Subtract(minXValue));

			//draw the tick values
			System.Drawing.Font theFont = new Font("Times New Roman", theFontSize);

			//how do I make sure that I am drawing out to the proper accuracy without using too many digits
			//to represent very small numbers?  Shouldn't I also be using string representations of numbers
			//in some cases in order to avoid float error?  Examine this problem.  Also I should build the string number
			//library when I get home.  This will allow me to look at the way the strings are being represented
			//under the formats I have given them.


			for(int j = 0; j < numberOfYAxisTicks; j++)
			{
				
				g.DrawString(string.Format("{0:0.000}",maxYAxisValue-j*YAxisTickDistance), theFont, Brushes.Black, 1, j*(heightOfGraph/(numberOfYAxisTicks-1)));
				g.DrawLine(blackPen, leftMargin, (bottomOfPictureBox - bottomMargin) - j*(((float)heightOfGraph)/((float)(numberOfYAxisTicks-1))), leftMargin-2, (bottomOfPictureBox - bottomMargin) - j*(((float)heightOfGraph)/((float)(numberOfYAxisTicks-1))));
			}

			for(int k = 0; k < numberOfXAxisTicks; k++)
			{
				g.DrawString(((minXValue.AddMinutes(k*(magnitudeX.TotalMinutes)/(numberOfXAxisTicks - 1))).ToShortDateString()), theFont, Brushes.Black, k*(widthOfGraph/(numberOfXAxisTicks-1)), heightOfGraph+topMargin+2);
			}
		}
	}
}