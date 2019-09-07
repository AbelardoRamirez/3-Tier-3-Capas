<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnProceso.aspx.cs" Inherits="_3_Capas.Ruta.EnProceso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />
    <script src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>
    <div class="container">
        <div class="row">
            <h3>Rutas en proceso</h3>
        </div>
        <div class="row">
            <div class="col-md-12">
                <table id="grid" class="display">
                    <thead>
                        <tr>
                            <th>Ruta</th>
                            <th>Chofer</th>
                             <th>Licencia</th>
                             <th>Foto Chofer</th>
                             <th>Camion</th>
                             <th>Foto Camión</th>
                             <th>Origen</th>
                             <th>Destino</th>
                             <th>Salida</th>
                             <th>Llegada Estimada</th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <th>Ruta</th>
                            <th>Chofer</th>
                             <th>Licencia</th>
                             <th>Foto Chofer</th>
                             <th>Camion</th>
                             <th>Foto Camión</th>
                             <th>Origen</th>
                             <th>Destino</th>
                             <th>Salida</th>
                             <th>Llegada Estimada</th>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {

            function renderTable(result)
            {
                var dtData = [];
                $.each(result, function () {
                    if (this.ATiempo)
                    {
                        this.ATiempo = "A Tiempo";
                    }
                    else {
                        this.ATiempo="Demorado"
                    }

                    var FSalida = moment(this.Salida).format("DD/MM/YYYY HH:mm");
                    var FLlegada = moment(this.Llegada).format("DD/MM/YYYY HH:mm");

                    dtData.push([
                        this.IdRuta,
                        this.Nombre,
                        this.Licencia,
                        "<img src='" + this.FotoCH + "' width='120'/>",
                        this.Matricula,
                        "<img src='" + this.FotoCamion + "' width='120'/>",
                        this.Origen,
                        this.Destino,
                        FSalida,
                        FLlegada
                    ]);

                });

                $("#grid").dataTable({
                    'aaData': dtData,
                    'bPaginate': true,
                    'bInfo': true,
                    'searching': true,
                    'ordering':true
                });
            }

            $.ajax({
                type: "GET",
                async: true,
                url: "../autocompletar.asmx/GetLstRutas",
                data: { 'Estatus': 1 },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) { renderTable(response.d); },
                error: function (errMsg)
                { swal("Error!", errMsg.responseText.toString(), "error") }
            });

        });
    </script>
</asp:Content>
