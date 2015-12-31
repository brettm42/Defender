namespace Defender.Infrastructure.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Text;
    using Defender.Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InfrastructureTests
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void SerialiseData()
        {
            //Serializer _serial = new Serializer(new ObservableCollection );

            //string serialised = _serial.SerialiseToString();

            //Assert.AreEqual(Resources/SerialisedData, serialised, "Expected encoded data to match, does not!");

            Assert.Inconclusive();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void DeserialiseData()
        {
            //Serializer _serial = new Serializer(new ObservableCollection );

            //ObservableCollection deserialised = _serial.DeserialiseFromString(Resources/BinaryData);

            //Assert.AreEqual(Resources/DataGrid, _serial.DataGrid, "Expected decoded data to match original data, it doesn't!");

            Assert.Inconclusive();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void SerialiseDeserialiseData()
        {
            //Serializer _serial = new Serializer(new ObservableCollection );

            //string serialised = _serial.SerialiseToString();

            //ObservableCollection deserialised = _serial.DeserialiseFromString(Resources/BinaryData);

            //Assert.AreEqual(Resources/SerialisedData, serialised, "Expected encoded data to match, does not!");
            //Assert.AreEqual(Resources/DataGrid, _serial.DataGrid, "Expected decoded data to match original data, it doesn't!");

            Assert.Inconclusive();
        }
    }
}
