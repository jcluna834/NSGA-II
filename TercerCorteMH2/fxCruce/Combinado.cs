using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.fxCruce
{
    public class Combinado : Cruce
    {
        private Individuo[] hijos;
        private int cantidad;

        public Combinado(Random rand, int cantidad)
        {
            this.rand = rand;
            this.cantidad = cantidad;
            hijos = new Individuo[cantidad];
        }

        public override Individuo[] cruzar(Individuo padre, Individuo madre)
        {
            List<int> indices = obtenerIndices(padre, madre);// se obtienen las posiciones iguales en ambos padres
            for (int indx = 0; indx < cantidad; indx++)
            {
                hijos[indx] = new Individuo(padre.funcion, padre.recorrido.Length);
                foreach (int i in indices)
                    hijos[indx].recorrido[i] = padre.recorrido[i];//se copian las posiciones en los hijos
            }
            for (int indx = 0; indx < cantidad; indx++)
                if (indx % 2 == 0) //se cruzan los hijos pares con respecto al padre
                    hijos[indx] = cruceHijo(hijos[indx], padre, madre);
                else //se cruzan los hijos impares con respecto a la madre
                    hijos[indx] = cruceHijo(hijos[indx], madre, padre);
            return hijos;
        }

        private List<int> obtenerIndices(Individuo padre, Individuo madre)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < padre.recorrido.Length; i++)
                if (padre.recorrido[i] == madre.recorrido[i])
                    indices.Add(i);
            return indices;
        }

        internal bool esta(Individuo progenitor, Individuo hijo, int i)
        {
            for (int j = 0; j < hijo.recorrido.Length; j++)
                if (progenitor.recorrido[i] == hijo.recorrido[j])
                    return true;
            return false;
        }

        internal Individuo cruceHijo(Individuo hijo, Individuo padre, Individuo madre)
        {
            int indx = 0, i = 0, tam = padre.recorrido.Length;
            bool gen = false;
            while (indx < tam)
            {
                //avanza a la posicion del hijo que no tenga un valor asignado
                while (indx < tam && hijo.recorrido[indx] != -1)
                    indx++;
                if (indx >= tam)//si todos los valores del hijo son asignados, terminar
                    break;
                i = (indx + 1) % tam;
                if (!gen)//si no hay gen del padre
                {
                    while (esta(padre, hijo, i))//mientras que el valor del padre ya esté en el hijo, buscar el siguiente
                        i = (i + 1) % tam;
                    //una vez encontrado el siguiente, se le asigna al hijo el gen del padre.
                    hijo.recorrido[indx] = padre.recorrido[i];
                    gen = true;
                }
                else//si hay gen de padre, se hace lo mismo para poner el gen de la madre
                {
                    while (esta(madre, hijo, i))
                        i = (i + 1) % tam;
                    hijo.recorrido[indx] = madre.recorrido[i];
                    gen = false;
                }
                indx++;
            }
            return hijo;
        }
    }
}