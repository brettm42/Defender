namespace Defender.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Defender.Data;
    using Defender.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DataTests
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        void SerialiseData()
        {
            //Serializer _serial = new Serializer(new ObservableCollection );

            //string serialised = _serial.SerialiseToString();

            //Assert.AreEqual(Resources/SerialisedData, serialised, "Expected encoded data to match, does not!");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        void DeserialiseData()
        {
            //Serializer _serial = new Serializer(new ObservableCollection );

            //ObservableCollection deserialised = _serial.DeserialiseFromString(Resources/BinaryData);

            //Assert.AreEqual(Resources/DataGrid, _serial.DataGrid, "Expected decoded data to match original data, it doesn't!");

        }

        [TestMethod]
        [TestCategory("UnitTest")]
        void SerialiseDeserialiseData()
        {
            //Serializer _serial = new Serializer(new ObservableCollection );

            //string serialised = _serial.SerialiseToString();

            //ObservableCollection deserialised = _serial.DeserialiseFromString(Resources/BinaryData);

            //Assert.AreEqual(Resources/SerialisedData, serialised, "Expected encoded data to match, does not!");
            //Assert.AreEqual(Resources/DataGrid, _serial.DataGrid, "Expected decoded data to match original data, it doesn't!");

        }
    }
}
