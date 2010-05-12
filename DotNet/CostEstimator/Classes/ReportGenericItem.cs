// Project: Classes, File: ReportGenericItem.cs
// Namespace: CostEstimator.Classes, Class: ReportGenericItem
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 32, Size of file: 480 Bytes
// Creation date: 4/29/2008 6:07 PM
// Last modified: 7/16/2008 12:55 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	public class ReportGenericItem : ReportItem
	{
		#region Variables
		private string _Name;
		private double _Quantity;
		private string _UnitName;
		private decimal _UnitPrice;
		private string _Group;
		private CostItemFactor _Item;
		private string _Comment = string.Empty;
		#endregion

		#region Properties
		/// <summary>
		/// Name
		/// </summary>
		/// <returns>String</returns>
		public string Name
		{
			get
			{
				return _Name;
			} // get

			set
			{
				_Name = value;
			} // set
		} // Name

		/// <summary>
		/// Quantity
		/// </summary>
		/// <returns>Double</returns>
		public double Quantity
		{
			get
			{
				return _Quantity;
			} // get

			set
			{
				_Quantity = value;
			} // set
		} // Quantity

		/// <summary>
		/// Unit name
		/// </summary>
		/// <returns>String</returns>
		public string UnitName
		{
			get
			{
				return _UnitName;
			} // get

			set
			{
				_UnitName = value;
			} // set
		} // UnitName

		/// <summary>
		/// Unit price
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal UnitPrice
		{
			get
			{
				return _UnitPrice;
			} // get

			set
			{
				_UnitPrice = value;
			} // set
		} // UnitPrice

		/// <summary>
		/// Group
		/// </summary>
		/// <returns>String</returns>
		public string Group
		{
			get
			{
				return _Group;
			} // get

			set
			{
				_Group = value;
			} // set
		} // Group

		/// <summary>
		/// Item
		/// </summary>
		/// <returns>Cost item factor</returns>
		public CostItemFactor Item
		{
			get
			{
				return _Item;
			} // get

			set
			{
				_Item = value;
			} // set
		} // Item
		
		/// <summary>
		/// Total cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal TotalCost
		{
			get
			{
				return (decimal)((double)_UnitPrice * _Quantity);
			} // get
		} // TotalCost

		/// <summary>
		/// Comment
		/// </summary>
		/// <returns>String</returns>
		public string Comment
		{
			get
			{
				return _Comment;
			} // get

			set
			{
				_Comment = value;
			} // set
		} // Comment
		#endregion

		#region Methods
		/// <summary>
		/// Write XML
		/// </summary>
		/// <param name="xw">Xw</param>
		public void WriteXML(XmlWriter xw)
		{
			xw.WriteStartElement("name");
			xw.WriteValue(_Name);
			xw.WriteEndElement();

			xw.WriteStartElement("quantity");
			xw.WriteValue(_Quantity);
			xw.WriteEndElement();

			xw.WriteStartElement("unitName");
			xw.WriteValue(_UnitName);
			xw.WriteEndElement();

			xw.WriteStartElement("unitPrice");
			xw.WriteValue(_UnitPrice);
			xw.WriteEndElement();

			xw.WriteStartElement("group");
			xw.WriteValue(_Group);
			xw.WriteEndElement();

			xw.WriteStartElement("item");
			xw.WriteValue(_Item.ID);
			xw.WriteEndElement();

			xw.WriteStartElement("comment");
			xw.WriteValue(_Comment);
			xw.WriteEndElement();
		} // WriteXML()
		#endregion
	}
}
