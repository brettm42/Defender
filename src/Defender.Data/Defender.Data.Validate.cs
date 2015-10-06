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

namespace Defender.Data
{
    public class Validate : IDisposable
    {
        internal const string Placeholder = @"-?-";

        public ObservableCollection<DataItem> XMLReader(string path, bool deletefiles = true)
        {
            // read temp directory XMLs and calculate statistics
            if (Directory.Exists(path) && Directory.GetFiles(path).Any())
            {
                ObservableCollection<DataItem> datagrid = new ObservableCollection<DataItem>();

                foreach (var file in Directory.EnumerateFiles(path, "*.xml", SearchOption.AllDirectories))
                {
                    // builds DataItem to store results
                    DataItem filedata = new DataItem()
                                        {
                                            Id = file.GetHashCode(),
                                            Folder = Directory.GetParent(file).Name,
                                            ItemName = Path.GetFileNameWithoutExtension(file),
                                        };
                    try
                    {
                        XDocument xdoc = XDocument.Load(file);
                        var read = xdoc.Descendants("Provider").Where(n => n.Element("Name").Value == "LocVer").Descendants("AutomationResult");

                        filedata.Errors   = read.Where(n => n.Element("messagetype").Value == "Error")?.Count() ?? 0;
                        filedata.Warnings = read.Where(n => n.Element("messagetype").Value == "Warning")?.Count() ?? 0;
                        filedata.Language = read.FirstOrDefault(n => !string.IsNullOrWhiteSpace(n.Element("culture").Value))?.Value ?? Placeholder;

                        datagrid.Add(filedata);
                    }
                    catch
                    {
                        throw new FileLoadException();
                    }
                }

                return datagrid;
            }

            throw new FileNotFoundException();
        }

        //TODO: implement smart disposal of temp folders and xml stat files
        public void Dispose()
        {
            //if (Directory.Exists(_tempfolder)) Directory.Delete(_tempfolder, true);
        }
    }
}
