using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Xml;
using System.Reflection;
using System.IO;

namespace SystemsAnalysis.EMGAATS.CrossSectionEditor
{
    public partial class frmXSectEditor : Form
    {
        public frmXSectEditor()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {

        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)            
                return;            

            ValidateRawXml(openFileDialog.FileName);

        }

        private void ValidateRawXml(string rawXml)
        {
            Stream xsdStream = System.Reflection.Assembly.
                            GetExecutingAssembly().GetManifestResourceStream("SystemsAnalysis.EMGAATS.CrossSectionEditor.LandXML-1.2.xsd");

            XmlSchema rawSchema;
            rawSchema = XmlSchema.Read(xsdStream, null);


            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add(rawSchema);

            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.ValidationType = ValidationType.Schema;

            xmlSettings.Schemas = schemaSet;            
            xmlSettings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBacKHandler);

            XmlReader reader = XmlReader.Create(openFileDialog.FileName, xmlSettings);

            //landXmlDoc.Schemas.Add(
            while (reader.Read()) ;

            reader.Close();
        }

        void ValidationCallBacKHandler(object sender, ValidationEventArgs e)
        {
            throw new NotImplementedException();
        }

      


    }
}

