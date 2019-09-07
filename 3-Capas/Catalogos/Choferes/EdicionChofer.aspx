<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EdicionChofer.aspx.cs" Inherits="_3_Capas.Catalogos.Choferes.EdicionChofer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="container">
		<div class="row">
			<div class="col-md-12">
				<h3>Editar Chofer:
					<asp:Label ID="lblIdChofer" runat="server"></asp:Label></h3>
			</div>
		</div>
		<div class="row">
			<div class="col-md-12">
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
					<ajaxToolkit:MaskedEditExtender ID="MEEtxtTelefono" TargetControlID="txtTelefono" Mask="(999) 999-9999" ClearMaskOnLostFocus="false" runat="server" />
				</div>
				<div class="form-group">
					<label for="<%= inFechaNacimiento.ClientID %>">Fecha de nacimiento</label>
					<input type="text" name="inFechaNacimiento" id="inFechaNacimiento" class="form-control" runat="server" placeholder="d/m/y" />
				</div>
				<div class="form-group">
					<label for="<%= txtLicencia.ClientID %>">Licencia</label>
					<asp:TextBox placeholder="Licencia" CssClass="form-control" ID="txtLicencia" runat="server" />
					<asp:RequiredFieldValidator CssClass="text-danger" Display="Dynamic" ErrorMessage="Campo requerido" ControlToValidate="txtLicencia" runat="server" />
					<asp:RegularExpressionValidator ValidationExpression="^[a-zA-Z]{1}-[0-9]{5}$" Display="Dynamic" CssClass="text-danger" ErrorMessage="El formato de licenciia es X-00000" ControlToValidate="txtLicencia" runat="server" />
				</div>
				<div class="form-group">
					<div class="checkbox" style="margin: 30px;">
						<label>
							<asp:CheckBox ID="chkDisponibilidad" runat="server" />
							Disponibilidad
						</label>
					</div>
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
							<asp:Button CssClass="btn btn-primary btn-xs" ID="btnCargarImagen" Text="Subir" runat="server" OnClick="btnSubeImagen_Click" OnClientClick="MostrarFoto();" />
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
			<div class="col-md-6 col-md-offset-6">
				<div class="form-group">
					<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
				</div>
			</div>
		</div>
	</div>
	<script>
		$(document).ready(function () {
			if ($("#<%=UrlFoto.ClientID%>").html() != "") {
				$("#imgFoto").show();
			}


			$("#<%=SubeImagen.ClientID%>").on('change', function () {
				var label = $(this)["0"].files["0"].name;
				$("#InfoImg").val(label);
			});
			$.datetimepicker.setLocale('es');//Declararlo en Español

			$('#<%= inFechaNacimiento.ClientID%>').datetimepicker({//Asignamos el calendario a los input de fecha
				format: 'd/m/y'
			});
		});
	</script>
	<script>
		function MuestraFoto() {
			$("#imgFoto").show();
			return true;
		}
	</script>
</asp:Content>
