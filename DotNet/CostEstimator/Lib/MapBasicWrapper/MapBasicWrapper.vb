Imports MapInfo
Imports System.IO.Path

Public Class MapBasicWrapper

    Dim mi As MapInfo.MapInfoApplication
    Dim currentMbxName As String
    Dim miStartTime As DateTime

    Public Sub New()
        Me.mi = New MapInfoApplicationClass
        Me.miStartTime = System.DateTime.Now
    End Sub

    Public Sub RunMenuCommand(ByVal commandID As String)
        Me.mi.Do("Run Menu Command ID " + commandID)
    End Sub

    Public Function ReadGlobal(ByVal GlobalName As String) As String
        Dim globalValue As String
        globalValue = mi.MBApplications.Item(GetFileNameWithoutExtension(Me.currentMbxName)).MBGlobals.item(GlobalName).Value()
        Return globalValue
    End Function

    Public Sub WriteGlobal(ByVal GlobalName As String, ByVal GlobalValue As String)
        mi.MBApplications.Item(GetFileNameWithoutExtension(Me.currentMbxName)).MBGlobals.item(GlobalName).Value = GlobalValue
    End Sub

    Public Property Visible() As Boolean
        Get
            Return mi.Visible
        End Get
        Set(ByVal Value As Boolean)
            mi.Visible = Value
        End Set
    End Property

    Public Sub CloseMapInfo()
        Try
            mi.Do("End MapInfo")

            System.Runtime.InteropServices.Marshal.ReleaseComObject(mi)
            If Me.GetProcessID() <> -1 Then
                Dim p As Process = Process.GetProcessById(Me.GetProcessID())
                p.Kill()
            End If
        Catch ex As Exception
            
        Finally
            Me.currentMbxName = ""
        End Try
    End Sub

    Public Sub ExecuteLibrary(ByVal mbxName As String)
        Me.currentMbxName = mbxName
        Me.mi.Do("Run Application " & Chr(34) & mbxName & Chr(34))
    End Sub

    Public Sub SwitchToMapInfo()
        Me.mi.Do("Set Window MapInfo Max")       
    End Sub

    Public Function GetProcessID() As Integer
        Dim myProcesses() As Process
        Dim miProcess As Process
        Dim anyProcess As Process
        Dim miProcessStartTime As DateTime

        myProcesses = Process.GetProcesses()
        ' Iterate through the process array.
        For i As Integer = 1 To myProcesses.Length - 1
            anyProcess = myProcesses(i)
            System.Diagnostics.Debug.WriteLine(anyProcess.ProcessName)
            If anyProcess.ProcessName = "MAPINFOW" Then
                Dim anyProcessDuration As TimeSpan = anyProcess.StartTime.Subtract(Me.miStartTime).Duration
                Dim miDuration As TimeSpan = miProcessStartTime.Subtract(Me.miStartTime).Duration
                If anyProcessDuration.TotalSeconds < miDuration.TotalSeconds Then
                    miProcess = anyProcess
                    miProcessStartTime = miProcess.StartTime
                End If
            End If

        Next

        If miProcess Is Nothing Then
            Return -1
        ElseIf (miProcess.StartTime.Subtract(Me.miStartTime).TotalSeconds > 5) Then
            Return -1
        Else
            Return miProcess.Id
        End If


    End Function

    Public Function GetError() As String
        Try
            Return Me.mi.LastErrorMessage()
        Catch
            Return "Unknown Error"
        End Try
    End Function

End Class
