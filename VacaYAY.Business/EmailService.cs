using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Requests;

namespace VacaYAY.Business
{
    public class EmailSender
    {
        protected MailMessage mailMessage;
        public EmailSender()
        {
            mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("VacaYAY.mailsender@gmail.com");
            mailMessage.IsBodyHtml = false;
        }
        public void AddReceiver(string receiverAddress)
        {
            if (!mailMessage.To.Contains(new MailAddress(receiverAddress)))
            {
                mailMessage.To.Add(new MailAddress(receiverAddress));
            }
        }
        public void AddReceivers(List<string> emails)
        {
            foreach (string email in emails)
                mailMessage.To.Add(email);
        }
        public void SetSubject(string subject)
        {
            mailMessage.Subject = subject;
        }
        public void AddAttachment(string filepath)
        {
            mailMessage.Attachments.Add(new Attachment(filepath));
        }
        public void SetBody(string body)
        {
            mailMessage.Body = body;
        }
        public void SendEmail()
        {
            using (SmtpClient sc = new SmtpClient("smtp.gmail.com", 587))
            {
                sc.Credentials = new NetworkCredential("VacaYAY.mailsender@gmail.com", "INGPraksa");
                sc.EnableSsl = true;
                sc.Send(mailMessage);
            }
            //var SMTP = new SmtpClient()
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    Credentials = new NetworkCredential("VacaYAY.mailsender@gmail.com", "INGPraksa"),
            //};
            //Thread t1 = new Thread(delegate ()
            //{
            //    SMTP.Send(mailMessage);
            //});
            //t1.Start();
        }
        public static void SendNewRequestEmailToAllManagers(Request request)
        {
            List<string> managerEmails = EmployeeService.GetManagerEmails();
            EmailSender es = new EmailSender();
            es.AddReceivers(managerEmails);
            Employee employee = EmployeeService.GetEmployee(request.EmployeeID);
            es.SetSubject("[INGSoftware] Zahtev za odmor - " + employee.Name + " " + employee.LastName);
            es.SetBody("Postovani" +
                "\n\n" +
                "Pristigao je novi zahtev za odmor." +
                "\n\n" +
                "Radnik: " + employee.Name + " " + employee.LastName +
                "\n" +
                "Pocetak odmora: " + request.StartDate.ToShortDateString() +
                "\n" +
                "Kraj odmora: " + request.EndDate.ToShortDateString() +
                "\n" +
                "Broj dana: " + request.NumberOfDays +
                "\n" +
                "Tip odmora: " + request.TypeOfDays +
                "\n\n" +
                "Vas INGSoftware!");
            es.SendEmail();
        }

        public static void SendEditRequestEmailToAllManagers(Request request)
        {
            EmailSender es = new EmailSender();
            es.AddReceiver(request.Employee.User.Email);
            List<string> managerEmails = EmployeeService.GetManagerEmails();
            es.AddReceivers(managerEmails);
            es.SetSubject("[INGSoftware] Zahtev za odmor je izmenjen - " + request.Employee.Name + " " + request.Employee.LastName);
            es.SetBody("Postovani" +
                "\n\n" +
                "Izmenjen je zahtev za odmor." +
                "\n\n" +
                "Radnik: " + request.Employee.Name + " " + request.Employee.LastName +
                "\n" +
                "Pocetak odmora: " + request.StartDate.ToShortDateString() +
                "\n" +
                "Kraj odmora: " + request.EndDate.ToShortDateString() +
                "\n" +
                "Broj dana: " + request.NumberOfDays +
                "\n" +
                "Tip odmora: " + request.TypeOfDays +
                "\n\n" +
                "Vas INGSoftware!");
            es.SendEmail();
        }

        public static void ApproveRequest(Request request, Employee HR, string file)
        {
            EmailSender es = new EmailSender();
            es.AddReceiver(request.Employee.User.Email);
            es.AddReceiver(HR.User.Email);
            List<string> managerEmails = EmployeeService.GetManagerEmails();
            es.AddReceivers(managerEmails);
            es.SetSubject("[INGSoftware] Odobren zahtev za odmor - " + request.Employee.Name + " " + request.Employee.LastName);
            es.SetBody("Postovani" +
                "\n\n" +
                "Odobren je zahtev za odmor." +
                "\n\n" +
                "Radnik: " + request.Employee.Name + " " + request.Employee.LastName +
                "\n" +
                "Pocetak odmora: " + request.StartDate.ToShortDateString() +
                "\n" +
                "Kraj odmora: " + request.EndDate.ToShortDateString() +
                "\n" +
                "Broj dana: " + request.NumberOfDays +
                "\n" +
                "Tip odmora: " + request.TypeOfDays +
                "\n" +
                "HR koji je odobrio zahtev: " + HR.Name + " " + HR.LastName +
                "\n\n" +
                "Vas INGSoftware!");
            es.AddAttachment(file);
            es.SendEmail();
        }
        public static void RejectRequest(Request request, Employee HR)
        {
            EmailSender es = new EmailSender();
            es.AddReceiver(request.Employee.User.Email);
            es.AddReceiver(HR.User.Email);
            es.SetSubject("[INGSoftware] Odbijen zahtev za odmor - " + request.Employee.Name + " " + request.Employee.LastName);
            es.SetBody("Postovani" +
                "\n\n" +
                "Odbijen je zahtev za odmor." +
                "\n\n" +
                "Radnik: " + request.Employee.Name + " " + request.Employee.LastName +
                "\n" +
                "Pocetak odmora: " + request.StartDate.ToShortDateString() +
                "\n" +
                "Kraj odmora: " + request.EndDate.ToShortDateString() +
                "\n" +
                "Broj dana: " + request.NumberOfDays +
                "\n" +
                "Tip odmora: " + request.TypeOfDays +
                "\n" +
                "HR koji je odbio zahtev: " + HR.Name + " " + HR.LastName +
                "\n\n" +
                "Vas INGSoftware!");
            es.SendEmail();
        }
        public static void SendCollective(Request request,Employee HR,string filename)
        {
            EmailSender es = new EmailSender();
            es.AddReceiver(request.Employee.User.Email);
            es.SetSubject("[INGSoftware] Kolektivni odmor kreiran od strane - " + HR.Name + " " + HR.LastName);
            es.SetBody("Postovani" +
                "\n\n" +
                "Kolektivni odmor je kreiran." +
                "\n\n" +
                "Pocetak odmora: " + request.StartDate.ToShortDateString() +
                "\n" +
                "Kraj odmora: " + request.EndDate.ToShortDateString() +
                "\n" +
                "Broj dana: " + request.NumberOfDays +
                "\n" +
                "Tip odmora: " + request.TypeOfDays +
                "\n" +
                "HR koji je kreirao kolektivni odmor: " + HR.Name + " " + HR.LastName +
                "\n\n" +
                "Vas INGSoftware!");
            es.AddAttachment(filename);
            Task t1 = Task.Factory.StartNew(() => es.SendEmail());
        }
    }
}
