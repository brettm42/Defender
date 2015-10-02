using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FSharp;
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
                if (Enumerable.Range(0,101).Contains(value))
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
                RaisePropertyChanged(nameof(_curfile));
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
        //private string[] _gameareas;
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
        //private string[] _langs;
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
        private string[] _filelist;

    }
}
