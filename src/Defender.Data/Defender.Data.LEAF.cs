namespace Defender.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Defender.Model;
    using Defender.Model.Extensions;
    using static Defender.Data.Constants;

    public class Leaf : DataBase
    {                
        public string LeafLocation { get; set; } = DefaultLeafLocation;

        public string CurrentFile { get; set; }

        public bool ProcessComplete { get; set; }

        public string ProcessOutput { get; set; } = string.Empty;

        public string ProcessErrors { get; set; } = string.Empty;

        public int LeafProgress { get; set; } = 0;

        public string[] FoundFiles { get; set; }

        public bool LeafFolderQuery(string path, string workingdir = @".\", string outputxml = @".\_temp.xml", string plugin = @"/SERVICEPROVIDERS  LocVer", string leaf = DefaultLeafLocation)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                IEnumerable<string> filenames = Directory.EnumerateFiles(path, "*.rqf", SearchOption.AllDirectories);

                if (filenames.Any())
                {
                    this.ProcessErrors = string.Empty;
                    this.ProcessOutput = string.Empty;

                    ProcessStartInfo processinfo = new ProcessStartInfo
                                                   {
                                                       FileName = leaf,
                                                       UseShellExecute = true,
                                                       //UseShellExecute = false,
                                                       //WindowStyle = ProcessWindowStyle.Hidden,
                                                       //RedirectStandardError = true,
                                                       //RedirectStandardOutput = true,
                                                       ErrorDialog = true,
                                                       WorkingDirectory = workingdir,
                                                       Arguments = new StringBuilder()
                                                                       .Append("Run Automation OpenFile /FILENAMES ")
                                                                       .AppendSequence(
                                                                           filenames,
                                                                           (sb, file) => sb.AppendFormat("{0};", file))
                                                                       .AppendFormat(" Validate /OUTPUTPATH {0} /RETURN Error Validate /SERVICEPROVIDERS LocVer /OUTPUTPATH {0} /RETURN Error", outputxml)
                                                                       .ToString(),
                                                   };

                    processinfo.Arguments = $"Run Automation OpenFile /FILENAMES {path} Validate /SERVICEPROVIDERS LocVer /OUTPUTPATH {outputxml} /RETURN Error";

                    DateTime start = DateTime.Now;

                    try
                    {
                        using (Process process = Process.Start(processinfo))
                        {
                            process?.WaitForExit();

                            var temp = process?.ExitCode;
                            
                            //using (StreamReader _reader = process.StandardOutput) this.ProcessOutput = _reader.ReadToEnd();
                            //using (StreamReader _reader = process.StandardError)  this.ProcessErrors = _reader.ReadToEnd();

                            DateTime end = DateTime.Now;

                            return File.Exists(outputxml)
                                   ? string.IsNullOrWhiteSpace(this.ProcessErrors)
                                   : false;
                        }
                    }
                    catch
                    {
                        throw new Exception($"Failed to launch {DefaultLeafExe}");
                    }
                }

                return true;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        
        public void LeafFileQuery(string path, string workingdir = @".\", string outputxml = @"_temp.xml", string plugin = @"/SERVICEPROVIDERS  LocVer", string leaf = DefaultLeafLocation)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                IEnumerable<string> filenames = Directory.EnumerateFiles(path, "*.rqf", SearchOption.AllDirectories);

                if (filenames.Any())
                {
                    this.ProcessErrors = string.Empty;
                    this.ProcessOutput = string.Empty;

                    foreach (string filename in filenames)
                    {
                        this.CurrentFile = Path.GetFileName(filename);

                        ProcessStartInfo processinfo = new ProcessStartInfo
                        {
                            FileName = leaf,
                            UseShellExecute = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            WorkingDirectory = workingdir,
                            Arguments = $"Run Automation OpenFile /FILENAMES {Path.GetFullPath(filename)} Validate /SERVICEPROVIDERS LocVer /OUTPUTPATH {Path.Combine(workingdir, Path.GetFileName(filename))}.xml /RETURN Error",
                        };

                        DateTime start = DateTime.Now;

                        try
                        {
                            using (Process process = Process.Start(processinfo))
                            {
                                process?.WaitForExit();

                                DateTime end = DateTime.Now;

                                //if (!File.Exists($"{Path.GetFileNameWithoutExtension(filename)}.xml")) break;
                            }
                        }
                        catch
                        {
                            throw new Exception($"Failed to launch {DefaultLeafExe}");
                        }
                    }

                    this.ProcessComplete = true;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task LeafFileQueryAsync(IProgress<int> progress, string path, string workingdir = @".\", string outputxml = @"_temp.xml", string plugin = @"/SERVICEPROVIDERS  LocVer", string leaf = DefaultLeafLocation)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                IEnumerable<string> filenames = Directory.EnumerateFiles(path, "*.rqf", SearchOption.AllDirectories);
                this.FoundFiles = filenames.ToArray();

                if (filenames.Any())
                {
                    this.ProcessErrors = string.Empty;
                    this.ProcessOutput = string.Empty;

                    await Task.Run(
                        () =>
                        {
                            foreach (string filename in filenames)
                            {
                                this.CurrentFile = Path.GetFileName(filename);
                                this.LeafProgress = this.LeafProgress + 100 / filenames.Count();

                                progress.Report(this.LeafProgress + 100 / filenames.Count());

                                ProcessStartInfo processinfo = new ProcessStartInfo
                                                               {
                                                                   FileName = leaf,
                                                                   UseShellExecute = false,
                                                                   //UseShellExecute = true,
                                                                   WindowStyle = ProcessWindowStyle.Hidden,
                                                                   RedirectStandardOutput = true,
                                                                   WorkingDirectory = workingdir,
                                                                   Arguments = $"Run Automation OpenFile /FILENAMES {Path.GetFullPath(filename)} Validate /SERVICEPROVIDERS LocVer /OUTPUTPATH {Path.Combine(workingdir, Path.GetFileName(filename))}.xml /RETURN Error",
                                                               };

                                DateTime start = DateTime.Now;

                                try
                                {
                                    using (Process process = Process.Start(processinfo))
                                    {
                                        process?.WaitForExit();

                                        using (StreamReader reader = process?.StandardOutput) this.ProcessOutput = reader?.ReadToEnd();
                                        //using (StreamReader reader = process?.StandardError)  this.ProcessErrors = reader?.ReadToEnd();

                                        DateTime end = DateTime.Now;

                                        // TODO: check if file exists; if not, populate data with query rqf name but leave stats blank
                                        if (!File.Exists($"{Path.Combine(workingdir, Path.GetFileName(filename))}.xml"))
                                        {
                                            this.ProcessErrors += $"{Path.Combine(workingdir, Path.GetFileName(filename))}.xml not found!";

                                            File.WriteAllText($"{Path.Combine(workingdir, Path.GetFileName(filename))}.xml", "File not found");
                                        }
                                    }
                                }
                                catch
                                {
                                    throw new Exception($"Failed to launch {DefaultLeafExe}");
                                }
                            }
                        });

                    this.ProcessComplete = true;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        internal string FindLeaf(string inPath)
        {
            // if path is simply missing the .exe name
            if (Directory.GetFiles(inPath, DefaultLeafExe, SearchOption.AllDirectories).Any())
            {
                return Path.Combine(inPath, DefaultLeafExe);
            }
            else
            {
                // recursively runs a directory up each pass until .exe found
                // TODO: catch full drive searches, eg. C:\
                return FindLeaf(
                           Directory.GetFiles(
                               Directory.GetParent(inPath).FullName, 
                               DefaultLeafExe, 
                               SearchOption.AllDirectories)
                           .FirstOrDefault());
            }
        }

        public Leaf(string leafpath = DefaultLeafLocation)
        {
            try
            {
                this.LeafLocation = File.Exists(leafpath) && leafpath.EndsWith(DefaultLeafExe)
                                    ? leafpath : this.FindLeaf(leafpath);
            }
            catch
            {
                // TODO: handle .exe not found more gracefully
                throw new FileNotFoundException($"Leaf could not be found at {leafpath}.");
            }
        }
    }
}
