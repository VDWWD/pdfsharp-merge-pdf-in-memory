using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new Models.HomeViewModel();
            return View(model);
        }


        [HttpPost]
        public FileStreamResult Index(Models.HomeViewModel model)
        {
            //check if the values are present
            if (model.pdf1 == null || model.pdf1 == null || model.pdf1.ContentType != "application/pdf" || model.pdf2.ContentType != "application/pdf")
            {
                return null;
            }

            //convert the uploads to a byte array and add them to a list
            //you could also use a single 'input type=file' with 'muliple' enabled to create a List<HttpPostedFileBase> directly
            var files = new List<Stream>()
            {
                model.pdf1.InputStream,
                model.pdf2.InputStream
            };

            //merge the pdf
            var bin = MergePDF.Merge(files);

            //or if you work with byte arrays
            var files_bytes = new List<byte[]>()
            {
                ConvertHttpPostedFileBaseToByteArray(model.pdf1),
                ConvertHttpPostedFileBaseToByteArray(model.pdf2)
            };

            //merge the pdf
            bin = MergePDF.Merge(files_bytes);

            //return the file to the browser
            return new FileStreamResult(new MemoryStream(bin), "application/pdf")
            {
                FileDownloadName = "VDWWD-pdfsharp-merge-multiple-pdf-demo.pdf"
            };
        }


        //convert HttpPostedFileBase to a byte array
        public byte[] ConvertHttpPostedFileBaseToByteArray(HttpPostedFileBase file)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                file.InputStream.Position = 0;
                file.InputStream.CopyTo(stream);
                return stream.ToArray();
            }
        }
    }
}