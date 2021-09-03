using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class HomeViewModel
    {
        [Display(Name = "Upload PDF 1")]
        public HttpPostedFileBase pdf1 { get; set; }

        [Display(Name = "Upload PDF 2")]
        public HttpPostedFileBase pdf2 { get; set; }
    }
}