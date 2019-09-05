using _3_Capas.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3_Capas.Catalogos.Camiones
{
	public partial class ListaCamiones : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				try
				{
					RefrescarGrid();
				}
				catch (Exception ex)
				{
					Util.Library.UtilControls.SweetBox("Error!", ex.Message, "danger", this.Page, this.GetType());
				}
			}
		}

		private void RefrescarGrid()
		{
			GVCamiones.DataSource = BLLCamiones.GetlistCamiones(null);
			GVCamiones.DataBind();
		}

		protected void GVCamiones_RowEditing(object sender, GridViewEditEventArgs e)
		{
			Label lbltipoC = (Label)GVCamiones.Rows[e.NewEditIndex].FindControl("lblTipoCamion");

			string tipoC = lbltipoC.Text;

			GVCamiones.EditIndex = e.NewEditIndex;
			RefrescarGrid();
			DropDownList DDLTipoCamiones = (DropDownList)GVCamiones.Rows[e.NewEditIndex].FindControl("DDLTipoCamion");
			Util.Library.UtilControls.EnumToListBox(typeof(Tipo), DDLTipoCamiones, false);
			DDLTipoCamiones.SelectedValue = tipoC;
		}

		protected void GVCamiones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GVCamiones.EditIndex = -1;
			RefrescarGrid();
		}

		protected void GVCamiones_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			string IdCamion = GVCamiones.DataKeys[e.RowIndex].Values["IdCamion"].ToString();
			string Matricula = e.NewValues["Matricula"].ToString();
			string Modelo = e.NewValues["Modelo"].ToString();
			string Marca = e.NewValues["Marca"].ToString();
			int Capacidad = Convert.ToInt32(e.NewValues["Capacidad"].ToString());
			float Kilometraje = float.Parse(e.NewValues["Kilometraje"].ToString());

			DropDownList TipoCamionAux = (DropDownList)GVCamiones.Rows[e.RowIndex].FindControl("DDLTipoCamion");
			string Tipocamion = TipoCamionAux.SelectedValue;

			bool Disponibilidad = bool.Parse(e.NewValues["Disponibilidad"].ToString());
			try
			{
				string Resultado = BLLCamiones.UpdCamion(int.Parse(IdCamion), Matricula, Tipocamion, int.Parse(Modelo), Marca, Capacidad, Kilometraje, Disponibilidad, null);
				GVCamiones.EditIndex = -1;
				RefrescarGrid();
				Util.Library.UtilControls.SweetBox(Resultado, "", "success", this.Page, this.GetType());
			}
			catch (Exception ex)
			{
				Util.Library.UtilControls.SweetBox("Error!", ex.Message, "danger", this.Page, this.GetType());
			}
		}//EndOnUpdating

		protected void GVCamiones_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			try
			{
				string IdCamion = GVCamiones.DataKeys[e.RowIndex].Values["IdCamion"].ToString();
				string Resultado = BLLCamiones.DelCamion(int.Parse(IdCamion));
				RefrescarGrid();
				string msj = "";
				string clase = "";
				if (Resultado == "Camión Eliminado")
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
			log.WriteLine(" :{0}", args[0]);
			log.WriteLine(" :{1}", args[1]);
			log.WriteLine(" :{2}", args[2]);
			log.WriteLine("----------------");
			log.Close();
		}

		protected void GVCamiones_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Select")
			{
				int index = int.Parse(e.CommandArgument.ToString());
				string IdCamion = GVCamiones.DataKeys[index].Values["IdCamion"].ToString();
				Response.Redirect("EdicionCamion.aspx?Id=" + IdCamion);
			}
		}
	}
}