using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FSharp;
using Defender.Model.Extensions;

namespace Defender.Model
{
    public class LEAF : ModelBase
    {
        private const string DefaultLeafExe = @"MSLeaf.exe";
        private const string DefaultLeafLocation = @"C:\Program Files (x86)\Microsoft LEAF\" + DefaultLeafExe;
                
        public string LeafLocation { get; set; } = DefaultLeafLocation;

        public string LeafOutput { get; set; }

        public string LeafErrors { get; set; }

        public int LeafProgress { get; set; } = 0;
        
        public bool LeafCommand(string[] filenames, string working_dir, string output_dir, string plugin = "Validate", string leaf = DefaultLeafLocation)
        {
            this.LeafErrors = string.Empty;
            this.LeafOutput = string.Empty;

            ProcessStartInfo process = new ProcessStartInfo()
                                       {
                                           FileName = leaf,
                                           UseShellExecute = false,
                                           RedirectStandardError = true,
                                           RedirectStandardOutput = true,
                                           CreateNoWindow = true,
                                           WorkingDirectory = working_dir
                                       };

            process.Arguments = new StringBuilder().Append("Run Automation OpenFile /FILENAMES")
                                                   .AppendSequence(
                                                       filenames,
                                                       (sb, file) => sb.AppendFormat("{0};", file))
                                                   .AppendFormat("Validate /OUTPUTPATH {0} /RETURN Error", output_dir)
                                                   .ToString();

            

            return true;  // true for success
        }
        
        internal string FindLeaf(string in_path)
        {
            // if path is simply missing the .exe name
            if (Directory.GetFiles(in_path, DefaultLeafExe, SearchOption.AllDirectories).Any())
            {
                return Path.Combine(in_path, DefaultLeafExe);
            }
            else
            {
                // TODO: LEAF location searcher
                
                return DefaultLeafLocation;
            }
        }

        public LEAF(string leaf_path = DefaultLeafLocation)
        {
            this.LeafLocation = (File.Exists(leaf_path) && leaf_path.EndsWith(DefaultLeafExe))
                                ? leaf_path
                                : this.FindLeaf(leaf_path);
        }
    }
}
