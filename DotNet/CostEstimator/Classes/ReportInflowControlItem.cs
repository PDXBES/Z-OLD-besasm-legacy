// Project: Classes, File: ReportInflowControlItem.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: ReportInflowControlItem
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 28, Size of file: 388 Bytes
// Creation date: 6/11/2008 11:16 AM
// Last modified: 7/16/2008 12:53 PM


using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// An inflow control reporting item
  /// </summary>
	public class ReportInflowControlItem : ReportItem
	{
		#region Variables
		private string _Name;
		private int _Id;
		private string _ControlType;
		private string _ControlSubtype;
		private decimal _Cost;
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
		/// ID
		/// </summary>
		/// <returns>Int</returns>
		public int ID
		{
			get
			{
				return _Id;
			} // get

			set
			{
				_Id = value;
			} // set
		} // ID

		/// <summary>
		/// Control type
		/// </summary>
		/// <returns>String</returns>
		public string ControlType
		{
			get
			{
				return _ControlType;
			} // get

			set
			{
				_ControlType = value;
			} // set
		} // ControlType

		/// <summary>
		/// Control subtype
		/// </summary>
		/// <returns>String</returns>
		public string ControlSubtype
		{
			get
			{
				return _ControlSubtype;
			}

			set
      {
      	_ControlSubtype = value;
      }
		} // ControlSubtype

		/// <summary>
		/// Cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal Cost
		{
			get
			{
				return _Cost;
			} // get

			set
			{
				_Cost = value;
			} // set
		} // Cost

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
				if (value == null)
					_Comment = "";
				else
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
			xw.WriteStartElement("id");
			xw.WriteValue(_Id);
			xw.WriteEndElement();

			xw.WriteStartElement("name");
			xw.WriteValue(_Name);
			xw.WriteEndElement();

			xw.WriteStartElement("controlType");
			xw.WriteValue(_ControlType);
			xw.WriteEndElement();

			xw.WriteStartElement("controlSubtype");
			xw.WriteValue(_ControlSubtype);
			xw.WriteEndElement();

			xw.WriteStartElement("cost");
			xw.WriteValue(_Cost);
			xw.WriteEndElement();

			xw.WriteStartElement("comment");
			xw.WriteValue(_Comment);
			xw.WriteEndElement();
		} // WriteXML()
		#endregion

	}
}
