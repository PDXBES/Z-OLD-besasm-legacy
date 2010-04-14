// Project: UI, File: DynamicTextField.cs
// Namespace: ArcSpooler.UI, Class: DynamicTextField
// Path: D:\Development\ArcSpooler\UI, Author: arnelm
// Code lines: 15, Size of file: 284 Bytes
// Creation date: 10/31/2008 7:38 PM
// Last modified: 11/3/2008 9:16 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace ArcSpooler.UI
{
	public class DynamicTextField
	{
		#region Constructors
		/// <summary>
		/// Create dynamic text field
		/// </summary>
		/// <param name="baseName">Base name</param>
		/// <param name="modifyToField">Modify to field</param>
		public DynamicTextField(string baseName, string modifyToField, string replaceFormat)
		{
			BaseName = baseName;
			ModifyToField = modifyToField;
			ReplaceFormat = replaceFormat;
		} // DynamicTextField(baseName, modifyToField)
		#endregion

		#region Properties
		/// <summary>
		/// Base name
		/// </summary>
		/// <returns>String</returns>
		public string BaseName { get; set; } // BaseName

		/// <summary>
		/// Replace format
		/// </summary>
		/// <returns>String</returns>
		public string ReplaceFormat { get; set; } // ReplaceFormat
		/// <summary>
		/// Modify to field
		/// </summary>
		/// <returns>String</returns>
		public string ModifyToField { get; set; } // ModifyToField

		/// <summary>
		/// Replace string
		/// </summary>
		/// <returns>String</returns>
		public string ReplaceString { get; set; } // ReplaceString

		/// <summary>
		/// Slot
		/// </summary>
		/// <returns>Int</returns>
		public int Slot { get; set; } // Slot

		/// <summary>
		/// Name
		/// </summary>
		/// <returns>String</returns>
		public string Name
		{
			get
			{
				return string.Format(BaseName, Slot);
			} // get
		} // Name

		/// <summary>
		/// Modify to
		/// </summary>
		/// <returns>String</returns>
		public string ModifyTo
		{
			get
			{
				return string.Format(ReplaceFormat, ReplaceString);
			} // get
		} // ModifyTo

		/// <summary>
		/// Boundary frame
		/// </summary>
		/// <returns>String</returns>
		public string BoundaryFrame { get; set; } // BoundaryFrame

		/// <summary>
		/// Border x from boundar
		/// </summary>
		/// <returns>Double</returns>
		public double BorderXFromBoundary { get; set; } // BorderXFromBoundar

		/// <summary>
		/// Border y from boundary
		/// </summary>
		/// <returns>Double</returns>
		public double BorderYFromBoundary { get; set; } // BorderYFromBoundary
		#endregion
	}
}
