Imports System.Web
Imports System.Data.OleDb

Public Class BarCodeConverter
    Implements IBarCodeConverter

    Public Function ConvertBarCodeToData(file_name As String) As DataSet Implements IBarCodeConverter.ConvertBarCodeToData
        Dim data As New DataSet()
        Dim data_table As New DataTable()
        Dim file_location As String
        Dim excel_connection_string As String = ""
        Dim file_extension As String = System.IO.Path.GetExtension(file_name + ".xlsx")
        Dim excel_connection As OleDbConnection
        Dim adapter As OleDbDataAdapter

        file_location = HttpContext.Current.Server.MapPath("~/Content/BarCodeFormat/")

        If file_extension = ".xls" Then
            excel_connection_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file_location + file_name +
                ";Extended Properties=\""Excel 8.0;HDR=Yes;IMEX=2\"""
        ElseIf file_extension = ".xlsx" Then
            excel_connection_string = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file_location + file_name +
                ";Extended Properties=Excel 12.0 XML;"
        End If

        excel_connection = New OleDbConnection(excel_connection_string)
        excel_connection.Open()
        Dim cmd As New OleDbCommand()
        cmd.Connection = excel_connection

        data_table = excel_connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        For Each row As DataRow In data_table.Rows
            Dim dt As New DataTable()
            Dim sheet_name As String = row("TABLE_NAME").ToString

            cmd.CommandText = "SELECT * FROM [" + sheet_name + "]"
            dt.TableName = sheet_name.Replace("$", String.Empty)
            adapter = New OleDbDataAdapter(cmd)

            adapter.Fill(dt)
            data.Tables.Add(dt)
        Next

        cmd = Nothing
        excel_connection.Close()

        Return data
    End Function
End Class
