using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Util.Library.Database
{
    public class DBConnection
    {
        private string _StringDeConexion;
        public string StringDeConexion
        {
            get { return _StringDeConexion; }
            set { _StringDeConexion = value; }
        }
        public DBConnection(string cnxnString)
        {
            this.StringDeConexion = cnxnString;
        
        }
        public static string connectionString = ConfigurationManager.ConnectionStrings["Conn"] != null ? ConfigurationManager.ConnectionStrings["Conn"].ConnectionString : "";
        public static DataSet ExecuteQuery(SqlConnection conn, SqlTransaction tran, string spName, out int retValue, params object[] parametros)
        {
            if (parametros != null && parametros.Length % 2 != 0)
                throw new Exception("Los parametros deben de venir en pares");

            SqlCommand comm = DBConnection.fillSqlCommand_Parameters(spName, parametros);
            comm.Connection = conn;
            if (tran != null)
                comm.Transaction = tran;
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            retValue = comm.Parameters["RetVal"].Value != null ? (int)comm.Parameters["RetVal"].Value : 0;
            return ds;
        }
        /// <summary>         
        /// 
        /// Ejecuta un query  y regresa el identity de la operacion.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        /// <param name="spName"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public static long ExecuteNonQueryGetIdentity(SqlConnection conn, SqlTransaction tran, string spName, params object[] parametros)
        {

            if (parametros != null && parametros.Length % 2 != 0)
                throw new Exception("Los parametros deben de venir en pares");
            SqlCommand comm = DBConnection.fillSqlCommand_Parameters(spName, parametros);
            comm.Connection = conn;
            if (tran != null)
                comm.Transaction = tran;
            object objReturn = comm.ExecuteScalar();
            comm = conn.CreateCommand();
            comm.Transaction = tran;
            comm.CommandText = "SELECT @@IDENTITY";
            object val = comm.ExecuteScalar();
            long identity = long.Parse(val.ToString());
            comm.Dispose();
            return identity;
        }
        /// <summary>
        /// Ejecuta un query  y regresa el identity de la operacion.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public static long ExecuteNonQueryGetIdentity(string spName, params object[] parametros)
        {
            return ExecuteGetIdentityWithStrCon(DBConnection.connectionString, spName, parametros);
        }
        public static long ExecuteGetIdentityWithStrCon(string stringDeConexion, string spName, params object[] parametros)
        {
            SqlConnection conn = new SqlConnection(stringDeConexion);
            conn.Open();
            long identity = ExecuteNonQueryGetIdentity(conn, null, spName, parametros);
            conn.Close();
            return identity;
        }       
        public static DataSet ExecuteDatasetWithStrCon(string stringDeConexion, string spName, out int retValue, params object[] parametros)
        {
            SqlConnection conn = new SqlConnection(stringDeConexion);
            conn.Open();
            DataSet ds = ExecuteQuery(conn, null, spName, out retValue, parametros);
            conn.Close();
            return ds;
        }
        public static  DataSet ExecuteDataset(string spName, out int retValue, params object[] parametros)
        {
           return ExecuteDatasetWithStrCon(DBConnection.connectionString, spName, out retValue, parametros);
        }
        public static DataSet ExecuteDataset(string spName, params object[] parametros)
        {
            int retValue;
            SqlConnection conn = new SqlConnection(DBConnection.connectionString);
            conn.Open();
            DataSet ds = ExecuteQuery(conn, null, spName, out retValue, parametros);
            conn.Close();
            return ds;
        }
        public static DataTable ExecuteQueryGetFirstDataTable(string spName, params object[] parametros)
        {
            int retValue;
            SqlConnection conn = new SqlConnection(DBConnection.connectionString);
            conn.Open();
            DataSet ds = ExecuteQuery(conn, null, spName, out retValue, parametros);
            conn.Close();
            return ds.Tables[0];
        }
        public static int ExecuteNonQuery(string spName, out int retValue, params object[] parametros)
        {
            return ExecuteNonQueryWithStrCon(DBConnection.connectionString, spName,out retValue,parametros);            
        }
        public static int ExecuteNonQueryWithStrCon(string stringDeConexion, string spName, out int retValue, params object[] parametros)
        {
            SqlConnection conn = new SqlConnection(stringDeConexion);
            conn.Open();
            int numRowAffected = 0;
            numRowAffected = ExecuteNonQuery(conn, null, spName, out retValue, parametros);
            conn.Close();
            return numRowAffected;
        }
        public static int ExecuteNonQuery(string spName, params object[] parametros)
        {
            int retVal;
            return ExecuteNonQuery(spName, out retVal, parametros);
        }
        public static int ExecuteNonQuery(SqlConnection conexion, SqlTransaction tran, string spName, out int retValue, params object[] parametros)
        {
            if (parametros != null && parametros.Length % 2 != 0)
                throw new Exception("Los parametros deben de venir en pares");

            SqlCommand comm = DBConnection.fillSqlCommand_Parameters(spName, parametros);
            comm.Connection = conexion;
            if (tran != null)
                comm.Transaction = tran;
            int numRowsAffected = comm.ExecuteNonQuery();
            retValue = comm.Parameters["RetVal"].Value != null ? (int)comm.Parameters["RetVal"].Value : 0;
            return numRowsAffected;
        }
        /// <summary>
        /// Ejecuta el query y obtiene el conjunto de datos de la tabla 0 en un DataTable
        /// </summary>
        /// <param name="stringDeConexion"></param>
        /// <param name="spName"></param>
        /// <param name="retValue"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public static DataTable ExecuteGetFirstDataTableWithStrCon(string stringDeConexion, string spName, out int retValue, params object[] parametros)
        {
            SqlConnection conn = new SqlConnection(stringDeConexion);
            conn.Open();
            DataSet ds = ExecuteQuery(conn, null, spName, out retValue, parametros);
            conn.Close();
            return ds.Tables[0];
        }
        /// <summary>
        /// Ejecuta el query y obtiene el conjunto de datos de la tabla 0 en un DataTable
        /// </summary>
        /// <param name="conexion"></param>
        /// <param name="tran"></param>
        /// <param name="spName"></param>
        /// <param name="retValue"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public static DataTable ExecuteQueryGetFirstDataTable(SqlConnection conexion, SqlTransaction tran, string spName, out int retValue, params object[] parametros)
        {
            return ExecuteQuery(conexion, tran, spName, out retValue, parametros).Tables[0];
        }
        public static string ExecuteNonQueryWithOutPutPar(SqlConnection conexion, SqlTransaction tran, string spName, out int retValue, string outputParName, params object[] parametros)
        {
            if (parametros != null && parametros.Length % 2 != 0)
                throw new Exception("Los parametros deben de venir en pares");
            SqlCommand comm = DBConnection.fillSqlCommand_ParametersWithOutput(spName, outputParName, parametros);
            comm.Connection = conexion;
            if (tran != null)
                comm.Transaction = tran;
            int numRowsAffected = comm.ExecuteNonQuery();
            string retorno = comm.Parameters[outputParName].Value.ToString();
            retValue = comm.Parameters["RetVal"].Value != null ? (int)comm.Parameters["RetVal"].Value : 0;
            return retorno;
        }
        public static object ExecuteScalar(string spName, out int retValue, params object[] parametros)
        {
            if (parametros != null && parametros.Length % 2 != 0)
                throw new Exception("Los parametros deben de venir en pares");

            SqlConnection conn = new SqlConnection(DBConnection.connectionString);
            SqlCommand comm = DBConnection.fillSqlCommand_Parameters(spName, parametros);

            conn.Open();
            comm.Connection = conn;
            object objReturn = comm.ExecuteScalar();
            conn.Close();

            retValue = comm.Parameters["RetVal"].Value != null ? (int)comm.Parameters["RetVal"].Value : 0;
            return objReturn;
        }
        public static object ExecuteScalar(string strConnectionString, string strStoredProcedure, out int intRetValue, params object[] objParametros)
        {
            if (objParametros != null && objParametros.Length % 2 != 0) { throw new Exception("Los parametros deben de venir en pares"); }

            SqlConnection miSqlConnection = new SqlConnection(strConnectionString);

            SqlCommand miSqlCommand = DBConnection.fillSqlCommand_Parameters(strStoredProcedure, objParametros);

            miSqlConnection.Open();

            miSqlCommand.Connection = miSqlConnection;

            object objReturn = miSqlCommand.ExecuteScalar();

            miSqlConnection.Close();

            intRetValue = miSqlCommand.Parameters["RetVal"].Value != null ? (int)miSqlCommand.Parameters["RetVal"].Value : 0;

            return objReturn;
        }
        private static SqlCommand fillSqlCommand_Parameters(string spName, params object[] parametros)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = spName;

            if (parametros != null)
            {
                for (int i = 0; i < parametros.Length; i = i + 2)
                {
                    comm.Parameters.AddWithValue(parametros[i].ToString(), parametros[i + 1]);
                }
            }

            SqlParameter retValue = new SqlParameter("RetVal", SqlDbType.Int);
            retValue.Direction = ParameterDirection.ReturnValue;
            comm.Parameters.Add(retValue);

            return comm;
        }
        private static SqlCommand fillSqlCommand_ParametersWithOutput(string spName, string outputParameterName, params object[] parametros)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = spName;

            if (parametros != null)
            {
                for (int i = 0; i < parametros.Length; i = i + 2)
                {
                    comm.Parameters.AddWithValue(parametros[i].ToString(), parametros[i + 1]);
                }
            }
            SqlParameter outputPar = new SqlParameter();
            outputPar.Direction = ParameterDirection.Output;
            outputPar.IsNullable = true;
            outputPar.ParameterName = outputParameterName;
            comm.Parameters.Add(outputPar);
            //Return Value
            SqlParameter retValue = new SqlParameter("RetVal", SqlDbType.Int);
            retValue.Direction = ParameterDirection.ReturnValue;
            comm.Parameters.Add(retValue);

            return comm;
        }

        //CommandTimeout
        public static DataTable ExecuteGetFirstDataTableWithStrConCommandTimeout(string strConnectionString, string strSPNombre, out int intResultado, params object[] objParametros)
        {
            try
            {
                SqlConnection miSqlConnection = new SqlConnection(strConnectionString);

                miSqlConnection.Open();

                DataSet miDataSet = ExecuteQueryWithCommandTimeout(miSqlConnection, null, strSPNombre, out intResultado, objParametros);

                miSqlConnection.Close();

                return miDataSet.Tables[0];
            }
            catch (Exception) { throw; }
        }
        public static DataSet ExecuteQueryWithCommandTimeout(SqlConnection miSqlConnection, SqlTransaction miSqlTransaction, string strSPNombre, out int intResultado, params object[] objParametros)
        {
            try
            {
                if (objParametros != null && objParametros.Length % 2 != 0) { throw new Exception("Los parámetros deben de venir en pares."); }

                SqlCommand miSqlCommand = DBConnection.fillSqlCommand_Parameters(strSPNombre, objParametros);

                miSqlCommand.Connection = miSqlConnection;

                if (ConfigurationManager.AppSettings["DB_CommandTimeout"] != null)
                {
                    int intCommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["DB_CommandTimeout"]);

                    miSqlCommand.CommandTimeout = intCommandTimeout;
                }
                if (miSqlTransaction != null) { miSqlCommand.Transaction = miSqlTransaction; }

                SqlDataAdapter miSqlDataAdapter = new SqlDataAdapter(miSqlCommand);

                DataSet miDataSet = new DataSet();

                miSqlDataAdapter.Fill(miDataSet);

                intResultado = miSqlCommand.Parameters["RetVal"].Value != null ? (int)miSqlCommand.Parameters["RetVal"].Value : 0;

                return miDataSet;
            }
            catch (Exception) { throw; }
        }

        public static int ExecuteNonQueryWithStrConCommandTimeout(string strConnectionString, string strSPNombre, out int intResultado, params object[] objParametros)
        {
            try
            {
                SqlConnection miSqlConnection = new SqlConnection(strConnectionString);

                miSqlConnection.Open();

                int intRowAffected = 0;

                intRowAffected = ExecuteNonQueryWithCommandTimeout(miSqlConnection, null, strSPNombre, out intResultado, objParametros);

                miSqlConnection.Close();

                return intRowAffected;
            }
            catch (Exception) { throw; }
        }
        public static int ExecuteNonQueryWithCommandTimeout(SqlConnection miSqlConnection, SqlTransaction miSqlTransaction, string strSPNombre, out int intResultado, params object[] objParametros)
        {
            try
            {
                if (objParametros != null && objParametros.Length % 2 != 0) { throw new Exception("Los parametros deben de venir en pares"); }

                SqlCommand miSqlCommand = DBConnection.fillSqlCommand_Parameters(strSPNombre, objParametros);

                miSqlCommand.Connection = miSqlConnection;

                if (ConfigurationManager.AppSettings["DB_CommandTimeout"] != null)
                {
                    int intCommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["DB_CommandTimeout"]);

                    miSqlCommand.CommandTimeout = intCommandTimeout;
                }
                if (miSqlTransaction != null) { miSqlCommand.Transaction = miSqlTransaction; }

                int intRowsAffected = miSqlCommand.ExecuteNonQuery();

                intResultado = miSqlCommand.Parameters["RetVal"].Value != null ? (int)miSqlCommand.Parameters["RetVal"].Value : 0;

                return intRowsAffected;
            }
            catch (Exception) { throw; }
        }
    }
}