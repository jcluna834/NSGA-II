using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.fxReemplazo
{
    class Aleatorio : Reemplazo
    {
        public Aleatorio(Random rand)
        {
            this.rand = rand;
        }

        public override List<Individuo> reemplazar(List<Individuo> poblacion, List<Individuo> individuos, int opcion)
        {
            List<Individuo> nueva = poblacion;
            individuos.ForEach(delegate(Individuo i)
            {
                //nueva.setIndividuo((int)(rand.NextDouble() * poblacion.getIndividuos().Length), i);
                nueva[(int)(rand.NextDouble() * poblacion.Count)] = i;
            });
            return nueva;
        }
    }
}
