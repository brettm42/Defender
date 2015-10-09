using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defender.Data
{
    public class DataBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Customized Event trigger for ViewModel.
        /// </summary>
        /// <param name="property">Name of property to raise an event for.</param>
        internal void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Fires property changed event delegate.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public bool Save(string file, string path)
        {
            File.WriteAllText(path, file, UTF8Encoding.UTF8);

            return File.Exists(path) ? true : false;
        }

        public string Open(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                throw new FileLoadException();
            }
        }
    }
}
