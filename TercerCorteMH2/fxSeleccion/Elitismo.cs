using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.fxSeleccion
{
    class Elitismo : Seleccion
    {
        public Elitismo(Random rand, int cantidad)
        {
            this.rand = rand;
            this.cantidad = cantidad;
        }

        public override List<Individuo> seleccionar(List<Individuo> poblacion, int opcion)
        {
            List<Individuo> seleccion = new List<Individuo>();
            Individuo[] individuos = poblacion.ToArray();
            if (opcion == 0)
                Array.Sort(individuos);
            else
            {
                Array.Sort(individuos);
                Array.Reverse(individuos);
            }
            for (int i = 0; i < cantidad; i++)
                seleccion.Add(individuos[i]);
            return seleccion;
        }
    }
}
