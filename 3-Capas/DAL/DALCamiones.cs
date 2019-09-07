using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//para acceder a la clase
using _3_Capas.VO;

namespace _3_Capas.DAL
{
	public class DALCamiones
	{
		//creamos el objeto conexion aprtide la clase statica
		static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

		//
		public static void InsCamion(string Matricula, string TipoCamion, int Modelo, string Marca, int Capacidad, float Kilometraje, string UrlFoto)
		{
			try
			{
				//varibblee de tip string donde vamos a 
				string Query = "InsCamion";
				//instancia de 
				SqlCommand cmd = new SqlCommand(Query, con);
				// indiga que es un strore prcedudee
				cmd.CommandType = CommandType.StoredProcedure;
				//Paso de parametros
				cmd.Parameters.AddWithValue("@Matricula", Matricula);
				cmd.Parameters.AddWithValue("@TipoCamion", TipoCamion);
				cmd.Parameters.AddWithValue("@Modelo", Modelo);
				cmd.Parameters.AddWithValue("@Marca", Marca);
				cmd.Parameters.AddWithValue("@Capacidad", Capacidad);

				cmd.Parameters.AddWithValue("@kilometraje", Kilometraje);
				cmd.Parameters.AddWithValue("@UrlFoto", UrlFoto);
				con.Open();
				//este es el que no devulve ningun valor 
				cmd.ExecuteNonQuery();

			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				con.Close();
			}
		}

		public static void UpdCamion(int IdCamion, string Matricula, string TipoCamion, int? Modelo, string Marca, int? Capacidad, float? Kilometraje, bool? Disponibilidad, string UrlFoto)
		{
			try
			{
				//varibblee de tip string donde vamos a 
				string Query = "UpdCamion";
				//instancia de 
				SqlCommand cmd = new SqlCommand(Query, con);
				// indiga que es un strore prcedudee
				cmd.CommandType = CommandType.StoredProcedure;
				//Paso de parametros
				cmd.Parameters.AddWithValue("@IdCamion", IdCamion);
				cmd.Parameters.AddWithValue("@Matricula", Matricula);
				cmd.Parameters.AddWithValue("@TipoCamion", TipoCamion);
				cmd.Parameters.AddWithValue("@Modelo", Modelo);
				cmd.Parameters.AddWithValue("@Marca", Marca);
				cmd.Parameters.AddWithValue("@Capacidad", Capacidad);

				cmd.Parameters.AddWithValue("@kilometraje", Kilometraje);
				cmd.Parameters.AddWithValue("@Disponibilidad", Disponibilidad);
				cmd.Parameters.AddWithValue("@UrlFoto", UrlFoto);
				con.Open();
				//este es el que no devulve ningun valor 
				cmd.ExecuteNonQuery();

			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				con.Close();
			}
		}

		//scjnsdjchbsdclskbdchkdsjbc 
		public static void DelCamion(int IdCamion)
		{
			try
			{
				//varibblee de tip string donde vamos a 
				string Query = "DelCamion";
				//instancia de 
				SqlCommand cmd = new SqlCommand(Query, con);
				// indiga que es un strore prcedudee
				cmd.CommandType = CommandType.StoredProcedure;
				//Paso de parametros
				cmd.Parameters.AddWithValue("@IdCamion", IdCamion);
				con.Open();
				//este es el que no devulve ningun valor 
				cmd.ExecuteNonQuery();

			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				con.Close();
			}
		}//DeleteCamion


		public static CamionVO GetCamionById(int IdCamion)
		{
			try
			{
				//varibblee de tip string donde vamos a 
				string Query = "GetCamionById";
				//instancia de 
				SqlCommand cmd = new SqlCommand(Query, con);

				cmd.Connection = con;
				SqlDataAdapter adapter = new SqlDataAdapter(cmd);

				// indiga que es un strore prcedudee
				cmd.CommandType = CommandType.StoredProcedure;
				//Paso de parametros
				cmd.Parameters.AddWithValue("@IdCamion", IdCamion);

				DataSet dsCamion = new DataSet();
				adapter.Fill(dsCamion);
				if (dsCamion.Tables[0].Rows.Count > 0)
				{
					//encontro un registro
					DataRow dr = dsCamion.Tables[0].Rows[0];
					CamionVO camion = new CamionVO(dr);
					return camion;
				}
				else
				{
					// la tabla esta vacía
					CamionVO camion = new CamionVO();
					return camion;
				}

			}
			catch (Exception)
			{

				throw;
			}
		}

		public static List<CamionVO> GetLstCamiones(bool? Disponibilidad)
		{
			string query = "LstCamiones";
			SqlCommand cmd = new SqlCommand(query, con);
			cmd.Connection = con;
			cmd.CommandType = CommandType.StoredProcedure;

			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			DataSet dsCamiones = new DataSet();

			try
			{
				if (Disponibilidad == null)
				{
					//encontro un registro
					adapter.Fill(dsCamiones);
				}
				else
				{
					// la tabla esta vacía
					cmd.Parameters.AddWithValue("@Disponibilidad", Disponibilidad);
					adapter.Fill(dsCamiones);
				}
				// 
				List<CamionVO> lstCamiones = new List<CamionVO>();
				foreach (DataRow dr in dsCamiones.Tables[0].Rows)
				{
					lstCamiones.Add(new CamionVO(dr));
				}
				return lstCamiones;
			}
			catch (Exception)
			{
				throw;
			}


		}

	}
}