using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Referencias
using _3_Capas.BLL;
using System.IO;

namespace _3_Capas.Catalogos.Camiones
{
	public partial class AltaCamion : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)//Es la primer carga de la pagina
			{
				Util.Library.UtilControls.EnumToListBox(typeof(Marca), DDLMarca, false);
				Util.Library.UtilControls.EnumToListBox(typeof(Tipo), DDLTipoCamion, false);

				DDLMarca.Items.Insert(0, new ListItem("--Selecciona una Marca--", ""));
				DDLModelo.Items.Insert(0, new ListItem("--Selecciona un  Modelo", ""));
				DDLTipoCamion.Items.Insert(0, new ListItem("--Selecciona Tipo de Camión--", ""));

				DDLMarca.SelectedIndex = 0;
				DDLModelo.SelectedIndex = 0;
				DDLTipoCamion.SelectedIndex = 0;

				LlenarModelo();
			}
		}//EndLoad

		private void LlenarModelo()
		{
			int Minimo = DateTime.Now.Year - 20;
			int Maximo = DateTime.Now.Year + 2;
			var rango = Enumerable.Range(Minimo, Maximo - Minimo);
			DDLModelo.DataSource = rango;
			DDLModelo.DataBind();
		}//EndLlenarModelo

		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				string Matricula = txtMatricula.Text;
				string TipoCamion = DDLTipoCamion.SelectedItem.Value;
				int Modelo = int.Parse(DDLModelo.SelectedItem.Value);
				string Marca = DDLMarca.SelectedItem.Value;
				int Capacidad = int.Parse(txtCapacidad.Text);
				float Kilometraje = float.Parse(txtKilometraje.Text);
				string urlFoto = UrlFoto.InnerText;
				string Resultado = BLLCamiones.InsCamion(Matricula, TipoCamion, Modelo, Marca, Capacidad, Kilometraje, urlFoto);
				if (Resultado.IndexOf("Camión agregado") > -1)
				{
					Util.Library.UtilControls.SweetBoxConfirm("OK!", Resultado, "success", "ListaCamiones.aspx", this.Page, this.GetType());
				}
				else
				{
					Util.Library.UtilControls.SweetBox("Atención!", Resultado, "warning", this.Page, this.GetType());
				}
			}
			catch (Exception ex)
			{
				//Enviar Mensaje de error
				Util.Library.UtilControls.SweetBox("ERROR!", ex.Message, "danger", this.Page, this.GetType());
			}
		}

		protected void btnSubImagen_Click(object sender, EventArgs e)
		{
			try
			{
				if (SubeImagen.Value != "")//Comprobar que  el usuario haya seleccionado un archivo
				{
					//Subimos el archivo
					string FileName = Path.GetFileName(SubeImagen.PostedFile.FileName);
					//Validamos que el archivo sea .jpg o .png
					string FileExt = Path.GetExtension(FileName).ToLower();
					if ((FileExt != ".jpg") && (FileExt != ".png"))
						//Informamos al usuario que el archivo no es valido
						Util.Library.UtilControls.SweetBox("Atención", "Seleccione un archivo válido", "warning", this.Page, this.GetType());
					else
					{
						//Verificar que el directorio exista
						string PathDir = Server.MapPath("~/Imagenes/Camiones/");
						if (!Directory.Exists(PathDir))
							//Creamos el directorio
							Directory.CreateDirectory(PathDir);

						//Guardamos el archivo
						SubeImagen.PostedFile.SaveAs(PathDir + FileName);
						string urlFoto = "/Imagenes/Camiones/" + FileName;
						UrlFoto.InnerText = urlFoto;
						imgFotoCamion.ImageUrl = urlFoto;
						btnGuardar.Visible = true;

					}
				}
				else
				{
					//Informamos al usuario que el archivo no es valido
					Util.Library.UtilControls.SweetBox("Atención!", "Seleccione un archivo válido", "warning", this.Page, this.GetType());
				}
			}
			catch (Exception ex)
			{
				//Enviar mensaje de error al usuario
				Util.Library.UtilControls.SweetBox("ERROR!", ex.Message, "danger", this.Page, this.GetType());
			}
		}//EndBtnSubeImagen
	}
}