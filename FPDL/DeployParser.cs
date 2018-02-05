using FPDL.Deploy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace FPDL
{
    /// <summary>
    /// Read a FPDL Deploy document
    /// </summary>
    public class DeployParser
    {
        /// <summary>
        /// Create a DeployObject from a FPDL Deploy document
        /// </summary>
        /// <param name="fpdlDoc"></param>
        /// <returns></returns>
        public static DeployObject Load(XDocument fpdlDoc)
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
            if (fpdlDoc.Element("Deploy") == null)
                throw new ApplicationException("Cannot parse: Not an FPDL Deploy file");

            return new DeployObject(fpdlDoc.Element("Deploy"));
        }

        /// <summary>
        /// Save a DeployObject to a FPDL Deploy document
        /// </summary>
        /// <param name="deploy"></param>
        /// <param name="filename"></param>
        /// <param name="overwrite"></param>
        public static void Save(DeployObject deploy, string filename, bool overwrite = true)
        {
            throw new NotImplementedException("Save method not implemented yet");
        }
    }
}
