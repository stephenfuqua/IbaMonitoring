using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Static.Extensions;

namespace safnet.iba.UnitTests
{

    /// <summary>
    ///This is a test class for tIDataReaderExtensions and is intended
    ///to contain all tIDataReaderExtensions Unit Tests
    ///</summary>
    [TestClass()]
    public class tIDataReaderExtensions
    {
        private const string columnName = "column";

        #region Public Methods

        /// <summary>
        ///A test for GetBoolFromName
        ///</summary>
        [TestMethod()]
        public void t_GetBoolFromName_false()
        {            
            bool value = false;
            IDataReader reader = setupReader(value);

            bool actual;
            actual = IDataReaderExtensions.GetBoolFromName(reader, columnName);
            Assert.IsFalse(actual);
        }

        /// <summary>
        ///A test for GetBoolFromName
        ///</summary>
        [TestMethod()]
        public void t_GetBoolFromName_null()
        {
            
            IDataReader reader = setupNullReader(typeof(bool));

            bool actual;
            actual = IDataReaderExtensions.GetBoolFromName(reader, columnName);
            Assert.IsFalse(actual);
        }

        /// <summary>
        ///A test for GetBoolFromName
        ///</summary>
        [TestMethod()]
        public void t_GetBoolFromName_true()
        {
            
            bool value = true;
            IDataReader reader = setupReader(value);

            bool actual;
            actual = IDataReaderExtensions.GetBoolFromName(reader, columnName);
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///A test for GetByteFromName
        ///</summary>
        [TestMethod()]
        public void t_GetByteFromName()
        {
            byte expected = 1;
            IDataReader reader = setupReader(expected);
            
            byte actual;
            actual = IDataReaderExtensions.GetByteFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByteFromName
        ///</summary>
        [TestMethod()]
        public void t_GetByteFromName_Null()
        {
            byte expected = 0;
            IDataReader reader = setupNullReader(typeof(byte));

            byte actual;
            actual = IDataReaderExtensions.GetByteFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetCharFromName
        ///</summary>
        [TestMethod()]
        public void t_GetCharFromName()
        {
            char expected = 'a';
            IDataReader reader = setupReader(expected);

            char actual;
            actual = IDataReaderExtensions.GetCharFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetCharFromName
        ///</summary>
        [TestMethod()]
        public void t_GetCharFromName_null()
        {
            char expected = ' ';
            IDataReader reader = setupReader(expected);

            char actual;
            actual = IDataReaderExtensions.GetCharFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetDateTimeFromName
        ///</summary>
        [TestMethod()]
        public void t_GetDateTimeFromName()
        {
            DateTime expected = DateTime.Now;

            IDataReader reader = setupReader(expected);

            DateTime actual;
            actual = IDataReaderExtensions.GetDateTimeFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetDateTimeFromName
        ///</summary>
        [TestMethod()]
        public void t_GetDateTimeFromName_null()
        {
            DateTime expected = DateTime.MinValue;

            IDataReader reader = setupNullReader(typeof(DateTime));

            DateTime actual;
            actual = IDataReaderExtensions.GetDateTimeFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetDecimalFromName
        ///</summary>
        [TestMethod()]
        public void t_GetDecimalFromName()
        {
            Decimal expected = 0.1835893M;

            IDataReader reader = setupReader(expected);

            Decimal actual;
            actual = IDataReaderExtensions.GetDecimalFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetDecimalFromName
        ///</summary>
        [TestMethod()]
        public void t_GetDecimalFromName_null()
        {
            Decimal expected = 0m;

            IDataReader reader = setupNullReader(typeof(Decimal));

            Decimal actual;
            actual = IDataReaderExtensions.GetDecimalFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetDoubleFromName
        ///</summary>
        [TestMethod()]
        public void t_GetDoubleFromName()
        {
            double expected = 0.234834234d;

            IDataReader reader = setupReader(expected);

            double actual;
            actual = IDataReaderExtensions.GetDoubleFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
         }

        /// <summary>
        ///A test for GetDoubleFromName
        ///</summary>
        [TestMethod()]
        public void t_GetDoubleFromName_Null()
        {
            double expected = 0d;

            IDataReader reader = setupNullReader(typeof(double));

            double actual;
            actual = IDataReaderExtensions.GetDoubleFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetFloatFromName
        ///</summary>
        [TestMethod()]
        public void t_GetFloatFromName()
        {
            float expected = 23.345342F;
            IDataReader reader = setupReader(expected);

            float actual;
            actual = IDataReaderExtensions.GetFloatFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetFloatFromName
        ///</summary>
        [TestMethod()]
        public void t_GetFloatFromName_Null()
        {
            float expected = 0F;
            IDataReader reader = setupNullReader(typeof(float));

            float actual;
            actual = IDataReaderExtensions.GetFloatFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetGuidFromName
        ///</summary>
        [TestMethod()]
        public void t_GetGuidFromName()
        {
            Guid expected = new Guid("CE0D1C95-C0C6-4D1E-8D5A-ADB8D0A297AF");

            IDataReader reader = setupReader(expected);

            Guid actual;
            actual = IDataReaderExtensions.GetGuidFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetGuidFromName
        ///</summary>
        [TestMethod()]
        public void t_GetGuidFromName_Null()
        {
            Guid expected = Guid.Empty;

            IDataReader reader = setupNullReader(typeof(Guid));

            Guid actual;
            actual = IDataReaderExtensions.GetGuidFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetIntFromName
        ///</summary>
        [TestMethod()]
        public void t_GetIntFromName()
        {
            int expected = 54896;
            IDataReader reader = setupReader(expected);

            int actual;
            actual = IDataReaderExtensions.GetIntFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetIntFromName
        ///</summary>
        [TestMethod()]
        public void t_GetIntFromName_Null()
        {
            int expected = 0;
            IDataReader reader = setupNullReader(typeof(Guid));

            int actual;
            actual = IDataReaderExtensions.GetIntFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetLongFromName
        ///</summary>
        [TestMethod()]
        public void t_GetLongFromName()
        {
            long expected = 12345678973156;
            IDataReader reader = setupReader(expected);
            
            long actual;
            actual = IDataReaderExtensions.GetLongFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetLongFromName
        ///</summary>
        [TestMethod()]
        public void t_GetLongFromName_Null()
        {
            long expected = 0;
            IDataReader reader = setupNullReader(typeof(long));

            long actual;
            actual = IDataReaderExtensions.GetLongFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetShortFromName
        ///</summary>
        [TestMethod()]
        public void t_GetShortFromName()
        {
            short expected = 255;

            IDataReader reader = setupReader(expected);
            
            short actual;
            actual = IDataReaderExtensions.GetShortFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetShortFromName
        ///</summary>
        [TestMethod()]
        public void t_GetShortFromName_NUll()
        {
            short expected = 0;

            IDataReader reader = setupNullReader(typeof(short));

            short actual;
            actual = IDataReaderExtensions.GetShortFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringFromName
        ///</summary>
        [TestMethod()]
        public void t_GetStringFromName()
        {
            string expected = "this is a test";
            IDataReader reader = setupReader(expected);
            
            string actual;
            actual = IDataReaderExtensions.GetStringFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringFromName
        ///</summary>
        [TestMethod()]
        public void t_GetStringFromName_Null()
        {
            string expected = string.Empty;
            IDataReader reader = setupNullReader(typeof(string));

            string actual;
            actual = IDataReaderExtensions.GetStringFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetValueFromName
        ///</summary>
        [TestMethod()]
        public void t_GetValueFromName()
        {
            object expected = new { Blah = "blah", Ack = 324 };
            IDataReader reader = setupReader(expected, typeof(object));
            
            object actual;
            actual = IDataReaderExtensions.GetValueFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetValueFromName
        ///</summary>
        [TestMethod()]
        public void t_GetValueFromName_Null()
        {
            object expected = null;
            IDataReader reader = setupNullReader(typeof(object));

            object actual;
            actual = IDataReaderExtensions.GetValueFromName(reader, columnName);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Private Methods

        private static IDataReader setupNullReader(Type dataType)
        {
            return setupReader(DBNull.Value, dataType);
        }

        private static IDataReader setupReader( object data, Type dataType)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn(columnName, dataType));
            table.Rows.Add(table.NewRow());
            table.Rows[0][columnName] = data;

            IDataReader reader = table.CreateDataReader();
            reader.Read();
            return reader;
        }

        private static IDataReader setupReader(object data)
        {

            Type dataType = data.GetType();

            return setupReader(data, dataType);
        }

        #endregion

    }
}
