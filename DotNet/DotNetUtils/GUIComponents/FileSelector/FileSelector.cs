using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using SystemsAnalysis.Utils.FileSelector;


namespace SystemsAnalysis.Utils.FileSelector
{
    public partial class FileSelector : UserControl
    {
        private bool directoryMode = false;
        private string xmlFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MRUList.xml";
        private string description = String.Empty;
        private int mruCount = 4;
        private string fileFilter = "*.txt|*.txt";
        private string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
        public DataView currentSection;

        public FileSelector()
        {
            InitializeComponent();
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            string browseLocation;

            if (directoryMode)
            {
                browseLocation = browseDirectory();
            }
            else
            {
                browseLocation = browseFile();
            }

            if (browseLocation != null)
            {
                this.SelectorComboBox.Value = browseLocation;
            }
        }

        private string browseFile()
        {
            string file;

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return null;
            }
            file = openFileDialog1.FileName;

            return file;
        }

        private string browseDirectory()
        {
            string dir;

            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            dir = this.folderBrowserDialog1.SelectedPath;
            if (!dir.EndsWith("\\"))
            {
                dir += "\\";
            }

            return dir;

        }

        public int MRUCount
        {
            get { return this.mruCount; }
            set { this.mruCount = value; }
        }

        public string XmlFile
        {
            get { return this.xmlFile; }
            set
            {
                try
                {
                    this.xmlFile = value;
                    if (File.Exists(this.xmlFile))
                    {
                        mruList1.ReadXml(this.xmlFile);
                    }
                    else
                    {
                        mruList1.WriteXml(this.xmlFile);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Invalid xml file specified for MRU List." + ex.ToString(), ex);
                }
            }
        }

        public bool DirectoryMode
        {
            get { return this.directoryMode; }
            set { this.directoryMode = value; }
        }

        public string Description
        {
            get { return this.description; }
            set
            {
                this.description = value;
                this.SelectorLabel.Text = this.description;
            }
        }

        public string FileFilter
        {
            get { return this.fileFilter; }
            set
            {
                this.fileFilter = value;
            }
        }

        public string InitialDirectory
        {
            get { return this.initialDirectory; }
            set
            {
                this.initialDirectory = value;
                if (!Directory.Exists(this.initialDirectory))
                {
                    Directory.CreateDirectory(this.initialDirectory);
                }
            }
        }

        private void SelectorComboBox_ValueChanged(object sender, EventArgs e)
        {
            string newMRU = SelectorComboBox.Text;
            
            MRUList.MRUSectionRow section;
            section = mruList1.MRUSection.FindBySectionName(description);
            if (section == null)
            {
                section = mruList1.MRUSection.AddMRUSectionRow(description);
            }
            
            currentSection = new DataView(mruList1.MRUFiles, "SectionID = " + section.SectionID, "", DataViewRowState.CurrentRows);
            SelectorComboBox.DataSource = currentSection;
            SelectorComboBox.DataMember = "File";
            SelectorComboBox.DisplayMember = "File";

            MRUList.MRUFilesRow[] rows;
            rows = section.GetMRUFilesRows();

            string newDate = System.DateTime.Now.ToString("G");

            int historyCount = System.Math.Min(mruCount, rows.Length + 1);

            for (int i = 0; i < historyCount; i++)
            {
                if (rows.Length <= i)
                {
                    mruList1.MRUFiles.AddMRUFilesRow(section, "", "");
                }

                MRUList.MRUFilesRow existingRow;
                existingRow = section.GetMRUFilesRows()[i];

                string tempMRU = existingRow.IsNull("File")
                     ? "" : existingRow.File;
                string tempDate = existingRow.IsNull("Date")
                     ? "" : existingRow.Date;


                if (tempMRU == newMRU)
                {
                    existingRow.Delete();
                    historyCount--;
                    i--;
                    continue;
                }

                existingRow.File = newMRU;
                existingRow.Date = newDate;

                newMRU = tempMRU;
                newDate = tempDate;
            }

            mruList1.WriteXml(xmlFile);
            return;
        }

        private void SelectorComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MessageBox.Show("YO");
        }

    }
}
