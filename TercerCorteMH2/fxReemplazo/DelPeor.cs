using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.fxReemplazo
{
    class DelPeor : Reemplazo
    {
        public DelPeor() { }

        private Individuo peorIndividuo(List<Individuo> poblacion, int opcion)
        {
            double peorAptitud = 0;
            Individuo peorIndividuo = null;
            foreach (Individuo individuo in poblacion)
            {
                if (peorIndividuo == null || comparar(peorIndividuo, individuo, opcion))
                {
                    peorIndividuo = individuo;
                    peorAptitud = individuo.getAptitud();
                }
            }
            return peorIndividuo;
        }

        public override List<Individuo> reemplazar(List<Individuo> poblacion, List<Individuo> individuos, int opcion)
        {
            List<Individuo> nueva = poblacion;
            individuos.ForEach(delegate(Individuo ind)
            {
                Individuo peorInd = peorIndividuo(poblacion, opcion);
                if (comparar(ind, peorInd, opcion))
                {
                    int pos = getPosicion(peorInd, poblacion);
                    nueva[pos] = ind;
                    poblacion[pos] = ind;
                }
            });
            return nueva;
        }

        private int getPosicion(Individuo peorIndividuo, List<Individuo> poblacion)
        {
            int i = 0;
            foreach (Individuo ind in poblacion)
            {
                if (ind.Equals(peorIndividuo))
                    break;
                i++;
            }
            return i;
        }
    }
}
