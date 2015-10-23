namespace Defender.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Threading.Tasks;
    using Defender.Model;
    using Defender.Model.Extensions;

    public class Serializer : DataBase
    {
        public ObservableCollection<DataItem> DataGrid { get; set; }
        
        public string SerialiseToString()
        {
            return SerialiseToString<ObservableCollection<DataItem>>(this.DataGrid);
        }

        public string SerialiseToString<T>(T dataitems)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, dataitems);
                stream.Flush();
                stream.Position = 0;

                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public ObservableCollection<DataItem> DeserialiseFromString(string binarystring)
        {
            return this.DataGrid = DeserialiseFromString<ObservableCollection<DataItem>>(binarystring);
        }

        public T DeserialiseFromString<T>(string dataitems)
        {
            byte[] bytearray = Convert.FromBase64String(dataitems);

            using (var stream = new MemoryStream(bytearray))
            {
                var formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);

                return (T)formatter.Deserialize(stream);
            }
        }
        
        public Serializer(ObservableCollection<DataItem> DataGrid_in)
        {
            this.DataGrid = DataGrid_in;
        }
    }
}
