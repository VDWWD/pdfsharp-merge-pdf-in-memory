using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;
using System.IO;

namespace WebApplication1
{
    public class MergePDF
    {
        public static byte[] Merge(List<byte[]> files)
        {
            var streams = new List<Stream>();

            //add the byte arrays to the stream list
            foreach (var file in files)
            {
                streams.Add(new MemoryStream(file));
            }

            return Merge(streams);
        }


        public static byte[] Merge(List<Stream> files)
        {
            using (var merged_pdf = new PdfDocument())
            {
                //loop all the uploaded files
                foreach (var file in files)
                {
                    //read the pdf file from the stream
                    var source_pdf = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                    //loop each page in the source and add them to the new pdf
                    for (int i = 0; i < source_pdf.PageCount; i++)
                    {
                        merged_pdf.AddPage(source_pdf.Pages[i]);
                    }
                }

                //copy the new pdf to a memorystream
                byte[] bin = null;
                using (var stream = new MemoryStream())
                {
                    merged_pdf.Save(stream, false);
                    bin = stream.ToArray();
                }

                //return the merged pdf as byte array
                return bin;
            }
        }
    }
}