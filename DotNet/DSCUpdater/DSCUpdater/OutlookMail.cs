using System;
using System.Collections.Generic;
using System.Text;


  public class OutlookMail
  {
    private Microsoft.Office.Interop.Outlook.Application oApp;
    private Microsoft.Office.Interop.Outlook._NameSpace oNameSpace;
    private Microsoft.Office.Interop.Outlook.MAPIFolder oOutboxFolder;

    public OutlookMail()
    {
      oApp = new Microsoft.Office.Interop.Outlook.Application();
      oNameSpace = oApp.GetNamespace("MAPI");
      oNameSpace.Logon(null, null, true, true);
      oOutboxFolder = oNameSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderOutbox);
    }

    public void AddToOutboxImpAreaChanges(string toValue, string subjectValue, string bodyValue)
    {
      Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
      String sSource = "C:\\temp\\IMPUPDATE.csv";
      String sDisplayName = "IMPUPDATE";
      int iPosition = 1;
      int iAttachType = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      Microsoft.Office.Interop.Outlook.Attachment oAttach = oMailItem.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);
      toValue = "jrubengb@gmail.com";
      subjectValue = "Request for Impervious Area Update";
      bodyValue = "This is an auto-generated email." + "\r\n" +
                "This message is a request for changes to the impervious area coverage." + "\r\n" +
                "The attached table lists parcels by DSCID that are in need of updates in the modeling system.";
      oMailItem.To = toValue;
      oMailItem.Subject = subjectValue;
      oMailItem.Body = bodyValue;
      oMailItem.SaveSentMessageFolder = oOutboxFolder;
      oMailItem.Send();
    }

    //AllChanges
    public void AddToOutboxAllRetroChanges(string toValue, string subjectValue, string bodyValue)
    {
      Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
      String sSource1 = "C:\\temp\\SiteAssessments.csv";
      String sDisplayName1 = "SiteAssessments";
      String sSource2 = "C:\\temp\\PotentialICs.csv";
      String sDisplayName2 = "PotentialICs";
      String sSource3 = "C:\\temp\\ConstructedICs.csv";
      String sDisplayName3 = "ConstructedICs";
      int iPosition1 = 1;
      int iAttachType1 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      int iPosition2 = 2;
      int iAttachType2 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      int iPosition3 = 3;
      int iAttachType3 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      Microsoft.Office.Interop.Outlook.Attachment oAttach = oMailItem.Attachments.Add(sSource1, iAttachType1, iPosition1, sDisplayName1);
      oAttach = oMailItem.Attachments.Add(sSource2, iAttachType2, iPosition2, sDisplayName2);
      oAttach = oMailItem.Attachments.Add(sSource3, iAttachType3, iPosition3, sDisplayName3);
      toValue = "jrubengb@gmail.com";
      subjectValue = "Request for DSC & Inflow Control Update";
      bodyValue = "This is an auto-generated email." + "\r\n" +
                "This message is a request for changes to the DSC and Inflow Controls coverages." + "\r\n" +
                "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      oMailItem.To = toValue;
      oMailItem.Subject = subjectValue;
      oMailItem.Body = bodyValue;
      oMailItem.SaveSentMessageFolder = oOutboxFolder;
      oMailItem.Send();
    }

    //AssessmentsPotentialChanges
    public void AddToOutboxAssessmentsPotentialChanges(string toValue, string subjectValue, string bodyValue)
    {
      Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
      String sSource1 = "C:\\temp\\SiteAssessments.csv";
      String sDisplayName1 = "SiteAssessments";
      String sSource2 = "C:\\temp\\PotentialICs.csv";
      String sDisplayName2 = "PotentialICs";
      int iPosition1 = 1;
      int iAttachType1 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      int iPosition2 = 2;
      int iAttachType2 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      Microsoft.Office.Interop.Outlook.Attachment oAttach = oMailItem.Attachments.Add(sSource1, iAttachType1, iPosition1, sDisplayName1);
      oAttach = oMailItem.Attachments.Add(sSource2, iAttachType2, iPosition2, sDisplayName2);
      toValue = "jrubengb@gmail.com";
      subjectValue = "Request for DSC & Inflow Control Update";
      bodyValue = "This is an auto-generated email." + "\r\n" +
                "This message is a request for changes to the DSC and Inflow Controls coverages." + "\r\n" +
                "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      oMailItem.To = toValue;
      oMailItem.Subject = subjectValue;
      oMailItem.Body = bodyValue;
      oMailItem.SaveSentMessageFolder = oOutboxFolder;
      oMailItem.Send();
    }

    //AssessmentsConstructedChanges
    public void AddToOutboxAssessmentConstructedChanges(string toValue, string subjectValue, string bodyValue)
    {
      Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
      String sSource1 = "C:\\temp\\SiteAssessments.csv";
      String sDisplayName1 = "SiteAssessments";
      String sSource2 = "C:\\temp\\ConstructedICs.csv";
      String sDisplayName2 = "ConstructedICs";
      int iPosition1 = 1;
      int iAttachType1 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      int iPosition2 = 2;
      int iAttachType2 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      Microsoft.Office.Interop.Outlook.Attachment oAttach = oMailItem.Attachments.Add(sSource1, iAttachType1, iPosition1, sDisplayName1);
      oAttach = oMailItem.Attachments.Add(sSource2, iAttachType2, iPosition2, sDisplayName2);
      toValue = "jrubengb@gmail.com";
      subjectValue = "Request for DSC & Inflow Control Update";
      bodyValue = "This is an auto-generated email." + "\r\n" +
                "This message is a request for changes to the DSC and Inflow Controls coverages." + "\r\n" +
                "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      oMailItem.To = toValue;
      oMailItem.Subject = subjectValue;
      oMailItem.Body = bodyValue;
      oMailItem.SaveSentMessageFolder = oOutboxFolder;
      oMailItem.Send();
    }

    //AssessmentsChangesOnly
    public void AddToOutboxAssessmentsChangesOnly(string toValue, string subjectValue, string bodyValue)
    {
      Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
      String sSource = "C:\\temp\\SiteAssessments.csv";
      String sDisplayName = "SiteAssessments";
      int iPosition1 = 1;
      int iAttachType1 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      Microsoft.Office.Interop.Outlook.Attachment oAttach = oMailItem.Attachments.Add(sSource, iAttachType1, iPosition1, sDisplayName);
      toValue = "jrubengb@gmail.com";
      subjectValue = "Request for DSC Update";
      bodyValue = "This is an auto-generated email." + "\r\n" +
                "This message is a request for changes to the DSC coverage." + "\r\n" +
                "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      oMailItem.To = toValue;
      oMailItem.Subject = subjectValue;
      oMailItem.Body = bodyValue;
      oMailItem.SaveSentMessageFolder = oOutboxFolder;
      oMailItem.Send();
    }

    //PotentialConstructedChanges
    public void AddToOutboxPotentialConstructedChanges(string toValue, string subjectValue, string bodyValue)
    {
      Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
      String sSource1 = "C:\\temp\\PotentialICs.csv";
      String sDisplayName1 = "PotentialICs";
      String sSource2 = "C:\\temp\\ConstructedICs.csv";
      String sDisplayName2 = "ConstructedICs";
      int iPosition1 = 1;
      int iAttachType1 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      int iPosition2 = 2;
      int iAttachType2 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      Microsoft.Office.Interop.Outlook.Attachment oAttach = oMailItem.Attachments.Add(sSource1, iAttachType1, iPosition1, sDisplayName1);
      oAttach = oMailItem.Attachments.Add(sSource2, iAttachType2, iPosition2, sDisplayName2);
      toValue = "jrubengb@gmail.com";
      subjectValue = "Request for Inflow Control Update";
      bodyValue = "This is an auto-generated email." + "\r\n" +
                "This message is a request for changes to the Inflow Controls coverages." + "\r\n" +
                "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      oMailItem.To = toValue;
      oMailItem.Subject = subjectValue;
      oMailItem.Body = bodyValue;
      oMailItem.SaveSentMessageFolder = oOutboxFolder;
      oMailItem.Send();
    }

    //PotentialChangesOnly
    public void AddToOutboxPotentialChangesOnly(string toValue, string subjectValue, string bodyValue)
    {
      Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
      String sSource = "C:\\temp\\PotentialICs.csv";
      String sDisplayName = "PotentialICs";
      int iPosition1 = 1;
      int iAttachType1 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      Microsoft.Office.Interop.Outlook.Attachment oAttach = oMailItem.Attachments.Add(sSource, iAttachType1, iPosition1, sDisplayName);
      toValue = "jrubengb@gmail.com";
      subjectValue = "Request for Inflow Control Update";
      bodyValue = "This is an auto-generated email." + "\r\n" +
                "This message is a request for changes to the Potential Inflow Controls coverage(s)." + "\r\n" +
                "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      oMailItem.To = toValue;
      oMailItem.Subject = subjectValue;
      oMailItem.Body = bodyValue;
      oMailItem.SaveSentMessageFolder = oOutboxFolder;
      oMailItem.Send();
    }

    //ConstructedChangesOnly
    public void AddToOutboxConstructedChangesOnly(string toValue, string subjectValue, string bodyValue)
    {
      Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
      String sSource = "C:\\temp\\ConstructedICs.csv";
      String sDisplayName = "ConstructedICs";
      int iPosition1 = 1;
      int iAttachType1 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      Microsoft.Office.Interop.Outlook.Attachment oAttach = oMailItem.Attachments.Add(sSource, iAttachType1, iPosition1, sDisplayName);
      toValue = "jrubengb@gmail.com";
      subjectValue = "Request for Inflow Control Update";
      bodyValue = "This is an auto-generated email." + "\r\n" +
                "This message is a request for changes to the Constructed Inflow Controls coverage(s)." + "\r\n" +
                "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      oMailItem.To = toValue;
      oMailItem.Subject = subjectValue;
      oMailItem.Body = bodyValue;
      oMailItem.SaveSentMessageFolder = oOutboxFolder;
      oMailItem.Send();
    }

    //NoChanges 
    public void AddToOutboxNoChanges(string toValue, string subjectValue, string bodyValue)
    {
      Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
      String sSource1 = "C:\\temp\\SiteAssessments.csv";
      String sDisplayName1 = "SiteAssessments";
      String sSource2 = "C:\\temp\\PotentialICs.csv";
      String sDisplayName2 = "PotentialICs";
      String sSource3 = "C:\\temp\\ConstructedICs.csv";
      String sDisplayName3 = "ConstructedICs";
      int iPosition1 = 1;
      int iAttachType1 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      int iPosition2 = 2;
      int iAttachType2 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      int iPosition3 = 3;
      int iAttachType3 = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      Microsoft.Office.Interop.Outlook.Attachment oAttach = oMailItem.Attachments.Add(sSource1, iAttachType1, iPosition1, sDisplayName1);
      oAttach = oMailItem.Attachments.Add(sSource2, iAttachType2, iPosition2, sDisplayName2);
      oAttach = oMailItem.Attachments.Add(sSource3, iAttachType3, iPosition3, sDisplayName3);
      toValue = "jrubengb@gmail.com";
      subjectValue = "No RETRO Changes Update";
      bodyValue = "This is an auto-generated email." + "r\n" +
                  "This message is to inform you that no changes were found." + "r\n" +
                  "The attached tables are test tables.";
      oMailItem.To = toValue;
      oMailItem.Subject = subjectValue;
      oMailItem.Body = bodyValue;
      oMailItem.SaveSentMessageFolder = oOutboxFolder;
      oMailItem.Send();
    }
  }
