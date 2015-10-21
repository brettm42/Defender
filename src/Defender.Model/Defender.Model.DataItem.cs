namespace Defender.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    public class DataItem
    {
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null)
                {
                    this._Id = value.GetHashCode();
                    _name = value;
                }
            }
        }
        private string _name;

        public string Project { get; set; }

        public string Folder { get; set; }
        
        public string Language { get; set; }

        public int Errors { get; set; }

        public int Warnings { get; set; }

        public int NotFinal { get; set; }

        public int ForReview { get; set; }
        
        public DateTime Date { get; set; }

        public string _User { get; set; }

        public string _File { get; set; }
        
        public string _Domain { get; set; }

        public string _Station { get; set; }

        public int _Id { get; set; }

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
