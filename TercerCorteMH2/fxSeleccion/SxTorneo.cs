using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.fxSeleccion
{
    class SxTorneo : Seleccion
    {
        private List<Individuo> seleccion;

        public SxTorneo(Random rand, int cantidad)
        {
            this.rand = rand;
            this.cantidad = cantidad;
        }

        private Individuo[] aletorio(List<Individuo> poblacion)
        {
            Individuo[] individuos = new Individuo[2];
            int x = rand.Next(poblacion.Count);
            int y = rand.Next(poblacion.Count);
            while (y == x)
                y = rand.Next(poblacion.Count);
            individuos[0] = poblacion[x];
            individuos[1] = poblacion[y];
            return individuos;
        }

        public override List<Individuo> seleccionar(List<Individuo> poblacion, int opcion)
        {
            seleccion = new List<Individuo>();
            do
            {
                Individuo[] ind = aletorio(poblacion);
                if (comparar(ind[0], ind[1], opcion))
                    seleccion.Add(ind[0]);
                else
                    seleccion.Add(ind[1]);
            } while (seleccion.Count != cantidad);
            return seleccion;
        }
    }
}
