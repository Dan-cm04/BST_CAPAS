using System;
using System.Collections.Generic;

namespace ClassBts
{
    public class BST<T> where T : IComparable<T>
    {
        public Nodo<T> Raiz { get; set; }

        public void Insertar(T valor)
        {
            Raiz = Insertar(Raiz, valor);
        }

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


    }


}
        
 

