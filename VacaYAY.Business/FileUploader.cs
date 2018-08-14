using Com.CloudRail.SI;
using Com.CloudRail.SI.ServiceCode.Commands.CodeRedirect;
using Com.CloudRail.SI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VacaYAY.Business
{
    public class FileUploader
    {

        private PCloud service;

        //PCloud service = new PCloud(
        //    new LocalReceiver(8082),
        //    "[PCloud Client Identifier]",
        //    "[PCloud Client Secret]",
        //    "http://localhost:8082/auth",
        //    "someState"
        //);
        // UPLOAD
        //service.Upload(
        //    "/myFolder/myFile.png",
        //    Stream,
        //    1024,
        //    true
        //);
        public FileUploader()
        {
            service = new PCloud(new LocalReceiver(55555), "2GhDsx5sSpY", "", "http://localhost:55555", "");
        }
        public void Upload(string path)
        {
            try
            {

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
