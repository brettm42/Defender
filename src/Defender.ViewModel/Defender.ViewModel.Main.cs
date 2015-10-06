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
        //private int _progr = 0;
        private int _progr = 43;

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
        //private string _curfile;
        private string _curfile = "Legends_UI_fr-FR.rqf";

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
        /*private ObservableCollection<DataItem> _stats = new ObservableCollection<DataItem>()
        {
            new DataItem()
            {
                Id = 123451,
                ItemName = "Legends_UI_fr-FR.rqf",
                Language = "fr-FR",
                Folder = "UI",
                ForReview = 1,
                Errors = 17,
                NotFinal = 3
            },
            new DataItem()
            {
                Id = 12345187,
                ItemName = "Legends_UI_de-DE.rqf",
                Language = "de-DE",
                Folder = "UI",
                ForReview = 1,
                Errors = 0,
                NotFinal = 3
            },
            new DataItem()
            {
                Id = -34123451,
                ItemName = "Legends_UI_es-ES.rqf",
                Language = "es-ES",
                Folder = "UI",
                ForReview = 1,
                Errors = 1,
                NotFinal = 32
            },
            new DataItem()
            {
                Id = -678123451,
                ItemName = "Legends_VO_Script_de-DE.rqf",
                Language = "de-DE",
                Folder = "VO_Script",
                ForReview = 21,
                Errors = 0,
                NotFinal = 543
            },
            new DataItem()
            {
                Id = 45100012,
                ItemName = "Legends_VO_Script_fr-FR.rqf",
                Language = "fr-FR",
                Folder = "VO_Script",
                ForReview = 1,
                Errors = 17,
                NotFinal = 3
            },
            new DataItem()
            {
                Id = 12388451,
                ItemName = "Legends_VO_Script_es-ES.rqf",
                Language = "es-ES",
                Folder = "VO_Script",
                ForReview = 21,
                Errors = 0,
                NotFinal = 543
            },
            new DataItem()
            {
                Id = -1123451,
                ItemName = "Monument_Subtitles_fr-FR.rqf",
                Language = "fr-FR",
                Folder = "Subtitles",
                ForReview = 125,
                Errors = 178,
                NotFinal = 398
            },
            new DataItem()
            {
                Id = 823451,
                ItemName = "Monument_Subtitles_de-DE.rqf",
                Language = "de-DE",
                Folder = "Subtitles",
                ForReview = 216,
                Errors = 109,
                NotFinal = 573
            }
        };*/

        public void ValidateFiles()
        {
            using (Validate _validation = new Validate())
            {
                this.Statistics = _validation.Validation(Folder);
            }

            this.Success = (this.Statistics.Where(l => l.Errors != 0).Any()) ? false : true;
        }
    }
}
