namespace Defender.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Defender.Model;
    using Defender.Model.Extensions;

    public class Validate : IDisposable
    {
        internal const string Placeholder = @"-?-";

        public string CurrentFile { get; set; } = string.Empty;

        public int CurrentProgress { get; set; } = 0;

        public ObservableCollection<DataItem> Validation(string path, bool deletefiles = false)
        {
            // read temp directory XMLs and calculate statistics
            if (Directory.Exists(path) && Directory.GetFiles(path).Any())
            {
                this.CurrentProgress = 0;                
                ObservableCollection<DataItem> datagrid = new ObservableCollection<DataItem>();
                
                foreach (var file in Directory.EnumerateFiles(path, "*.xml", SearchOption.AllDirectories))
                {
                    // TODO: proper percent logic
                    this.CurrentProgress = this.CurrentProgress + 10;
                    this.CurrentFile = Path.GetFileNameWithoutExtension(file);

                    // builds DataItem to store results
                    DataItem filedata = new DataItem()
                                        {
                                            Project  = ParseFilename(Path.GetFileNameWithoutExtension(file), '_').FirstOrDefault(),
                                            Folder   = ParseFilename(Path.GetFileNameWithoutExtension(file), '_')[1],
                                            Name     = Path.GetFileNameWithoutExtension(file),
                                            Date     = DateTime.Now,
                                            _Id      = file.GetHashCode(),
                                            _User    = Environment.UserName,
                                            _Station = Environment.MachineName,
                                            _Domain  = Environment.UserDomainName,
                                        };
                    try
                    {
                        XDocument xdoc = XDocument.Load(file);
                        var read = xdoc.Descendants("Provider").Where(n => n.Element("Name").Value == "LocVer").Descendants("AutomationResult");

                        filedata.Errors   = read.Where(n => n.Element("messagetype").Value == "Error")?.Count() ?? 0;
                        filedata.Warnings = read.Where(n => n.Element("messagetype").Value == "Warning")?.Count() ?? 0;
                        filedata.Language = read.Descendants("culture").FirstOrDefault()?.Value
                                                ?? ParseFilename(Path.GetFileNameWithoutExtension(file), '_').LastOrDefault();

                        datagrid.Add(filedata);
                    }
                    catch
                    {
                        throw new FileLoadException();
                    }

                    // removes file after processing
                    if (deletefiles) File.Delete(path);
                }

                this.CurrentProgress = 99;

                return datagrid;
            }

            throw new FileNotFoundException();
        }

        internal string[] ParseFilename(string filename, char delim)
        {
            if (!string.IsNullOrWhiteSpace(filename))
            {
                string[] path   = Path.GetFileNameWithoutExtension(filename).Split(delim);
                string[] chunks = new string[3];

                chunks[0] = path[0];
                chunks[2] = path[path.Length - 1];

                for (int i = 1; i < path.Length - 1; i++)
                {
                    chunks[1] = (chunks[1] + delim + path[i]).TrimStart(delim);
                }

                return chunks;
            }

            throw new ArgumentNullException();
        }

        //TODO: implement smart disposal of temp folders and xml stat files
        public void Dispose()
        {
            //if (Directory.Exists(_tempfolder)) Directory.Delete(_tempfolder, true);
        }
    }
}
