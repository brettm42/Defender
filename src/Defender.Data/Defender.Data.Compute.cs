using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defender.Data
{
    public class Compute : IDisposable
    {
        public object XMLStatReader(string path, object outtable, bool deletefiles)
        {
            // read temp directory XMLs and calculate statistics

            return null;
        }

        //TODO: implement smart disposal of temp folders and xml stat files
        void IDisposable.Dispose() 
        {
        }
    }
}
