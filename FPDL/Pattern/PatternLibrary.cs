using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FPDL.Pattern
{
    /// <summary>
    /// Pattern Library
    /// </summary>
    public class PatternLibrary
    {
        // Type, GUID, Name, Version, Filename
        /// <summary>
        /// ConfigMgmt object for Library document
        /// </summary>
        public ConfigMgmt ConfigMgmt;
        /// <summary>
        /// Library
        /// </summary>
        public IReadOnlyList<Entry> Library { get { return _library; } }

        private List<Entry> _library = new List<Entry>();
        private bool initialised = false;
        /// <summary>
        /// Construct a Library object
        /// </summary>
        public PatternLibrary()
        {
            initialised = false;
        }
        /// <summary>
        /// Construct a Library object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public PatternLibrary(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Library object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "PatternLibrary")
                throw new ApplicationException("Cannot parse: Not an FPDL pattern library");
            try
            {
                ConfigMgmt = new ConfigMgmt(fpdl.Descendants("configMgmt").FirstOrDefault());
                foreach (XElement _entry in fpdl.Elements("entry"))
                {
                    Entry entry = new Entry
                    {
                        Type = (PatternObject.Type)Enum.Parse(typeof(PatternObject.Type), fpdl.Element("patternGenericType").Value),
                        Reference = Guid.Parse(fpdl.Element("patternReference").Value),
                        Name = fpdl.Element("patternName").Value,
                        Version = fpdl.Element("patternVersion").Value,
                        Filename = fpdl.Element("fileName").Value
                    };
                    _library.Add(entry);
                }
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("Invalid pattern type: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Library parse error: " + e.Message);
            }
            initialised = true;
        }
        /// <summary>
        /// Initialise a Library object
        /// </summary>
        /// <param name="author"></param>
        public void Initialise(string author)
        {
            if (initialised)
                throw new ApplicationException("Attempt to initialise an initialised pattern library");

            ConfigMgmt.Initialise(author, 1, 0, "Initial Version");
            initialised = true;
        }
        /// <summary>
        /// Add a Pattern to the Library
        /// </summary>
        /// <remarks>Store the Pattern in the library</remarks>
        /// <param name="pattern"></param>
        internal void Add(PatternObject pattern)
        {
            if (initialised)
            {
                var entry = new Entry
                {
                    Type = pattern.PatternType,
                    Reference = pattern.ConfigMgmt.DocReference,
                    Name = pattern.PatternName,
                    Version = ConfigMgmt.VersionToString(pattern.ConfigMgmt.CurrentVersion),
                    Pattern = pattern
                };
                _library.Add(entry);
            }
            else
                throw new ApplicationException("Attempt to update unitialised pattern library");
        }
        /// <summary>
        /// Add a Pattern to the Library
        /// </summary>
        /// <remarks>Store the pattern in a file</remarks>
        /// <param name="pattern"></param>
        /// <param name="filename"></param>
        internal void Add(PatternObject pattern, string filename)
        {
            if (initialised)
            {
                if (filename == "")
                    filename = @"Patterns\" + pattern.ConfigMgmt.DocReference.ToString() + ".xml";
                var entry = new Entry
                {
                    Type = pattern.PatternType,
                    Reference = pattern.ConfigMgmt.DocReference,
                    Name = pattern.PatternName,
                    Version = ConfigMgmt.VersionToString(pattern.ConfigMgmt.CurrentVersion),
                    Filename = filename
                };
                _library.Add(entry);
            }
            else
                throw new ApplicationException("Attempt to update unitialised pattern library");
        }
        /// <summary>
        /// Add a Pattern to the Library
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="author"></param>
        /// <param name="changeNotes"></param>
        /// <param name="updateMajor"></param>
        /// <param name="updateMinor"></param>
        public void Add(PatternObject pattern, string author, string changeNotes, bool updateMajor = false, bool updateMinor = true)
        {
            Add(pattern);

            int major = ConfigMgmt.CurrentVersion.Item1;
            int minor = ConfigMgmt.CurrentVersion.Item2;
            if (updateMajor)
            {
                major++;
                minor = 0;
            }
            if (updateMinor && !updateMajor)
            {
                minor++;
            }
            ConfigMgmt.NewVersion(author, major, minor, changeNotes);
        }

        /// <summary>
        /// Remove a Pattern from the Library
        /// </summary>
        /// <param name="pattern"></param>
        internal void Remove(PatternObject pattern)
        {
            Remove(pattern.ConfigMgmt.DocReference);
        }
        /// <summary>
        /// Remove a Pattern from the Library
        /// </summary>
        /// <param name="patternRef"></param>
        public void Remove(Guid patternRef)
        {
            if (initialised)
            {
                foreach (Entry entry in _library)
                {
                    if (entry.Reference == patternRef)
                    {
                        _library.Remove(entry);
                        return;
                    }
                }
                throw new ApplicationException("Remove failed: Not in library: " + patternRef.ToString());
            }
            else
                throw new ApplicationException("Attempt to update unitialised pattern library");
        }

    }
}
