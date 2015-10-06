using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Defender.Model;

namespace Defender.Data
{
    public class Compute : IDisposable
    {
        public ObservableCollection<DataItem> XMLStatReader(string path, object outtable, bool deletefiles)
        {
            // read temp directory XMLs and calculate statistics

            return null;
        }

        //TODO: implement smart disposal of temp folders and xml stat files
        public void Dispose()
        {
            //if (Directory.Exists(_tempfolder)) Directory.Delete(_tempfolder, true);
        }
    }
}
