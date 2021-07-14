using System;

namespace Ejercicio1
{
    class Ejercicio1
    {
        public enum Dias : byte
        {
            Lunes = 1,
            Martes = 2,
            Miércoles = 3,
            Jueves = 4,
            Viernes = 5,
            Sábado = 6,
            Domingo = 7
        }

        static void Main(string[] args)
        {
            float temperaturá = 0;
            float temperaturaMinima = float.MaxValue; // Se puede asignar un número grande.
            byte diaMenorTemperatura = 1;
            string mensaje = "";

            for (byte i = 1; i <= Enum.GetNames(typeof(Dias)).Length; i++)
            {
                Console.Write("Ingrese la temperatura del día " + Enum.GetName(typeof(Dias), i) + ": ");
                temperaturá = float.Parse(Console.ReadLine());
                if (temperaturá < temperaturaMinima)
                {
                    temperaturaMinima = temperaturá;
                    diaMenorTemperatura = i;
                }
            }

            mensaje = "La temperatura más baja fue de " + temperaturaMinima + " grados y se produjo el día " +
                      Enum.GetName(typeof(Dias), diaMenorTemperatura);

            Console.WriteLine(mensaje);
            Console.ReadKey();
        }
    }
}