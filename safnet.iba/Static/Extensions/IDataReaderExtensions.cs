using System;
using System.Data;

namespace safnet.iba.Static.Extensions
{
    /// <summary>
    /// Extension methods for the IDataReader interface
    /// </summary>
    public static class IDataReaderExtensions
    {
        /// <summary>
        /// Extension to get a string value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>string value</returns>
        public static string GetStringFromName(this IDataReader reader, string colName)
        {
            string value = string.Empty;
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetString(reader.GetOrdinal(colName));
            }
            return value;
        }

        /// <summary>
        /// Extension to get a char value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>char value</returns>
        public static char GetCharFromName(this IDataReader reader, string colName)
        {
            char value = ' ';
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetChar(reader.GetOrdinal(colName));
            }
            return value;
        }

        /// <summary>
        /// Extension to get a byte value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>byte value</returns>
        public static byte GetByteFromName(this IDataReader reader, string colName)
        {
            byte value = 0;
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetByte(reader.GetOrdinal(colName));
            }
            return value;
        }

        /// <summary>
        /// Extension to get a short value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>short value</returns>
        public static short GetShortFromName(this IDataReader reader, string colName)
        {
            short value = 0;
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetInt16(reader.GetOrdinal(colName));
            }
            return value;
        }

        /// <summary>
        /// Extension to get a int value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>int value</returns>
        public static int GetIntFromName(this IDataReader reader, string colName)
        {
            int value = 0;
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetInt32(reader.GetOrdinal(colName));
            }
            return value;
        }

        /// <summary>
        /// Extension to get a long value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>long value</returns>
        public static long GetLongFromName(this IDataReader reader, string colName)
        {
            long value = 0;
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetInt64(reader.GetOrdinal(colName));
            }
            return value;
        }

        /// <summary>
        /// Extension to get a bool value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>bool value</returns>
        public static bool GetBoolFromName(this IDataReader reader, string colName)
        {
            bool value = false;
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetBoolean(reader.GetOrdinal(colName));
            }
            return value;
        }

        /// <summary>
        /// Extension to get a double value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>double value</returns>
        public static double GetDoubleFromName(this IDataReader reader, string colName)
        {
            double value = 0d;
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetDouble(reader.GetOrdinal(colName));
            }
            return value;
        }


        /// <summary>
        /// Extension to get a decimal value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>decimal value</returns>
        public static decimal GetDecimalFromName(this IDataReader reader, string colName)
        {
            decimal value = 0M;
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetDecimal(reader.GetOrdinal(colName));
            }
            return value;
        }

        /// <summary>
        /// Extension to get a float value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>float value</returns>
        public static float GetFloatFromName(this IDataReader reader, string colName)
        {
            float value = 0f;
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetFloat(reader.GetOrdinal(colName));
            }
            return value;
        }

        /// <summary>
        /// Extension to get a Guid value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>Guid value</returns>
        public static Guid GetGuidFromName(this IDataReader reader, string colName)
        {
            Guid value = Guid.Empty;
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetGuid(reader.GetOrdinal(colName));
            }
            return value;
        }

        /// <summary>
        /// Extension to get a object value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>object value</returns>
        public static object GetValueFromName(this IDataReader reader, string colName)
        {
            object value = null;
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetValue(reader.GetOrdinal(colName));
            }
            return value;
        }

        /// <summary>
        /// Extension to get a DateTime value from an IDataReader by name instead of by ordinal.
        /// </summary>
        /// <param name="reader">IDataReader object</param>
        /// <param name="colName">Column name</param>
        /// <returns>DateTime value</returns>
        public static DateTime GetDateTimeFromName(this IDataReader reader, string colName)
        {
            DateTime value = DateTime.MinValue;
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
            {
                value = reader.GetDateTime(reader.GetOrdinal(colName));
            }
            return value;
        }
    }
}
