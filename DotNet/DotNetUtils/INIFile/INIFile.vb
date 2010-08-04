Imports System.io

Public Class INIFile

    Public INIFileInfo As FileInfo
    Private di As DirectoryInfo

    'Work with INI files In VBS (ASP/WSH)
    'v1.00
    '2003 Antonin Foller, PSTRUH Software, http://www.motobit.com
    'Function GetINIString(Section, KeyName, Default, FileName)
    'Sub WriteINIString(Section, KeyName, Value, FileName)

    Public Sub New(ByVal strFullFileName As String)
        INIFileInfo = New FileInfo(strFullFileName)
        di = INIFileInfo.Directory
        If Not di.Exists Then
            Throw New DirectoryNotFoundException
        End If
        If Not INIFileInfo.Exists Then
            INIFileInfo.Create()
        End If
    End Sub

    Public Sub WriteINIString(ByVal Section As String, ByVal KeyName As String, ByVal Value As String)
        Dim INIContents As String
        Dim PosSection, PosEndSection As Integer

        'Get contents of the INI file As a string

        Dim tr As System.IO.TextReader
        tr = New System.IO.StreamReader(INIFileInfo.FullName)
        INIContents = tr.ReadToEnd
        tr.Close()
        'INIContents = INIFileInfo.OpenText.ReadToEnd

        'Find section
        PosSection = InStr(1, INIContents, "[" & Section & "]", vbTextCompare)
        If PosSection > 0 Then
            'Section exists. Find end of section
            PosEndSection = InStr(PosSection, INIContents, vbCrLf & "[")
            '?Is this last section?
            If PosEndSection = 0 Then PosEndSection = Len(INIContents) + 1

            'Separate section contents
            Dim OldsContents, NewsContents, Line
            Dim sKeyName As String
            Dim Found As Boolean
            OldsContents = Mid(INIContents, PosSection, PosEndSection - PosSection)
            OldsContents = Split(OldsContents, vbCrLf)

            'Temp variable To find a Key
            sKeyName = LCase(KeyName & "=")

            'Enumerate section lines
            For Each Line In OldsContents
                If LCase(Left(Line, Len(sKeyName))) = sKeyName Then
                    Line = KeyName & "=" & Value
                    Found = True
                End If
                NewsContents = NewsContents & Line & vbCrLf
            Next

            If Found = False Then
                'key Not found - add it at the end of section
                NewsContents = NewsContents & KeyName & "=" & Value
            Else
                'remove last vbCrLf - the vbCrLf is at PosEndSection
                NewsContents = Left(NewsContents, Len(NewsContents) - 2)
            End If

            'Combine pre-section, new section And post-section data.
            INIContents = Left(INIContents, PosSection - 1) & _
              NewsContents & Mid(INIContents, PosEndSection)
        Else 'if PosSection>0 Then
            'Section Not found. Add section data at the end of file contents.
            If Right(INIContents, 2) <> vbCrLf And Len(INIContents) > 0 Then
                INIContents = INIContents & vbCrLf
            End If
            INIContents = INIContents & "[" & Section & "]" & vbCrLf & _
              KeyName & "=" & Value
        End If 'if PosSection>0 Then
        WriteFile(INIFileInfo.FullName, INIContents)
    End Sub

    Public Function GetINIString(ByVal Section As String, ByVal KeyName As String, ByVal DefaultString As String) As String
        Dim INIContents As String
        Dim PosSection, PosEndSection As Integer
        Dim sContents, Value As String
        Dim Found As Boolean

        'Get contents of the INI file As a string
        INIContents = INIFileInfo.OpenText.ReadToEnd        

        'Find section
        PosSection = InStr(1, INIContents, "[" & Section & "]", vbTextCompare)
        If PosSection > 0 Then
            'Section exists. Find end of section
            PosEndSection = InStr(PosSection, INIContents, vbCrLf & "[")
            '?Is this last section?
            If PosEndSection = 0 Then PosEndSection = Len(INIContents) + 1

            'Separate section contents
            sContents = Mid(INIContents, PosSection, PosEndSection - PosSection)

            If InStr(1, sContents, vbCrLf & KeyName & "=", vbTextCompare) > 0 Then
                Found = True
                'Separate value of a key.
                Value = SeparateField(sContents, vbCrLf & KeyName & "=", vbCrLf)
            End If
        End If
        If Found <> True Then Value = DefaultString
        GetINIString = Value

    End Function

    Public Function GetINIKeys(ByVal Section As String) As System.Collections.Specialized.StringCollection
        Dim INIContents As System.String
        Dim PosSection, PosEndSection As System.String
        Dim sContents, Value As System.String
        Dim Found As Boolean
        Dim foundSection As System.String
        Dim foundKey As System.String
        Dim INIFileReader As StreamReader
        INIFileReader = INIFileInfo.OpenText

        'Get contents of the INI file As a string

        Do
            INIContents = INIFileReader.ReadLine
            If INIContents Is Nothing Then Return Nothing
            If INIContents.ToUpper() = "[" & Section.ToUpper() & "]" Then
                foundSection = INIContents
                Exit Do
            End If
        Loop While Not INIContents Is Nothing

        If foundSection Is Nothing Then Return Nothing

        Dim foundKeys As System.Collections.Specialized.StringCollection
        foundKeys = New System.Collections.Specialized.StringCollection

        INIContents = INIFileReader.ReadLine
        Dim keyEnd As Integer = INIContents.IndexOf("=")                
        Do While keyEnd <> -1            
            foundKey = INIContents.Substring(0, keyEnd)
            foundKeys.Add(foundKey)
            INIContents = INIFileReader.ReadLine
            keyEnd = INIContents.IndexOf("=")
        Loop

        Return foundKeys

    End Function

    'Separates one field between sStart And sEnd
    Private Function SeparateField(ByVal sFrom, ByVal sStart, ByVal sEnd)
        Dim PosB : PosB = InStr(1, sFrom, sStart, 1)
        If PosB > 0 Then
            PosB = PosB + Len(sStart)
            Dim PosE : PosE = InStr(PosB, sFrom, sEnd, 1)
            If PosE = 0 Then PosE = InStr(PosB, sFrom, vbCrLf, 1)
            If PosE = 0 Then PosE = Len(sFrom) + 1
            SeparateField = Mid(sFrom, PosB, PosE - PosB)
        End If
    End Function

    'File functions
    Private Function GetFile(ByVal FileName)
        Dim FS As System.IO.File 'FileSystem 'System.IO.FileInfo ': FS = CreateObject("Scripting.FileSystemObject")
        'Go To windows folder If full path Not specified.
        'If InStr(FileName, ":\") = 0 And Left(FileName, 2) <> "\\" Then
        'FileName = FS.GetSpecialFolder(0) & "\" & FileName
        'End If
        On Error Resume Next
        GetFile = FS.OpenText(FileName).ReadToEnd()
        'GetFile = FS.OpenTextFile(FileName).ReadAll
    End Function

    Private Function WriteFile(ByVal FileName, ByVal Contents)
        Dim FS As System.IO.TextWriter
        FS = New System.IO.StreamWriter(CStr(FileName))
        'Dim FS : FS = CreateObject("Scripting.FileSystemObject")
        'On Error Resume Next

        'Go To windows folder If full path Not specified.
        'If InStr(FileName, ":\") = 0 And Left(FileName, 2) <> "\\" Then
        'FileName = FS.GetSpecialFolder(0) & "\" & FileName
        'End If
        FS.Write(Contents)
        FS.Close()

        'Dim OutStream : OutStream = FS.OpenTextFile(FileName, 2, True)
        'OutStream.Write(Contents)
    End Function


End Class
