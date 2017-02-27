using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2.fxMutacion
{
    class Intercambio : Mutacion
    {
        public Intercambio(Random rand)
        {
            this.rand = rand;
        }

        public override Individuo mutar(Individuo sujeto)
        {
            int posicion1 = (int)(rand.NextDouble() * sujeto.recorrido.Length);
            int posicion2 = posicion1;
            while (posicion2 == posicion1)
                posicion2 = (int)(rand.NextDouble() * sujeto.recorrido.Length);
            var gen1 = sujeto.recorrido[posicion1];
            var gen2 = sujeto.recorrido[posicion2];
            sujeto.recorrido[posicion1] = gen2;
            sujeto.recorrido[posicion2] = gen1;
            return sujeto.copiar();
        }
    }
}
