using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.Abstract
{
    public abstract class Reemplazo
    {
        protected Random rand;

        /**
         * @param poblacion
         * @param individuos
         * @param opcion
         * @return 
         */
        public abstract List<Individuo> reemplazar(List<Individuo> poblacion, List<Individuo> individuos, int opcion);

        protected Boolean comparar(Individuo ind1, Individuo ind2, int opcion)
        {
            double d1 = ind1.getAptitud();
            double d2 = ind2.getAptitud();
            if (opcion == 0)
                return (d1 < d2);
            return (d1 > d2);
        }
    }
}
