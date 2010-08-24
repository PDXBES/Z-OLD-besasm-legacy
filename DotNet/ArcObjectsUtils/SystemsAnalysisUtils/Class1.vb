Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Utility
Imports dotnetutils

Public Class FeatureClassUtils
    'Returns the number of locks on a database, including the lock created by this test (i.e. is this is greater than 1 there is an external lock)
    Public Shared Function checkDatabaseLocks(ByVal pFC As FeatureClass) As Integer
        Dim pSchemaLock As ISchemaLock
        Dim pSchemaEdit As IClassSchemaEdit
        Dim pEnumSchemaLocks As IEnumSchemaLockInfo
        Dim pschemalockinfo As ISchemaLockInfo
        Dim numLocks As Integer
        pSchemaEdit = pFC
        pSchemaLock = pFC
        Call pSchemaLock.GetCurrentSchemaLocks(pEnumSchemaLocks)

        pschemalockinfo = pEnumSchemaLocks.Next 'Gets the 1st schema lock, ie the one held by the SchemaLockInfo
        numLocks = 1
        Do Until pschemalockinfo Is Nothing
            numLocks = numLocks + 1
            pschemalockinfo = pEnumSchemaLocks.Next 'Gets the 1st schema lock, ie the one held by the SchemaLockInfo
        Loop
        checkDatabaseLocks = numLocks

    End Function
    Public Shared Function FCNameExists(ByRef pWS As IWorkspace, ByVal sName As String) As Boolean

        Dim pEnumDSN As IEnumDatasetName
        pEnumDSN = pWS.DatasetNames(esriDatasetType.esriDTAny)
        Dim pDSName As IDatasetName
        pDSName = pEnumDSN.Next
        FCNameExists = False
        Do Until pDSName Is Nothing
            If UCase(pDSName.Name) = UCase(sName) Then
                FCNameExists = True
                Exit Do
            ElseIf pDSName.Type = esriDatasetType.esriDTFeatureDataset Then
                If FCNameExistsInFDS(pDSName, sName) Then
                    FCNameExists = True
                    Exit Do
                End If
            End If
            pDSName = pEnumDSN.Next
        Loop
        SystemUtils.ReleaseComObject(pEnumDSN)
        SystemUtils.ReleaseComObject(pDSName)

        pEnumDSN = Nothing
        pDSName = Nothing

    End Function
    Public Shared Function FCNameExistsInFDS(ByVal pFDSName As IDatasetName, ByVal sName As String) As Boolean
        Dim pEnumDSN As IEnumDatasetName
        pEnumDSN = pFDSName.SubsetNames
        FCNameExistsInFDS = False
        If Not pEnumDSN Is Nothing Then
            Dim pDSName As IDatasetName
            pDSName = pEnumDSN.Next
            Do Until pDSName Is Nothing
                If UCase(pDSName.Name) = UCase(sName) Then
                    FCNameExistsInFDS = True
                    Exit Do
                End If
                pDSName = pEnumDSN.Next
            Loop
        End If
    End Function
    Public Shared Function deleteFC(ByRef pWS As IWorkspace, ByVal strTableName As String) As Boolean

        Dim pDataset As IDataset
        Dim pEnumDSN As IEnumDatasetName
        pEnumDSN = pWS.DatasetNames(esriDatasetType.esriDTAny)
        If Not pEnumDSN Is Nothing Then
            Dim pDSName As IDatasetName
            pDSName = pEnumDSN.Next
            Do Until pDSName Is Nothing
                If UCase(pDSName.Name) = UCase(strTableName) Then
                    Dim pDS As IDataset
                    If pDSName.Type = esriDatasetType.esriDTFeatureClass Then
                        Dim pFWS As IFeatureWorkspace = pWS
                        pDS = pFWS.OpenFeatureClass(strTableName)
                    ElseIf pDSName.Type = esriDatasetType.esriDTFeatureDataset Then
                        Dim pfWS As IFeatureWorkspace = pWS
                        pDS = pfws.OpenFeatureDataset(strTableName)
                    End If
                    If pDS.CanDelete Then pDS.Delete()
                    Exit Do
                End If
                pDSName = pEnumDSN.Next
            Loop
        End If
        pEnumDSN = pWS.DatasetNames(esriDatasetType.esriDTFeatureDataset)


        'pDataset = pWS.OpenFeatureClass(strTableName)
        ' deleteFC = pDataset.CanDelete
        ' If pDataset.CanDelete Then pDataset.Delete()
        ' ReleaseComObject(pDataset)

    End Function

End Class

