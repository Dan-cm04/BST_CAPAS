using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassBts
{
    public class Nodo<T>
    {
        public T Valor { get; set; }
        public Nodo<T> Izquierdo { get; set; }
        public Nodo<T> Derecho { get; set; }

        public Nodo(T valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

}
