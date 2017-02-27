using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.fxObjetivo
{
    class fxAgenteViajero : PaisajeAptitud
    {
        public fxAgenteViajero(int opc, double[][] matrizDistancia, double[][] matrizTiempo)
        {
            this.opc = opc;
            this.matrizDistancia = matrizDistancia;
            this.matrizTiempo = matrizTiempo;
        }

        public double obtenerDistancia(int posActual, int sigPosicion)
        {
            return matrizDistancia[posActual][sigPosicion];
        }

        public double obtenerTiempo(int posActual, int sigPosicion)
        {
            return matrizTiempo[posActual][sigPosicion];
        }

        public override double evaluarObjetivo1(Individuo individuo)
        {
            if (individuo.FitnessDistancia == -1)
            {
                double acumulado = 0.0;
                for (int i = 0; i < (individuo.recorrido.Length - 1); i++)
                {
                    var x = i + 1;
                    if (x >= individuo.recorrido.Length - 1)
                        x = 0;
                    acumulado += obtenerDistancia(individuo.recorrido[i], individuo.recorrido[x]);
                }
                individuo.FitnessDistancia = acumulado;
            }
            return individuo.FitnessDistancia;
        }

        public override double evaluarObjetivo2(Individuo individuo)
        {
            if (individuo.FitnessTiempo == -1)
            {
                double acumulado = 0.0;
                for (int i = 0; i < (individuo.recorrido.Length - 1); i++)
                {
                    var x = i + 1;
                    if (x >= individuo.recorrido.Length - 1)
                        x = 0;
                    acumulado += obtenerTiempo(individuo.recorrido[i], individuo.recorrido[x]);
                }
                individuo.FitnessTiempo = acumulado;
            }
            return individuo.FitnessTiempo;
        }
    }
}