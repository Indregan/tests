using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Xsl;

using System.Web.Mvc;

namespace ProductsAPI.MedalHTMLHelper
{
    public static class MedalHTMLHelper
    {
        public static HtmlString RenderXMLData(string xml, string xsltPath)
        {
            XsltArgumentList args = new XsltArgumentList();
            // Create XslCompiledTransform object to loads and compile XSLT file.  
            XslCompiledTransform tranformObj = new XslCompiledTransform();
            tranformObj.Load(xsltPath);

            // Create XMLReaderSetting object to assign DtdProcessing, Validation type  
            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.DtdProcessing = DtdProcessing.Parse;
            xmlSettings.ValidationType = ValidationType.DTD;

            // Create XMLReader object to Transform xml value with XSLT setting   
            using (XmlReader reader = XmlReader.Create(new StringReader(xml), xmlSettings))
            {
                StringWriter writer = new StringWriter();
                tranformObj.Transform(reader, args, writer);

                // Generate HTML string from StringWriter  
                HtmlString htmlString = new HtmlString(writer.ToString());
                return htmlString;
            }
        }
    }
}
