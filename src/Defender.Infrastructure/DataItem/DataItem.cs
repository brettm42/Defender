namespace Defender.Infrastructure
{
    using System;
    using System.Linq;
    using System.Reflection;

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
                    this._id = value.GetHashCode();
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

        public string _user { get; set; }

        public string _file { get; set; }
        
        public string _domain { get; set; }

        public string _station { get; set; }

        public int _id { private get; set; }

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

        public override string ToString() =>
            this.GetType().GetProperties()
                          .Where(p => p.Name != "Item")
                          .Aggregate(
                              string.Empty,
                              (str, prop) => 
                                  str += $"{prop.Name} - {prop.GetValue(this)?.ToString() ?? string.Empty}\n");
    }
}
