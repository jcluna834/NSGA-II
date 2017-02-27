using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.Abstract
{
    public abstract class Algoritmo
    {
        protected Cruce funcionCruce;
        protected Mutacion funcionMutacion;
        protected Seleccion funcionSeleccion;
        protected PaisajeAptitud funcionAptitud;
        protected Reemplazo funcionReemplazo;
        protected Random rand;
        protected int numIteraciones;
        protected List<List<Individuo>> P;

        public abstract List<Individuo> ejecutar();

        public void inicializar(Cruce funcionCruce, Mutacion funcionMutacion, Seleccion funcionSeleccion, PaisajeAptitud funcionAptitud, Reemplazo funcionReemplazo, int numIteraciones)
        {
            this.funcionCruce = funcionCruce;
            this.funcionMutacion = funcionMutacion;
            this.funcionSeleccion = funcionSeleccion;
            this.funcionAptitud = funcionAptitud;
            this.funcionReemplazo = funcionReemplazo;
            this.numIteraciones = numIteraciones;
        }

        public double Quality(Individuo i)
        {
            if (funcionAptitud.getObjetivo() == 0)
                return Int32.MaxValue - i.getAptitud();
            else
                return i.getAptitud();
        }

        public static Individuo[] Quicksort(Individuo [] elements, int opc, int left, int right)
        {
            int i = left, j = right;
            Individuo pivot = elements[(left + right) / 2];
            while (i <= j)
            {
                if (opc == 0)
                {
                    while (elements[i].getDistancia() > pivot.getDistancia()) { i++; }
                    while (elements[j].getDistancia() < pivot.getDistancia()) { j--; }
                }
                else
                {
                    while (elements[i].getTiempo() > pivot.getTiempo()) { i++; }
                    while (elements[j].getTiempo() < pivot.getTiempo()) { j--; }
                }
                if (i <= j)
                {
                    Individuo tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;
                    i++;
                    j--;
                }
            }
            if (left < j) Quicksort(elements, opc, left, j);
            if (i < right) Quicksort(elements, opc, i, right);
            return elements;
        }

    }
}
