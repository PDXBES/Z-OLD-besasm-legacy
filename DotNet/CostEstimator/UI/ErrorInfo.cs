// Project: UI, File: ErrorInfo.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.UI, Class: ErrorInfo
// Path: C:\Development\DotNet\CostEstimator\UI, Author: Arnel
// Code lines: 26, Size of file: 389 Bytes
// Creation date: 7/31/2008 4:12 PM
// Last modified: 7/31/2008 4:16 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
	public class ErrorInfo
	{
		#region Variables
		/// <summary>
		/// Flag for whether an error has occurred
		/// </summary>
    private bool _NoError = true;

    /// <summary>
    /// Holds the error message for display to the user
    /// </summary>
		private string _Message = string.Empty;
		#endregion

		#region Constructors
		/// <summary>
		/// Create error info
		/// </summary>
		/// <param name="noError">No error</param>
		/// <param name="message">Message</param>
		public ErrorInfo(bool noError, string message)
		{
			_NoError = noError;
			_Message = message;
		} // ErrorInfo(noError, message)
		#endregion

		#region Properties
		/// <summary>
		/// No error
		/// </summary>
		/// <returns>Bool</returns>
		public bool NoError
		{
			get
			{
				return _NoError;
			} // get

			set
			{
				_NoError = value;
			} // set
		} // NoError

		/// <summary>
		/// Message
		/// </summary>
		/// <returns>String</returns>
		public string Message
		{
			get
			{
				return _Message;
			} // get

			set
			{
				_Message = value;
			} // set
		} // Message
		#endregion
	}
}
