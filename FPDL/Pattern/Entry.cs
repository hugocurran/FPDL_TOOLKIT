using System;

namespace FPDL.Pattern
{
    /// <summary>
    /// Library entry
    /// </summary>
    public class Entry
    {
        // Type, GUID, Name, Version, Filename
        /// <summary>
        /// Pattern type
        /// </summary>
        public PatternObject.Type Type;
        /// <summary>
        /// Pattern reference
        /// </summary>
        public Guid Reference;
        /// <summary>
        /// Pattern name
        /// </summary>
        public string Name;
        /// <summary>
        /// Pattern version
        /// </summary>
        public string Version;
        /// <summary>
        /// Pattern filename (optional)
        /// </summary>
        public string Filename;
        /// <summary>
        /// Pattern object (optional)
        /// </summary>
        public PatternObject Pattern = null;
    }
}
