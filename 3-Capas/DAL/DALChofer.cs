using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using _3_Capas.VO;

namespace _3_Capas
{
    public class DALChofer
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

        public static void InsertCatChofer(string nombre, string telefono, int edad, string licencia, string urlFto)
        {

            try
            {
                conn.Open();
                string Query = "InsertCatChofer";
                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@Edad", edad);
                cmd.Parameters.AddWithValue("@Licencia", licencia);
                cmd.Parameters.AddWithValue("@UrlFoto", urlFto);
                cmd.ExecuteNonQuery();//No retornar Nada
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
        }

        public static void UpdateCatChofer(int IdChofer, string nombre, string telefono, int? edad, string Licencia, string UrlFoto, bool? Disponibilidad)
        {

            try
            {
                conn.Open();
                string Query = "UpdateCatChofer";
                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdChofer", IdChofer);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@Edad", edad);
                cmd.Parameters.AddWithValue("@Licencia", Licencia);
                cmd.Parameters.AddWithValue("@UrlFoto", UrlFoto);
                cmd.Parameters.AddWithValue("@Disponibilidad", Disponibilidad);
          
                cmd.ExecuteNonQuery();//No retornar Nada
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public static void DeleteCatChofer(int IdChofer)
        {
            try
            {
                conn.Open();
                string Query = "DeleteCatChofer";
                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdChofer", IdChofer);

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

        }
        public static ChoferVO GetChoferById(int IdChofer)
        {
            try
            {
                string Query = "GetChoferById";
                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Connection = conn;
                //DataTable Obtiene la Tabla
                SqlDataAdapter da = new SqlDataAdapter(cmd);//Obtiene lo de un Select Command
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdChofer", IdChofer);

                DataSet dsChofer = new DataSet();//Realiza una representacion en memoria
                da.Fill(dsChofer);
                if (dsChofer.Tables[0].Rows.Count > 0)
                {
                    //Encontro el Registro
                    DataRow dr = dsChofer.Tables[0].Rows[0];
                    ChoferVO chofer = new ChoferVO(dr);
                    return chofer;
                }
                else
                {
                    //No se encontro el Camion
                    ChoferVO chofer = new ChoferVO();
                    return chofer;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }//GetCamionById
        public static List<ChoferVO> GetLstChofer(bool? Disponibilidad)
        {
            try
            {
                conn.Open();
                string Query = "lstChofer";
                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsChoferes = new DataSet();

                if (Disponibilidad == null)
                    //Traigo los camiones de la tabla
                    da.Fill(dsChoferes);
                else
                {
                    cmd.Parameters.AddWithValue("@Disponibilidad", Disponibilidad);
                    da.Fill(dsChoferes);
                }
                List<ChoferVO> LstChoferes = new List<ChoferVO>();
                foreach (DataRow dr in dsChoferes.Tables[0].Rows)
                    LstChoferes.Add(new ChoferVO(dr));
                return LstChoferes;
            }
            catch (Exception)
            {

                throw;
            }//El SqlDataAdapter realiza Conn.Close();
        }//GetLstCamiones

    }

}