using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.fxCruce
{
    public class DeNPuntos : Cruce
    {
        public Individuo[] hijos;
        private int inicial;
        private int fin;

        public DeNPuntos(Random rand)
        {
            this.rand = rand;
            hijos = new Individuo[2];
        }

        public override Individuo[] cruzar(Individuo padre, Individuo madre)
        {
            this.inicial = (int)(rand.NextDouble() * madre.recorrido.Length);
            this.fin = (int)(rand.NextDouble() * padre.recorrido.Length);
            Individuo hijo1 = padre;
            Individuo hijo2 = madre;
            if (inicial > fin)
            {
                int aux = inicial;
                inicial = fin;
                fin = aux;
            }
            for (int i = inicial; i < fin; i++)
            {
                hijo1.recorrido[i] = madre.recorrido[i];
                hijo2.recorrido[i] = padre.recorrido[i];
            }
            hijos[0] = hijo1;
            hijos[1] = hijo2;
            return hijos;
        }
    }
}
