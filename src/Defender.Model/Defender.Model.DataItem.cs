using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Defender.Model
{
    public class DataItem
    {
        internal int Id { get; set; }
                
        public string Project { get; set; }

        public string Folder { get; set; }

        public string Language { get; set; }

        public int NotFinal { get; set; }

        public int ForReview { get; set; }

        public int Errors { get; set; }

        public int Warnings { get; set; }

        public string ItemName
        {
            get
            {
                return _item;
            }
            set
            {
                if (value != null)
                {
                    this.Id = value.GetHashCode();
                    _item = value;
                }
            }
        }
        private string _item;

        public object this[string prop]
        {
            get
            {
                return typeof(DataItem).GetProperty(prop);
            }
            set
            {
                PropertyInfo propinfo = typeof(DataItem).GetProperty(prop);
                propinfo.SetValue(this, value, null);
            }
        }

        public override string ToString()
        {
            return this.GetType().GetProperties()
                                 .Where(p => p.Name != "Item")
                                 .Aggregate(
                                     string.Empty,
                                     (str, prop) => 
                                         str += $"{prop.Name} - {prop.GetValue(this)?.ToString() ?? string.Empty}\n");
        }
    }
}
