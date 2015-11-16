namespace Defender.ViewModel
{
    using System.ComponentModel;
    using System.Linq;
    using Defender.Model.Extensions;

    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Fires property changed event delegate.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Customized Event trigger for ViewModel.
        /// </summary>
        /// <param name="property">Name of property to raise an event for.</param>
        internal void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
