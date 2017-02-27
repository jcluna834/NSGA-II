using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.Abstract
{
    public abstract class Seleccion
    {
        protected Random rand;
        protected int cantidad;

        /**
         *
         * @param poblacion
         * @param opcion
         * @return
         */
        public abstract List<Individuo> seleccionar(List<Individuo> poblacion, int opcion);

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
