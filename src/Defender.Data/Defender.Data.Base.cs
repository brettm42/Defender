using System;
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
    }
}
