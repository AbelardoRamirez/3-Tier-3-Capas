<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaCamiones.aspx.cs" Inherits="_3_Capas.Catalogos.Camiones.ListaCamiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="container">
		<div class="row">
			<div class="col-12">
				<h3>Lista Camiones</h3>
			</div>
		</div>
		<div class="row">
			<div class="col-12">
				<asp:GridView runat="server"
					ID="GVCamiones"
					CssClass="table table-responsive table-bordered table-striped table-hover bg-dark"
					AutoGenerateColumns="false"
					OnRowEditing="GVCamiones_RowEditing"
					OnRowCancelingEdit="GVCamiones_RowCancelingEdit"
					OnRowUpdating="GVCamiones_RowUpdating"
					OnRowDeleting="GVCamiones_RowDeleting"
					OnRowCommand="GVCamiones_RowCommand"
					DataKeyNames="IdCamion">
					<Columns>
						<asp:BoundField HeaderText="Id" DataField="IdCamion" ReadOnly="true" SortExpression="IdChofer" />
						<asp:ImageField HeaderText="Fotografía" ReadOnly="true" ControlStyle-Width="160px" DataImageUrlField="UrlFoto"></asp:ImageField>
						<asp:BoundField HeaderText="Matricula" DataField="Matricula" />
						<asp:TemplateField HeaderText="Tipo Camión"
							ControlStyle-Width="150px">
							<ItemTemplate>
								<asp:Label ID="lblTipoCamion" Text='<%#Eval("TipoCamion")%>' runat="server"></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:DropDownList ID="DDLTipoCamion" CssClass="form-control" runat="server"></asp:DropDownList>
							</EditItemTemplate>
						</asp:TemplateField>
						<asp:BoundField
							HeaderText="Modelo"
							DataField="Modelo" />
						<asp:BoundField
							HeaderText="Marca"
							DataField="Marca" />
						<asp:BoundField
							HeaderText="Capacidad"
							DataField="Capacidad" />
						<asp:BoundField
							HeaderText="Kilometraje"
							DataField="Kilometraje" />
						<asp:CheckBoxField
							HeaderText="Disponibilidad"
							DataField="Disponibilidad" />
						<asp:ButtonField Text="Seleccionar" CommandName="Select" ButtonType="Button" ControlStyle-CssClass="btn btn-primary" />
						<asp:CommandField ShowEditButton="true" EditText="Editar" ControlStyle-CssClass="btn btn-secondary" />
						<asp:CommandField ShowDeleteButton="true" DeleteText="Eliminar" ControlStyle-CssClass="btn btn-danger" />
					</Columns>
					<EmptyDataTemplate>
						<h1 class="text-danger">No hay camiones registrados aun!</h1>
						<p>Presiona <a href="/Catalogos/Camiones/AltaCamion" class="btn btn-primary">Aquí</a> para registrar uno</p>
					</EmptyDataTemplate>
				</asp:GridView>
				<a href="AltaCamion" class="btn btn-info">Agregar nuevo camión</a>
			</div>
		</div>
	</div>
</asp:Content>
