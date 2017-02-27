using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.Abstract
{
    public abstract class PaisajeAptitud
    {
        public int opc { set; get; }
        public double[][] matrizDistancia;
        public double[][] matrizTiempo;

        public double evaluar(List<Individuo> poblacion)
        {
            double aptitudTotal = 0.0;
            foreach (Individuo i in poblacion)
                aptitudTotal += i.getAptitud();
            return aptitudTotal;
        }

        public abstract double evaluarObjetivo1(Individuo individuo);
        public abstract double evaluarObjetivo2(Individuo individuo);

        public double getObjetivo()
        {
            return opc;
        }
    }
}
