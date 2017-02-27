using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TercerCorteMH2.Abstract;
using TercerCorteMH2.Algortimo;
using TercerCorteMH2.Archivo;
using TercerCorteMH2.fxCruce;
using TercerCorteMH2.fxMutacion;
using TercerCorteMH2.fxObjetivo;
using TercerCorteMH2.fxReemplazo;
using TercerCorteMH2.fxSeleccion;
using TercerCorteMH2.Individuos;

namespace TercerCorteMH2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Individuo> solucion = new List<Individuo>();
            Seleccion seleccion;
            Mutacion mutacion;
            Reemplazo reemplazo;
            Cruce cruce;
            PaisajeAptitud paisaje;
            Random rand;
            Archivo.Archivo datosDistancia;
            Archivo.Archivo datosTiempo;
            int opc = 0;
            datosDistancia = new Archivo.Archivo("DataSetDistancia.txt", 0);
            datosTiempo = new Archivo.Archivo("DataSetTiempo.txt", 0);
            double[][] matrizDistacncia, matrizTiempo;
            matrizDistacncia = datosDistancia.getMatriz();
            matrizTiempo = datosTiempo.getMatriz();
            do
            {
                try
                {
                    int dimensiones, numIteraciones, semilla;
                    dimensiones = datosDistancia.cantLineas;
                    //Parametros a afinar
                    semilla = (new Random()).Next(1, 1001);
                    numIteraciones = 500;

                    if (semilla == -1)
                        rand = new Random();
                    else
                        rand = new Random(semilla);
                    NSGAII algoritmo;
                    algoritmo = new NSGAII(rand);
                    paisaje = new fxAgenteViajero(opc, matrizDistacncia, matrizTiempo);
                    cruce = new Combinado(rand, 2);
                    mutacion = new Intercambio(rand);
                    seleccion = new SxTorneo(rand, 2);
                    reemplazo = new DelPeor();
                    algoritmo.inicializar(cruce, mutacion, seleccion, paisaje, reemplazo, numIteraciones, dimensiones, 0.50);
                    solucion = algoritmo.ejecutar();
                    Console.WriteLine("Semilla: " + semilla + " Tamaño poblacion: " + algoritmo.tamañoPoblacion + " Ciudades: " + algoritmo.dimensiones);
                    /*Console.WriteLine("Mejor individuo");
                    solucion[0].mostrar();
                    Console.WriteLine("Aptitud: " + solucion[0].getAptitud());*/
                    foreach (var ind in solucion)
                    {
                        Debug.WriteLine(ind.FitnessDistancia + "\t" + ind.FitnessTiempo + "\t" + ind.Rank);
                    }

                }
                catch (Exception e) { Console.WriteLine("Error: " + e.Message + "\n" + e.StackTrace); }
                Console.Write("Presiona 0 para salir...\nOtra tecla para volver a ejecutar...");
                try
                {
                    opc = Int32.Parse(Console.ReadKey(false).KeyChar.ToString());
                }
                catch (Exception e) { opc = -1; }
                //Console.Clear();
            } while (opc != 0);
            datosDistancia.close();
            datosTiempo.close();
        }
    }
}