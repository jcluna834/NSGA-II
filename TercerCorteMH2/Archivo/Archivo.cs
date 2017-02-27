using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TercerCorteMH2.Archivo
{
    public class Archivo
    {
        private System.IO.FileStream archivo { set; get; }
        public int cantLineas;

        public Archivo(string file, int opc)
        {
            if (opc == 0)
            {
                archivo = new System.IO.FileStream(file, FileMode.Open);
                cantidadElementos();
            }
            else
                archivo = new System.IO.FileStream(file, FileMode.OpenOrCreate);
        }

        internal double[] configuracion()
        {
            archivo.Position = 0;
            System.IO.StreamReader archivoLeer = new System.IO.StreamReader(archivo);
            double[] datos = new double[2];
            string dato = archivoLeer.ReadLine();
            string[] aux = dato.Split(' ');
            datos[0] = double.Parse(aux[0]);
            datos[1] = double.Parse(aux[1]);
            return datos;
        }

        internal int cantidadElementos()
        {
            archivo.Position = 0;
            System.IO.StreamReader archivoLeer = new System.IO.StreamReader(archivo);
            int lineCount = 0;
            while (archivoLeer.ReadLine() != null)
                lineCount++;
            cantLineas = lineCount;
            return lineCount;
        }
        public void close()
        {
            archivo.Dispose();
            archivo.Close();
        }

        public void agregarLinea(string texto)
        {
            System.IO.StreamWriter writer = new StreamWriter(archivo);
            writer.WriteLine(texto);
            writer.Flush();
        }

        public List<double[]> getList()
        {
            archivo.Position = 0;
            List<double[]> x = new List<double[]>();
            string p;
            System.IO.StreamReader archivoLeer = new System.IO.StreamReader(archivo);
            while ((p = archivoLeer.ReadLine()) != null)
            {
                string[] aux = p.Split(' ');
                double[] datos = new double[aux.Length];
                int i = 0;
                foreach (string d in aux)
                {
                    datos[i] = Double.Parse(d);
                    i++;
                }
                x.Add(datos);
            }
            return x;
        }

        public double[][] getMatriz()
        {
            archivo.Position = 0;
            double[][] x = new double[cantLineas][];
            string p;
            int j = 0;
            System.IO.StreamReader archivoLeer = new System.IO.StreamReader(archivo);
            while ((p = archivoLeer.ReadLine()) != null)
            {
                x[j] = new double[cantLineas];
                string[] aux = p.Split(' ');
                int i = 0;
                foreach (String d in aux)
                {
                    x[j][i] = double.Parse(d);
                    i++;
                }
                j++;
            }
            return x;
        }
    }
}
