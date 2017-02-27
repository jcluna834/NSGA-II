using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.Algortimo
{
    public class NSGAII : Algoritmo
    {
        public int tamañoPoblacion;
        public int dimensiones;
        private List<List<Individuo>> Q;
        private List<Individuo> Pt1;
        private List<Individuo> hijos;
        private Individuo[] padres;

        public NSGAII(Random rand)
        {
            this.rand = rand;
            Q = new List<List<Individuo>>();
        }

        public void inicializar(Cruce funcionCruce, Mutacion funcionMutacion, Seleccion funcionSeleccion, PaisajeAptitud funcionAptitud, Reemplazo funcionReemplazo, int numIteraciones, int dimensiones, double porcentaje)
        {
            this.dimensiones = dimensiones;
            this.tamañoPoblacion = (int)((dimensiones / 2) * porcentaje);
            inicializar(funcionCruce, funcionMutacion, funcionSeleccion, funcionAptitud, funcionReemplazo, numIteraciones);
            this.P = new List<List<Individuo>>();
            int i = 0, fraccion = (int)(dimensiones * porcentaje);
            
            padres = new Individuo[tamañoPoblacion];
            for (; i < tamañoPoblacion; i++)
            {
                padres[i] = new Individuo(funcionAptitud, dimensiones);
                inicializarAleatorio(padres, i, fraccion);
                if (dimensiones > fraccion)
                    inicializarSecuencial(padres, i);
            }
            P.Add(padres.ToList());
        }

        public override List<Individuo> ejecutar()
        {
            int i = 0, generacion = 0;
            List<List<Individuo>> R = new List<List<Individuo>>();
            List<List<Individuo>> F;
            do
            {
                double tot = funcionAptitud.evaluar(P[generacion]);
                Console.WriteLine("Generación: " + generacion+" Aptitud Total de la Población: "+tot);
                if (Q.Count == 0)
                    R.Add(P[generacion]);
                else
                    R.Add(union(P[generacion], Q[generacion - 1]));

                F = fastNonDominatedSort(R[generacion]);
                Pt1 = new List<Individuo>();
                i = 0;
                while (F[i].Count != 0 && (Pt1.Count + F[i].Count) < tamañoPoblacion)
                {
                    F[i] = distaciaCrowding(F[i]);
                    Pt1 = union(Pt1, F[i]);
                    i++;
                }
                F[i].Sort(); //Ordenamiento por rango y distancia de crowding
                int k = 0;
                while (F[i].Count != 0 && Pt1.Count < tamañoPoblacion)
                {
                    Pt1.Add(F[i][k].copiar());
                    k++;
                }
                P.Add(Pt1);
                Q.Add(reproduccion(Pt1));
                generacion++;
            } while (generacion < numIteraciones);
            return P[generacion - 1];
        }

        private List<Individuo> obtenerBest(List<Individuo> list)
        {
            funcionAptitud.evaluar(list);
            Individuo Best = null;
            foreach (Individuo ind in list)
                if (Best == null || Quality(Best) < Quality(ind))
                    Best = ind;
            var res = new List<Individuo>();
            res.Add(Best);
            return res;
        }

        private List<Individuo> union(List<Individuo> list1, List<Individuo> list2)
        {
            List<Individuo> union = new List<Individuo>();
            union.AddRange(list1);
            union.AddRange(list2);
            return union;
        }

        private void inicializarSecuencial(Individuo[] padres, int j)
        {
            int lastVisited = -1, pos = -1, i = 0;
            if (padres[j].recorrido[0] == -1)
            {
                lastVisited = rand.Next(dimensiones);
                padres[j].recorrido[0] = lastVisited;
                pos = 1;
            }
            else
            {
                while (padres[j].recorrido[i] != -1) { i++; }
                lastVisited = padres[j].recorrido[i - 1];
                pos = i;
            }
            i = lastVisited;
            while (pos < dimensiones)
            {
                i = (i + 1) % dimensiones;
                if (!seHaVisitado(i, padres[j].recorrido))
                {
                    padres[j].recorrido[pos] = i;
                    pos++;
                }

            }
        }

        private bool seHaVisitado(int i, int[] p)
        {
            foreach (int recorridas in p)
                if (i == recorridas)
                    return true;
            return false;
        }

        private void inicializarAleatorio(Individuo[] padres, int j, int fraccion)
        {
            int pos = -1, i = 0, count = 0;
            if (padres[j].recorrido[0] == -1)
            {
                int nextVisited = rand.Next(dimensiones);
                padres[j].recorrido[0] = nextVisited;
                pos = 1;
                count++;
            }
            else
            {
                while (padres[j].recorrido[i] != -1) { i++; }
                pos = i;
            }
            while (pos < dimensiones)
            {
                i = rand.Next(dimensiones);
                if (!seHaVisitado(i, padres[j].recorrido))
                {
                    padres[j].recorrido[pos] = i;
                    pos++;
                    count++;
                }
                if (count == fraccion)
                    break;
            }
        }

        internal List<List<Individuo>> fastNonDominatedSort(List<Individuo> poblacion)
        {
            var frente1 = new List<Individuo>();
            var F = new List<List<Individuo>>();
            for (var i = 0; i < poblacion.Count; i++)
            {
                poblacion[i].Rank = -1;
                poblacion[i].NumeroDeJefes = 0;
                poblacion[i].ConjuntoDeDominados = new List<int>();
                for (var j = 0; j < poblacion.Count; j++)
                {
                    if (i == j) continue;

                    if (poblacion[i].dominaA(poblacion[j]))
                        poblacion[i].ConjuntoDeDominados.Add(j);
                    else if (poblacion[j].dominaA(poblacion[i]))
                        poblacion[i].NumeroDeJefes ++;
                }
                if (poblacion[i].NumeroDeJefes == 0)
                {
                    frente1.Add(poblacion[i]);
                    poblacion[i].Rank = 0;
                }
            }
            F.Add(frente1);
            var numeroFrente = 1;
            while (F[numeroFrente-1].Count != 0)
            {
                var nuevoFrente = new List<Individuo>();
                foreach (var i in F[numeroFrente-1])
                {
                    foreach (var j in i.ConjuntoDeDominados)
                    {
                        poblacion[j].NumeroDeJefes = poblacion[j].NumeroDeJefes - 1;
                        if (poblacion[j].NumeroDeJefes == 0)
                        {
                            poblacion[j].Rank = numeroFrente;
                            nuevoFrente.Add(poblacion[j]);
                        }
                    }
                }
                F.Add(nuevoFrente);
                numeroFrente++;
            }
            return F;
        }

        internal List<Individuo> distaciaCrowding(List<Individuo> poblacion)
        {
            foreach (Individuo individuo in poblacion)
                individuo.DistanciaCrowding = 0;
            //Ordenar los individuos por el objetivo de Distancia
            Individuo[] individuos = Quicksort(poblacion.ToArray(), 0, 0, poblacion.Count-1);

            individuos[0].DistanciaCrowding = int.MaxValue;
            individuos[individuos.Length - 1].DistanciaCrowding = int.MaxValue;
            for (int i = 1; i < individuos.Length - 1; i++)
                individuos[i].DistanciaCrowding += ((individuos[i].getDistancia() - individuos[i - 1].getDistancia()) + (individuos[i + 1].getDistancia() - individuos[i].getDistancia()));
            //Ordenar los individuos por el objetivo de Tiempo
            individuos = Quicksort(individuos, 1, 0, poblacion.Count-1);

            individuos[0].DistanciaCrowding = int.MaxValue;
            individuos[individuos.Length - 1].DistanciaCrowding = int.MaxValue;
            for (int i = 1; i < individuos.Length - 1; i++)
                individuos[i].DistanciaCrowding += ((individuos[i].getTiempo() - individuos[i - 1].getTiempo()) + (individuos[i + 1].getTiempo() - individuos[i].getTiempo()));
            return individuos.ToList();
        }

        private List<Individuo> reproduccion(List<Individuo> poblacion)
        {
            int cantidadSelecciones = (int)(tamañoPoblacion / 2);
            hijos = new List<Individuo>();
            for (int fat = 0; fat < cantidadSelecciones; fat++)
            {
                List<Individuo> seleccionados = funcionSeleccion.seleccionar(poblacion, funcionAptitud.opc);
                padres = seleccionados.ToArray();
                Individuo padre = seleccionados[0];
                Individuo madre = seleccionados[1];
                Individuo[] hijosNoMutados = new Individuo[2];
                hijosNoMutados = funcionCruce.cruzar(padre, madre);
                foreach (Individuo individuo in hijosNoMutados)
                    hijos.Add(funcionMutacion.mutar(individuo));
            }
            return hijos;
        }
    }
}