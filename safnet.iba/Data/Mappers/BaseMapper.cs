using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using safnet.iba.Business.Entities;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Provides methods for loading <see cref="SafnetBaseEntity"/>
    /// </summary>
    public static class BaseMapper
    {
        /// <summary>
        /// Deletes the object from the database. Table name is inferred from the object's type
        /// </summary>
        /// <param name="entity">The object.</param>
        public static void DeleteObject(SafnetBaseEntity entity)
        {
            DeleteObject(entity, entity.GetType().Name);
        }

        /// <summary>
        /// Deletes the object from the database.
        /// </summary>
        /// <param name="entity">The object.</param>
        /// <param name="tableName">Name of the table.</param>
        public static void DeleteObject(SafnetBaseEntity entity, string tableName)
        {
            string storedProcedure = tableName + "_Delete";
            Database iba = null;
            DbCommand cmd = null;
            CreateDatabaseCommand(storedProcedure, new List<QueryParameter>() { new QueryParameter("Id", entity.Id) }, out iba, out cmd);

            iba.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Saves the object to the database. The table name is inferred from the object type.
        /// </summary>
        /// <param name="entity">The object.</param>
        /// <param name="queryParameterList">The query parameter list.</param>
        /// <returns>int</returns>
        public static int SaveObject(SafnetBaseEntity entity, List<QueryParameter> queryParameterList)
        {
            return SaveObject(entity, queryParameterList, entity.GetType().Name);
        }

        /// <summary>
        /// Saves the object to the database.
        /// </summary>
        /// <param name="entity">The object.</param>
        /// <param name="queryParameterList">The query parameter list.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>int</returns>
        public static int SaveObject(SafnetBaseEntity entity, List<QueryParameter> queryParameterList, string tableName)
        {
            string storedProcedure = tableName + "_Save";
            Database iba = null;
            DbCommand cmd = null;
            CreateDatabaseCommand(storedProcedure, queryParameterList, out iba, out cmd);

            return iba.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Saves the object to the database.
        /// </summary>
        /// <param name="entity">The object.</param>
        /// <param name="queryParameterList">The query parameter list.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>int</returns>
        public static int SaveObjectWithIdentity(SafnetBaseEntity entity, List<QueryParameter> queryParameterList, string tableName)
        {
            string storedProcedure = tableName + "_Save";
            Database iba = null;
            DbCommand cmd = null;
            CreateDatabaseCommand(storedProcedure, queryParameterList, out iba, out cmd);

            return int.Parse(iba.ExecuteScalar(cmd).ToString());
        }

        /// <summary>
        /// Delegate for a method to map query results into an object.
        /// </summary>
        /// <typeparam name="T">A <see cref="SafnetBaseEntity"/> class</typeparam>
        /// <param name="reader">Instance of IDataReader</param>
        /// <returns>New <see cref="SafnetBaseEntity"/></returns>
        public delegate T LoadObject<T>(IDataReader reader);

        /// <summary>
        /// Loads a single object with a query by ID value
        /// </summary>
        /// <typeparam name="T">A <see cref="SafnetBaseEntity"/> class</typeparam>
        /// <param name="method">A LoadObject method</param>
        /// <param name="storedProc">Stored procedure name</param>
        /// <param name="id">ID value to query</param>
        /// <returns>New <see cref="SafnetBaseEntity"/></returns>
        public static T LoadSingleObjectById<T>(LoadObject<T> method, string storedProc, Guid id)
        {
            Database iba = DatabaseFactory.CreateDatabase();
            DbCommand cmd = iba.GetStoredProcCommand(storedProc);
            iba.AddInParameter(cmd, "Id", DbType.Guid, id);

            T entity = default(T);
            using (IDataReader reader = iba.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = method(reader);
                }
            }
            return entity;
        }

		/// <summary>
        /// Loads a single object with a query by ID value
        /// </summary>
        /// <typeparam name="T">A <see cref="SafnetBaseEntity"/> class</typeparam>
        /// <param name="method">A LoadObject delegate method</param>
        /// <param name="storedProc">Stored procedure name</param>
        /// <param name="id">ID value to query</param>
        /// <returns>New <see cref="SafnetBaseEntity"/></returns>
		public static T LoadSingleObjectByIntegerId<T>(LoadObject<T> method, string storedProc, int id)
        {
            Database iba = DatabaseFactory.CreateDatabase();
            DbCommand cmd = iba.GetStoredProcCommand(storedProc);
            iba.AddInParameter(cmd, "Id", DbType.Int32, id);

            T entity = default(T);
            using (IDataReader reader = iba.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = method(reader);
                }
            }
            return entity;
        }



        /// <summary>
        /// Loads a single object with a query by ID value
        /// </summary>
        /// <typeparam name="T">A <see cref="SafnetBaseEntity"/> class</typeparam>
        /// <param name="method">A LoadObject method</param>
        /// <param name="storedProc">Stored procedure name</param>
		/// <param name="idName">Name of the identity field</param>
        /// <param name="id">ID value to query</param>
        /// <returns>New <see cref="SafnetBaseEntity"/></returns>
        public static T LoadSingleObjectById<T>(LoadObject<T> method, string storedProc, string idName, string id)
        {
            Database iba = DatabaseFactory.CreateDatabase();
            DbCommand cmd = iba.GetStoredProcCommand(storedProc);
            iba.AddInParameter(cmd, idName, DbType.String, id);

            T entity = default(T);
            using (IDataReader reader = iba.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = method(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// Loads all records from a table.
        /// </summary>
        /// <typeparam name="T">A <see cref="SafnetBaseEntity"/> class</typeparam>
        /// <param name="method">A LoadObject method</param>
        /// <param name="storedProc">Stored procedure name</param>
        /// <returns>New <see cref="SafnetBaseEntity"/></returns>
        public static List<T> LoadAll<T>(LoadObject<T> method, string storedProc)
        {
            QueryParameter parm = new QueryParameter("All", true);
            return LoadAllQuery<T>(method, storedProc, new List<QueryParameter>() { parm });
        }

        /// <summary>
        /// Loads all records from a table with a list of query parameters
        /// </summary>
        /// <typeparam name="T">A <see cref="SafnetBaseEntity"/> class</typeparam>
        /// <param name="method">A LoadObject method</param>
        /// <param name="storedProc">Stored procedure name</param>
        /// <param name="parameters">List of <see cref="QueryParameter"/> objects</param>
        /// <returns>New <see cref="SafnetBaseEntity"/></returns>
        public static List<T> LoadAllQuery<T>(LoadObject<T> method, string storedProc, List<QueryParameter> parameters)
        {
            List<T> list = new List<T>();
            Database iba = null;
            DbCommand cmd = null;
            CreateDatabaseCommand(storedProc, parameters, out iba, out cmd);

            using (IDataReader reader = iba.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    T entity = method(reader);
                    list.Add(entity);
                }
            }
            return list;
        }

        /// <summary>
        /// Creates the database command.
        /// </summary>
        /// <param name="storedProc">The stored procedure.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <param name="iba">The Database Connection.</param>
        /// <param name="cmd">The Database Command.</param>
        public static void CreateDatabaseCommand(string storedProc, List<QueryParameter> parameters, out Database iba, out DbCommand cmd)
        {
            iba = DatabaseFactory.CreateDatabase();
            cmd = iba.GetStoredProcCommand(storedProc);

            foreach (QueryParameter parm in parameters)
            {
                iba.AddInParameter(cmd, parm.Name, parm.DataType, parm.Value);
            }
        }
    }
}