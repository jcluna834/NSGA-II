using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.fxCruce
{
    class Uniforme : Cruce
    {
        private Individuo[] hijos;
        private int[] mascara;

        public Uniforme(Random rand)
        {
            this.rand = rand;
            hijos = new Individuo[2];
        }

        public override Individuo[] cruzar(Individuo padre, Individuo madre)
        {
            mascara = new int[padre.recorrido.Length];
            Individuo hijo1 = madre;
            Individuo hijo2 = padre;
            for (int i = 0; i < padre.recorrido.Length; i++)
            {
                mascara[i] = (int)Math.Floor(rand.NextDouble());
            }
            for (int i = 0; i < padre.recorrido.Length; i++)
            {
                if (mascara[i] == 0)
                {
                    hijo1.recorrido[i] = madre.recorrido[i];
                    hijo2.recorrido[i] = padre.recorrido[i];
                }
                else
                {
                    hijo1.recorrido[i] = padre.recorrido[i];
                    hijo2.recorrido[i] = madre.recorrido[i];
                }
            }
            hijos[0] = hijo1;
            hijos[1] = hijo2;
            return hijos;
        }
    }
}
