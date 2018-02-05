using FPDL.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPDL
{
    /// <summary>
    /// Read a FPDL Design document and create a DesignObject
    /// </summary>
    public class DesignParser
    {
        /// <summary>
        /// Create a DesignObject from a FPDL Deploy document
        /// </summary>
        /// <param name="fpdlDoc"></param>
        /// <returns></returns>
        public static DesignObject Load(XDocument fpdlDoc)
        {
            // We need to get rid of all of the XMLNS prefixes
            // XMLNS declarations
            fpdlDoc.Descendants()
                .Attributes()
                .Where(x => x.IsNamespaceDeclaration)
                .Remove();
            // Namespace prefixes
            foreach (var elem in fpdlDoc.Descendants())
                elem.Name = elem.Name.LocalName;

            // Now parse the FPDL file            
            if (fpdlDoc.Element("Design") == null)
                throw new ApplicationException("Cannot parse: Not an FPDL Design file");

            return new DesignObject(fpdlDoc.Element("Design"));
        }

        /// <summary>
        /// Save a DesignObject to a FPDL Design document
        /// </summary>
        /// <param name="deploy"></param>
        /// <param name="filename"></param>
        /// <param name="overwrite"></param>
        public static void Save(DesignObject deploy, string filename, bool overwrite = true)
        {
            throw new NotImplementedException("Save method not implemented yet");
        }
    }
}
