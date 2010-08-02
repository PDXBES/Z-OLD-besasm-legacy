// Project: UI, File: BatchPage.cs
// Namespace: CostEstimator.UI, Class: BatchPage
// Path: C:\Development\DotNet\CostEstimator\UI, Author: Arnel
// Code lines: 26, Size of file: 459 Bytes
// Creation date: 4/16/2008 1:31 PM
// Last modified: 4/21/2008 10:59 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.Misc.UltraWinNavigationBar;
using Infragistics.Win.UltraWinListView;
using SystemsAnalysis.Analysis.CostEstimator.Classes;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
  /// <summary>
  /// Batch page
  /// </summary>
	public partial class BatchPage : CostEstimator.UI.ChildFormTemplate
	{
		#region Variables
		Project _Project;
		#endregion

		#region Constructors
		private BatchPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Create batch page
		/// </summary>
		/// <param name="project">Project</param>
		public BatchPage(Project project)
			: this()
		{
			_Project = project;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Load files and folders
		/// </summary>
		/// <param name="path">Path</param>
		private void LoadFilesAndFolders(string path)
		{
			DirectoryInfo[] dirs = new DirectoryInfo(path + "\\").GetDirectories();

			lstFiles.Items.Clear();

			UltraListViewItem item = null;

			// Populate WinListView with folders and files.
			foreach (DirectoryInfo dir in dirs)
			{
				item = lstFiles.Items.Add(dir.FullName, dir.Name);
				item.Tag = dir;
			} // foreach  (dir)
		} // LoadFilesAndFolders(path)

		/// <summary>
		/// Process alternatives
		/// </summary>
		public void ProcessAlternatives()
		{
			foreach (UltraListViewItem item in lstBatchItems.Items)
			{
				string modelPath = item.Key;
				if (Directory.Exists(modelPath) && File.Exists(modelPath + "\alternative_package.mdb"))
				{
					Project altProject = new Project();
					SelectedAlternative currentAlt = (SelectedAlternative)item.Value;
					string error;
					altProject.CreateEstimateFromAlternative(bkgworkerProcessAlternative, currentAlt.ModelPath,
						currentAlt.AlternativeName, out error);
				} // if
			} // foreach  (item)
		} // ProcessAlternatives()
		#endregion

		private void BatchPage_Load(object sender, EventArgs e)
		{
			navFiles.RootLocation.DisplayText = "Local Disk (C:)";
			navFiles.RootLocation.Text = "C:";
			LoadFilesAndFolders(navFiles.RootLocation.Text);
		}

		private void navFiles_InitializeLocations(object sender, Infragistics.Win.Misc.UltraWinNavigationBar.InitializeLocationsEventArgs e)
		{
			DirectoryInfo[] dirs =
				new DirectoryInfo(e.ParentLocation.GetFullPath(FullPathFormat.EditMode)
					+ "\\").GetDirectories();

			foreach (DirectoryInfo dir in dirs)
			{
				e.ParentLocation.Locations.Add(dir.FullName, dir.Name);
			}
		}

		private void navFiles_SelectedLocationChanged(object sender, SelectedLocationChangedEventArgs e)
		{
			LoadFilesAndFolders(e.SelectedLocation.GetFullPath(Infragistics.Win.Misc.FullPathFormat.EditMode));
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			foreach (UltraListViewItem selectedItem in lstAlternatives.SelectedItems)
			{
				if (!lstBatchItems.Items.Exists(selectedItem.Key))
					lstBatchItems.Items.Add(selectedItem.Key, selectedItem.Value);
			}
		}

		private void lstFiles_ItemActivated(object sender, ItemActivatedEventArgs e)
		{
			// get the subfolders under the alternatives directory -- these will be
			// the alternative IDs
			string modelPath = e.Item.Key;
			List<string> altIDs = new List<string>();
			if (Directory.Exists(modelPath + "\\alternatives"))
			{
				DirectoryInfo[] altDirs = new DirectoryInfo(modelPath + "\\alternatives\\").GetDirectories();
				foreach (DirectoryInfo dirInfo in altDirs)
				{
					if (File.Exists(dirInfo.FullName + "\\alternative_package.mdb"))
						altIDs.Add(dirInfo.Name);
				} // foreach  (dirInfor)
			}

			// list these alternative IDs in the lstAlternatives list view
			lstAlternatives.Items.Clear();
			foreach (string altID in altIDs)
			{
				lstAlternatives.Items.Add(altID, new SelectedAlternative(modelPath, altID));
			} // foreach  (altID)
		}

		private void lstAlternatives_ItemDoubleClick(object sender, ItemDoubleClickEventArgs e)
		{
			lstBatchItems.Items.Add(e.Item.Key, e.Item.Value);
		}

		private void btnDone_Click(object sender, EventArgs e)
		{
			ProcessAlternatives();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{

		} // BatchPage(project)
	}
}

