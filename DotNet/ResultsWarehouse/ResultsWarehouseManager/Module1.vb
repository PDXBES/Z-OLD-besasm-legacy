Module Module1
    Public Function findMaxColumnValue(ByVal ds As DataTable, ByVal col As DataColumn) As Integer
        Dim rows As DataRowCollection
        Dim row As DataRow
        Dim value As Integer
        Dim maxValue As Integer
        maxValue = Integer.MinValue

        rows = ds.Rows
        For Each row In rows
            value = row.Item(col.ColumnName)
            If value > maxValue Then maxValue = value
        Next
        findMaxColumnValue = maxValue
    End Function
End Module
