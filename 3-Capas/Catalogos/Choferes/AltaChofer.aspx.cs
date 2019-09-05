using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3_Capas.Catalogos.Choferes
{
	public partial class AltaChofer : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnCargarImagen_Click(object sender, EventArgs e)
		{
			try
			{
				if (SubeImagen.Value != "")
				{
					string FileName = Path.GetFileName(SubeImagen.PostedFile.FileName);
					string FileExt = Path.GetExtension(FileName.ToLower());
					if ((FileExt != ".jpg") && (FileExt != ".png"))
					{
						Util.Library.UtilControls.SweetBox("Atención!", "Seleccione un archivo en fomato .jpg|.png", "warning", this.Page, this.GetType());
					}
					else
					{
						string PathDir = Server.MapPath("~/Imagenes/Choferes/");
						if (!Directory.Exists(PathDir))
						{
							Directory.CreateDirectory(PathDir);
						}

						SubeImagen.PostedFile.SaveAs(PathDir + FileName);
						string urlFoto = "/Imagenes/Choferes/" + FileName;
						UrlFoto.InnerText = urlFoto;
						imgFotoChofer.ImageUrl = urlFoto;
						btnGuardar.Visible = true;
					}
				}
				else
				{
					Util.Library.UtilControls.SweetBox("Atención!", "Seleccione un archivo válido", "warning", this.Page, this.GetType());
				}
			}
			catch (Exception ex)
			{
				Util.Library.UtilControls.SweetBox("Error!", ex.Message, "error", this.Page, this.GetType());
			}
		}

		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				string Nombre = txtNombre.Text;
				string ApPaterno = txtApPaterno.Text;
				string ApMaterno = txtApMaterno.Text;
				string Telefono = txtTelefono.Text;
				DateTime FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text);
				string Licencia = txtLicencia.Text;
				string urlFoto = UrlFoto.InnerText;
				string Resultado = BLL.BLLChofer.InsChofer(Nombre, ApPaterno, ApMaterno, Telefono, FechaNacimiento, Licencia, urlFoto);
				if (Resultado.IndexOf("Chofer agregado") > -1)
					Util.Library.UtilControls.SweetBoxConfirm("OK!", Resultado, "success", "/Catalogos/Choferes/ListaChoferes.aspx", this.Page, this.GetType());
				else
					Util.Library.UtilControls.SweetBox("Atención!", Resultado, "warning", this.Page, this.GetType());
			}
			catch (Exception ex)
			{
				Util.Library.UtilControls.SweetBox("Error!", ex.Message, "error", this.Page, this.GetType());
			}
		}
	}
}