// Project: UI, File: UserSettings.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.UI, Class: UserSettings
// Path: C:\Development\DotNet\CostEstimator\UI, Author: Arnel
// Code lines: 27, Size of file: 493 Bytes
// Creation date: 7/9/2008 10:09 PM
// Last modified: 8/18/2009 1:59 PM

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Drawing;
using System.Collections;

namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
	public class UserSettings : ApplicationSettingsBase
	{
		/// <summary>
		/// Form size
		/// </summary>
		/// <returns>Size</returns>
		[UserScopedSetting]
		[DefaultSettingValue("800,600")]
		public Size FormSize
		{
			get
			{
				return (Size)this["FormSize"];
			} // get
			set
			{
				this["FormSize"] = value;
			} // set
		} // FormSize

		/// <summary>
		/// Form location
		/// </summary>
		/// <returns>Point</returns>
		[UserScopedSetting]
		[DefaultSettingValue("0,0")]
		public Point FormLocation
		{
			get
			{
				return (Point)this["FormLocation"];
			} // get
			set
			{
				this["FormLocation"] = value;
			} // set
		} // FormLocation

		/// <summary>
		/// Most recently used files
		/// </summary>
		/// <returns>Array list</returns>
		[UserScopedSetting]
		[DefaultSettingValue("")]
		public ArrayList MostRecentlyUsedFiles
		{
			get
			{
				return (ArrayList)this["MostRecentlyUsedFiles"];
			} // get
			set
			{
				this["MostRecentlyUsedFiles"] = value;
			} // set
		} // MostRecentlyUsedFiles

		/// <summary>
		/// Num most recently used files
		/// </summary>
		/// <returns>Int</returns>
		[UserScopedSetting]
		[DefaultSettingValue("5")]
		public int NumMostRecentlyUsedFiles
		{
			get
			{
				return (int)this["NumMostRecentlyUsedFiles"];
			} // get
			set
			{
				this["NumMostRecentlyUsedFiles"] = value;
			} // set
		} // NumMostRecentlyUsedFiles

		/// <summary>
		/// Last model directories
		/// </summary>
		/// <returns>Array list</returns>
		[UserScopedSetting]
		[DefaultSettingValue("")]
		public string LastModelDirectory
		{
			get
			{
				return (string)this["LastModelDirectory"];
			} // get
			set
			{
				this["LastModelDirectory"] = value;
			} // set
		} // LastModelDirectories

		/// <summary>
		/// Include RIK in costs
		/// </summary>
		/// <returns>Bool</returns>
		[UserScopedSetting]
		[DefaultSettingValue("true")]
		public bool IncludeRIKInCosts
		{
			get
			{
				return (bool)this["IncludeRIKInCosts"];
			} // get
			set
			{
				this["IncludeRIKInCosts"] = value;
			} // set
		} // IncludeRIKInCosts

		/// <summary>
		/// Master pip XP location
		/// </summary>
		/// <returns>String</returns>
		[UserScopedSetting]
		[DefaultSettingValue(@"W:\SAMaster_22\Links\mst_PipXP_ac.mdb")]
		public string MasterPipXPLocation
		{
			get
			{
				return (string)this["MasterPipXPLocation"];
			} // get
			set
			{
				this["MasterPipXPLocation"] = value;
			} // set
		} // MasterPipXPLocation

		/// <summary>
		/// Allow ENRCCI change
		/// </summary>
		/// <returns>Bool</returns>
		[UserScopedSetting]
		[DefaultSettingValue("false")]
		public bool AllowENRCCIChange
		{
			get
			{
				return (bool)this["AllowENRCCIChange"];
			} // get
			set
			{
				this["AllowENRCCIChange"] = value;
			} // set
		} // AllowENRCCIChange
	}
}
