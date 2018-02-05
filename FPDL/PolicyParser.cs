using FPDL.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPDL
{
    /// <summary>
    /// Read a FPDL Policy document and creata a PolicyObject
    /// </summary>
    class PolicyParser
    {
        /// <summary>
        /// Create a DesignObject from a FPDL Deploy document
        /// </summary>
        /// <param name="fpdlDoc"></param>
        /// <returns></returns>
        public static PolicyObject Load(XDocument fpdlDoc)
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
            if (fpdlDoc.Element("Policy") == null)
                throw new ApplicationException("Cannot parse: Not an FPDL Policyn file");

            return new PolicyObject(fpdlDoc.Element("Policy"));
        }

        /// <summary>
        /// Save a PolicyObject to a FPDL Deploy document
        /// </summary>
        /// <param name="deploy"></param>
        /// <param name="filename"></param>
        /// <param name="overwrite"></param>
        public static void Save(PolicyObject deploy, string filename, bool overwrite = true)
        {
            throw new NotImplementedException("Save method not implemented yet");
        }
    }
}
    
