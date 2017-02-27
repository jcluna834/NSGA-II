using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;

namespace TercerCorteMH2.Individuos
{
    public class Individuo : System.IComparable<Individuo>
    {
        public double distanciaEcluidiana;
        public double FitnessTiempo;
        public double FitnessDistancia;
        public int Rank;
        public int NumeroDeJefes;
        public double DistanciaCrowding;
        public List<int> ConjuntoDeDominados;
        public int[] recorrido;
        public PaisajeAptitud funcion;
        public Individuo(PaisajeAptitud funcion, int cantidad)
        {
            this.funcion = funcion;
            FitnessTiempo = -1.0;
            FitnessDistancia = -1.0;
            distanciaEcluidiana = -1.0;
            Rank = -1;
            NumeroDeJefes = 0;
            DistanciaCrowding = 0;
            ConjuntoDeDominados = new List<int>();
            recorrido = new int[cantidad];
            int i = 0;
            for (; i < cantidad; i++)
                recorrido[i] = -1;
        }

        /*public bool dominaA(Individuo otro)
        {
            return (funcion.evaluarObjetivo1(this) < funcion.evaluarObjetivo1(otro) && funcion.evaluarObjetivo2(this) < funcion.evaluarObjetivo2(otro));
        }*/

        public bool dominaA(Individuo otro)
        {
            var si = false;
            // Se asume MINIMIZACION por tanto si hay un objetivo mas alto que el del otro, yo no lo domino
            if (funcion.evaluarObjetivo1(this) > funcion.evaluarObjetivo1(otro))
                return false;
            if (funcion.evaluarObjetivo2(this) < funcion.evaluarObjetivo2(otro))
                si = true;
            return si;
        }

        public void mostrar()
        {
            Console.WriteLine("Aptitud distancia : " + FitnessDistancia + " Aptitud tiempo : " + FitnessTiempo);
            foreach (int o in recorrido)
                Console.WriteLine(o + " ");
            Console.WriteLine();
        }

        int IComparable<Individuo>.CompareTo(Individuo o)
        {
            if (Rank != o.Rank)
                return Rank.CompareTo(o.Rank);
            return -1 * DistanciaCrowding.CompareTo(o.DistanciaCrowding);
        }

        /*int CompareTo(Individuo o)
        {
            double d1 = o.getAptitud();
            double d2 = getAptitud();
            if (d1 > d2)
                return 1;
            if (d2 > d1)
                return -1;
            return 0;
        }*/

        public double getAptitud()
        {
            if (distanciaEcluidiana == -1)
                distanciaEcluidiana = Math.Sqrt((getDistancia() * getDistancia()) + (getTiempo() * getTiempo()));
            return distanciaEcluidiana;
        }

        public double getDistancia()
        {
            if (FitnessDistancia == -1)
                FitnessDistancia = funcion.evaluarObjetivo1(this);
            return FitnessDistancia;
        }

        public double getTiempo()
        {
            if (FitnessTiempo == -1)
                FitnessTiempo = funcion.evaluarObjetivo2(this);
            return FitnessTiempo;
        }

        public Individuo copiar()
        {
            Individuo copia = new Individuo(this.funcion, this.recorrido.Length);
            this.recorrido.CopyTo(copia.recorrido, 0);
            int[] dominados = new int[ConjuntoDeDominados.Count];
            this.ConjuntoDeDominados.CopyTo(dominados, 0);
            copia.DistanciaCrowding = DistanciaCrowding;
            copia.NumeroDeJefes = NumeroDeJefes;
            copia.Rank = Rank;
            return copia;
        }
    }

}