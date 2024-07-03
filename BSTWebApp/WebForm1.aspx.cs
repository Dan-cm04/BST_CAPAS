using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ClassBts;
using Newtonsoft.Json;

namespace BSTWebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static BST<string> bst = new BST<string>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DibujarArbol(Nodo<string> raiz)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "dibujarArbol", $"DibujarArbol({JsonConvert.SerializeObject(raiz)});", true);
        }

        // INSERTAR
        protected void Insertar_Click(object sender, EventArgs e)
        {
            string fecha = Request.Form["fecha"];
            string matricula = Request.Form["matricula"];
            string asistencia = Request.Form["asistencia"];
            string valor = $"{fecha},{matricula},{asistencia}";

            bst.Insertar(valor);
            mensaje.Text = "Se ha insertado correctamente.";

            // Dibujar el árbol después de la inserción
            DibujarArbol(bst.Raiz);

            // Mostrar recorridos
            MostrarRecorridos();
        }

        // MOSTRAR RECORRIDOS
        protected void MostrarRecorridos()
        {
            MostrarRecorridosInOrden();
            MostrarRecorridosPreOrden();
            MostrarRecorridosPostOrden();
            MostrarRecorridosPorNiveles();
        }

        protected void MostrarRecorridosInOrden()
        {
            List<string> resultado = new List<string>();
            bst.InOrden(bst.Raiz, resultado);
            InOrdenDropDownList.Items.Clear();
            foreach (var item in resultado)
            {
                InOrdenDropDownList.Items.Add(new ListItem(item));
            }
        }

        protected void MostrarRecorridosPreOrden()
        {
            List<string> resultado = new List<string>();
            bst.PreOrden(bst.Raiz, resultado);
            PreOrdenDropDownList.Items.Clear();
            foreach (var item in resultado)
            {
                PreOrdenDropDownList.Items.Add(new ListItem(item));
            }
        }

        protected void MostrarRecorridosPostOrden()
        {
            List<string> resultado = new List<string>();
            bst.PostOrden(bst.Raiz, resultado);
            PostOrdenDropDownList.Items.Clear();
            foreach (var item in resultado)
            {
                PostOrdenDropDownList.Items.Add(new ListItem(item));
            }
        }

        protected void MostrarRecorridosPorNiveles()
        {
            List<string> resultado = new List<string>();
            bst.PorNiveles(bst.Raiz, resultado);
            PorNivelesDropDownList.Items.Clear();
            foreach (var item in resultado)
            {
                PorNivelesDropDownList.Items.Add(new ListItem(item));
            }
        }

        protected void InOrdenDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = InOrdenDropDownList.SelectedItem.Value;
            mensaje.Text = FormatearDatos(selectedValue);
        }

        protected void PreOrdenDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = PreOrdenDropDownList.SelectedItem.Value;
            mensaje.Text = FormatearDatos(selectedValue);
        }

        protected void PostOrdenDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = PostOrdenDropDownList.SelectedItem.Value;
            mensaje.Text = FormatearDatos(selectedValue);
        }

        protected void PorNivelesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = PorNivelesDropDownList.SelectedItem.Value;
            mensaje.Text = FormatearDatos(selectedValue);
        }

        // BUSCAR
        protected void Buscar_Click(object sender, EventArgs e)
        {
            string matricula = buscarMatricula.Value.Trim();
            var resultado = bst.Buscar(matricula);
            if (resultado != null)
            {
                mensaje.Text = FormatearDatos(resultado.Valor);
            }
            else
            {
                mensaje.Text = "No se ha encontrado la matrícula.";
            }
        }

        // ELIMINAR
        protected void Eliminar_Click(object sender, EventArgs e)
        {
            string matricula = eliminarMatricula.Value.Trim();
            var resultado = bst.Buscar(matricula);
            if (resultado != null)
            {
                bst.Eliminar(matricula);
                mensaje.Text = "Valor eliminado correctamente.";

                // Mostrar el árbol actualizado después de la eliminación
                DibujarArbol(bst.Raiz);

                // Mostrar recorridos
                MostrarRecorridos();
            }
            else
            {
                mensaje.Text = "No se ha encontrado la matrícula.";
            }
        }

        // OBTENER MINIMO
        protected void ObtenerMinimo_Click(object sender, EventArgs e)
        {
            try
            {
                var min = bst.Minimo();
                mensaje.Text = "Valor mínimo: " + FormatearDatos(min);
            }
            catch (InvalidOperationException)
            {
                mensaje.Text = "El árbol está vacío.";
            }
        }

        // OBTENER MAXIMO
        protected void ObtenerMaximo_Click(object sender, EventArgs e)
        {
            try
            {
                var max = bst.Maximo();
                mensaje.Text = "Valor máximo: " + FormatearDatos(max);
            }
            catch (InvalidOperationException)
            {
                mensaje.Text = "El árbol está vacío.";
            }
        }

        private string FormatearDatos(string datos)
        {
            var partes = datos.Split(',');
            if (partes.Length < 3)
            {
                return "Datos incompletos para formatear.";
            }

            string fechaStr = partes[0].Trim();
            DateTime fecha;
            if (!DateTime.TryParseExact(fechaStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fecha))
            {
                return "Fecha inválida.";
            }

            string matricula = partes[1].Trim();
            string asistencia = partes[2].Trim();

            return $"Fecha: {fecha:dd/MM/yyyy}, Matrícula: {matricula}, Asistencia: {asistencia}";
        }

        // BALANCEAR
        protected void Balancear_Click(object sender, EventArgs e)
        {
            // Llamar al método Balancear para balancear el árbol
            Balancear();

        }

        public void Balancear()
        {
            List<string> elementos = new List<string>();
            EnOrden(bst.Raiz, elementos);

            bst.Raiz = ConstruirBalanceado(elementos, 0, elementos.Count - 1);

            // Dibujar el árbol balanceado
            DibujarArbol(bst.Raiz);
        }

        private Nodo<string> ConstruirBalanceado(List<string> elementos, int inicio, int fin)
        {
            if (inicio > fin)
                return null;

            int medio = (inicio + fin) / 2;
            Nodo<string> nuevoNodo = new Nodo<string>(elementos[medio]);

            nuevoNodo.Izquierdo = ConstruirBalanceado(elementos, inicio, medio - 1);
            nuevoNodo.Derecho = ConstruirBalanceado(elementos, medio + 1, fin);

            return nuevoNodo;
        }

        private void EnOrden(Nodo<string> nodo, List<string> elementos)
        {
            if (nodo == null)
                return;

            EnOrden(nodo.Izquierdo, elementos);
            elementos.Add(nodo.Valor);
            EnOrden(nodo.Derecho, elementos);
        }

        // OBTENER ÁRBOL COMO JSON
        public void ObtenerArbol()
        {
            var json = JsonConvert.SerializeObject(ObtenerDatosDelNodo(bst.Raiz));
            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();
        }

        private object ObtenerDatosDelNodo(Nodo<string> nodo)
        {
            if (nodo == null) return null;

            var datos = nodo.Valor.Split(',');
            var fecha = DateTime.Parse(datos[0]);
            var matricula = datos[1];
            var asistencia = datos[2] == "True";

            return new
            {
                valor = new
                {
                    Fecha = fecha.ToString("o"),
                    Matricula = matricula,
                    Asistencia = asistencia ? "True" : "False"
                },
                izquierdo = ObtenerDatosDelNodo(nodo.Izquierdo),
                derecho = ObtenerDatosDelNodo(nodo.Derecho)
            };
        }
    }
}
