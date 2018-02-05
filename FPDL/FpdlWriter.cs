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
    public class FpdlWriter
    {

        /// <summary>
        /// Write an FPDL Document to the filesystem
        /// </summary>
        /// <param name="fpdlObj"></param>
        /// <param name="filename"></param>
        public void Write(IFpdlObject fpdlObj, string filename)
        {
            XElement fpdl = fpdlObj.ToFPDL();

            // Add XMLNS into the Root element
            fpdl.Add(
                new XAttribute("xmlns", "http://www.niteworks.net/fpdl"),
                new XAttribute("xmlns: xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute("xsi:schemaLocation", "http://www.niteworks.net/fpdl FPDL-ver0.3.xsd")
                );

            // Add the namespace ref to each element
            XNamespace ns = "http://www.niteworks.net/fpdl";
            foreach (var elem in fpdl.Descendants())
                elem.Name = ns.GetName(elem.Name.LocalName);

            using (FileStream stream = new FileStream(filename, FileMode.Create))
            using (XmlWriter writer = XmlWriter.Create(stream))
            {
                XDocument fpdlDoc = fpdl.Document;
                fpdlDoc.WriteTo(writer);
            }
        }
    }
}
