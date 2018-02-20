using FPDL.Common;
using System;
using System.Xml.Linq;

namespace FPDL.Pattern
{
    /// <summary>
    /// Library entry
    /// </summary>
    public class Entry
    {
        // Type, GUID, Name, Version, Filename
        /// <summary>
        /// Embedded pattern = true
        /// </summary>
        public bool Embedded;
        /// <summary>
        /// Pattern type
        /// </summary>
        public Enums.PatternType Type { get; set; }
        /// <summary>
        /// Pattern reference
        /// </summary>
        public Guid Reference { get; set; }
        /// <summary>
        /// Pattern name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Pattern version
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Pattern filename (optional)
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// Pattern object (optional)
        /// </summary>
        public PatternObject Pattern { get; set; }
        /// <summary>
        /// Construct Entry object
        /// </summary>
        public Entry()
        {
            Embedded = true;
            Pattern = null;
        }
    }
}
