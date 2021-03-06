﻿namespace Defender.Infrastructure
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using Defender.Infrastructure;
    using Defender.Infrastructure.Extensions;

    public class Serializer : InfrastructureBase
    {
        public ObservableCollection<DataItem> DataGrid { get; set; }
        
        public string SerialiseToString() => SerialiseToString(this.DataGrid);
        
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

        public ObservableCollection<DataItem> DeserialiseFromString(string binarystring) => 
            this.DataGrid = DeserialiseFromString<ObservableCollection<DataItem>>(binarystring);
        
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
        
        public Serializer(ObservableCollection<DataItem> dataGridIn)
        {
            this.DataGrid = dataGridIn;
        }
    }
}
