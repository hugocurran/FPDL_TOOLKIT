using FPDL.Pattern;
using System;
using System.Linq;
using System.Xml.Linq;

namespace FPDL
{
    /// <summary>
    /// FPDL Pattern Parser
    /// </summary>
    public class PatternParser
    {
        /// <summary>
        /// Create a PatternObject from an FPDL Pattern document
        /// </summary>
        /// <param name="fpdlDoc"></param>
        /// <returns></returns>
        public static PatternObject Load(XDocument fpdlDoc)
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
            if (fpdlDoc.Element("Pattern") == null)
                throw new ApplicationException("Cannot parse: Not an FPDL Pattern file");

            return new PatternObject(fpdlDoc.Element("Pattern"));
        }
        /// <summary>
        /// Create a PatternLibrary from a FPDL Pattern Library document
        /// </summary>
        /// <param name="fpdlDoc"></param>
        /// <returns></returns>
        public static PatternLibrary LoadLibrary(XDocument fpdlDoc)
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
            if (fpdlDoc.Element("PatternLibrary") == null)
                throw new ApplicationException("Cannot parse: Not an FPDL Library Pattern file");

            return new PatternLibrary(fpdlDoc.Element("PatternLibrary"));
        }
        /// <summary>
        /// Save a PatternObject to a FPDL Pattern document
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="filename"></param>
        /// <param name="overwrite"></param>
        public static void Save(PatternObject pattern, string filename, bool overwrite = true)
        {
            throw new NotImplementedException("Save method not implemented yet");
        }
        /// <summary>
        /// Save a PatternObject to a FPDL Pattern document
        /// </summary>
        /// <param name="library"></param>
        /// <param name="filename"></param>
        /// <param name="overwrite"></param>
        public static void Save(PatternLibrary library, string filename, bool overwrite = true)
        {
            throw new NotImplementedException("Save method not implemented yet");
        }
    }
}
