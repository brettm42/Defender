using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FSharp;
using Defender.Data;
using Defender.Model;
using Defender.Model.Extensions;

namespace Defender.ViewModel
{
    public class ViewModel : ViewModelBase
    {
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
        private bool _success = false;

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

        public string LeafPath { get; set; }

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

                FileList  = _stats.Select(l => l.ItemName).Distinct().ToArray();
                Languages = _stats.Select(l => l.Language).Distinct().ToArray();
                Gameareas = _stats.Select(l => l.Folder).Distinct().ToArray();

                this.Success = (_stats.Where(l => l.Errors != 0).Any()) ? false : true;
            }
        }
        private ObservableCollection<DataItem> _stats;

        public void ValidateFiles()
        {
            using (Validate _validation = new Validate())
            {
                this.Statistics  = _validation.Validation(Folder);
                this.Progress    = _validation.CurrentProgress;
                this.CurrentFile = _validation.CurrentFile;
            }
            
            this.Success = (this.Statistics.Where(l => l.Errors != 0).Any()) ? false : true;
        }

        public bool ExportResults(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                Serializer writer = new Serializer(this.Statistics);

                return this.Success = writer.Save(writer.SerialiseToString(), path);
            }
            else
            {
                return false;
            }
        }

        public bool ImportResults(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                Serializer reader = new Serializer(this.Statistics);

                this.Statistics = reader.DeserialiseFromString(reader.Open(path));

                return (this.Statistics != null) ? true : false;
            }
            else
            {
                return false;
            }
        }
    }
}
