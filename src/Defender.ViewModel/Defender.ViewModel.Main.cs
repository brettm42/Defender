namespace Defender.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using Microsoft.FSharp;
    using Microsoft.Win32;
    using Defender.Data;
    using Defender.Model;
    using Defender.Model.Extensions;

    public class ViewModel : ViewModelBase
    {
        public ICommand ToggleExecute
        {
            get
            {
                return _toggleexecute;
            }
            set
            {
                _toggleexecute = value;
            }
        }
        private ICommand _toggleexecute { get; set; }
        
        public bool CanExecute
        {
            get
            {
                return _canexecute;
            }
            set
            {
                if (_canexecute == value) return;

                _canexecute = value;
                RaisePropertyChanged(nameof(CanExecute));
            }
        }
        private bool _canexecute = true;

        public ICommand SaveCommand { get; set; }


        public string LeafPath { get; set; }

        public string Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _folder = Path.GetDirectoryName(value);
                    RaisePropertyChanged(nameof(Folder));
                }
            }
        }
        private string _folder = string.Empty;

        public bool Success
        {
            get
            {
                return _success;
            }
            set
            {
                _success = value;
                RaisePropertyChanged(nameof(Success));
            }
        }
        private bool _success = true;

        public int Progress
        {
            get
            {
                return _progr;
            }
            set
            {
                if (Enumerable.Range(0, 101).Contains(value))
                {
                    _progr = value;
                    RaisePropertyChanged(nameof(Progress));
                }
            }
        }
        private int _progr = 0;

        public string CurrentFile
        {
            get
            {
                return _curfile;
            }
            set
            {
                _curfile = value;
                RaisePropertyChanged(nameof(CurrentFile));
            }
        }
        private string _curfile;

        public string[] Gameareas
        {
            get
            {
                return _gameareas;
            }
            set
            {
                _gameareas = value;
                RaisePropertyChanged(nameof(Gameareas));
            }
        }
        private string[] _gameareas;

        public string[] Languages
        {
            get
            {
                return _langs;
            }
            set
            {
                _langs = value;
                RaisePropertyChanged(nameof(Languages));
            }
        }
        private string[] _langs;

        public string[] FileList
        {
            get
            {
                return _filelist;
            }
            set
            {
                _filelist = value;
                RaisePropertyChanged(nameof(FileList));
            }
        }
        private string[] _filelist;

        public ObservableCollection<DataItem> Statistics
        {
            get
            {
                return _stats;
            }
            set
            {
                _stats = value;
                RaisePropertyChanged(nameof(Statistics));

                UpdateStringLists(_stats);

                this.Success = AnyErrors(_stats);
            }
        }
        private ObservableCollection<DataItem> _stats;
        
        public string Errors
        {
            get
            {
                return _err;
            }
            set
            {
                _err = value;
                RaisePropertyChanged(nameof(Errors));
            }
        }
        private string _err;

        public string Output
        {
            get
            {
                return _out;
            }
            set
            {
                _out = value;
                RaisePropertyChanged(nameof(Output));
            }
        }
        private string _out;


        public void RunQueries()
        {
            var _leaf = new Leaf();

            this.Progress = _leaf.LeafProgress;
            this.Output = _leaf.ProcessOutput;
            this.Errors = _leaf.ProcessErrors;

            this.Success = _leaf.LeafQuery(this.Folder);

            //this.Success = (_leaf.ProcessErrors.Any()) ? true : false;
        }

        public void ValidateFiles()
        {
            using (Validate _validation = new Validate())
            {
                this.Progress    = _validation.CurrentProgress;
                this.CurrentFile = _validation.CurrentFile;

                this.Statistics  = _validation.Validation(Folder);
            }

            this.Success = AnyErrors(this.Statistics);
        }

        private void UpdateStringLists(ObservableCollection<DataItem> results)
        {
            FileList  = results.Select(l => l.Name).Distinct().ToArray();
            Languages = results.Select(l => l.Language).Distinct().ToArray();
            Gameareas = results.Select(l => l.Folder).Distinct().ToArray();
        }

        internal bool AnyErrors(ObservableCollection<DataItem> results)
        {
            return (_stats.Where(l => l.Errors != 0).Any()) ? false : true;
        }

        public bool ExportResults(string path)
        {
            if (!string.IsNullOrWhiteSpace(path) && this.FileList.Any())
            {
                Serializer writer = new Serializer(this.Statistics);

                return writer.Save(writer.SerialiseToString(), path);
            }
            else
            {
                return false;
            }
        }

        internal void SaveResults()
        {
            SaveFileDialog savefile = new SaveFileDialog()
                                      {
                                          Title = "Save Handback file as...",
                                          Filter = "Handback file (*.hback)|*.hback|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                                          FileName = $"{this.Folder}.hback",
                                          AddExtension = true,
                                      };

            this.ExportResults(
                (savefile.ShowDialog() == true)
                ? savefile.FileName
                : null);
        }

        public bool ImportResults(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                this.Folder = path;
                this.CurrentFile = Path.GetFileName(path);

                Serializer reader = new Serializer(this.Statistics);

                this.Statistics = reader.DeserialiseFromString(reader.Open(path));

                return (this.Statistics != null) ? true : false;
            }
            else
            {
                return this.Success = false;
            }
        }


        public ViewModel()
        {
            //SaveCommand = new RelayCommand(() => SaveResults, e => CanExecute);
        }
    }
}
