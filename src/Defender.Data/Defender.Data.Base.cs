namespace Defender.Data
{
    using System;
    using System.IO;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

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
            if (!string.IsNullOrWhiteSpace(path) && Path.HasExtension(path) && !string.IsNullOrWhiteSpace(file))
            {
                File.WriteAllText(path, file, UTF8Encoding.UTF8);

                return File.Exists(path) ? true : false;
            }

            return false;
        }

        public string Open(string path)
        {
            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                return File.ReadAllText(path);
            }

            //throw new FileLoadException();
            return null;
        }
    }
}
