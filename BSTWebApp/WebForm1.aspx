<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="BSTWebApp.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <title>Árbol Binario de Búsqueda</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
   
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/fabric.js/4.5.0/fabric.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>

    <script src="script.js"></script>

</head>
<body>
    <br />
    <h2 class="text-center">Árbol Binario de Búsqueda</h2>

    
    <form id="form1" runat="server" class="container mt-4">
        <div class="msg-container">
             <asp:TextBox ID="mensaje" runat="server" class="msg" ReadOnly="true"></asp:TextBox>
        </div>
        <br />
        <div class="row">
        
            <div class="col-md-6">

                <!-- Insertar Datos -->
                <div class="form-group">
                    <h3 class="text-center">Insertar Datos</h3>
                    <div class="form-row">
                        <label for="fecha">Fecha:</label>
                        <input type="date" id="fecha" runat="server" class="form-control" />
                    </div>
                    <br />
                    <div class="form-row">
                        <label for="matricula">Matrícula:</label>
                        <input type="text" id="matricula" runat="server" class="form-control" />
                    </div>
                    <br />
                    <div class="form-row">
                        <label for="asistencia">Asistencia:</label>
                        <br />
                        <select id="asistencia" runat="server" class="form-control">
                            <option value="true">True</option>
                            <option value="false">False</option>
                        </select>
                    </div>
                    <br />
                    <div class="text-center">
                        <asp:Button ID="btnInsertar" runat="server" Text="Insertar" OnClick="Insertar_Click" CssClass="btn btn-primary " style="width: 50%;" />
                    </div>
                </div>
                <br />

                <!-- Buscar Datos por Matrícula -->
                <div class="form-group">
                    <h3 class="text-center">Buscar Datos por Matrícula</h3>
                    <div class="form-row">
                        <label for="buscarMatricula">Matrícula:</label>
                        <input type="text" id="buscarMatricula" runat="server" class="form-control" />
                    </div>
                    <br />
                    <div class="text-center">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="Buscar_Click" CssClass="btn btn-primary " style="width: 50%;" />
                    </div>
                </div>

                <!-- Obtener Mínimo y Máximo -->
                <div class="form-group">
                    <h3 class="text-center">Obtener Mínimo y Máximo</h3>
                    <div class="text-center">
                        <asp:Button ID="btnMinimo" runat="server" Text="Obtener Mínimo" OnClick="ObtenerMinimo_Click" CssClass="btn btn-info " />
                        <asp:Button ID="btnMaximo" runat="server" Text="Obtener Máximo" OnClick="ObtenerMaximo_Click" CssClass="btn btn-info " />
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <!-- Recorridos -->
                <div class="form-group">
                    <h3 class="text-center">Recorridos</h3>
                    <div class="form-row">
                        <label for="InOrdenDropDownList">InOrden</label>
                        <br />
                        <asp:DropDownList ID="InOrdenDropDownList" runat="server" CssClass="form-control" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="InOrdenDropDownList_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <br />
                    <div class="form-row">
                        <label for="PreOrdenDropDownList">PreOrden</label>
                        <br />
                        <asp:DropDownList ID="PreOrdenDropDownList" runat="server" CssClass="form-control" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="PreOrdenDropDownList_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <br />
                    <div class="form-row">
                        <label for="PostOrdenDropDownList">PostOrden</label>
                        <br />
                        <asp:DropDownList ID="PostOrdenDropDownList" runat="server" CssClass="form-control" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="PostOrdenDropDownList_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <br />
                    <div class="form-row">
                        <label for="PorNivelesDropDownList">Por niveles</label>
                        <br />
                        <asp:DropDownList ID="PorNivelesDropDownList" runat="server" CssClass="form-control" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="PorNivelesDropDownList_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <!-- Eliminar Datos por Matrícula -->
                <div class="form-group">
                    <h3 class="text-center">Eliminar Datos por Matrícula</h3>
                    <div class="form-row">
                        <label for="eliminarMatricula">Matrícula:</label>
                        <input type="text" id="eliminarMatricula" runat="server" class="form-control" />
                    </div>
                    <br />
                    <div class="text-center">
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="Eliminar_Click" CssClass="btn btn-danger" style="width: 50%;" />
                    </div>
                </div>

                <!-- Balancear Árbol -->
                <div class="form-group">
                    <h3 class="text-center">Balancear Árbol</h3>
                    <div class="text-center">
                        <asp:Button ID="btnBalancear" runat="server" Text="Balancear" OnClick="Balancear_Click" CssClass="btn btn-secondary " />
                    </div>
                </div>
            </div>
        </div>

        <!-- GRAFICAR-->
        <div>
            <h3 class="text-center">Representación Gráfica</h3>
            <br />
            <canvas id="arbolCanvas" width="1120" height="800" class="border border-dark"></canvas>
        </div>
    </form>

    <style>
    .msg-container {
        display: flex;
        justify-content: center;
        width: 100%;
    }

    .msg {
        padding: .375rem .75rem;
        font-size: 1rem;
        line-height: 1.5;
        color: #495057;
        background-color: #fff;
        background-clip: padding-box;
        border: 1px solid #ced4da;
        border-radius: .25rem;
        transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;
        max-width: 1100px;
        width: 50%;
    }

    </style>

</body>
</html>
