using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Requests;
using VacaYAY.Entities.Resolutions;

namespace VacaYAY.Business
{
    public class PDFGen
    {
        public Document Document;
        private PdfWriter pdfWriter;

        public PDFGen()
        {
            
        }
        public string GenerateResolution(Request request)
        {
            Document = new Document(PageSize.A4, 20, 20, 42, 35);
            string filename = DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".pdf";
            pdfWriter = PdfWriter.GetInstance(Document, new FileStream(filename, FileMode.Create));
            Document.Open();
            Document.AddTitle("Resolution for " + request.Employee.Name + " " + request.Employee.LastName);
            Document.Add(new Paragraph(15, "Radnik: " + request.Employee.Name + " " + request.Employee.LastName, new Font(Font.FontFamily.HELVETICA)));
            Document.Add(new Paragraph(15, "Pocetak odmora: " + request.StartDate.ToShortDateString(), new Font(Font.FontFamily.HELVETICA)));
            Document.Add(new Paragraph(15, "Zavrsetak odmora: " + request.EndDate.ToShortDateString(), new Font(Font.FontFamily.HELVETICA)));
            Document.Add(new Paragraph(15, "Tip odmora: " + request.TypeOfDays, new Font(Font.FontFamily.HELVETICA)));
            Document.Add(new Paragraph(15, "Broj dana: " + request.NumberOfDays, new Font(Font.FontFamily.HELVETICA)));
            Document.Add(new Paragraph(15, "\n\nPoyy", new Font(Font.FontFamily.HELVETICA)));
            Close();
            return filename;
        }
        private void Close()
        {
            Document.Close();
            pdfWriter.Close();
        }

    }
}
