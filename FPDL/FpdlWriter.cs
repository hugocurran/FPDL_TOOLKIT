using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FPDL
{
    /// <summary>
    /// Write FPDL documents to the filesystem
    /// </summary>
    public static class FpdlWriter
    {

        /// <summary>
        /// Write an FPDL Document to the filesystem
        /// </summary>
        /// <param name="fpdl"></param>
        /// <param name="filename"></param>
        public static void Write(XElement fpdl, string filename)
        {
            //// Add XMLNS into the Root element
            //fpdl.Add(
            //    new XAttribute("xmlns", "http://www.niteworks.net/fpdl"),
            //    new XAttribute("xmlns: xsi", "http://www.w3.org/2001/XMLSchema-instance"),
            //    new XAttribute("xsi:schemaLocation", "http://www.niteworks.net/fpdl FPDL-ver0.3.xsd")
            //    );

            //XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            //XNamespace xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
            //XNamespace ns = XNamespace.Get("http://www.niteworks.net/fpdl");

            ////XElement firstnode = (XElement)fpdl.FirstNode;

            //XElement foo = fpdl;

            //fpdl.Add(new XAttribute("xmlns", ns.NamespaceName),
            //        new XAttribute(XNamespace.Xmlns + "xsd", xsd.NamespaceName),
            //        new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName)
            //        );


            //// Add the namespace ref to each element
            ////XNamespace ns = "http://www.niteworks.net/fpdl";
            ////fpdl.Add(new XAttribute(XNamespace.Xmlns, "http://www.niteworks.net/fpdl"));
            ////foreach (var elem in fpdl.Descendants())
            ////    elem.Name = ns.GetName(elem.Name.LocalName);

            XDocument doc = new XDocument(fpdl);
            doc.Save(filename + ".xml");

            //using (FileStream stream = new FileStream(filename, FileMode.Create))
            //using (XmlWriter writer = XmlWriter.Create(stream))
            //{
            //    XDocument fpdlDoc = fpdl.Document;
            //    fpdlDoc.WriteTo(writer);
            //}
        }

        /// <summary>
        /// Write an FPDL Document to the filesystem
        /// </summary>
        /// <param name="fpdlObj"></param>
        /// <param name="filename"></param>
        public static void Write(IFpdlObject fpdlObj, string filename)
        {
            Write(fpdlObj.ToFPDL(), filename);
        }
    }
}
