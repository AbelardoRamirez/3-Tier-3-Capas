using _3_Capas.BLL;
using _3_Capas.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3_Capas.Catalogos.Camiones
{
	public partial class EdicionCamion : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)//Primera carga de la página
			{
				Util.Library.UtilControls.EnumToListBox(typeof(Marca), DDLMarca, false);
				Util.Library.UtilControls.EnumToListBox(typeof(Tipo), DDLTipoCamion, false);
				LlenarModelo();

				DDLMarca.Items.Insert(0, new ListItem("Selecciona Marca", ""));
				DDLTipoCamion.Items.Insert(0, new ListItem("Selecciona Modelo", ""));

				DDLMarca.SelectedIndex = 0;
				DDLModelo.SelectedIndex = 0;
				DDLTipoCamion.SelectedIndex = 0;

				string Id = Request.QueryString["Id"];
				if ((string.IsNullOrEmpty(Id)) || (!IsNumeric(Id)))
				{
					Response.Redirect("ListaCamiones.aspx");
				}
				else
				{
					CamionVO Camion = BLLCamiones.GetCamionById(int.Parse(Id));
					if (Camion.IdCamion == int.Parse(Id))
					{
						lblIdCamion.Text = Camion.IdCamion.ToString();
						txtMatricula.Text = Camion.Matricula;
						txtCapacidad.Text = Camion.Capacidad.ToString();
						txtKilometraje.Text = Camion.Kilometraje.ToString();
						DDLTipoCamion.SelectedValue = Camion.TipoCamion;
						DDLMarca.SelectedValue = Camion.Marca;
						DDLModelo.SelectedValue = Camion.Modelo.ToString();
						imgFotoCamion.ImageUrl = Camion.UrlFoto;
						UrlFoto.InnerText = Camion.UrlFoto;
						chkDisponibilidad.Checked = Camion.Disponibilidad;
					}
					else
					{
						Response.Redirect("ListaCamiones.aspx");
					}
				}
			}
		}

		private bool IsNumeric(string id)
		{
			float output;
			return float.TryParse(id, out output);
		}

		private void LlenarModelo()
		{
			int Minimo = DateTime.Now.Year - 20;
			int Maximo = DateTime.Now.Year + 2;
			var Rango = Enumerable.Range(Minimo, Maximo - Minimo);
			DDLModelo.DataSource = Rango;
			DDLModelo.DataBind();
		}

		protected void btnSubeImagen_Click(object sender, EventArgs e)
		{
			//Guardar foto.
			try
			{
				//Validar que el usuario haya seleccionado un archivo
				if (SubeImagen.Value != "")
				{
					//Subimos el archivo
					string FileName = Path.GetFileName(SubeImagen.PostedFile.FileName);
					//Validamos que el archivo sea .jpg o .png
					string FileExt = Path.GetExtension(FileName).ToLower();
					if ((FileExt != ".jpg") && (FileExt != ".png"))
					{
						//Informamos al usuario que el archivo no es válido
						Util.Library.UtilControls.SweetBox("Atención!", "Seleccione un archivo válido", "warning", this.Page, this.GetType());
					}
					else
					{
						//Verificar que el directorio destino exista
						string PathDir = Server.MapPath("~/Imagenes/Camiones/");
						if (!Directory.Exists(PathDir))
						{
							//Creamos el directorio
							Directory.CreateDirectory(PathDir);
						}
						//Guardamos el archivo 
						SubeImagen.PostedFile.SaveAs(PathDir + FileName);
						string urlfoto = "/Imagenes/Camiones/" + FileName;
						UrlFoto.InnerText = urlfoto;
						imgFotoCamion.ImageUrl = urlfoto;
					}
				}
				else
				{
					//Informamos al usuario que el archivo no es válido
					Util.Library.UtilControls.SweetBox("Atención!", "Seleccione un archivo válido", "warning", this.Page, this.GetType());
				}

			}
			catch (Exception ex)
			{
				//Enviar mensaje de error al usuario
				Util.Library.UtilControls.SweetBox("ERROR!", ex.Message, "danger", this.Page, this.GetType());
			}
		}

		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				int IdCamion = int.Parse(lblIdCamion.Text);
				string Matricula = txtMatricula.Text;
				string TipoCamion = DDLTipoCamion.SelectedValue;
				int Modelo = int.Parse(DDLModelo.SelectedValue);
				string Marca = DDLMarca.SelectedValue;
				int Capacidad = int.Parse(txtCapacidad.Text);
				float Kilometraje = float.Parse(txtKilometraje.Text);
				string urlfoto = UrlFoto.InnerText;
				bool Disponibilidad = chkDisponibilidad.Checked;
				string Resultado = BLLCamiones.UpdCamion(IdCamion, Matricula, TipoCamion, Modelo, Marca, Capacidad, Kilometraje, Disponibilidad, urlfoto);
				if (Resultado.IndexOf("Camión actualizado correctamente") > -1)
				{
					Util.Library.UtilControls.SweetBoxConfirm("OK!", Resultado, "success", "ListaCamiones.aspx", this.Page, this.GetType());
				}
				else
				{
					//Mensaje de error
					Util.Library.UtilControls.SweetBox("Atención!", Resultado, "warning", this.Page, this.GetType());
				}
			}
			catch (Exception ex)
			{
				Util.Library.UtilControls.SweetBox("ERROR!", ex.Message, "danger", this.Page, this.GetType());
			}
		}
	}
}