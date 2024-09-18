using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.RegularExpressions;
using DU = Domain.Utility;

namespace Utility.Export
{
    public class ExportToPDF
    {

        IHttpContextAccessor _httpContextAccessor;
        public ExportToPDF(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public byte[] ExportContentToPdf(string reportContent, string reportName, int left = 6, int right = 6, int top = 65, int bottom = 15, bool isLandScape = false, ExportDataTitle reportTitle = null, bool withFooter = true)
        {
            MemoryStream _memoryStream = new MemoryStream();
            try
            {

                //BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                //Font times = new Font(bfTimes, 12, Font.ITALIC, BaseColor.BLACK);

                // arialuniTff = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.TTF");//Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
                //FontFactory.Register(arialuniTff);

                Document doc = new Document(PageSize.A4, left, right, top, bottom);
                if (isLandScape)
                    doc.SetPageSize(PageSize.A4.Rotate());


                reportName = reportName + ".pdf";



                //_httpContextAccessor.HttpContext.Response.ContentType = "application/pdf";
                //_httpContextAccessor.HttpContext.Response.Headers.Add("content-disposition",
                // "attachment;filename=" + reportName + "");


                //_httpContextAccessor.HttpContext.Response.ContentEncoding = Encoding.UTF8;
                //_httpContextAccessor.HttpContext.Response.HeaderEncoding = Encoding.UTF8;

                //HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                //HttpContext.Current.Response.HeaderEncoding = Encoding.UTF8;
                //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);


                //HttpContext.Current.Response.ContentType = "application/pdf";
                //HttpContext.Current.Response.AddHeader("content-disposition",
                // "attachment;filename=" + reportName + "");
                //HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                //HttpContext.Current.Response.HeaderEncoding = Encoding.UTF8;
                //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);


                string html = reportContent.ToString();
                html = Regex.Replace(html, "</?(a|A).*?>", "");
                //html = Regex.Replace(html, "&(?!nbsp;)", "&amp;");
                //html = Regex.Replace(html, "&", "&amp;");

                PdfWriter writer = PdfWriter.GetInstance(doc, _memoryStream);
                //writer.CloseStream = false;
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(reportName, FileMode.Create));
                if (withFooter)
                {
                    ITextEvent eventHead = new ITextEvent();
                    eventHead.FormatData = reportTitle;
                    writer.PageEvent = eventHead;
                }
                doc.Open();

                // CSS

                //var cssFilePath = DU.Utility.ServerPath + "\\print.css";

                var cssFilePath = DU.Utility.UploadingFolderPath + "print.css";
                var cssResolver = new StyleAttrCSSResolver();
                var cssFile = XMLWorkerHelper.GetCSS(new FileStream($@"{cssFilePath}", FileMode.Open));
                cssResolver.AddCss(cssFile);

                CssAppliers ca = new CssAppliersImpl();
                HtmlPipelineContext hpc = new HtmlPipelineContext(ca);
                hpc.SetTagFactory(Tags.GetHtmlTagProcessorFactory());


                //// PIPELINES
                PdfWriterPipeline pdf = new PdfWriterPipeline(doc, writer);
                HtmlPipeline htmlPipe = new HtmlPipeline(hpc, pdf);
                CssResolverPipeline css = new CssResolverPipeline(cssResolver, htmlPipe);

                XMLWorker worker = new XMLWorker(css, true);
                XMLParser p = new XMLParser(worker, Encoding.UTF8);
                StringReader sr = new StringReader(html);
                p.Parse(sr);
                doc.Close();

                //_memoryStream.Dispose();

                return _memoryStream.ToArray();



            }
            catch (Exception ex)
            {
                //eallySimpleLog.WriteLog(ex);
            }
            return _memoryStream.ToArray();
        }


        public static void ExportEmployeeContentToPDF(string reportContent, string reportName, string candidatePhoto, int left = 6, int right = 6, int top = 65, int bottom = 15, bool isLandScape = false, ExportDataTitle reportTitle = null)
        {
            try
            {
                candidatePhoto = (candidatePhoto == null) ? "" : candidatePhoto;
                var candidatePhotoPath = DU.Utility.UploadingFolderPath;
                if (!candidatePhoto.Contains(candidatePhotoPath))
                    candidatePhotoPath = candidatePhotoPath + candidatePhoto;

                Document doc = new Document(PageSize.A4, left, right, top, bottom);
                if (isLandScape)
                    doc.SetPageSize(PageSize.A4.Rotate());

                reportName = reportName + ".pdf";

                string html = reportContent;
                html = Regex.Replace(html, "</?(a|A).*?>", "");

                MemoryStream _memoryStream = new MemoryStream();

                PdfWriter writer = PdfWriter.GetInstance(doc, _memoryStream);
                ITextEvent eventHead = new ITextEvent();
                eventHead.FormatData = reportTitle;
                writer.PageEvent = eventHead;
                doc.Open();


                //candidate image
                if (candidatePhoto != null && !string.IsNullOrEmpty(candidatePhoto))
                {
                    try
                    {
                        //string imageURL = @"D:\Emerald\ProfessionalProjects\EMS_UploadFiles\Application\EA\EA_PH_0_432021111.jpg";
                        iTextSharp.text.Image candidateImg = iTextSharp.text.Image.GetInstance(candidatePhotoPath);
                        //Resize image depend upon your need
                        candidateImg.ScaleToFit(140f, 100f);
                        candidateImg.SpacingBefore = 10f;
                        candidateImg.SpacingAfter = 1f;
                        candidateImg.BorderWidthLeft = 5f;
                        candidateImg.BorderWidthTop = 5f;
                        candidateImg.BorderWidthRight = 5f;
                        candidateImg.BorderWidthBottom = 5f;
                        candidateImg.BorderColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Gray);
                        candidateImg.SetAbsolutePosition(440, 620);
                        doc.Add(candidateImg);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }


                // CSS
                var cssFilePath = DU.Utility.UploadingFolderPath + "print.css";
                var cssResolver = new StyleAttrCSSResolver();
                var cssFile = XMLWorkerHelper.GetCSS(new FileStream($@"{cssFilePath}", FileMode.Open));
                cssResolver.AddCss(cssFile);

                // HTML
                CssAppliers ca = new CssAppliersImpl();
                HtmlPipelineContext hpc = new HtmlPipelineContext(ca);
                hpc.SetTagFactory(Tags.GetHtmlTagProcessorFactory());

                // PIPELINES
                PdfWriterPipeline pdf = new PdfWriterPipeline(doc, writer);
                HtmlPipeline htmlPipe = new HtmlPipeline(hpc, pdf);
                CssResolverPipeline css = new CssResolverPipeline(cssResolver, htmlPipe);

                XMLWorker worker = new XMLWorker(css, true);
                XMLParser p = new XMLParser(worker);
                StringReader sr = new StringReader(html);
                p.Parse(sr);

                doc.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public byte[] GeneratePdfBiteArray(string htmlTable)
        {
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                using (var doc = new iTextSharp.text.Document())
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();

                        using (var srHtml = new StringReader(htmlTable))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
                        }
                        doc.Close();
                    }
                }
                bytes = ms.ToArray();
            }
            return bytes;
        }

        public byte[] ExportReceiptContentToPdf(string reportContent, string reportName, int left = 6, int right = 6, int top = 65, int bottom = 15, bool isLandScape = false, ExportDataTitle reportTitle = null, bool withFooter = true)
        {
            MemoryStream _memoryStream = new MemoryStream();
            try
            {
                //Document doc = new Document(PageSize.A4, left, right, top, bottom);
                //if (isLandScape)
                //    doc.SetPageSize(PageSize.A4.Rotate());

                var pgSize = new Rectangle(250, 600);

                Document doc = new Document(pgSize, left, right, top, bottom);
                if (isLandScape)
                    doc.SetPageSize(PageSize.A4.Rotate());

                reportName = reportName + ".pdf";

                string html = reportContent.ToString();
                html = Regex.Replace(html, "</?(a|A).*?>", "");

                PdfWriter writer = PdfWriter.GetInstance(doc, _memoryStream);
                //writer.CloseStream = false;
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(reportName, FileMode.Create));
                if (withFooter)
                {
                    ITextEvent eventHead = new ITextEvent();
                    eventHead.FormatData = reportTitle;
                    writer.PageEvent = eventHead;
                }
                doc.Open();

                // CSS

                //var cssFilePath = DU.Utility.ServerPath + "\\print.css";

                var cssFilePath = DU.Utility.UploadingFolderPath + "print.css";
                var cssResolver = new StyleAttrCSSResolver();
                var cssFile = XMLWorkerHelper.GetCSS(new FileStream($@"{cssFilePath}", FileMode.Open));
                cssResolver.AddCss(cssFile);

                CssAppliers ca = new CssAppliersImpl();
                HtmlPipelineContext hpc = new HtmlPipelineContext(ca);
                hpc.SetTagFactory(Tags.GetHtmlTagProcessorFactory());


                //// PIPELINES
                PdfWriterPipeline pdf = new PdfWriterPipeline(doc, writer);
                HtmlPipeline htmlPipe = new HtmlPipeline(hpc, pdf);
                CssResolverPipeline css = new CssResolverPipeline(cssResolver, htmlPipe);

                XMLWorker worker = new XMLWorker(css, true);
                XMLParser p = new XMLParser(worker, Encoding.UTF8);
                StringReader sr = new StringReader(html);
                p.Parse(sr);
                doc.Close();

                //_memoryStream.Dispose();

                return _memoryStream.ToArray();



            }
            catch (Exception ex)
            {
                //eallySimpleLog.WriteLog(ex);
            }
            return _memoryStream.ToArray();
        }
    }
}
