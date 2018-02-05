using FPDL.Deploy;
using FPDL.Design;
using FPDL.Pattern;
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
    /// Parser support for all FPDL documents
    /// </summary>
    public static class FpdlReader
    {
        /// <summary>
        /// Parse any FPDL document into the appropriate type
        /// </summary>
        /// <param name="filename">FPDL file Pathname</param>
        /// <returns>IFpdlObject</returns>
        public static IFpdlObject Parse(string filename)
        {
            XDocument fpdlDoc = XDocument.Load(filename);
            return Parse(fpdlDoc);
        }

        /// <summary>
        /// Parse any FPDL document into the appropriate type
        /// </summary>
        /// <param name="fpdlDoc"></param>
        /// <returns>IFpdlObject</returns>
        public static IFpdlObject Parse(XDocument fpdlDoc)
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

            // Now read the FPDL file to work out what sort it is
            switch (fpdlDoc.Root.Name.ToString())
            {
                case "Policy":
                    return new PolicyObject(fpdlDoc.Element("Policy"));
                case "Design":
                    return new DesignObject(fpdlDoc.Element("Design"));
                case "Deploy":
                    return new DeployObject(fpdlDoc.Element("Deploy"));
                case "Pattern":
                    return new PatternObject(fpdlDoc.Element("Pattern"));
                case "PatternLibrary":
                    return new PatternLibrary(fpdlDoc.Element("PatternLibrary"));
                default:
                    throw new ApplicationException("Cannot parse: Not an FPDL document");
            }
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
