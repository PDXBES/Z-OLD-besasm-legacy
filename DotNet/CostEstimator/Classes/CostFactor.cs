// Project: Classes, File: CostFactor.cs
// Namespace: CostEstimator.Classes, Class: CostFactor
// Path: C:\Development\CostEstimatorV2\Classes, Author: Arnel
// Code lines: 28, Size of file: 348 Bytes
// Creation date: 3/1/2008 3:44 PM
// Last modified: 3/26/2008 10:12 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Determines how cost factor is applied. Simple = CIF.Cost * Factor,
  /// Additive = CIF.Cost * (1 + Factor), IndirectAdditive = CIF.Cost * (1 + Sum
  /// of all Indirect Factors on same level), IndirectSimple = CIF.Cost * (Sum
  /// of all Indirect Factors on same level)
  /// </summary>
	public enum CostFactorType
	{
		Simple,
		Additive,
		IndirectAdditive,
		IndirectSimple
	} // enum CostFactorType

  /// <summary>
  /// A factor applied to a CostItemFactor's cost, after all child costs and
  /// factors have been calculated and added to the CostItemFactor's CostItem,
  /// if any
  /// </summary>
	public class CostFactor
	{
		#region Variables
		private static int _CurrentID = 0;

		private int _Id;
		private string _Name;
		private double _FactorValue;
		private CostFactorType _FactorType;
		private string _Comment;
		#endregion

		#region Constructors
		/// <summary>
		/// Create cost factor
		/// </summary>
		public CostFactor()
		{
			_Id = _CurrentID;
			_CurrentID++;

		} // CostFactor()

		/// <summary>
		/// Create cost factor
		/// </summary>
		/// <param name="factorValue">Factor value</param>
		public CostFactor(string name, double factorValue)
		{
			_Id = _CurrentID;
			_CurrentID++;

			_Name = name;
			_FactorValue = factorValue;
			_FactorType = CostFactorType.Additive;
		} // CostFactor(factorValue)

		/// <summary>
		/// Create cost factor
		/// </summary>
		/// <param name="name">Name</param>
		/// <param name="factorValue">Factor value</param>
		/// <param name="factorType">Factor type</param>
		public CostFactor(string name, double factorValue, CostFactorType factorType)
		{
			_Id = _CurrentID;
			_CurrentID++;

			_Name = name;
			_FactorValue = factorValue;
			_FactorType = factorType;
		} // CostFactor(name, factorValue, factorType)

		/// <summary>
		/// Create cost factor
		/// </summary>
		/// <param name="costFactorType">Cost factor type</param>
		public CostFactor(CostFactor costFactor)
		{
			_Id = _CurrentID;
			_CurrentID++;

			_Name = costFactor.Name;
			_FactorType = costFactor._FactorType;
			_FactorValue = costFactor.FactorValue;
		} // CostFactor(costFactorType)

		/// <summary>
		/// Create cost factor
		/// </summary>
		/// <param name="costFactor">Cost factor</param>
		/// <param name="newName">New name</param>
		public CostFactor(CostFactor costFactor, string newName)
		{
			_Id = _CurrentID;
			_CurrentID++;

			_Name = newName;
			_FactorType = costFactor._FactorType;
			_FactorValue = costFactor.FactorValue;
		} // CostFactor(costFactor, newName)

		/// <summary>
		/// Create cost factor
		/// </summary>
		/// <param name="id">Id</param>
		public CostFactor(int id, string name, double factorValue, CostFactorType factorType, string comment)
		{
			_Id = id;
			_Name = name;
			_FactorValue = factorValue;
			_FactorType = factorType;
			_Comment = comment;

			if (id > _CurrentID)
				_CurrentID = id + 1;
		} // CostFactor()
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
		/// Factor value
		/// </summary>
		/// <returns>Double</returns>
		public double FactorValue
		{
			get
			{
				return _FactorValue;
			} // get
			set
			{
				_FactorValue = value;
			} // set
		} // FactorValue

		/// <summary>
		/// Factor type
		/// </summary>
		/// <returns>Double</returns>
		public CostFactorType FactorType
		{
			get
			{
				return _FactorType;
			} // get
			set
			{
				_FactorType = value;
			} // set
		} // FactorType

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
			_CurrentID = 0;
		} // ResetIDGenerator()

		/// <summary>
		/// Revert ID
		/// </summary>
		/// <returns>Int</returns>
		public static int RevertID()
		{
			_CurrentID--;
			if (_CurrentID == 0)
				_CurrentID = 0;
			return _CurrentID;
		} // RevertID()

		/// <summary>
		/// Set current ID
		/// </summary>
		/// <param name="currentID">Current ID</param>
		public static void SetCurrentID(int currentID)
		{
			_CurrentID = currentID;
		} // SetCurrentID(currentID)

		/// <summary>
		/// Assign ID
		/// </summary>
		public void AssignID()
		{
			_Id = _CurrentID;
			_CurrentID++;
		} // AssignID()

		/// <summary>
		/// Write XML for saving CostFactor information to file
		/// </summary>
		/// <param name="xw">Xw</param>
		public void WriteXML(XmlWriter xw)
		{
			xw.WriteStartElement("CostFactor");

			xw.WriteStartElement("id");
			xw.WriteValue(_Id);
			xw.WriteEndElement();

			xw.WriteStartElement("name");
			xw.WriteValue(_Name);
			xw.WriteEndElement();

			xw.WriteStartElement("factorvalue");
			xw.WriteValue(_FactorValue);
			xw.WriteEndElement();

			xw.WriteStartElement("factortype");
			xw.WriteValue(_FactorType.ToString());
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
