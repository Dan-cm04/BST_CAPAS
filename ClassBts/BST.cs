using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace ClassBts
{
    public class BST<T> where T : IComparable<T>
    {
        public Nodo<T> Raiz { get; set; }

        public void Insertar(T valor)
        {
            Raiz = Insertar(Raiz, valor);
        }

        //INSERTAR
        private Nodo<T> Insertar(Nodo<T> nodo, T valor)
        {
            if (nodo == null)
                return new Nodo<T>(valor);

            int compare = valor.CompareTo(nodo.Valor);

            if (compare < 0)
                nodo.Izquierdo = Insertar(nodo.Izquierdo, valor);
            else if (compare > 0)
                nodo.Derecho = Insertar(nodo.Derecho, valor);

            return nodo;
        }

        //BUSCAR
        public Nodo<T> Buscar(string matricula)
        {
            return Buscar(Raiz, matricula);
        }

        private Nodo<T> Buscar(Nodo<T> nodo, string matricula)
        {
            if (nodo == null)
                return null;

            string nodoMatricula = nodo.Valor.ToString().Split(',')[1];
            if (nodoMatricula.Trim().Equals(matricula.Trim(), StringComparison.OrdinalIgnoreCase))
                return nodo;

            int compare = matricula.CompareTo(nodoMatricula);

            if (compare < 0)
                return Buscar(nodo.Izquierdo, matricula);
            else
                return Buscar(nodo.Derecho, matricula);
        }

        //ELIMINAR
        public void Eliminar(string matricula)
        {
            Raiz = Eliminar(Raiz, matricula);
        }

        private Nodo<T> Eliminar(Nodo<T> nodo, string matricula)
        {
            if (nodo == null)
                return nodo;

            string nodoMatricula = nodo.Valor.ToString().Split(',')[1];
            int compare = matricula.CompareTo(nodoMatricula);

            if (compare < 0)
                nodo.Izquierdo = Eliminar(nodo.Izquierdo, matricula);
            else if (compare > 0)
                nodo.Derecho = Eliminar(nodo.Derecho, matricula);
            else
            {
                if (nodo.Izquierdo == null)
                    return nodo.Derecho;
                else if (nodo.Derecho == null)
                    return nodo.Izquierdo;

                nodo.Valor = Minimo(nodo.Derecho).Valor;
                nodo.Derecho = Eliminar(nodo.Derecho, nodo.Valor.ToString().Split(',')[1]);
            }

            return nodo;
        }

        //MINIMO
        private Nodo<T> Minimo(Nodo<T> nodo)
        {
            while (nodo.Izquierdo != null)
                nodo = nodo.Izquierdo;

            return nodo;
        }

        public T Minimo()
        {
            return Minimo(Raiz).Valor;
        }

        //MAXIMO
        public T Maximo()
        {
            return Maximo(Raiz).Valor;
        }

        private Nodo<T> Maximo(Nodo<T> nodo)
        {
            while (nodo.Derecho != null)
                nodo = nodo.Derecho;

            return nodo;
        }

        //RECORRIDOS
        public void InOrden(Nodo<T> nodo, List<string> resultado)
        {
            if (nodo == null) return;

            InOrden(nodo.Izquierdo, resultado);
            resultado.Add(FormatearDatos(nodo.Valor.ToString()));
            InOrden(nodo.Derecho, resultado);
        }

        public void PreOrden(Nodo<T> nodo, List<string> resultado)
        {
            if (nodo == null) return;

            resultado.Add(FormatearDatos(nodo.Valor.ToString()));
            PreOrden(nodo.Izquierdo, resultado);
            PreOrden(nodo.Derecho, resultado);
        }

        public void PostOrden(Nodo<T> nodo, List<string> resultado)
        {
            if (nodo == null) return;

            PostOrden(nodo.Izquierdo, resultado);
            PostOrden(nodo.Derecho, resultado);
            resultado.Add(FormatearDatos(nodo.Valor.ToString()));
        }

        public void PorNiveles(Nodo<T> nodo, List<string> resultado)
        {
            if (nodo == null) return;

            Queue<Nodo<T>> queue = new Queue<Nodo<T>>();
            queue.Enqueue(nodo);

            while (queue.Count > 0)
            {
                Nodo<T> current = queue.Dequeue();
                resultado.Add(FormatearDatos(current.Valor.ToString()));

                if (current.Izquierdo != null)
                    queue.Enqueue(current.Izquierdo);
                if (current.Derecho != null)
                    queue.Enqueue(current.Derecho);
            }
        }

        // BALANCEAR
        public void Balancear()
        {
            // Obtener los nodos en orden
            List<T> nodosEnOrden = new List<T>();
            ObtenerNodosEnOrden(Raiz, nodosEnOrden);

            // Construir el árbol balanceado
            Raiz = ConstruirArbolBalanceado(nodosEnOrden, 0, nodosEnOrden.Count - 1);
        }

        private void ObtenerNodosEnOrden(Nodo<T> nodo, List<T> nodosEnOrden)
        {
            if (nodo == null) return;

            ObtenerNodosEnOrden(nodo.Izquierdo, nodosEnOrden);
            nodosEnOrden.Add(nodo.Valor);
            ObtenerNodosEnOrden(nodo.Derecho, nodosEnOrden);
        }

        private Nodo<T> ConstruirArbolBalanceado(List<T> nodosEnOrden, int inicio, int fin)
        {
            if (inicio > fin)
                return null;

            int medio = (inicio + fin) / 2;
            Nodo<T> nodo = new Nodo<T>(nodosEnOrden[medio])
            {
                Izquierdo = ConstruirArbolBalanceado(nodosEnOrden, inicio, medio - 1),
                Derecho = ConstruirArbolBalanceado(nodosEnOrden, medio + 1, fin)
            };

            return nodo;
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

        // OBTENER DATOS 
        public string SerializarJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
