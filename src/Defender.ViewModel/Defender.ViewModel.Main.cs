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
        //private bool _success = false;
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
        //private ObservableCollection<string> _gameareas;
        private string[] _gameareas = new string[] { "UI", "VO_Script" };

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
        //private ObservableCollection<string> _langs;
        private string[] _langs = new string[] { "de-DE", "es-ES", "es-MX", "fr-FR", "fr-CA", "it-IT", "ja-JP", "ko-KR", "hi-IN", "ru-RU", "zh-TW", "zh-CN" };

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
        //private ObservableCollection<string> _filelist;
        private string[] _filelist = new string[] { "Legends_UI_fr-FR.rqf", "Legends_VO_de-DE.rqf", "Monument_UI_ko-KR.rqf", "Legends_VO_fr-FR.rqf", "Legends_UI_ru-RU.rqf", "Monument_Subtitles_it-IT.rqf"};

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
            }
        }
        //private ObservableCollection<DataItem> _stats;
        private ObservableCollection<DataItem> _stats = new ObservableCollection<DataItem>()
        {
            new DataItem()
            {
                ItemName = "Legends_UI_fr-FR.rqf",
                Language = "fr-FR",
                Folder = "UI",
                ForReview = 1,
                Errors = 17,
                NotFinal = 3
            },
            new DataItem()
            {
                ItemName = "Legends_UI_de-DE.rqf",
                Language = "de-DE",
                Folder = "UI",
                ForReview = 1,
                Errors = 0,
                NotFinal = 3
            },
            new DataItem()
            {
                ItemName = "Legends_UI_es-ES.rqf",
                Language = "es-ES",
                Folder = "UI",
                ForReview = 1,
                Errors = 1,
                NotFinal = 32
            },
            new DataItem()
            {
                ItemName = "Legends_VO_Script_de-DE.rqf",
                Language = "de-DE",
                Folder = "VO_Script",
                ForReview = 21,
                Errors = 0,
                NotFinal = 543
            },
            new DataItem()
            {
                ItemName = "Legends_VO_Script_fr-FR.rqf",
                Language = "fr-FR",
                Folder = "VO_Script",
                ForReview = 1,
                Errors = 17,
                NotFinal = 3
            },
            new DataItem()
            {
                ItemName = "Legends_VO_Script_es-ES.rqf",
                Language = "es-ES",
                Folder = "VO_Script",
                ForReview = 21,
                Errors = 0,
                NotFinal = 543
            },
            new DataItem()
            {
                ItemName = "Monument_Subtitles_fr-FR.rqf",
                Language = "fr-FR",
                Folder = "Subtitles",
                ForReview = 125,
                Errors = 178,
                NotFinal = 398
            },
            new DataItem()
            {
                ItemName = "Monument_Subtitles_de-DE.rqf",
                Language = "de-DE",
                Folder = "Subtitles",
                ForReview = 216,
                Errors = 109,
                NotFinal = 573
            }
        };
    }
}
