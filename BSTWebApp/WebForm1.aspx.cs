using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ClassBts; // Asegúrate de que la referencia a tu clase BST esté correctamente importada

namespace BSTWebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static BST<string> bst = new BST<string>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

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

        protected void Buscar_Click(object sender, EventArgs e)
{
    string matricula = buscarMatricula.Value.Trim(); // Eliminar espacios en blanco
    var resultado = bst.Buscar(matricula); // Llamada correcta al método Buscar
    if (resultado != null)
    {
        mensaje.Text = FormatearDatos(resultado.Valor);
    }
    else
    {
        mensaje.Text = "No se ha encontrado la matrícula.";
    }
}

protected void Eliminar_Click(object sender, EventArgs e)
{
    string matricula = eliminarMatricula.Value.Trim(); // Eliminar espacios en blanco
    var resultado = bst.Buscar(matricula); // Llamada correcta al método Buscar
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

private string FormatearDatos(string datos)
{
    var partes = datos.Split(',');
    return $"Fecha: {partes[0]}, Matricula: {partes[1]}, Asistencia: {partes[2]}";
}




        protected void DibujarArbol(Nodo<string> raiz)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "dibujarArbol", $"DibujarArbol({Newtonsoft.Json.JsonConvert.SerializeObject(raiz)});", true);
        }


        protected void ObtenerMinimo_Click(object sender, EventArgs e)
        {
            try
            {
                var min = bst.Minimo();
                mensaje.Text = "Valor mínimo: " + min;
            }
            catch (InvalidOperationException)
            {
                mensaje.Text = "El árbol está vacío.";
            }
        }

        protected void ObtenerMaximo_Click(object sender, EventArgs e)
        {
            try
            {
                var max = bst.Maximo();
                mensaje.Text = "Valor máximo: " + max;
            }
            catch (InvalidOperationException)
            {
                mensaje.Text = "El árbol está vacío.";
            }
        }

        protected void InOrden_Click(object sender, EventArgs e)
        {
            MostrarRecorridos();
        }

        protected void PreOrden_Click(object sender, EventArgs e)
        {
            MostrarRecorridos();
        }

        protected void PostOrden_Click(object sender, EventArgs e)
        {
            MostrarRecorridos();
        }

        protected void PorNiveles_Click(object sender, EventArgs e)
        {
            MostrarRecorridos();
        }

        protected void InOrdenDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = InOrdenDropDownList.SelectedItem.Value;
            mensaje.Text = selectedValue;
        }

        protected void PreOrdenDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = PreOrdenDropDownList.SelectedItem.Value;
            mensaje.Text = selectedValue;
        }

        protected void PostOrdenDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = PostOrdenDropDownList.SelectedItem.Value;
            mensaje.Text = selectedValue;
        }

        protected void PorNivelesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = PorNivelesDropDownList.SelectedItem.Value;
            mensaje.Text = selectedValue;
        }


        protected void MostrarRecorridos()
        {
            // Mostrar recorridos inorden
            List<string> resultado = new List<string>();
            bst.InOrden(bst.Raiz, resultado);
            InOrdenDropDownList.Items.Clear();
            foreach (var item in resultado)
            {
                InOrdenDropDownList.Items.Add(new ListItem(item));
            }

            // Mostrar recorridos preorden
            resultado.Clear();
            bst.PreOrden(bst.Raiz, resultado);
            PreOrdenDropDownList.Items.Clear();
            foreach (var item in resultado)
            {
                PreOrdenDropDownList.Items.Add(new ListItem(item));
            }

            // Mostrar recorridos postorden
            resultado.Clear();
            bst.PostOrden(bst.Raiz, resultado);
            PostOrdenDropDownList.Items.Clear();
            foreach (var item in resultado)
            {
                PostOrdenDropDownList.Items.Add(new ListItem(item));
            }

            // Mostrar recorridos por niveles
            resultado.Clear();
            bst.PorNiveles(bst.Raiz, resultado);
            PorNivelesDropDownList.Items.Clear();
            foreach (var item in resultado)
            {
                PorNivelesDropDownList.Items.Add(new ListItem(item));
            }
        }

    }
}
