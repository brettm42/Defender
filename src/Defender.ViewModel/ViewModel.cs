namespace Defender.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Microsoft.Win32;
    using Defender.Infrastructure;
    using Defender.Infrastructure.Extensions;
    using static Defender.ViewModel.Constants;

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

        //public Progress<int> Progress
        //{
        //    get
        //    {
        //        return _prog;
        //    }
        //    set
        //    {
        //        _prog = value;
        //        RaisePropertyChanged(nameof(Progress));
        //    }
        //}
        //private Progress<int> _prog;

        public int Progress
        {
            get
            {
                return _progr;
            }
            set
            {
                _progr = value;
                RaisePropertyChanged(nameof(Progress));

                //ProgressChanged(this, new ProgressChangedEventArgs(value, value));
            }
        }
        private int _progr = 0;

        //public event ProgressChangedEventHandler ProgressChanged;

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

        public string[] Projects
        {
            get
            {
                return _proj;
            }
            set
            {
                _proj = value;
                RaisePropertyChanged(nameof(Projects));
            }
        }
        private string[] _proj;

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

        public string[] FoundFiles
        {
            get
            {
                return _ffiles;
            }
            set
            {
                _ffiles = value;
                RaisePropertyChanged(nameof(FoundFiles));
            }
        }
        private string[] _ffiles;

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

        public async Task RunQueriesAsync()
        {
            var leaf = new Leaf();
            var prog = new Progress<int>(p => this.Progress = p);

            this.Output   = leaf.ProcessOutput;
            this.Errors   = leaf.ProcessErrors;
            this.CurrentFile = leaf.CurrentFile;
            this.FoundFiles = leaf.FoundFiles;
            
            await leaf.LeafFileQueryAsync(prog, this.Folder, this.Folder);
            //this.Success  = leaf.LeafQuery(this.Folder, this.Folder, tempxml);

            this.Success = leaf.ProcessErrors.Any();

            this.Output = leaf.ProcessOutput;
            this.Errors = leaf.ProcessErrors;
        }

        public void ValidateFiles()
        {
            using (Validate validation = new Validate())
            {
                this.Progress    = validation.CurrentProgress;
                this.CurrentFile = validation.CurrentFile;
                this.Statistics  = validation.Validation(this.Folder);
            }

            this.Success = AnyErrors(this.Statistics);
        }

        private void UpdateStringLists(ObservableCollection<DataItem> results)
        {
            this.FileList  = results?.Select(l => l.Name).Distinct().ToArray();
            this.Projects  = results?.Select(l => l.Project).Distinct().ToArray();
            this.Gameareas = results?.Select(l => l.Folder).Distinct().ToArray();
            this.Languages = results?.Select(l => l.Language).Distinct().ToArray();
        }

        internal bool AnyErrors(ObservableCollection<DataItem> results) => results?.Any(l => l.Errors != 0) ?? false;
        
        public bool ExportResults(string path)
        {
            if (!string.IsNullOrWhiteSpace(path) && (this.FileList?.Any() ?? false))
            {
                Serializer writer = new Serializer(this.Statistics);

                return writer.Save(writer.SerialiseToString(), path);
            }

            return false;
        }

        public void SaveResults()
        {
            SaveFileDialog savefile = new SaveFileDialog
                                      {
                                          Title = "Save Handback file as...",
                                          Filter = "Handback file (*.hback)|*.hback|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                                          FileName = $"{this.Folder}.hback",
                                          AddExtension = true,
                                      };

            this.ExportResults(savefile.ShowDialog() == true ? savefile.FileName : null);
        }

        public bool ImportResults(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                this.Folder = path;
                this.CurrentFile = Path.GetFileName(path);

                Serializer reader = new Serializer(this.Statistics);

                this.Statistics = reader.DeserialiseFromString(reader.Open(path));

                return this.Statistics != null;
            }

            return this.Success = false;
        }

        //public ViewModel()
        //{
        //    SaveCommand = new RelayCommand(() => SaveResults, e => CanExecute);
        //}
    }
}
