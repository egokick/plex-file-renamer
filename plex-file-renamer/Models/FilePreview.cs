using System;
using System.Collections.Generic;
using System.Linq;

namespace plex_file_renamerv2.Models
{
    public class FilePreview
    {
        public string FilePath { get; set; }
        public string CurrentFileName { get; set; }
        public string PreviewFileName { get; set; }
        public List<Replacement> Replacements { get; set; } = new List<Replacement>();
    }

    public class Replacement
    {
    
        public string TextToRemove { get; set; }
        public string ReplaceWith { get; set; }
        public bool IsRegex { get; set; } = false;
        public bool Capitalise { get; set; } = false;
        public bool LowerCase { get; set; } = false;
    }
}
