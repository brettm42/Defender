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
        public ObservableCollection<DataItem> XMLStatReader(string path, bool deletefiles = true)
        {
            // read temp directory XMLs and calculate statistics
            if (Directory.Exists(path) && Directory.GetFiles(path).Any())
            {
                foreach (var file in Directory.EnumerateFiles(path, "*.xml", SearchOption.AllDirectories))
                {
                    XDocument xdoc = XDocument.Load(file);

                }
            }

            return null;
        }

        //TODO: implement smart disposal of temp folders and xml stat files
        public void Dispose()
        {
            //if (Directory.Exists(_tempfolder)) Directory.Delete(_tempfolder, true);
        }
    }
}
