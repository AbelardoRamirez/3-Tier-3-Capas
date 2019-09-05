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

		public static void InsChofer(string nombre, string appaterno, string apmaterno, string telefono, DateTime fechanacimiento, string licencia, string urlFto)
		{

			try
			{
				conn.Open();
				string Query = "InsChofer";
				SqlCommand cmd = new SqlCommand(Query, conn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Nombre", nombre);
				cmd.Parameters.AddWithValue("@ApPaterno", appaterno);
				cmd.Parameters.AddWithValue("@ApMaterno", apmaterno);
				cmd.Parameters.AddWithValue("@Telefono", telefono);
				cmd.Parameters.AddWithValue("@FechaNacimiento", fechanacimiento);
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
				conn.Close();
			}
		}

		public static void UpdChofer(int IdChofer, string nombre, string appaterno, string apmaterno, string telefono, DateTime? fechanacimiento, string Licencia, string UrlFoto, bool? Disponibilidad)
		{

			try
			{
				conn.Open();
				string Query = "UpdCatChofer";
				SqlCommand cmd = new SqlCommand(Query, conn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@IdChofer", IdChofer);
				cmd.Parameters.AddWithValue("@Nombre", nombre);
				cmd.Parameters.AddWithValue("@ApPaterno", appaterno);
				cmd.Parameters.AddWithValue("@ApMaterno", apmaterno);
				cmd.Parameters.AddWithValue("@Telefono", telefono);
				cmd.Parameters.AddWithValue("@FechaNacimiento", fechanacimiento);
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
		public static void DelCatChofer(int IdChofer)
		{
			try
			{
				conn.Open();
				string Query = "DelCatChofer";
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
		public static List<ChoferVO> GetLstChoferes(bool? Disponibilidad)
		{
			try
			{
				conn.Open();
				string Query = "LstChoferes";
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
			}
			finally
			{
				conn.Close();
			}
		}//GetLstCamiones

	}

}