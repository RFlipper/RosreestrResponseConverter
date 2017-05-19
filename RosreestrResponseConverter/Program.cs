using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using Pechkin;

namespace RosreestrResponseConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] != null)
            {
                ConvertHTMLIntoPDF(ConvertIntoHTML(args[0]));
            }
            //ConvertHTMLIntoPDF(ConvertIntoHTML("C:\\Users\\ivsemionov\\Desktop\\В работе\\Response №40-14011425\\QU_0_59-0001-140--5745-14_04_160412.xml"));
        }
        public static string ConvertIntoHTML(string fileName)
        {
            string stylesheet = GetTemplate(fileName);
            string htmlFileName = Tools.GetFilePathWithoutExtention(fileName)+".html";
            XslTransform xslt = new XslTransform();
            xslt.Load(stylesheet);
            XPathDocument xpathdocument = new
            XPathDocument(fileName);
            XmlTextWriter writer = new XmlTextWriter(htmlFileName, Encoding.UTF8);
            xslt.Transform(xpathdocument, null, writer, null);
            return htmlFileName;
        }
        public static void ConvertHTMLIntoPDF(string fileName)
        {
            Uri uri = new Uri(fileName);
            byte[] pdfBuf = new SimplePechkin(new GlobalConfig()).Convert(uri);
            File.WriteAllBytes(Tools.GetFilePathWithoutExtention(fileName)+".pdf", pdfBuf);
        }

        public static string GetTemplate(string file)
        {
            string outputstring = File.ReadAllText(file);
            XDocument xmDocument = XDocument.Parse(outputstring);
            IEnumerable<string> cssUrlQuery = from node in xmDocument.Nodes()
                                              where node.NodeType == XmlNodeType.ProcessingInstruction
                                              select Regex.Match(((XProcessingInstruction)node).Data, "href=\"(?<url>.*?)\"").Groups["url"].Value;
            return cssUrlQuery.ElementAt(0);
        }
    }
}
