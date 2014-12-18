using System;
using System.Data;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Wrapper for query parameters
    /// </summary>
    public class QueryParameter
    {
        private int _intValue;
        private long _longValue;
        private short _shortValue;
        private byte _byteValue;
        private decimal _decimalValue;
        private double _doubleValue;
        private Guid _guidValue;
        private string _stringValue;
        private bool _boolValue;
        private DateTime _datetimeValue;

        /// <summary>
        /// Constructor for integer parameter
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public QueryParameter(string name, int value)
        {
            Name = name;
            DataType = DbType.Int32;
            _intValue = value;
        }

        /// <summary>
        /// Constructor for long parameter
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public QueryParameter(string name, long value)
        {
            Name = name;
            DataType = DbType.Int64;
            _longValue = value;
        }

        /// <summary>
        /// Constructor for short parameter
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public QueryParameter(string name, short value)
        {
            Name = name;
            DataType = DbType.Int16;
            _shortValue = value;
        }

        /// <summary>
        /// Constructor for byte parameter
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public QueryParameter(string name, byte value)
        {
            Name = name;
            DataType = DbType.Byte;
            _byteValue = value;
        }

        /// <summary>
        /// Constructor for decimal parameter
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public QueryParameter(string name, decimal value)
        {
            Name = name;
            DataType = DbType.Decimal;
            _decimalValue = value;
        }

        /// <summary>
        /// Constructor for double parameter
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public QueryParameter(string name, double value)
        {
            Name = name;
            DataType = DbType.Double;
            _doubleValue = value;
        }

        /// <summary>
        /// Constructor for Guid parameter
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public QueryParameter(string name, Guid value)
        {
            Name = name;
            DataType = DbType.Guid;
            _guidValue = value;
        }

        /// <summary>
        /// Constructor for string parameter
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public QueryParameter(string name, string value)
        {
            Name = name;
            DataType = DbType.AnsiString;
            _stringValue = value;
        }

        /// <summary>
        /// Constructor for bool parameter
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public QueryParameter(string name, bool value)
        {
            Name = name;
            DataType = DbType.Boolean;
            _boolValue = value;
        }

        /// <summary>
        /// Constructor for DateTime parameter
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public QueryParameter(string name, DateTime value)
        {
            Name = name;
            DataType = DbType.DateTime;
            _datetimeValue = value;
        }

        /// <summary>
        /// Gets the parameter name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the parameter data type
        /// </summary>
        public DbType DataType { get; private set; }

        /// <summary>
        /// Gets the parameter value
        /// </summary>
        /// <returns></returns>
        public object Value
        {
            get
            {
                switch (DataType)
                {
                    case DbType.AnsiString:
                        return _stringValue;
                    case DbType.Boolean:
                        return _boolValue;
                    case DbType.Byte:
                        return _byteValue;
                    case DbType.DateTime:
                        return _datetimeValue;
                    case DbType.Decimal:
                        return _decimalValue;
                    case DbType.Double:
                        return _doubleValue;
                    case DbType.Guid:
                        return _guidValue;
                    case DbType.Int16:
                        return _shortValue;
                    case DbType.Int32:
                        return _intValue;
                    case DbType.Int64:
                        return _longValue;
                    default:
                        throw new IbaArgumentException("Invalid DataType specified for the QueryParameter: " + DataType.ToString());
                }
            }
        }
    }
}
