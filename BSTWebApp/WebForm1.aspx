<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="BSTWebApp.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Árbol Binario de Búsqueda</title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/fabric.js/4.5.0/fabric.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Árbol Binario de Búsqueda</h1>

            <div>
                <!-- Mensajes -->
                <h3>Mensajes</h3>
                <asp:TextBox ID="mensaje" runat="server" ReadOnly="true" Width="400px"></asp:TextBox>
            </div>
            
            <div>
                <!-- Insertar Datos -->
                <h3>Insertar Datos</h3>
                <label>Fecha:</label>
                <input type="date" id="fecha" runat="server" />
                <label>Matrícula:</label>
                <input type="text" id="matricula" runat="server" />
                <label>Asistencia:</label>
                <select id="asistencia" runat="server">
                    <option value="true">True</option>
                    <option value="false">False</option>
                </select>
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" OnClick="Insertar_Click" />
            </div>
            
            <div>
                <!-- Recorridos -->
                <h3>Recorridos</h3>
                <label>InOrden</label>
                <br />
                <asp:DropDownList ID="InOrdenDropDownList" runat="server" Width="400px" AutoPostBack="true" OnSelectedIndexChanged="InOrdenDropDownList_SelectedIndexChanged"></asp:DropDownList>
                <br />
                <label>PreOrden</label>
                <asp:DropDownList ID="PreOrdenDropDownList" runat="server" Width="400px" AutoPostBack="true" OnSelectedIndexChanged="PreOrdenDropDownList_SelectedIndexChanged"></asp:DropDownList>
                <br />
                <label>PostOrden</label>
                <br />
                <asp:DropDownList ID="PostOrdenDropDownList" runat="server" Width="400px" AutoPostBack="true" OnSelectedIndexChanged="PostOrdenDropDownList_SelectedIndexChanged"></asp:DropDownList>
                <br />
                <label>Por niveles</label>
                <asp:DropDownList ID="PorNivelesDropDownList" runat="server" Width="400px" AutoPostBack="true" OnSelectedIndexChanged="PorNivelesDropDownList_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <div>
                <!-- Buscar Datos por Matrícula -->
                <h3>Buscar Datos por Matrícula</h3>
                <label>Matrícula:</label>
                <input type="text" id="buscarMatricula" runat="server" />
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="Buscar_Click" />
            </div>

            <div>
                <!-- Eliminar Datos por Matrícula -->
                <h3>Eliminar Datos por Matrícula</h3>
                <label>Matrícula:</label>
                <input type="text" id="eliminarMatricula" runat="server" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="Eliminar_Click" />
            </div>
            
            <div>
                <!-- Obtener Mínimo y Máximo -->
                <h3>Obtener Mínimo y Máximo</h3>
                <asp:Button ID="btnMinimo" runat="server" Text="Obtener Mínimo" OnClick="ObtenerMinimo_Click" />
                <asp:Button ID="btnMaximo" runat="server" Text="Obtener Máximo" OnClick="ObtenerMaximo_Click" />
            </div>
            
            <div>
                <!-- Visualizar Árbol -->
                <h3>Visualizar Árbol</h3>
                <canvas id="treeCanvas" runat="server" width="800" height="600" style="border: 1px solid #000000;"></canvas>
            </div>
        </div>
    </form>
    <script>
        // Función para dibujar el árbol en el canvas
        function DibujarArbol(treeData) {
            var canvas = document.getElementById("treeCanvas");
            var ctx = canvas.getContext("2d");
            ctx.clearRect(0, 0, canvas.width, canvas.height);

            // Función recursiva para dibujar nodos
            function DibujarNodo(nodo, x, y, xOffset, yOffset) {
                if (!nodo) return;
                ctx.beginPath();
                ctx.arc(x, y, 20, 0, 2 * Math.PI);
                ctx.fillText(nodo.Valor, x - 5, y + 5);
                ctx.stroke();

                if (nodo.Izquierdo) {
                    ctx.moveTo(x, y);
                    ctx.lineTo(x - xOffset, y + yOffset);
                    ctx.stroke();
                    DibujarNodo(nodo.Izquierdo, x - xOffset, y + yOffset, xOffset / 2, yOffset);
                }

                if (nodo.Derecho) {
                    ctx.moveTo(x, y);
                    ctx.lineTo(x + xOffset, y + yOffset);
                    ctx.stroke();
                    DibujarNodo(nodo.Derecho, x + xOffset, y + yOffset, xOffset / 2, yOffset);
                }
            }

            // Dibujar el árbol comenzando desde la raíz
            DibujarNodo(treeData, canvas.width / 2, 30, canvas.width / 4, 30);
        }
    </script>
</body>
</html>

