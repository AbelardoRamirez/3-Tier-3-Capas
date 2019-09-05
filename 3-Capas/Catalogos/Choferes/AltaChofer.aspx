<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaChofer.aspx.cs" Inherits="_3_Capas.Catalogos.Choferes.AltaChofer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="container">
		<div class="row">
			<div class="col">
				<h1>Alta Chofer</h1>
				<hr />
			</div>
		</div>
		<div class="row">
			<div class="col">
				<div class="form-group">
					<label for="<%= txtNombre.ClientID %>">Nombre</label>
					<asp:TextBox ID="txtNombre" placeholder="Nombre" runat="server" CssClass="form-control" />
					<asp:RequiredFieldValidator CssClass="text-danger" Display="Dynamic" ErrorMessage="Campo requerido" ControlToValidate="txtNombre" runat="server" />
				</div>
				<div class="form-group">
					<label for="<%= txtApPaterno.ClientID %>">Apellido Paterno</label>
					<asp:TextBox ID="txtApPaterno" placeholder="Apellido Paterno" runat="server" CssClass="form-control" />
					<asp:RequiredFieldValidator CssClass="text-danger" Display="Dynamic" ErrorMessage="Campo requerido" ControlToValidate="txtApPaterno" runat="server" />
				</div>
				<div class="form-group">
					<label for="<%= txtApMaterno.ClientID %>">Apellido Materno</label>
					<asp:TextBox ID="txtApMaterno" placeholder="Apellido Materno" runat="server" CssClass="form-control" />
					<asp:RequiredFieldValidator CssClass="text-danger" Display="Dynamic" ErrorMessage="Campo requerido" ControlToValidate="txtApMaterno" runat="server" />
				</div>
				<div class="form-group">
					<label for="<%= txtTelefono.ClientID %>">Telefono</label>
					<asp:TextBox placeholder="(123)-456-7890" ID="txtTelefono" CssClass="form-control" runat="server" />
					<asp:RequiredFieldValidator ErrorMessage="Campo requerido" ControlToValidate="txtTelefono" runat="server" CssClass="text-danger" Display="Dynamic" />
					<asp:RegularExpressionValidator ValidationExpression="^\([0-9]{3}\)-[0-9]{3}-[0-9]{4}$" ErrorMessage="Telefono debe contener el Siguiente formato (123)-456-7890 " ControlToValidate="txtTelefono" runat="server" CssClass="text-danger" Display="Dynamic" />
				</div>
				<div class="form-group">
					<label for="<%= txtFechaNacimiento.ClientID %>">Fecha de nacimiento</label>
					<asp:TextBox placeholder="AAAA/MM/DD" ID="txtFechaNacimiento" CssClass="form-control" runat="server" />
					<asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Campo requerido" ControlToValidate="txtFechaNacimiento" runat="server" />
				</div>
				<div class="form-group">
					<label for="<%= txtLicencia.ClientID %>">Licencia</label>
					<asp:TextBox placeholder="Licencia" CssClass="form-control" ID="txtLicencia" runat="server" />
					<asp:RequiredFieldValidator CssClass="text-danger" Display="Dynamic" ErrorMessage="Campo requerido" ControlToValidate="txtLicencia" runat="server" />
					<asp:RegularExpressionValidator ValidationExpression="^[\w]{7}$" Display="Dynamic" CssClass="text-danger" ErrorMessage="Debe de contener caracteres de a-z, &amp; 0-9 exactamente 7 veces" ControlToValidate="txtLicencia" runat="server" />
				</div>
				<div class="form-group">
					<label for="">Seleccionar Foto</label>
					<div class="form-group">
						<div class="input-group">
							<label class="input-group-btn">
								<span class="btn btn-primary">Buscar Imagen</span>
								<input type="file" id="SubeImagen" class="btn btn-file" style="display: none;" runat="server" />
							</label>
							<input type="text" id="InfoImg" readonly style="background-color: transparent; border: 0; margin-left: 10px;" />
							<asp:Button CssClass="btn btn-primary btn-xs" ID="btnCargarImagen" Text="Subir" runat="server" OnClick="btnCargarImagen_Click" OnClientClick="MostrarFoto();" />
						</div>
					</div>
				</div>
				<div class="form-group" id="imgFoto" style="display: none;">
					<label for="">Foto</label>
					<asp:Image ID="imgFotoChofer" runat="server" Width="150px" />
					<label id="UrlFoto" runat="server"></label>
				</div>
			</div>
		</div>
		<div class="row">
			<div class="col">
				<asp:Button CssClass="btn btn-success btn-block" ID="btnGuardar" Visible="false" Text="Guardar" runat="server" OnClick="btnGuardar_Click" />
			</div>
		</div>
	</div>

	<script>
		$(function () {

			if ($('#<%= UrlFoto.ClientID%>').html() != '') {
				$('#imgFoto').show();
			}

			$('#<%= SubeImagen.ClientID %>').on('change', function () {
				var label = $(this)[0].files["0"].name;
				$('#InfoImg').val(label);
			});

		})
	</script>
	<script>
		function MostrarFoto() {
			$('#imgFoto').show();
			return true;
		}
	</script>
</asp:Content>
