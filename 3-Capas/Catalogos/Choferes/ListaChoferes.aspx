<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaChoferes.aspx.cs" Inherits="_3_Capas.Catalogos.Choferes.ListaChoferes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="container">
		<div class="row">
			<div class="col">
				<h1>Lista Choferes</h1>
			</div>
		</div>
		<div class="row">
			<div class="col">
				<asp:GridView runat="server"
					ID="GVChoferes"
					DataKeyNames="IdChofer"
					AutoGenerateColumns="false"
					OnRowDeleting="GVChoferes_RowDeleting"
					OnRowUpdating="GVChoferes_RowUpdating"
					OnRowEditing="GVChoferes_RowEditing"
					OnRowCommand="GVChoferes_RowCommand"
					OnRowCancelingEdit="GVChoferes_RowCancelingEdit"
					CssClass="table table-striped table-hover">
					<Columns>
						<asp:BoundField ControlStyle-CssClass="form-control" DataField="IdChofer" ReadOnly="true" HeaderText="ID" />
						<asp:TemplateField HeaderText="Fotografía">
							<ItemTemplate>
								<img src='<%# Eval("UrlFoto") %>' alt="Alternate Text" width="150" height="150" class="img-responsive" />
							</ItemTemplate>
						</asp:TemplateField>
						<asp:BoundField ControlStyle-CssClass="form-control" DataField="Nombre" SortExpression="Nombre" HeaderText="Nombre" />
						<asp:BoundField ControlStyle-CssClass="form-control" DataField="ApPaterno" SortExpression="ApPaterno" HeaderText="Apellido Paterno" />
						<asp:BoundField ControlStyle-CssClass="form-control" DataField="ApMaterno" SortExpression="ApPMaterno" HeaderText="Apellido Materno" />
						<asp:TemplateField HeaderText="Telefono" SortExpression="Telefono">
							<ItemTemplate>
								<asp:Label Text='<%# Eval("Telefono") %>' runat="server" />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox Text='<%# Bind("Telefono") %>' runat="server" CssClass="form-control" ID="txtTelefono" />
								<asp:RequiredFieldValidator ErrorMessage="Campo requerido" CssClass="text-danger" ControlToValidate="txtTelefono" runat="server" />
								<asp:RegularExpressionValidator ValidationExpression="^\([0-9]{3}\)\s[0-9]{3}-[0-9]{4}$" ErrorMessage="Debe de tener el formato: (123) 456-7890" ControlToValidate="txtTelefono" runat="server" CssClass="text-danger" />
							</EditItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Fecha de nacimiento">
							<ItemTemplate>
								<asp:Label Text='<%# Eval("FechaNacimiento") %>' runat="server" />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox Text='<%# Bind("FechaNacimiento") %>' runat="server" ID="txtFechaNacimiento" />
								<asp:RequiredFieldValidator ErrorMessage="Campo requerido" CssClass="text-danger" ControlToValidate="txtFechaNacimiento" runat="server" />
								<asp:RegularExpressionValidator ValidationExpression="^[0-9]{2}/[0-9]{2}/[0-9]{4}$" ErrorMessage="Debe de tener el formato: YYYY/MM/DD" ControlToValidate="txtFechaNacimiento" runat="server" CssClass="text-danger" />
							</EditItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Licencia">
							<ItemTemplate>
								<asp:Label Text='<%# Eval("Licencia") %>' runat="server" />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox Text='<%# Bind("Licencia") %>' runat="server" CssClass="form-control" ID="txtLicencia" />
								<asp:RequiredFieldValidator ErrorMessage="Campo requerido" CssClass="text-danger" ControlToValidate="txtLicencia" runat="server" />
								<asp:RegularExpressionValidator ValidationExpression="^[a-zA-Z]{1}-[0-9]{5}$" ErrorMessage="La Licencia debe de ser x-12345" ControlToValidate="txtLicencia" runat="server" CssClass="text-danger" />
							</EditItemTemplate>
						</asp:TemplateField>
						<asp:CheckBoxField HeaderText="Disponibilidad" DataField="Disponibilidad" SortExpression="Disponibilidad" />
						<asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Opciones" ControlStyle-CssClass="btn btn-success" Text="Seleccionar" />
						<asp:CommandField ShowEditButton="true" EditText="Editar" ControlStyle-CssClass="btn btn-info" HeaderText="Edición" />
						<asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />

					</Columns>
					<EmptyDataTemplate>
						<h1 class="text-danger">No hay choferes registrados aun!</h1>
						<p>Presiona <a href="/Catalogos/Choferes/AltaChofer" class="btn btn-primary">Aquí</a> para registrar uno</p>
					</EmptyDataTemplate>
				</asp:GridView>
			</div>
		</div>
	</div>
</asp:Content>
