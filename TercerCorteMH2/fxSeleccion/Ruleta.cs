using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.fxSeleccion
{
    class Ruleta : Seleccion
    {
        public Ruleta(Random rnd, int cantidad)
        {
            this.rand = rnd;
            this.cantidad = cantidad;
        }

        public override List<Individuo> seleccionar(List<Individuo> poblacion, int opcion)
        {
            double aptitud, acumulado = 0.0;
            List<Individuo> seleccion = new List<Individuo>();
            Individuo[] individuos = poblacion.ToArray();
            foreach (Individuo i in poblacion)
                acumulado += i.getAptitud();
            do
            {
                double random = rand.NextDouble();
                foreach (Individuo individuo in individuos)
                {
                    aptitud = individuo.getAptitud();
                    double division = aptitud / acumulado;
                    if (division > random)
                        seleccion.Add(individuo);
                }
            } while (seleccion.Count < cantidad);
            return seleccion;
        }
    }
}
