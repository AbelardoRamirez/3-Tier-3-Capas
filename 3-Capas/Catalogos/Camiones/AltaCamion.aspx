<%@ Page Title="Alta Camión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaCamion.aspx.cs" Inherits="_3_Capas.Catalogos.Camiones.AltaCamion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="container">
		<div class="row">
			<div class="col-md-12">
				<h3>Alta Camión</h3>
			</div>
		</div>
		<div class="row">
			<div class="col-md-12">
				<%-- Matricula --%>
				<div class="form-group">
					<label for="<%= txtMatricula.ClientID %>">Matricula</label>
					<asp:TextBox ID="txtMatricula" CssClass="form-control" runat="server" />
					<asp:RequiredFieldValidator ID="RFVMatricula" ErrorMessage="Matricula Requerida" Display="Dynamic" CssClass="text-danger" ControlToValidate="txtMatricula" runat="server" />
					<asp:RegularExpressionValidator Display="Dynamic" ID="REVMatricula" ErrorMessage="El formato de la matrícula debe de ser XXX-0000" ControlToValidate="txtMatricula" runat="server" ValidationExpression="^[a-zA-Z]{3}-[0-9]{4}$" CssClass="text-danger" />
				</div>

				<%-- Tipo de Cliente --%>
				<div class="form-group">
					<label for="<%= DDLTipoCamion.ClientID %>">Tipo Camión</label>
					<asp:DropDownList runat="server" ID="DDLTipoCamion" CssClass="form-control">
					</asp:DropDownList>
					<asp:RequiredFieldValidator ID="RFVTipoCamion" CssClass="text-danger" ErrorMessage="Selecciona el tipo de camión" ControlToValidate="DDLTipoCamion" runat="server" Display="Dynamic" />
				</div>

				<%-- Tipo de Modelo --%>
				<div class="form-group">
					<label for="<%= DDLModelo.ClientID %>">Modelo</label>
					<asp:DropDownList runat="server" ID="DDLModelo" CssClass="form-control">
					</asp:DropDownList>
					<asp:RequiredFieldValidator ID="RFVModelo" CssClass="text-danger" ErrorMessage="Selecciona el modelo de camión" ControlToValidate="DDLModelo" runat="server" Display="Dynamic" />
				</div>

				<%-- Tipo de Modelo --%>
				<div class="form-group">
					<label for="<%= DDLMarca.ClientID %>">Marca</label>
					<asp:DropDownList runat="server" ID="DDLMarca" CssClass="form-control">
					</asp:DropDownList>
					<asp:RequiredFieldValidator ID="RFVMarca" CssClass="text-danger" ErrorMessage="Selecciona la marca del camión" ControlToValidate="DDLMarca" runat="server" Display="Dynamic" />
				</div>

				<%-- Capacidad --%>
				<div class="form-group">
					<label for="<%= txtCapacidad.ClientID %>">Capacidad</label>
					<asp:TextBox CssClass="form-control" ID="txtCapacidad" runat="server" />
					<asp:RequiredFieldValidator CssClass="text-danger" ID="RFVCapacidad" Display="Dynamic" ErrorMessage="Capacidad requerida" ControlToValidate="txtCapacidad" runat="server" />
					<asp:RegularExpressionValidator ValidationExpression="^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$" CssClass="text-danger" ErrorMessage="Capacidad debe de ser numérica ej: 45, 45.00 45.1" ControlToValidate="txtCapacidad" runat="server" ID="REVCapacidad" Display="Dynamic" />
				</div>

				<div class="form-group">
					<label for="<%= txtKilometraje.ClientID %>">Kilometraje</label>
					<asp:TextBox ID="txtKilometraje" runat="server" CssClass="form-control" />
					<asp:RequiredFieldValidator Display="Dynamic" CssClass="text-danger" ErrorMessage="Kilometraje requerido" ControlToValidate="txtKilometraje" runat="server" />
					<asp:RegularExpressionValidator ValidationExpression="^[0-9]*\.?[0-9]+$" CssClass="text-danger" Display="Dynamic" ErrorMessage="Kilometraje debe ser numérico" ControlToValidate="txtKilometraje" runat="server" />
				</div>

				<div class="form-group">
					<label for="">Seleccionar Foto</label>
					<div class="row">
						<div class="col-md-6">
							<div class="input-group">
								<label class="input-group-btn">
									<span class="btn btn-primary">Buscar
										<input type="file" id="SubeImagen" class="btn btn-file" style="display: none;" runat="server" />
									</span>
								</label>
								<input type="text" id="InfoImg" readonly style="background-color: transparent; border: 0; margin-left: 10px;" />
							</div>
						</div>
						<div class="col-md-6">
							<asp:Button ID="btnSubImagen" CssClass="btn btn-primary btn-xs" Text="Subir" runat="server" OnClick="btnSubImagen_Click" OnClientClick="MuestraFoto();" />
						</div>
					</div>
				</div>
				<div class="form-group" id="imgFoto" style="display: none;">
					<label>Foto</label>
					<asp:Image ID="imgFotoCamion" runat="server" Style="width: 150px;" />
					<label id="UrlFoto" runat="server"></label>
				</div>
			</div>
			<%-- EndForm  --%>
		</div>
		<div class="row">
			<div class="col-md-6">
				<asp:Button Text="Guardar" ID="btnGuardar" CssClass="btn btn-success block"
					Visible="false" OnClick="btnGuardar_Click" runat="server" />
			</div>
		</div>
	</div>
	<script>
		$(function () {
			if ($('#<%= UrlFoto.ClientID %>').html() != '') {
				$('#imgFoto').show();
			}

			$('#<%= SubeImagen.ClientID %>').on('change', function () {
				var label = $(this)[0].files["0"].name;
				$('#InfoImg').val(label);
			})
		});
	</script>

	<script>
		function MuestraFoto() {
			$('#imgFoto').show();
			return true;
		}
	</script>
</asp:Content>
