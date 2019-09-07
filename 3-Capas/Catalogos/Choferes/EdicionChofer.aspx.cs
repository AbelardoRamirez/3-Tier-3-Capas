using _3_Capas.BLL;
using _3_Capas.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3_Capas.Catalogos.Choferes
{
	public partial class EdicionChofer : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)//Primera carga de la página
			{
				string Id = Request.QueryString["Id"];
				if ((string.IsNullOrEmpty(Id)) || (!IsNumeric(Id)))
				{
					Response.Redirect("ListaChoferes.aspx");
				}
				else
				{
					ChoferVO Chofer = BLLChofer.GetChoferById(int.Parse(Id));
					if (Chofer.IdChofer == int.Parse(Id))
					{
						lblIdChofer.Text = Chofer.IdChofer.ToString();
						txtNombre.Text = Chofer.Nombre;
						txtApPaterno.Text = Chofer.ApPaterno.ToString();
						txtApMaterno.Text = Chofer.ApMaterno.ToString();
						txtTelefono.Text = Chofer.Telefono.ToString();
						inFechaNacimiento.Value = Chofer.FechaNacimiento.ToString();
						txtLicencia.Text = Chofer.Licencia.ToString();
						imgFotoChofer.ImageUrl = Chofer.Urlfoto;
						UrlFoto.InnerText = Chofer.Urlfoto;
						chkDisponibilidad.Checked = Chofer.Disponibilidad;
					}
					else
					{
						Response.Redirect("ListaChoferes.aspx");
					}
				}
			}
		}

		private bool IsNumeric(string id)
		{
			float output;
			return float.TryParse(id, out output);
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
						string PathDir = Server.MapPath("~/Imagenes/Choferes/");
						if (!Directory.Exists(PathDir))
						{
							//Creamos el directorio
							Directory.CreateDirectory(PathDir);
						}
						//Guardamos el archivo 
						SubeImagen.PostedFile.SaveAs(PathDir + FileName);
						string urlfoto = "/Imagenes/Choferes/" + FileName;
						UrlFoto.InnerText = urlfoto;
						imgFotoChofer.ImageUrl = urlfoto;
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
				int IdChofer = int.Parse(lblIdChofer.Text);
				string Nombre = txtNombre.Text;
				string ApPaterno = txtApPaterno.Text;
				string ApMaterno = txtApMaterno.Text;
				string Telefono = txtTelefono.Text;
				DateTime FechaNacimiento = DateTime.Parse(inFechaNacimiento.Value);
				string Licencia = txtLicencia.Text;
				string urlFoto = UrlFoto.InnerText;
				bool Dispobilidad = chkDisponibilidad.Checked;

				string Resultado = BLLChofer.UpdChofer(IdChofer, Nombre, ApPaterno, ApMaterno, Telefono, FechaNacimiento, Licencia, Dispobilidad, urlFoto);

				if (Resultado.IndexOf("Chofer Actualizado") > -1)
					Util.Library.UtilControls.SweetBoxConfirm("OK!", Resultado, "success", "/Catalogos/Choferes/ListaChoferes.aspx", this.Page, this.GetType());
				else
					Util.Library.UtilControls.SweetBox("Atención!", Resultado, "warning", this.Page, this.GetType());
			}
			catch (Exception ex)
			{
				Util.Library.UtilControls.SweetBox("ERROR!", ex.Message, "danger", this.Page, this.GetType());
			}
		}
	}
}