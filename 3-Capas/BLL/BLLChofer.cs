using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _3_Capas.DAL;
using _3_Capas.VO;

namespace _3_Capas.BLL
{
	public class BLLChofer
	{
		//Aqui se aplican reglas del Negocio;
		public static string InsChofer(string nombre, string appaterno, string apmaterno, string telefono, DateTime fechanacimiento, string licencia, string urlFto)
		{
			try
			{
				List<ChoferVO> LstChofer = DALChofer.GetLstChoferes(null);

				bool Exists = false;
				foreach (ChoferVO _chofer in LstChofer)
					if (_chofer.Licencia == licencia) Exists = true;

				if (Exists) { return "the Licencencia can be used "; }

				else
				{
					DALChofer.InsChofer(nombre, appaterno, apmaterno, telefono, fechanacimiento, licencia, urlFto);
					return "Chofer agregado";
				}
			}
			catch (Exception ex) { return ex.Message; }
		}

		public static string UpdChofer(int IdChofer, string nombre, string appaterno, string apmaterno, string telefono, DateTime? fechanacimiento, string licencia, bool? Disponibilidad, string UrlFoto)
		{
			try
			{
				List<ChoferVO> LstChofer = DALChofer.GetLstChoferes(null);
				bool Existe = false;
				foreach (ChoferVO chofer in LstChofer)
					//No puedo utilizar la placa del camion en otro camion
					if ((chofer.IdChofer != IdChofer) && (chofer.Licencia == licencia)) Existe = true;

				if (Existe) return "La Licencia ya ha sido asignada a otro chofer";
				else
				{
					DALChofer.UpdChofer(IdChofer, nombre, appaterno, appaterno, telefono, fechanacimiento, licencia, UrlFoto, Disponibilidad);
					return "Licencia agraga correctamente a chofer";
				}
			}
			catch (Exception ex) { return ex.Message; }
		}

		public static string DeleteCatChofer(int IdChofer)
		{
			try
			{
				ChoferVO camion = DALChofer.GetChoferById(IdChofer);
				if (camion.Disponibilidad)
				{
					DALChofer.DelCatChofer(IdChofer);
					return "Chofer eliminado";
				}
				else return "El Chofer se encuentra en una ruta o no está disponible";
			}
			catch (Exception ex) { return ex.Message; }
		}//DelCamion

		public static ChoferVO GetChoferById(int IdChofer)
		{
			try
			{
				return DALChofer.GetChoferById(IdChofer);
			}
			catch (Exception)
			{
				return new ChoferVO();
			}
		}//GetCamionById

		public static List<ChoferVO> GetChofer(bool? Disponibilidad)
		{
			List<ChoferVO> LstChoferes = new List<ChoferVO>();
			try
			{
				return DALChofer.GetLstChoferes(Disponibilidad);
			}
			catch (Exception)
			{
				return LstChoferes;
			}
		}//GetLstCamiones

	}
}