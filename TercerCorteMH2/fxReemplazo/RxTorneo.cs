using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.fxReemplazo
{
    class RxTorneo : Reemplazo
    {
        public RxTorneo(Random rand)
        {
            this.rand = rand;
        }

        public override List<Individuo> reemplazar(List<Individuo> poblacion, List<Individuo> individuos, int opcion)
        {
            List<Individuo> nueva = poblacion;
            individuos.ForEach(delegate(Individuo i)
            {
                int pos = (int)(rand.NextDouble() * poblacion.Count);
                if (comparar(i, poblacion[pos], opcion))
                    nueva[pos] = i;
            });
            return nueva;
        }
    }
}
