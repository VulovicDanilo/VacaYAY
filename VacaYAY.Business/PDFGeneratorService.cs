using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VacaYAY.Business.DTOs;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Requests;
using VacaYAY.Entities.Resolutions;

namespace VacaYAY.Business
{
    public class PDFGen
    {
        public static void GenerateUnpaidResolution(Employee employee, Request request, Resolution resolution,Employee HR)
        {
            RegularReportDTO dto = new RegularReportDTO(employee, request, resolution);
            ReportViewer viewer = CreateReportViewer();

            List<RegularReportDTO> dtos = new List<RegularReportDTO>();
            dtos.Add(dto);

            viewer.LocalReport.ReportPath = ".\\Reports\\PaidReport.rdlc";
            viewer.LocalReport.DataSources.Add(new ReportDataSource("RegularReportDataSet", dtos));

            string filename = SavePDF(viewer, resolution, employee);
            FileInfo file = new FileInfo(filename);
            EmailSender.ApproveRegularVacation(request, file.FullName);
        }

        public static void GeneratePaidResolution(Employee employee,Request request,Resolution resolution,Employee HR)
        {
            PaidReportDTO dto = new PaidReportDTO(employee, request, resolution);
            ReportViewer viewer = CreateReportViewer();
            List<PaidReportDTO> dtos = new List<PaidReportDTO>();
            dtos.Add(dto);
            viewer.LocalReport.ReportPath = ".\\Reports\\PaidReport.rdlc";
            viewer.LocalReport.DataSources.Add(new ReportDataSource("PaidReportDataSet", dtos));

            string filename= SavePDF(viewer, resolution,employee);
            FileInfo file = new FileInfo(filename);
            EmailSender.ApprovePaidVacation(request, file.FullName);
        }
        private static string SavePDF(ReportViewer viewer,Resolution resolution,Employee employee)
        {
            byte[] bytes = viewer.LocalReport.Render("PDF", "");

            string filepath = "~/Resolutions/" + employee.UserID + "/";
            var directory = HttpContext.Current.Server.MapPath(filepath);
            Directory.CreateDirectory(directory);

            var filename = DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".pdf";
            var fullpath = Path.Combine(directory, filename);
            using (FileStream fs = new FileStream(fullpath, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
            resolution.Link = filepath + filename;
            return fullpath;
        }
        private static ReportViewer CreateReportViewer()
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.LocalReport.EnableHyperlinks = true;
            return reportViewer;
        }

    }
}
