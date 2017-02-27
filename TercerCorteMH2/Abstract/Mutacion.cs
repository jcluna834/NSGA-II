using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.Abstract
{
    public abstract class Mutacion
    {
        protected Random rand;
        /**
         * 
         * @param sujeto
         * @return 
         */
        public abstract Individuo mutar(Individuo sujeto);
    }
}
