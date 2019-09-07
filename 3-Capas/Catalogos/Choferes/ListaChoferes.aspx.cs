using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using _3_Capas.BLL;
using System.IO;

namespace _3_Capas.Catalogos.Choferes
{
	public partial class ListaChoferes : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				try
				{
					RecargarGrid();
				}
				catch (Exception ex)
				{
					Util.Library.UtilControls.SweetBox("Error!", ex.Message, "danger", this.Page, this.GetType());
				}
			}
		}

		private void RecargarGrid()
		{
			GVChoferes.DataSource = BLLChofer.GetChoferes(null);
			GVChoferes.DataBind();
		}


		protected void GVChoferes_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Select")
			{
				int index = int.Parse(e.CommandArgument.ToString());
				string IdCamion = GVChoferes.DataKeys[index].Values["IdChofer"].ToString();
				Response.Redirect("EdicionChofer.aspx?Id=" + IdCamion);
			}
		}

		protected void GVChoferes_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GVChoferes.EditIndex = e.NewEditIndex;
			RecargarGrid();
		}

		protected void GVChoferes_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			string IdChofer = GVChoferes.DataKeys[e.RowIndex].Values["IdChofer"].ToString();
			string Nombre = e.NewValues["Nombre"].ToString();
			string ApPaterno = e.NewValues["ApPaterno"].ToString();
			string ApMaterno = e.NewValues["ApMaterno"].ToString();
			string Telefono = e.NewValues["Telefono"].ToString();
			DateTime FechaNacimiento = DateTime.Parse(e.NewValues["FechaNacimiento"].ToString());
			string Licencia = e.NewValues["Licencia"].ToString();
			bool Disponibilidad = bool.Parse(e.NewValues["Disponibilidad"].ToString());

			try
			{
				string Resultado = BLLChofer.UpdChofer(int.Parse(IdChofer), Nombre, ApPaterno, ApMaterno, Telefono, FechaNacimiento, Licencia, Disponibilidad, null);
				GVChoferes.EditIndex = -1;
				RecargarGrid();
				Util.Library.UtilControls.SweetBox(Resultado, "", "success", this.Page, this.GetType());
			}
			catch (Exception ex)
			{
				Util.Library.UtilControls.SweetBox("Error!", ex.Message, "danger", this.Page, this.GetType());
			}
		}

		protected void GVChoferes_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			try
			{
				string IdChofer = GVChoferes.DataKeys[e.RowIndex].Values["IdChofer"].ToString();
				string Resultado = BLLChofer.DelChofer(int.Parse(IdChofer));
				RecargarGrid();
				string msj = "";
				string clase = "";
				if (Resultado == "Chofer Eliminado")
				{
					msj = "Ok";
					clase = "success";
				}
				else
				{
					msj = "Atención!";
					clase = "danger";
				}
				//Creamos el log de borrado
				string[] args = new string[3];
				args[0] = msj;
				args[1] = Resultado;// sub;
				args[2] = DateTime.Now.ToShortDateString();
				WriteLog(args);
				Util.Library.UtilControls.SweetBox(msj, Resultado, clase, this.Page, this.GetType());
			}
			catch (Exception ex)
			{
				Util.Library.UtilControls.SweetBox("Error!", ex.Message, "danger", this.Page, this.GetType());
			}
		}
		private void WriteLog(string[] args)
		{
			StreamWriter log;
			string ServerUnc = Server.MapPath("Logs");
			if (!Directory.Exists(ServerUnc))
			{
				Directory.CreateDirectory(ServerUnc);
			}
			string FileLog = ServerUnc + @"\logFile.log";
			if (!File.Exists(FileLog))
			{
				log = new StreamWriter(FileLog);
			}
			else
			{
				log = File.AppendText(FileLog);
			}
			log.Write("\r\nLog Entry: ");
			log.WriteLine("{0}-{1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongDateString());
			log.WriteLine(" : ");
			log.WriteLine(" : {0}", args[0]);
			log.WriteLine(" : {0}", args[1]);
			log.WriteLine(" : {0}", args[2]);
			log.WriteLine("----------------");
			log.Close();
		}

		protected void GVChoferes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GVChoferes.EditIndex = -1;
			RecargarGrid();
		}
	}
}