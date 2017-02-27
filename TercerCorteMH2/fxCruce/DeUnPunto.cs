using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.fxCruce
{
    class DeUnPunto : Cruce
    {
        private int cruce;

        public DeUnPunto(Random rand)
        {
            this.rand = rand;
        }

        public override Individuo[] cruzar(Individuo padre, Individuo madre)
        {
            Individuo[] hijos = new Individuo[2];
            cruce = (int)(rand.NextDouble() * padre.recorrido.Length);
            hijos[0] = madre;
            hijos[1] = padre;
            for (int i = 0; i < cruce; i++)
            {
                hijos[0].recorrido[i] = padre.recorrido[i];
                hijos[1].recorrido[i] = madre.recorrido[i];
            }
            return hijos;
        }
    }
}
