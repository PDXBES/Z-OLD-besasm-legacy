// Project: Classes, File: CostItem.cs
// Namespace: CostEstimator.Classes, Class: CostItem
// Path: C:\Development\CostEstimatorV2\Classes, Author: Arnel
// Code lines: 28, Size of file: 342 Bytes
// Creation date: 3/1/2008 3:44 PM
// Last modified: 6/19/2008 3:22 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	public class CostItem
	{
		#region Variables
		private static int _CurrentId = 0;

		private int _Id;
		private string _Name;
		private double _Quantity;
		private decimal _UnitCost;
		private string _UnitName;
		private string _Comment;
		#endregion

		#region Constructors
		/// <summary>
		/// Create cost item
		/// </summary>
		public CostItem()
		{
			AssignID();
		} // CostItem()

		/// <summary>
		/// Create cost item
		/// </summary>
		/// <param name="name">Name</param>
		public CostItem(string name)
		{
			AssignID();

			_Name = name;
		} // CostItem(name)

		/// <summary>
		/// Create cost item
		/// </summary>
		/// <param name="name">Name</param>
		/// <param name="quantity">Quantity</param>
		/// <param name="unitCost">Unit cost</param>
		/// <param name="unitName">Unit name</param>
		public CostItem(string name, double quantity, decimal unitCost, string unitName)
		{
			AssignID();

			_Name = name;
			_Quantity = quantity;
			_UnitCost = unitCost;
			_UnitName = unitName;
		} // CostItem(name, quantity, unitCost)

		/// <summary>
		/// Create cost item
		/// </summary>
		/// <param name="costItem">Cost item</param>
		public CostItem(CostItem costItem)
		{
			AssignID();

			_Name = costItem.Name;
			_Quantity = costItem.Quantity;
			_UnitCost = costItem._UnitCost;
			_UnitName = costItem.UnitName;
		} // CostItem(costItem)

		/// <summary>
		/// Create cost item
		/// </summary>
		/// <param name="costItem">Cost item</param>
		/// <param name="newName">New name</param>
		public CostItem(CostItem costItem, string newName)
		{
			AssignID();

			_Name = newName;
			_Quantity = costItem.Quantity;
			_UnitCost = costItem._UnitCost;
			_UnitName = costItem.UnitName;
		} // CostItem(costItem, newName)

		/// <summary>
		/// Create cost item
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="name">Name</param>
		/// <param name="quantity">Quantity</param>
		/// <param name="unitCost">Unit cost</param>
		/// <param name="unitName">Unit name</param>
		/// <param name="comment">Comment</param>
		public CostItem(int id, string name, double quantity, decimal unitCost,
			string unitName, string comment)
		{
			_Id = id;
			_Name = name;
			_Quantity = quantity;
			_UnitCost = unitCost;
			_UnitName = unitName;
			_Comment = comment;

			if (id > _CurrentId)
				_CurrentId = id + 1;
		} // CostItem(id, name, quantity)
		#endregion

		#region Properties
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
		} // ID

		/// <summary>
		/// Cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal Cost
		{
			get
			{
				return (decimal)((double)UnitCost * Quantity);
			} // get
		} // Cost

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
		/// Unit cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal UnitCost
		{
			get
			{
				return _UnitCost;
			} // get
			set
			{
				_UnitCost = value;
			} // set
		} // UnitCost

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
		/// Reset ID generator
		/// </summary>
		public static void ResetIDGenerator()
		{
			_CurrentId = 0;
		} // ResetIDGenerator()

		/// <summary>
		/// Revert ID
		/// </summary>
		/// <returns>Int</returns>
		public static int RevertID()
		{
			_CurrentId--;
			if (_CurrentId == 0)
				_CurrentId = 0;
			return _CurrentId;
		} // RevertID()

		/// <summary>
		/// Set current ID
		/// </summary>
		/// <param name="currentID">Current ID</param>
		public static void SetCurrentID(int currentID)
		{
			_CurrentId = currentID;
		} // SetCurrentID(currentID)

		/// <summary>
		/// Assign ID
		/// </summary>
		public void AssignID()
		{
			_Id = _CurrentId;
			_CurrentId++;
		} // AssignID()

		/// <summary>
		/// Write XML
		/// </summary>
		public void WriteXML(XmlWriter xw)
		{
			xw.WriteStartElement("CostItem");
			
			xw.WriteStartElement("id");
			xw.WriteValue(_Id);
			xw.WriteEndElement();

			xw.WriteStartElement("name");
			xw.WriteValue(_Name);
			xw.WriteEndElement();

			xw.WriteStartElement("quantity");
			xw.WriteValue(_Quantity);
			xw.WriteEndElement();

			xw.WriteStartElement("unitcost");
			xw.WriteValue(_UnitCost);
			xw.WriteEndElement();

			xw.WriteStartElement("unitname");
			xw.WriteValue(_UnitName);
			xw.WriteEndElement();

			xw.WriteStartElement("comment");
			if (_Comment != null)
				xw.WriteValue(_Comment);
			xw.WriteEndElement();

			xw.WriteEndElement();
		} // WriteXML()

		/// <summary>
		/// To string
		/// </summary>
		/// <returns>String</returns>
		public override string ToString()
		{
			return string.Format("{0}-{1}", _Id, _Name);
		} // ToString()

		#endregion
	}
}
