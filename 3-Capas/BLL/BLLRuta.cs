using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _3_Capas.DAL;

namespace _3_Capas.BLL
{
	public class BLLRuta
	{
		public static object DALChoferes { get; private set; }

		public static long InsRuta(int IdCamion, int IdChofer, int IdOrigen, int IdDestino, double Distancia, DateTime FSalida, DateTime FLlegadaE)
		{
			//Cambiar la disponibilidad del Chofer y del Camión
			DALCamiones.UpdCamion(IdCamion, null, null, null, null, null, null, false, null);
			DALChofer.UpdChofer(IdChofer, null, null, null, null, null, null, null, false);
			return DALRuta.InsRuta(IdCamion, IdChofer, IdOrigen, IdDestino, Distancia, FSalida, FLlegadaE);

		}
	}
}