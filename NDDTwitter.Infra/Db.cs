using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace NDDTwitter.Infra
{
    public static class Db
    {
        private static readonly string dataProvider = ConfigurationManager.AppSettings.Get("DataProvider");
        private static readonly DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);

        private static readonly string connectionStringName = ConfigurationManager.AppSettings.Get("ConnectionStringName");
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

        //wololo
        /// <summary>
        /// Busca um item de acordo com o comando Sql e parâmetros.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="make"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static T Get<T>(string sql, Func<IDataReader, T> make, object[] parms = null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.SetParameters(parms);  // Extension method

                    connection.Open();

                    T t = default(T);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                        t = make(reader);

                    return t;
                }
            }
        }

        /// <summary>
        /// Busca uma lista de itens de acordo com o comando Sql e parâmetros.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="make"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<T> GetAll<T>(string sql, Func<IDataReader, T> make, object[] parms = null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.SetParameters(parms);

                    connection.Open();

                    var list = new List<T>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                        list.Add(make(reader));

                    return list;
                }
            }
        }

        /// <summary>
        /// Insere itens na base se dados
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static long Insert(string sql, object[] parms = null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.SetParameters(parms);                     // Extension method  
                    command.CommandText = sql.AppendIdentitySelect(); // Extension method                    

                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        /// <summary>
        /// Atualiza itens na base de dados
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        public static void Update(string sql, object[] parms = null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.SetParameters(parms);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deleta itens na base de dados.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        public static void Delete(string sql, object[] parms = null)
        {
            Update(sql, parms);
        }



        /// <summary>
        /// Extension method: Adiciona a sintaxe especifica ao comando para recuperar o ID auto-incremento
        /// </summary>
        /// <param name="sql">Comando sql que será feita a adição.</param>
        /// <returns>O comando Sql com o comando para recuperar o ID</returns>
        private static string AppendIdentitySelect(this string sql)
        {
            switch (dataProvider)
            {
                // Microsoft Access não tem suporte a esse tipo de comando
                case "System.Data.OleDb": return sql;
                case "System.Data.SqlClient": return sql + ";SELECT SCOPE_IDENTITY()";
                case "System.Data.OracleClient": return sql + ";SELECT MySequence.NEXTVAL FROM DUAL";
                case "System.Data.SqlServerCe.4.0": return sql + "SELECT @@IDENTITY";
                default: return sql + ";SELECT @@IDENTITY";
            }
        }

        /// <summary>
        /// Extention method: Adiciona os parametros para o comando.
        /// </summary>
        /// <param name="command">Objeto que representa o comando</param>
        /// <param name="parms">Lista de parametros</param>
        private static void SetParameters(this DbCommand command, object[] parms)
        {
            if (parms != null && parms.Length > 0)
            {
                for (int i = 0; i < parms.Length; i += 2)
                {
                    string name = parms[i].ToString();

                    if (parms[i + 1] is string && (string)parms[i + 1] == "")
                        parms[i + 1] = null;

                    object value = parms[i + 1] ?? DBNull.Value;

                    var dbParameter = command.CreateParameter();
                    dbParameter.ParameterName = name;
                    dbParameter.Value = value;

                    command.Parameters.Add(dbParameter);
                }
            }
        }


    }
}
