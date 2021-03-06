﻿namespace Defender.Infrastructure
{
    using System.IO;
    using System.ComponentModel;
    using System.Text;

    public class InfrastructureBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Fires property changed event delegate.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Customized Event trigger for ViewModel.
        /// </summary>
        /// <param name="property">Name of property to raise an event for.</param>
        public void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        
        public bool Save(string file, string path)
        {
            if (!string.IsNullOrWhiteSpace(path) && Path.HasExtension(path) && !string.IsNullOrWhiteSpace(file))
            {
                File.WriteAllText(path, file, UTF8Encoding.UTF8);

                return File.Exists(path);
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
