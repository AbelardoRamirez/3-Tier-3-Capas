using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;



using Util.Library.Database;
using _3CapasGen2Q.VO;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Services;

namespace _3_Capas
{
	/// <summary>
	/// Summary description for autocompletar
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
	public class autocompletar : System.Web.Services.WebService
	{
		[WebMethod]
		public string[] GetDirecciones(string prefixText)
		{
			DataSet dsDirecciones = Util.Library.Database.DBConnection.ExecuteDataset("GetDireccion", "@Direccion", prefixText);
			string[] items = new string[dsDirecciones.Tables[0].Rows.Count];

			//declaro una variable para tener la posicion del arreglo
			int registro = 0;

			//Recorrer el DataSet
			foreach (DataRow dr in dsDirecciones.Tables[0].Rows)
			{
				items[registro++] = dr["DireccionCompleta"].ToString();
			}

			return items;
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = true)]
		public List<RutaVO> GetLstRutas(int Estatus)
		{
			DataSet dsRutas = DBConnection.ExecuteDataset("GetLstRutas", "@Estatus", Estatus);
			List<RutaVO> Rutas = new List<RutaVO>();
			foreach (DataRow dr in dsRutas.Tables[0].Rows)
				Rutas.Add(new RutaVO(dr));
			return Rutas;
		}
	}
}