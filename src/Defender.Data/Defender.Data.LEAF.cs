namespace Defender.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.FSharp;
    using Defender.Model;
    using Defender.Model.Extensions;

    public class Leaf : DataBase
    {
        private const string DefaultLeafExe = @"MSLeaf.exe";
        private const string DefaultLeafDir = @"Microsoft LEAF";
        private const string DefaultLeafLocation = @"C:\Program Files (x86)\" + DefaultLeafDir + @"\" + DefaultLeafExe;
                
        public string LeafLocation { get; set; } = DefaultLeafLocation;

        public string ProcessOutput { get; set; }

        public string ProcessErrors { get; set; }

        public int LeafProgress { get; set; } = 0;

        public bool LeafQuery(string path, string workingdir = @"\", string outputdir = @"_temp", string plugin = "Validate", string leaf = DefaultLeafLocation)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                IEnumerable<string> filenames = Directory.EnumerateFiles(path, "*.rqf", SearchOption.AllDirectories);

                if (filenames.Any())
                {
                    this.ProcessErrors = string.Empty;
                    this.ProcessOutput = string.Empty;

                    ProcessStartInfo processinfo = new ProcessStartInfo()
                    {
                        FileName = leaf,
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        WorkingDirectory = workingdir,
                        Arguments = new StringBuilder()
                                        .Append("Run Automation OpenFile /FILENAMES ")
                                        .AppendSequence(
                                            filenames,
                                            (sb, file) => sb.AppendFormat("{0};", file))
                                        .AppendFormat(" Validate /OUTPUTPATH {0} /RETURN Error Validate /SERVICEPROVIDERS LocVer /OUTPUTPATH {0} /RETURN Error", outputdir)
                                        .ToString(),
                    };

                    processinfo.Arguments = $"Run Automation OpenFile /FILENAMES {path} Validate /OUTPUTPATH {outputdir} /RETURN Error Validate /SERVICEPROVIDERS LocVer /OUTPUTPATH {outputdir} /RETURN Error";

                    try
                    {
                        using (Process process = Process.Start(processinfo))
                        {
                            process.WaitForExit(9000);
                            
                            using (StreamReader _reader = process.StandardOutput) this.ProcessOutput = _reader.ReadToEnd();
                            using (StreamReader _reader = process.StandardError)  this.ProcessErrors = _reader.ReadToEnd();

                            return string.IsNullOrWhiteSpace(this.ProcessErrors) ? true : false;
                        }
                    }
                    catch
                    {
                        throw new Exception($"Failed to launch {DefaultLeafExe}");
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
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
                // recursively runs a directory up each pass until .exe found
                // TODO: catch full drive searches, eg. C:\
                return FindLeaf(
                           Directory.GetFiles(
                               Directory.GetParent(in_path).FullName, 
                               DefaultLeafExe, 
                               SearchOption.AllDirectories)
                           .FirstOrDefault());
            }
        }

        public Leaf(string leafpath = DefaultLeafLocation)
        {
            this.LeafLocation = (File.Exists(leafpath) && leafpath.EndsWith(DefaultLeafExe))
                                ? leafpath
                                : this.FindLeaf(leafpath);
        }
    }
}
