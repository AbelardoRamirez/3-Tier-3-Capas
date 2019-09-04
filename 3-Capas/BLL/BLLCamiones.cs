using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// se agregan 
using _3_Capas.DAL;
using _3_Capas.VO;


namespace _3_Capas.BLL
{
	public class BLLCamiones
	{
		public static string InsCamion(string Matricula, string TipoCamion, int Modelo, string Marca, int Capacidad, float Kilometraje, string UrlFoto)
		{
			try
			{
				List<CamionVO> LstCamiones = DALCamiones.GetLstCamiones(null);
				//bandera 
				bool existe = false;
				foreach (CamionVO item in LstCamiones)
				{
					//comparo con la lista de Lista y con la bandera que me mandaron en el metodo!! 
					if (item.Matricula == Matricula)
					{
						existe = true;
					}

				}
				if (existe)
				{
					return "La matricula del camion ya fue capturadad con anterioridad";
				}
				else
				{
					DALCamiones.InsCamion(Matricula, TipoCamion, Modelo, Marca, Capacidad, Kilometraje, UrlFoto);
					return "Camión agregado";
				}
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
		public static string UpdateCamion(int IdCamion, string Matricula, string TipoCamion, int Modelo, string Marca, int Capacidad, float Kilometraje, bool Disponibilidad, string UrlFoto)
		{
			try
			{

				List<CamionVO> LstUpCamiones = DALCamiones.GetLstCamiones(null);
				bool Existe = false;
				foreach (CamionVO item in LstUpCamiones)
				{
					if ((item.IdCamion != IdCamion) && (item.Matricula != Matricula))
					{
						Existe = true;
					}
				}
				if (Existe)
				{
					return "La Matricula del camionn ya fue utilizada con anterioridad!";
				}
				else
				{
					DALCamiones.UpdCamion(IdCamion, Matricula, TipoCamion, Modelo, Marca, Capacidad, Kilometraje, Disponibilidad, UrlFoto);
					return "La actualiazacion fue correcta!!";
				}
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		public static string DelCamion(int IdCamion)
		{
			try
			{
				CamionVO camion = DALCamiones.GetCamionById(IdCamion);
				if (camion.Disponiblidad)
				{
					DALCamiones.DelCamion(IdCamion);
					return "Camion eliminado";
				}
				else
				{
					return "El camion no se puede eliminar se encuentra en ruta";
				}

			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
		public static CamionVO GetCamionById(int IdCamion)
		{
			try
			{
				return DALCamiones.GetCamionById(IdCamion);
			}
			catch (Exception)
			{
				return new CamionVO();
				throw;
			}
		}
		public static List<CamionVO> GetlistCamiones(bool? Disponibilidad)
		{
			List<CamionVO> LstCamiones = new List<CamionVO>();
			try
			{
				return DALCamiones.GetLstCamiones(Disponibilidad);
			}
			catch (Exception)
			{

				return LstCamiones;
			}
		}
	}
}