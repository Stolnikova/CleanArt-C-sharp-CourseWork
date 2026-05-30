using CleaningCrm.Entities;
using CleaningCrm.Services.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CleaningCrm.Services;

public class DocumentService : IDocumentService
{
    public byte[] GenerateAct(Order order)
    {
        using MemoryStream stream = new MemoryStream();

        using (WordprocessingDocument document = WordprocessingDocument.Create(
            stream, WordprocessingDocumentType.Document))
        {
            MainDocumentPart mainPart = document.AddMainDocumentPart();
            mainPart.Document = new Document();
            Body body = mainPart.Document.AppendChild(new Body());

            AddTitle(body);
            AddEmptyLine(body);
            AddCompanyInfo(body, order);
            AddEmptyLine(body);
            AddServicesTable(body, order);
            AddEmptyLine(body);
            AddTotal(body, order.TotalAmount);

            mainPart.Document.Save();
        }

        return stream.ToArray();
    }

    private static void AddTitle(Body body)
    {
        Paragraph paragraph = new Paragraph();

        ParagraphProperties paragraphProperties = new ParagraphProperties();
        Justification justification = new Justification()
        {
            Val = JustificationValues.Center
        };
        paragraphProperties.Append(justification);
        paragraph.Append(paragraphProperties);

        Run run = new Run();
        RunProperties runProperties = new RunProperties();
        Bold bold = new Bold();
        FontSize fontSize = new FontSize() { Val = "32" };
        runProperties.Append(bold);
        runProperties.Append(fontSize);
        run.Append(runProperties);
        run.Append(new Text("АКТ ВИКОНАНИХ РОБІТ"));
        paragraph.Append(run);

        body.Append(paragraph);
    }

    private static void AddCompanyInfo(Body body, Order order)
    {
        string companyName = order.Company.Name;
        string contactPersonName = order.ContactPerson.FullName;
        string contactPersonPhone = order.ContactPerson.Phone;
        string date = order.ScheduledDate.ToString("dd.MM.yyyy");
        string? addressLine = order.Address != null ? order.Address.Line : null;

        AddInfoLine(body, "Компанія:", companyName);
        AddInfoLine(body, "Контактна особа:", contactPersonName);
        AddInfoLine(body, "Телефон:", contactPersonPhone);

        if (addressLine != null)
        {
            AddInfoLine(body, "Адреса:", addressLine);
        }

        AddInfoLine(body, "Дата виконання робіт:", date);
    }

    private static void AddInfoLine(Body body, string label, string value)
    {
        Paragraph paragraph = new Paragraph();

        Run labelRun = new Run();
        RunProperties labelRunProperties = new RunProperties();
        Bold bold = new Bold();
        labelRunProperties.Append(bold);
        labelRun.Append(labelRunProperties);
        labelRun.Append(new Text(label + " ") { Space = SpaceProcessingModeValues.Preserve });

        Run valueRun = new Run();
        valueRun.Append(new Text(value));

        paragraph.Append(labelRun);
        paragraph.Append(valueRun);
        body.Append(paragraph);
    }

    private static void AddServicesTable(Body body, Order order)
    {
        Table table = new Table();

        TableProperties tableProperties = new TableProperties();
        TableBorders tableBorders = new TableBorders(
            new TopBorder() { Val = BorderValues.Single, Size = 4 },
            new BottomBorder() { Val = BorderValues.Single, Size = 4 },
            new LeftBorder() { Val = BorderValues.Single, Size = 4 },
            new RightBorder() { Val = BorderValues.Single, Size = 4 },
            new InsideHorizontalBorder() { Val = BorderValues.Single, Size = 4 },
            new InsideVerticalBorder() { Val = BorderValues.Single, Size = 4 }
        );
        tableProperties.Append(tableBorders);
        table.Append(tableProperties);

        table.Append(CreateHeaderRow());

        foreach (OrderItem item in order.Items)
        {
            table.Append(CreateItemRow(item));
        }

        body.Append(table);
    }

    private static TableRow CreateHeaderRow()
    {
        return CreateRow(
            isHeader: true,
            "Назва послуги",
            "Од. вим.",
            "Кількість",
            "Ціна",
            "Сума"
        );
    }

    private static TableRow CreateItemRow(OrderItem item)
    {
        decimal total = item.Quantity * item.PriceSnapshot;

        return CreateRow(
            isHeader: false,
            item.ServiceName,
            item.UnitSnapshot,
            item.Quantity.ToString(),
            item.PriceSnapshot.ToString("F2"),
            total.ToString("F2")
        );
    }

    private static TableRow CreateRow(bool isHeader, params string[] values)
    {
        TableRow row = new TableRow();

        foreach (string value in values)
        {
            TableCell cell = new TableCell();

            TableCellProperties cellProperties = new TableCellProperties();
            TableCellWidth cellWidth = new TableCellWidth()
            {
                Type = TableWidthUnitValues.Auto
            };
            cellProperties.Append(cellWidth);
            cell.Append(cellProperties);

            Paragraph paragraph = new Paragraph();
            Run run = new Run();

            if (isHeader)
            {
                RunProperties runProperties = new RunProperties();
                runProperties.Append(new Bold());
                run.Append(runProperties);
            }

            run.Append(new Text(value));
            paragraph.Append(run);
            cell.Append(paragraph);
            row.Append(cell);
        }

        return row;
    }

    private static void AddTotal(Body body, decimal totalAmount)
    {
        Paragraph paragraph = new Paragraph();

        Run run = new Run();
        RunProperties runProperties = new RunProperties();
        runProperties.Append(new Bold());
        run.Append(runProperties);
        run.Append(new Text($"Загальна сума: {totalAmount:F2} грн"));

        paragraph.Append(run);
        body.Append(paragraph);
    }

    private static void AddEmptyLine(Body body)
    {
        body.Append(new Paragraph());
    }
}