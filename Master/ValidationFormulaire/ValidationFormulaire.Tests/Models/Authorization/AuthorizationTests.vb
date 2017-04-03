Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports ValidationFormulaire.Core
Imports Moq
Imports System.Web
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

<TestClass()> Public Class AuthorizationTests
    Private authorization As Authorization
    Private extractor As Mock(Of ITextExtractor)
    Private bar_code_reader As Mock(Of IBarCodeReader)
    Private search_pdf_text As Mock(Of ISearchPDFText)
    Private bar_code_converter As Mock(Of IBarCodeConverter)
    Private text_cleaner As Mock(Of ITextCleaner)
    Private file_mock As Mock(Of HttpPostedFileBase)
    Private file_stream As MemoryStream

    <TestInitialize()> _
    Public Sub Initialize()
        extractor = New Mock(Of ITextExtractor)()
        bar_code_reader = New Mock(Of IBarCodeReader)()
        search_pdf_text = New Mock(Of ISearchPDFText)()
        bar_code_converter = New Mock(Of IBarCodeConverter)()
        text_cleaner = New Mock(Of ITextCleaner)()
        file_mock = New Mock(Of HttpPostedFileBase)
        file_stream = New MemoryStream()

        Using file_mock_bitmap = New Bitmap(1, 1)
            Dim file_mock_graphics = Graphics.FromImage(file_mock_bitmap)
            file_mock_graphics.FillRectangle(Brushes.Black, 0, 0, 1, 1)
            file_mock_bitmap.Save(file_stream, ImageFormat.Jpeg)

            file_mock.Setup(Function(pf) pf.InputStream).Returns(file_stream)
        End Using
    End Sub

    <TestCleanup()> _
    Public Sub CleanUp()
        file_stream.Close()
    End Sub

    <TestMethod()> Public Sub VerifyThatCorrectAlertIsSentWhenBarCodeTextIsEmpty()
        bar_code_reader.Setup(Sub(s) s.ReadBarCode(file_mock.Object))
        bar_code_reader.Setup(Function(f) f.GetBarCodeText()).Returns(Nothing)
        bar_code_reader.Setup(Function(f) f.GetBarCodeId()).Returns("13RS")

        authorization = New Authorization(extractor.Object, bar_code_reader.Object, search_pdf_text.Object,
                                          bar_code_converter.Object, text_cleaner.Object)
        authorization.AuthorizePDF(file_mock.Object)

        Assert.IsTrue(AlertsManager.GetAllAlerts.ContainsKey("Incapable de lire le code à bar de données!"))
    End Sub

    <TestMethod()> Public Sub VerifyThatCorrectAlertIsSentWhenBarCodeIdIsEmpty()
        bar_code_reader.Setup(Sub(s) s.ReadBarCode(file_mock.Object))
        bar_code_reader.Setup(Function(f) f.GetBarCodeText()).Returns("")
        bar_code_reader.Setup(Function(f) f.GetBarCodeId()).Returns(Nothing)

        authorization = New Authorization(extractor.Object, bar_code_reader.Object, search_pdf_text.Object,
                                          bar_code_converter.Object, text_cleaner.Object)
        authorization.AuthorizePDF(file_mock.Object)

        Assert.IsTrue(AlertsManager.GetAllAlerts.ContainsKey("Incapable de lire le code à bar d'identification!"))
    End Sub

    <TestMethod()> Public Sub VerifyThatDataDictionaryIsProperlyFilled()
        Dim pdf_words(1) As Dictionary(Of Point, String)
        Dim cleaned_pdf_words(1) As Dictionary(Of Point, String)
        Dim data As New DataSet()
        Dim data_dictionary As New Dictionary(Of String, BarCodeData)()
        Dim failed_data_dictionary As New Dictionary(Of String, BarCodeData)()

        data.Tables.Add("test")
        data.Tables("test").Columns.Add("No Champ")
        data.Tables("test").Columns.Add("Description du champ")
        data.Tables("test").Columns.Add("Pos. Début")
        data.Tables("test").Columns.Add("Pos. Fin")
        data.Tables("test").Columns.Add("Longueur")
        data.Tables("test").Columns.Add("Format")
        data.Tables("test").Columns.Add("Champ")
        data.Tables("test").Rows.Add({1, "année", 14, 17, 4, "9(04)", "txtAnnee"})
        data.Tables("test").Rows.Add({2, "code", 18, 19, 1, "X(01)", "txtCodeReleve"})

        data_dictionary.Add("txtAnnee", New BarCodeData With {.value = "2017",
                                                                                      .description = "année",
                                                                                      .start_position = 14,
                                                                                      .end_position = 17,
                                                                                      .length = 4})
        data_dictionary.Add("txtCodeReleve", New BarCodeData With {.value = "D",
                                                                                      .description = "code",
                                                                                      .start_position = 18,
                                                                                      .end_position = 19,
                                                                                      .length = 1})

        bar_code_reader.Setup(Sub(s) s.ReadBarCode(file_mock.Object))
        bar_code_reader.Setup(Function(f) f.GetBarCodeText()).Returns("13RSFS99999992017D")
        bar_code_reader.Setup(Function(f) f.GetBarCodeId()).Returns("13RS")
        extractor.Setup(Function(f) f.PDFToText(file_mock.Object, True)).Returns(pdf_words)
        bar_code_converter.Setup(Function(f) f.ConvertBarCodeToData("13RS")).Returns(data)
        text_cleaner.Setup(Function(f) f.RemoveOffLimitsWords(pdf_words)).Returns(cleaned_pdf_words)
        search_pdf_text.Setup(Function(f) f.FindBarCodeValues(cleaned_pdf_words, data_dictionary)).Returns(failed_data_dictionary)

        authorization = New Authorization(extractor.Object, bar_code_reader.Object, search_pdf_text.Object,
                                          bar_code_converter.Object, text_cleaner.Object)
        authorization.AuthorizePDF(file_mock.Object)

        Assert.AreEqual(data_dictionary.ElementAt(0).Key, authorization.GetData.ElementAt(0).Key)
        Assert.AreEqual(data_dictionary.ElementAt(0).Value.value, authorization.GetData.ElementAt(0).Value.value)
        Assert.AreEqual(data_dictionary.ElementAt(0).Value.description, authorization.GetData.ElementAt(0).Value.description)
        Assert.AreEqual(data_dictionary.ElementAt(0).Value.start_position, authorization.GetData.ElementAt(0).Value.start_position)
        Assert.AreEqual(data_dictionary.ElementAt(0).Value.end_position, authorization.GetData.ElementAt(0).Value.end_position)
        Assert.AreEqual(data_dictionary.ElementAt(0).Value.length, authorization.GetData.ElementAt(0).Value.length)

        Assert.AreEqual(data_dictionary.ElementAt(1).Key, authorization.GetData.ElementAt(1).Key)
        Assert.AreEqual(data_dictionary.ElementAt(1).Value.value, authorization.GetData.ElementAt(1).Value.value)
        Assert.AreEqual(data_dictionary.ElementAt(1).Value.description, authorization.GetData.ElementAt(1).Value.description)
        Assert.AreEqual(data_dictionary.ElementAt(1).Value.start_position, authorization.GetData.ElementAt(1).Value.start_position)
        Assert.AreEqual(data_dictionary.ElementAt(1).Value.end_position, authorization.GetData.ElementAt(1).Value.end_position)
        Assert.AreEqual(data_dictionary.ElementAt(1).Value.length, authorization.GetData.ElementAt(1).Value.length)
    End Sub

    <TestMethod()> Public Sub VerifyThatAllAuthorizationLogicFunctionsAreCalledInCorrectOrder()
        Dim sequence = New MockSequence()
        Dim pdf_words(1) As Dictionary(Of Point, String)
        Dim cleaned_pdf_words(1) As Dictionary(Of Point, String)
        Dim data As New DataSet()
        Dim data_dictionary As New Dictionary(Of String, BarCodeData)()
        Dim failed_data_dictionary As New Dictionary(Of String, BarCodeData)()

        bar_code_reader.Setup(Sub(s) s.ReadBarCode(file_mock.Object))
        bar_code_reader.Setup(Function(f) f.GetBarCodeText()).Returns("13RSFS99999992017D")
        bar_code_reader.Setup(Function(f) f.GetBarCodeId()).Returns("13RS")
        extractor.InSequence(sequence).Setup(Function(f) f.PDFToText(file_mock.Object, True)).Returns(pdf_words)
        bar_code_converter.InSequence(sequence).Setup(Function(f) f.ConvertBarCodeToData("13RS")).Returns(data)
        text_cleaner.InSequence(sequence).Setup(Function(f) f.RemoveOffLimitsWords(pdf_words)).Returns(cleaned_pdf_words)
        search_pdf_text.InSequence(sequence).Setup(Function(f) f.FindBarCodeValues(cleaned_pdf_words, data_dictionary)).Returns(failed_data_dictionary)

        authorization = New Authorization(extractor.Object, bar_code_reader.Object, search_pdf_text.Object,
                                          bar_code_converter.Object, text_cleaner.Object)
        authorization.AuthorizePDF(file_mock.Object)

        extractor.Verify(Function(f) f.PDFToText(file_mock.Object, True), Times.Exactly(1))
        bar_code_converter.Verify(Function(f) f.ConvertBarCodeToData("13RS"), Times.Exactly(1))
        text_cleaner.Verify(Function(f) f.RemoveOffLimitsWords(pdf_words), Times.Exactly(1))
        search_pdf_text.Verify(Function(f) f.FindBarCodeValues(cleaned_pdf_words, data_dictionary), Times.Exactly(1))
    End Sub
End Class