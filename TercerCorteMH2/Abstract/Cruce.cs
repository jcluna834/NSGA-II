using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.Abstract
{
    public abstract class Cruce
    {
        protected Random rand;

        /**
         * 
         * @param padre
         * @param madre
         * @return 
         */
        public abstract Individuo[] cruzar(Individuo padre, Individuo madre);

    }
}
