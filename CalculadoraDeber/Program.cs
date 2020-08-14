/*using System;

namespace CalculadoraDeber
{
    class Calculadora
    {
        static void Main(string[] args)
        {
            Console.Title = "Calculadora";
            string respuesta = "";
            do
            {
                int primerNumero = 0;
                int segundoNumero = 0;
                int resultado = 0;

                Console.WriteLine("Presione el simbolo para realizar la operacion: ");
                Console.WriteLine("+ para sumar");
                Console.WriteLine("- para restar");
                Console.WriteLine("* para multiplicar");
                Console.WriteLine("/ para dividir");
                Console.Write("Eliga una opción: ");

                respuesta = Console.ReadLine();
                string eleccion = Convert.ToString(respuesta);

                Console.WriteLine("Ingrese sus dos numeros");
                Console.Write("Primer número: ");
                primerNumero = int.Parse(Console.ReadLine());

                Console.Write("Segundo número: ");
                segundoNumero = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (eleccion)
                {
                    case "+":
                        { 
                            Console.WriteLine("El resultado de la suma es:");
                            resultado = primerNumero + segundoNumero;
                            Console.WriteLine("{0} + {1} = {2}", primerNumero, segundoNumero, resultado);
                            break;
                        }
                    case "-":
                        {
                            Console.WriteLine("El resultado de la resta es:");
                            resultado = primerNumero - segundoNumero;
                            Console.WriteLine("{0} - {1} = {2}", primerNumero, segundoNumero, resultado);
                            break;
                        }
                    case "*":
                        {
                            Console.WriteLine("El resultado de la multiplicación es:");
                            resultado = primerNumero * segundoNumero;
                            Console.WriteLine("{0} * {1} = {2}", primerNumero, segundoNumero, resultado);
                            break;
                        }
                    case "/":
                        {
                            Console.WriteLine("El resultado de la dicisión es:");
                            resultado = primerNumero / segundoNumero;
                            Console.WriteLine("{0} / {1} = {2}", primerNumero, segundoNumero, resultado);
                            break;
                        }
                }
                Console.Write("¿Desea Continuar? Si/No: ");
                respuesta = Console.ReadLine();
            }
            while (respuesta == "Si" || respuesta == "Si") ;
        }
    }
}
*/
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CalculadoraDeber
{
    public class MultiplyMatrices
    {
        #region Sequential_Loop
        static void MultiplyMatricesSequential(double[,] matA, double[,] matB, double[,] result)
        {
            int matACols = matA.GetLength(1);
            int matBCols = matB.GetLength(1);
            int matARows = matA.GetLength(0);

            for (int i = 0; i < matARows; i++)
            {
                for (int j = 0; j < matBCols; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < matACols; k++)
                    {
                        temp += matA[i, k] * matB[k, j];
                    }
                    result[i, j] += temp;
                }
            }
        }
        #endregion

        #region Parallel_Loop
        static void MultiplyMatricesParallel(double[,] matA, double[,] matB, double[,] result)
        {
            int matACols = matA.GetLength(1);
            int matBCols = matB.GetLength(1);
            int matARows = matA.GetLength(0);

            Parallel.For(0, matARows, i =>
            {
                for (int j = 0; j < matBCols; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < matACols; k++)
                    {
                        temp += matA[i, k] * matB[k, j];
                    }
                    result[i, j] = temp;
                }
            });
        }
        #endregion

        #region Main
        private static void Main(string[] args)
        {
            int colCount = 180;
            int rowCount = 2000;
            int colCount2 = 270;
            double[,] m1 = InitializeMatrix(rowCount, colCount);
            double[,] m2 = InitializeMatrix(colCount, colCount2);
            double[,] result = new double[rowCount, colCount2];

            Console.Error.WriteLine("Ejecutando bucle secuencial ...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            MultiplyMatricesSequential(m1, m2, result);
            stopwatch.Stop();
            Console.Error.WriteLine("Tiempo de ciclo secuencial en milisegundos: {0}", stopwatch.ElapsedMilliseconds);

            OfferToPrint(rowCount, colCount2, result);

            stopwatch.Reset();
            result = new double[rowCount, colCount2];

            Console.Error.WriteLine("Ejecución de bucle paralelo...");
            stopwatch.Start();
            MultiplyMatricesParallel(m1, m2, result);
            stopwatch.Stop();
            Console.Error.WriteLine("Tiempo de ciclo paralelo en milisegundos: {0}",
                                    stopwatch.ElapsedMilliseconds);
            OfferToPrint(rowCount, colCount2, result);

            Console.Error.WriteLine("Presiona cualquier tecla para salir.");
            Console.ReadKey();
        }
        #endregion

        #region Helper_Methods
        static double[,] InitializeMatrix(int rows, int cols)
        {
            double[,] matrix = new double[rows, cols];

            Random r = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = r.Next(100);
                }
            }
            return matrix;
        }

        private static void OfferToPrint(int rowCount, int colCount, double[,] matrix)
        {
            Console.Error.Write("Computación completa. Imprimir resultados (s/n)? ");
            char c = Console.ReadKey(true).KeyChar;
            Console.Error.WriteLine(c);
            if (Char.ToUpperInvariant(c) == 'S')
            {
                if (!Console.IsOutputRedirected) Console.WindowWidth = 90;
                Console.WriteLine();
                for (int x = 0; x < rowCount; x++)
                {
                    Console.WriteLine("FILA {0}: ", x);
                    for (int y = 0; y < colCount; y++)
                    {
                        Console.Write("{0:#.##} ", matrix[x, y]);
                    }
                    Console.WriteLine();
                }
            }
        }
        #endregion
    }

}
