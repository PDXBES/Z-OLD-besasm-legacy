using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ExtnHelpers;
using DocManager.BESTrim;

using System.Diagnostics;
using System.Xml.Serialization;
using System.Web.Services.Protocols;
using System.Web.Services;

using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Attachments;
using Microsoft.Web.Services2.Dime;
using Microsoft.Web.Services3;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using System.Net;

namespace DocManager
{
    public class DocMgrSearch
    {
      private ExtnHelpers.Logger _logger;
      private DataTable Gridtable;
      private clsData TrimData;
     

        // constructor
        public DocMgrSearch()
        {
            // _logger = logger;
        }
      
      public DocMgrSearch(Logger logger)
        {
            _logger = logger;
        }
      public void DocDownload(string URI, string sFilename, string sPath)
      {
          HackDownload(URI, sFilename, sPath);
      }

      /// <summary>
      /// A function that's get the properties from URI
      /// TRIM web service -- just to light it up.
      /// </summary>
      /// <param name="titleWord">URI.</param>
      public void retrievePropertyfromURI(string URI)
      {
          try
          {
          // set up the request
          TrimRequest request = new TrimRequest();
          // construct a search clause...
          RecordUriSearchClause clause = new RecordUriSearchClause(); 

          // string[] searchURI = new string[]{URI};
          clause.Uri = URI ;
          // construct a record search
          RecordSearch search = new RecordSearch();
          // Try to update the URI?
          search.IsForUpdate = false;
          search.Items = new RecordClause[] { clause };
          search.Id = "PropertySearch";
          // specify what results we want back
          SpecificationProperty title = new SpecificationProperty();
          title.Name = "rectitle";
          SpecificationProperty detail = new SpecificationProperty();
          detail.Name = "recedetails";
          SpecificationProperty author = new SpecificationProperty();
          author.Name = "recauthorloc";
          SpecificationProperty ownerloc = new SpecificationProperty();
          ownerloc.Name = "recownerloc";
          SpecificationProperty datecreated = new SpecificationProperty();
          datecreated.Name = "recdatecreated";
          SpecificationProperty dateupdated = new SpecificationProperty();
          dateupdated.Name = "recdateupdated";
          SpecificationProperty datemodified = new SpecificationProperty();
          datemodified.Name = "recdatemodified";
          // Define a fetch
          Fetch fetch = new Fetch();
          fetch.Items = new SpecificationProperty[] { title, detail, author, ownerloc, datecreated, dateupdated, datemodified };
          // Request that fetch
          request.Items = new Operation[] { search, fetch };
          // send it off...
          
          Engine engine = new Engine();
          IWebProxy iProxy = WebRequest.DefaultWebProxy;
          Uri WServiceURI = new Uri(DocManager.Properties.Settings.Default.DocManager_mosier_Engine.ToString());
          WebProxy myProxy = new WebProxy(iProxy.GetProxy(WServiceURI));
          // potentially, setup credentials on the proxy here           
          myProxy.Credentials = CredentialCache.DefaultCredentials;
          myProxy.UseDefaultCredentials = true;
          engine.Proxy = myProxy;
          engine.Credentials = System.Net.CredentialCache.DefaultCredentials;

          // Send a quest to TRIM
          TrimResponse response = engine.Execute(request);
          // Get a result back
          foreach (Result result in response.Items)
          {
              switch (result.GetType().Name.ToString())
              {
                  case "EndResponse":
                      
                      break;
                  case "SuccessResult":
                      
                      break;
                  case "SearchResult":
                      SearchResult sResult = (SearchResult)result;
                      
                      break;
                  case "FetchResult":

                      FetchResult fResult = (FetchResult)result;
                     
                      foreach (TrimObject obj in fResult.Objects)
                      {
                         
                          
                          string fileName = "", details = "", DateCreated = "";
                          string Author = "", Location = "", DateUpdated = "", DateModified = "";
                          foreach (Value val in obj.Values)
                          {
                              // Get a property and add it in the Property in checked list box
                              if (val.Name == "rectitle")
                              {
                                  fileName = val.Val.ToString();
                                  _logger.AddProperty("Title: " + fileName);
                              }
                              if (val.Name == "recedetails")
                              {
                                  details = val.Val.ToString();
                                  _logger.AddProperty("Deatails: " + details);
                              }
                              if (val.Name == "recauthorloc")
                              {
                                  Author = val.Val.ToString();
                                  _logger.AddProperty("Author: " + Author);
                              }
                              if (val.Name == "recownerloc")
                              {
                                  Location = val.Val.ToString();
                                  _logger.AddProperty("Location: " + Location);
                              }
                             
                              if (val.Name == "recdatecreated")
                              {
                                  DateCreated = val.Val.ToString();
                                  _logger.AddProperty("Date Created: " + DateCreated);
                              }
                              if (val.Name == "recdateupdated")
                              {
                                  DateUpdated = val.Val.ToString();
                                  _logger.AddProperty("Date Updated: " + DateUpdated);
                              }
                              if (val.Name == "recdatemodified")
                              {
                                  DateModified = val.Val.ToString();
                                  _logger.AddProperty("Date Modified: " + DateModified);
                              }
                          }
                          
                          
                      }
                      break;
                  case "ErrorResult":
                      ErrorResult err = (ErrorResult)result;
                     
                      break;
                  default:
                      
                      break;
              }
          }
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }

      }
        /// <summary>
        /// Initiate the search for a record using the TitleWord and a range of dates
        /// </summary>
        /// <param name="titleWord"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void DoSearch(String titleWord, string startDate, string endDate)
        {
            try
            {
                String msg;
                TrimData = new clsData();
                msg = String.Format("Do Search called, with '{0}'.", titleWord);
                _logger.Log(msg, 2);
                Gridtable = TrimData.CreateDataTable();
                HackSearch(titleWord, startDate, endDate);
            }
                      
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
              }
        }

        /// <summary>
        /// A function that's the hacked up, hard coded first round call to the
        /// TRIM web service -- just to light it up.
        /// </summary>
        /// <param name="titleWord">A word to search file in document titles, I think.</param>
        private void HackSearch(String titleWord, string startDate, string endDate)
        {

            String msg;
            DateTime start, end;
            TimeSpan interval;

            try
            {

                start = new DateTime();
                end = new DateTime();

                start = DateTime.Now;

                // set up the request
                TrimRequest request = new TrimRequest();
                // construct a search clause...
                RecordStringSearchClause clause = new RecordStringSearchClause();
                clause.Type = RecordStringSearchClauseType.TitleWord;
                clause.Arg = titleWord;
                // another search clause that's working in the regular TRIM UI
                RecordDateRangeSearchClause RDClause = new RecordDateRangeSearchClause();
                RDClause.Type = RecordDateRangeSearchClauseType.DateCreated;
                RDClause.StartTime = startDate;//"1/8/2007";
                RDClause.EndTime = endDate;//"9/12/2007";

                // construct a record search
                RecordSearch search = new RecordSearch();
                // only pick one of the two searches above for the Hack...
                if (startDate != "" && endDate != "" && titleWord != "" && startDate != endDate)
                {
                    search.Items = new RecordClause[] { RDClause, clause };   // this is an array of clauses
                }
                else if (titleWord != "")
                {
                    search.Items = new RecordClause[] { clause };   // this is an array of clauses
                }
                else if (startDate != "" && endDate != "")
                {
                    search.Items = new RecordClause[] { RDClause };   // this is an array of clauses
                }
                else if (startDate == endDate)
                {
                    search.Items = new RecordClause[] { clause };
                }
                search.Id = "HackSearch";
                // specify what results we want back
                SpecificationProperty title = new SpecificationProperty();
                title.Name = "rectitle";
                SpecificationProperty date = new SpecificationProperty();
                date.Name = "recdatecreated";
                SpecificationProperty extension = new SpecificationProperty();
                extension.Name = "recExtension";
                Fetch fetch = new Fetch();
                fetch.Items = new SpecificationProperty[] { title, date, extension };

                request.Items = new Operation[] { search, fetch };
                // send it off...
                _logger.Log("Creating engine...", 1);
                Engine engine = new Engine();
                IWebProxy iProxy = WebRequest.DefaultWebProxy;
                Uri WServiceURI = new Uri(DocManager.Properties.Settings.Default.DocManager_mosier_Engine.ToString());
                WebProxy myProxy = new WebProxy(iProxy.GetProxy(WServiceURI));
                // potentially, setup credentials on the proxy here           
                myProxy.Credentials = CredentialCache.DefaultCredentials;
                myProxy.UseDefaultCredentials = true;
                engine.Proxy = myProxy;
                //engine.Credentials = new System.Net.NetworkCredential(username, password);
                engine.Credentials = System.Net.CredentialCache.DefaultCredentials;

                _logger.Log("About to send request...", 1);
                TrimResponse response = engine.Execute(request);
                _logger.Log("Back from execute...", 1);
                foreach (Result result in response.Items)
                {
                    switch (result.GetType().Name.ToString())
                    {
                        case "EndResponse":
                            _logger.Log("End of Response", 1);
                            break;
                        case "SuccessResult":
                            _logger.Log("Successful Result", 1);
                            break;
                        case "SearchResult":
                            SearchResult sResult = (SearchResult)result;
                            msg = String.Format("Found {0} records in ID {1}"
                                , sResult.FoundCount.ToString()
                                , sResult.Id
                                );
                            _logger.Log(msg, 1);
                            break;
                        case "FetchResult":
                            FetchResult fResult = (FetchResult)result;
                            msg = String.Format("Fetch Count: {0}"
                                        , fResult.Count.ToString()
                                        );
                            _logger.Log(msg, 1);
                            // Note: Start to move in the Result Grid
                            foreach (TrimObject obj in fResult.Objects)
                            {
                                // the version appears to be null and causes problems,
                                //      obj.Version.ToString()
                                msg = String.Format("URI: {0}"
                                        , obj.Uri.ToString()
                                        );
                                _logger.Log(msg, 1);
                                string fileName = "", fileExtension = "", Formatteddate = "";
                                DateTime Datecreated;
                                foreach (Value val in obj.Values)
                                {
                                    msg = String.Format("Name: {0} Value: {1} Err: {2} Msg: {3}"
                                            , val.Name
                                            , val.Val.ToString()
                                            , val.ErrorOccurred.ToString()
                                            , val.ErrorMessage
                                            );
                                    _logger.Log(msg, 1);
                                    if (val.Name == "rectitle")
                                    {
                                        fileName = val.Val.ToString();
                                    }
                                    if (val.Name == "recExtension")
                                    {
                                        fileExtension = val.Val.ToString();
                                    }
                                    if (val.Name == "recdatecreated")
                                    {
                                        if (val.Val.ToString() != "")
                                        {
                                            Formatteddate = val.Val.ToString();
                                        }
                                        else
                                        {
                                            Formatteddate = "01/01/01";
                                        }
                                    }
                                }
                                // Finally add the result to datatable to display in the Result GRID
                                if (obj.Uri.ToString() != "0" && fileName != "")
                                {
                                    TrimData.AddDataToTable(obj.Uri.ToString(), fileName + "." + fileExtension, DateTime.Parse(Formatteddate), Gridtable, "");
                                }
                            }
                            break;
                        case "ErrorResult":
                            ErrorResult err = (ErrorResult)result;
                            msg = String.Format("Error result. ID: {0} Code: {1} Message: {2}"
                                    , result.Id
                                    , err.ErrorNumber.ToString()
                                    , err.Message
                                    );
                            _logger.Log(msg, 1);
                            break;
                        default:
                            msg = String.Format("Result type: {0} Search ID: {1}"
                                , result.GetType().Name.ToString()
                                , result.Id
                                );
                            _logger.Log(msg, 1);
                            break;
                    }
                }
                end = DateTime.Now;
                interval = end - start;

                msg = String.Format("Query took {0:N} seconds.", interval.Seconds);
                _logger.Log(msg, 1);

                _logger.Log("All done.", 1);

                // Bind to Result to the GRid
                _logger.PopulateGrid(Gridtable);
            }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
              }
        }

        /// <summary>
        /// This routine does a hard coded download of a record, using the WSE engine...
        /// </summary>
        private void HackDownload(string sURI, string fileName, string sPath)
        {
          try
          {
            EngineWse engine = new EngineWse();
            IWebProxy iProxy = WebRequest.DefaultWebProxy;
            Uri WServiceURI = new Uri(DocManager.Properties.Settings.Default.DocManager_mosier_Engine.ToString());//"http://mosier/trimct2/trim.asmx");
            WebProxy myProxy = new WebProxy(iProxy.GetProxy(WServiceURI));
            // potentially, setup credentials on the proxy here           
            myProxy.Credentials = CredentialCache.DefaultCredentials;
            myProxy.UseDefaultCredentials = true;
            engine.Proxy = myProxy;  

            engine.Credentials = System.Net.CredentialCache.DefaultCredentials;//new System.Net.NetworkCredential("<user_name>", "<password>", "<domain>");
            engine.Timeout = 1000000;

            ShortcutRecordUri sru = new ShortcutRecordUri();
            sru.Uri = sURI;

            Download down = new Download();
            down.Checkout = false;
            down.Comments = "Testing TCT Download";
            down.MaximumTransferBytes = 0;
            down.TransferInset = 0;
            down.TransferType = TransferTypeType.inline;

            TrimRequest request = new TrimRequest();
            request.Items = new Operation[] { sru, down };
            TrimResponse response = engine.Execute(request);

            foreach (Result res in response.Items)
            {
              if (res.GetType().Name.ToString() == "DownloadResult")
              {
                DownloadResult dres = (DownloadResult)res;
                //...
                byte[] data = Convert.FromBase64String(dres.Base64Payload);
                if (dres.IsTrimMail == true)
                {
                  System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                  string wholeEmail = enc.GetString(data);
                  System.IO.StringReader sr = new System.IO.StringReader(wholeEmail);
                  string holder = null;
                  while ((holder = sr.ReadLine()) != null)
                  {
                    if (holder.StartsWith("TRIM-Embedded:"))
                    {
                      string[] parser = holder.Split(char.Parse(","));
                      string docName = parser[0].ToString().Split(char.Parse("\""))[1];
                      string ext = parser[1].ToString().Split(char.Parse("\""))[1];
                      int size = int.Parse(parser[2].ToString().Split(char.Parse("\""))[1]);
                      int offset = int.Parse(parser[3].ToString().Split(char.Parse("\""))[1], System.Globalization.NumberStyles.HexNumber);
                      int esize = int.Parse(parser[4].ToString().Split(char.Parse("\""))[1]);

                      byte[] cr8Att = Convert.FromBase64String(wholeEmail.Substring(offset, esize));

                      using (System.IO.FileStream fileout = new System.IO.FileStream(@docName + "." + ext, System.IO.FileMode.Append))
                      {
                        fileout.Write(cr8Att, 0, cr8Att.Length);
                        fileout.Flush();
                        fileout.Close();
                      }
                    }
                  }
                }
                else
                {
                  // Note: default Path to download
                  // using (System.IO.FileStream output = new System.IO.FileStream("C:\\TOWER\\TCT Testing\\" + fileName, System.IO.FileMode.Create))
                    using (System.IO.FileStream output = new System.IO.FileStream(sPath + fileName, System.IO.FileMode.Create))
                  {
                    output.Write(data, 0, data.Length);
                    output.Flush();
                    output.Close();
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
              MessageBox.Show("Error in Download: " + ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
        }

    }
}
