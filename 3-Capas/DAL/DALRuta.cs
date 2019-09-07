using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Util.Library.Database;

namespace _3_Capas.DAL
{
	public class DALRuta
	{
		public static long InsRuta(int IdCamion, int IdChofer, int IdOrigen, int IdDestino, double Distancia, DateTime FSalida, DateTime FLlegadaE)
		{
			try
			{
				return
					DBConnection.ExecuteNonQueryGetIdentity
						("InsRutas", "@IdCamion", IdCamion,
									"@IdChofer", IdChofer,
									"@IdOrigen", IdOrigen,
									"@IdDestino", IdDestino,
									"@Distancia", Distancia,
									"@FHSalida", FSalida,
									"@FHLlegadaE", FLlegadaE);
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}