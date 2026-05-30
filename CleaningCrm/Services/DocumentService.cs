using CleaningCrm.Entities;
using CleaningCrm.Services.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CleaningCrm.Services;

public class DocumentService : IDocumentService
{
    private const string ExecutorName = "СПД ФО-П Стольніков О.В.";
    private const string ExecutorEdrpou = "ЄДРПОУ 2769014830";
    private const string ExecutorAccount = "р/р UA 813052990000026004016706472";
    private const string ExecutorBank = "в ПАТ КБ \"ПРИВАТБАНК\" м.Київ";
    private const string ExecutorMfo = "МФО 305299";
    private const string ExecutorCertificate = "номер свідоцтва платника ЄП: А 429360";
    private const string ExecutorAddress = "Адреса: м.Київ, вул.Зодчих 38, кв.205";
    private const string ExecutorSigner = "Стольніков О.В.";

    // ─── ACT ────────────────────────────────────────────────────────────────

    public byte[] GenerateAct(Order order)
    {
        using MemoryStream stream = new MemoryStream();

        using (WordprocessingDocument document = WordprocessingDocument.Create(
            stream, WordprocessingDocumentType.Document))
        {
            MainDocumentPart mainPart = document.AddMainDocumentPart();
            mainPart.Document = new Document();
            Body body = mainPart.Document.AppendChild(new Body());

            AddActIntroText(body, order);
            AddEmptyLine(body);
            AddActTable(body, order);
            AddEmptyLine(body);
            AddBoldLine(body, $"Всього на суму: {AmountToWords(order.TotalAmount)}");
            AddBoldLine(body, "Без ПДВ.");
            AddEmptyLine(body);
            AddSignatureBlock(body);

            mainPart.Document.Save();
        }

        return stream.ToArray();
    }

    private static void AddActIntroText(Body body, Order order)
    {
        string companyName = order.Company.Name;
        string contactPerson = order.ContactPerson.FullName;
        string date = order.ScheduledDate.ToString("dd.MM.yyyy");

        string intro = $"Ми, представник Замовника {companyName}, в особі {contactPerson} з одного боку, " +
                       $"та представник Виконавця {ExecutorName}, з іншого боку, склали цей акт про нижченаведене:" +
                       $" Виконавцем були надані послуги згідно рахунку від {date}р. в повному обсязі:";

        body.Append(CreateParagraph(intro));
    }

    private static void AddActTable(Body body, Order order)
    {
        Table table = new Table();
        table.Append(CreateTableProperties());

        int[] widths = { 600, 3500, 1200, 1200, 1500 };

        table.Append(CreateTableRow(
            isHeader: true,
            widths: widths,
            "№ п/п", "Найменування послуги", "Ціна, грн", "Об'єм", "Вартість, грн"
        ));

        int index = 1;
        foreach (OrderItem item in order.Items)
        {
            decimal total = item.Quantity * item.PriceSnapshot;
            table.Append(CreateTableRow(
                isHeader: false,
                widths: widths,
                index.ToString(),
                item.ServiceName,
                item.PriceSnapshot.ToString("F2"),
                item.Quantity.ToString("F2"),
                total.ToString("F2")
            ));
            index++;
        }

        table.Append(CreateTableRow(
            isHeader: true,
            widths: widths,
            "", "Всього:", "", "", order.TotalAmount.ToString("F2")
        ));

        body.Append(table);
    }

    // ─── INVOICE ────────────────────────────────────────────────────────────

    public byte[] GenerateInvoice(Order order)
    {
        using MemoryStream stream = new MemoryStream();

        using (WordprocessingDocument document = WordprocessingDocument.Create(
            stream, WordprocessingDocumentType.Document))
        {
            MainDocumentPart mainPart = document.AddMainDocumentPart();
            mainPart.Document = new Document();
            Body body = mainPart.Document.AppendChild(new Body());

            AddInvoiceHeader(body, order);
            AddEmptyLine(body);
            AddInvoiceTable(body, order);
            AddEmptyLine(body);
            AddBoldLine(body, $"Всього на суму: {AmountToWords(order.TotalAmount)}");
            AddBoldLine(body, "Без ПДВ.");
            AddEmptyLine(body);
            AddInvoiceSignature(body);

            mainPart.Document.Save();
        }

        return stream.ToArray();
    }

    private static void AddInvoiceHeader(Body body, Order order)
    {
        string date = order.ScheduledDate.ToString("dd.MM.yyyy");

        AddLabelValueLine(body, "Виконавець:", ExecutorName);
        AddSimpleLine(body, ExecutorEdrpou);
        AddSimpleLine(body, ExecutorAccount);
        AddSimpleLine(body, ExecutorBank);
        AddSimpleLine(body, ExecutorMfo);
        AddSimpleLine(body, ExecutorCertificate);
        AddSimpleLine(body, ExecutorAddress);
        AddEmptyLine(body);
        AddLabelValueLine(body, "Замовник:", order.Company.Name);
        AddLabelValueLine(body, "Платник:", "Той самий");
        AddLabelValueLine(body, "Умова продажу:", "Безготівковий розрахунок");
        AddEmptyLine(body);
        AddCenteredBoldLine(body, $"Рахунок №_____ від {date} року");
    }

    private static void AddInvoiceTable(Body body, Order order)
    {
        Table table = new Table();
        table.Append(CreateTableProperties());

        int[] widths = { 600, 3500, 1200, 1200, 1500 };

        table.Append(CreateTableRow(
            isHeader: true,
            widths: widths,
            "№ п/п", "Вид послуги", "Одиниця", "Кількість", "Сума"
        ));

        int index = 1;
        foreach (OrderItem item in order.Items)
        {
            decimal total = item.Quantity * item.PriceSnapshot;
            table.Append(CreateTableRow(
                isHeader: false,
                widths: widths,
                index.ToString(),
                item.ServiceName,
                item.UnitSnapshot,
                item.Quantity.ToString("F2"),
                total.ToString("F2")
            ));
            index++;
        }

        table.Append(CreateTableRow(
            isHeader: true,
            widths: widths,
            "", "", "", "Разом без ПДВ:", order.TotalAmount.ToString("F2")
        ));

        body.Append(table);
    }

    private static void AddInvoiceSignature(Body body)
    {
        AddEmptyLine(body);
        AddLabelValueLine(body, "Виписав(ла):", ExecutorSigner);
        AddSimpleLine(body, "М.П.");
    }

    // ─── SHARED HELPERS ─────────────────────────────────────────────────────

    private static TableProperties CreateTableProperties()
    {
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
        return tableProperties;
    }

    private static TableRow CreateTableRow(bool isHeader, int[] widths, params string[] values)
    {
        TableRow row = new TableRow();

        for (int i = 0; i < values.Length; i++)
        {
            TableCell cell = new TableCell();

            TableCellProperties cellProperties = new TableCellProperties();
            cellProperties.Append(new TableCellWidth()
            {
                Type = TableWidthUnitValues.Dxa,
                Width = widths[i].ToString()
            });
            cell.Append(cellProperties);

            Paragraph paragraph = new Paragraph();
            Run run = new Run();

            if (isHeader)
            {
                RunProperties runProperties = new RunProperties();
                runProperties.Append(new Bold());
                run.Append(runProperties);
            }

            run.Append(new Text(values[i]));
            paragraph.Append(run);
            cell.Append(paragraph);
            row.Append(cell);
        }

        return row;
    }

    private static void AddLabelValueLine(Body body, string label, string value)
    {
        Paragraph paragraph = new Paragraph();

        Run labelRun = new Run();
        RunProperties labelProps = new RunProperties();
        labelProps.Append(new Bold());
        labelRun.Append(labelProps);
        labelRun.Append(new Text(label + " ") { Space = SpaceProcessingModeValues.Preserve });

        Run valueRun = new Run();
        valueRun.Append(new Text(value));

        paragraph.Append(labelRun);
        paragraph.Append(valueRun);
        body.Append(paragraph);
    }

    private static void AddSimpleLine(Body body, string text)
    {
        body.Append(CreateParagraph(text));
    }

    private static void AddBoldLine(Body body, string text)
    {
        Paragraph paragraph = new Paragraph();
        Run run = new Run();
        RunProperties runProperties = new RunProperties();
        runProperties.Append(new Bold());
        run.Append(runProperties);
        run.Append(new Text(text));
        paragraph.Append(run);
        body.Append(paragraph);
    }

    private static void AddCenteredBoldLine(Body body, string text)
    {
        Paragraph paragraph = new Paragraph();

        ParagraphProperties paragraphProperties = new ParagraphProperties();
        paragraphProperties.Append(new Justification() { Val = JustificationValues.Center });
        paragraph.Append(paragraphProperties);

        Run run = new Run();
        RunProperties runProperties = new RunProperties();
        runProperties.Append(new Bold());
        runProperties.Append(new FontSize() { Val = "28" });
        run.Append(runProperties);
        run.Append(new Text(text));
        paragraph.Append(run);
        body.Append(paragraph);
    }

    private static void AddSignatureBlock(Body body)
    {
        AddLabelValueLine(body, "Виконавець:", ExecutorSigner);
        AddLabelValueLine(body, "Замовник:", "___________________");
        AddSimpleLine(body, "М.П.");
    }

    private static Paragraph CreateParagraph(string text)
    {
        Paragraph paragraph = new Paragraph();
        Run run = new Run();
        run.Append(new Text(text));
        paragraph.Append(run);
        return paragraph;
    }

    private static void AddEmptyLine(Body body)
    {
        body.Append(new Paragraph());
    }

    // ─── AMOUNT TO WORDS ────────────────────────────────────────────────────

    private static string AmountToWords(decimal amount)
    {
        long hryvnias = (long)Math.Floor(amount);
        int kopecks = (int)Math.Round((amount - hryvnias) * 100);

        string hryvniaWords = NumberToWords(hryvnias);
        string hryvniaForm = GetHryvniaForm(hryvnias);

        return $"{hryvniaWords} {hryvniaForm} {kopecks:00} коп.";
    }

    private static string NumberToWords(long number)
    {
        if (number == 0) return "нуль";

        string[] ones = { "", "одна", "дві", "три", "чотири", "п'ять", "шість", "сім", "вісім", "дев'ять",
                          "десять", "одинадцять", "дванадцять", "тринадцять", "чотирнадцять", "п'ятнадцять",
                          "шістнадцять", "сімнадцять", "вісімнадцять", "дев'ятнадцять" };
        string[] tens = { "", "", "двадцять", "тридцять", "сорок", "п'ятдесят",
                          "шістдесят", "сімдесят", "вісімдесят", "дев'яносто" };
        string[] hundreds = { "", "сто", "двісті", "триста", "чотириста", "п'ятсот",
                              "шістсот", "сімсот", "вісімсот", "дев'ятсот" };

        string result = string.Empty;

        if (number >= 1000000)
        {
            long millions = number / 1000000;
            result += NumberToWords(millions) + " " + GetMillionForm(millions) + " ";
            number %= 1000000;
        }

        if (number >= 1000)
        {
            long thousands = number / 1000;
            string[] thousandOnes = { "", "одна", "дві", "три", "чотири", "п'ять", "шість", "сім", "вісім", "дев'ять" };
            string thousandWords = string.Empty;

            if (thousands >= 100)
            {
                thousandWords += hundreds[thousands / 100] + " ";
                thousands %= 100;
            }
            if (thousands >= 20)
            {
                thousandWords += tens[thousands / 10] + " ";
                thousands %= 10;
            }
            if (thousands > 0 && thousands < 20)
            {
                thousandWords += (thousands < 10 ? thousandOnes[thousands] : ones[thousands]) + " ";
            }

            result += thousandWords + GetThousandForm(number / 1000) + " ";
            number %= 1000;
        }

        if (number >= 100)
        {
            result += hundreds[number / 100] + " ";
            number %= 100;
        }

        if (number >= 20)
        {
            result += tens[number / 10] + " ";
            number %= 10;
        }

        if (number > 0)
        {
            result += ones[number] + " ";
        }

        return result.Trim();
    }

    private static string GetHryvniaForm(long number)
    {
        long lastTwo = number % 100;
        long lastOne = number % 10;

        if (lastTwo >= 11 && lastTwo <= 19) return "грн.";
        if (lastOne == 1) return "грн.";
        if (lastOne >= 2 && lastOne <= 4) return "грн.";
        return "грн.";
    }

    private static string GetThousandForm(long number)
    {
        long lastTwo = number % 100;
        long lastOne = number % 10;

        if (lastTwo >= 11 && lastTwo <= 19) return "тисяч";
        if (lastOne == 1) return "тисяча";
        if (lastOne >= 2 && lastOne <= 4) return "тисячі";
        return "тисяч";
    }

    private static string GetMillionForm(long number)
    {
        long lastTwo = number % 100;
        long lastOne = number % 10;

        if (lastTwo >= 11 && lastTwo <= 19) return "мільйонів";
        if (lastOne == 1) return "мільйон";
        if (lastOne >= 2 && lastOne <= 4) return "мільйони";
        return "мільйонів";
    }
}