using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defender.Model
{
    public class DataItem
    {
        internal string Id { get; set; }

        public string ItemName { get; set; }
        
        public string Folder { get; set; }

        public string Language { get; set; }

        public int NotFinal { get; set; }

        public int ForReview { get; set; }

        public int Errors { get; set; }

        // TODO: finish implementing this :P
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
