using System;
using System.Threading;

namespace Ejercicio6
{
    class Program
    {
        // Constants
        private const string ShipWithBomb    = "|--[I]--|";
        private const string ShipWithoutBomb = "|--[ ]--|";
        private const string Bomb            =  "I";
        
        // Methods
        
        /// <summary>
        /// Inicializa la consola haciendo que el cursor no esté visible, limpiando la pantalla, seteando la
        /// posición del cursor en cero respecto al ancho y en el centro respecto del alto de la consola
        /// y muestra la nave con la bomba en su interior 
        /// </summary>
        private static void Initialize()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Thread.Sleep(500); // Espero este tiempo por la consola integrada
            Draw(ShipWithBomb, 0, GetWindowHeightMiddle());
        }
        
        /// <summary>
        /// Chequea que la variable presionada no sea la correspondiente al Escape
        /// </summary>
        /// <param name="keyPressed">Parámetro que corresponde a la tecla presionada</param>
        /// <returns>
        ///     <list type="true|false">
        ///         <item>
        ///             <term>true</term>
        ///             <description>Si la tecla presionada no corresponde al Escape</description>
        ///         </item>
        ///         <item>
        ///             <term>false</term>
        ///             <description>Si la tecla presionada corresponde al Escape</description>
        ///         </item>
        ///     </list>
        /// </returns>
        private static bool KeyPressedIsNotEscape(ConsoleKey keyPressed)
        {
            return ConsoleKey.Escape != keyPressed;
        }

        /// <summary>
        /// Escribe en la consola el texto indicado en el parámetro objectToDraw; ubicandolo en la posición indicada
        /// por lo parámetros left y top 
        /// </summary>
        /// <param name="objectToDraw"></param>
        /// <param name="left">Ubicación en el eje X</param>
        /// <param name="top">Ubicación en el eje Y</param>
        private static void Draw(string objectToDraw, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(objectToDraw);
        }

        /// <summary>
        /// Mueve la nave de acuerdo a la tecla presionada teniendo en cuenta el alto y ancho de la consola sin permitir
        /// que se superen los extremos
        /// </summary>
        /// <param name="ship">Es la nave a dibujar</param>
        /// <param name="keyPressed">Es la tecla presionada</param>
        static void MoveShip(string ship, ConsoleKey keyPressed)
        {
            Tuple<int, int> currentPosition = GetCurrentPosition();
            int left = currentPosition.Item1;
            int top = currentPosition.Item2;

            Console.Clear();
            int newLeftPosition = 0;
            int newTopPosition = 0;
            
            switch (keyPressed)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    if (left - ship.Length - 2 >= 0)
                    {
                        newLeftPosition = (left - ship.Length) - 2;
                    }

                    Draw(ship, newLeftPosition, top);
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    if (Console.WindowWidth > left + 1) //No llegó al tope derecho
                        newLeftPosition = left - (ship.Length - 2) - 2;
                    else //Llegó al tope derecho por lo que no avanzo
                        newLeftPosition = Console.WindowWidth - ship.Length - 2;
                    
                    Draw(ship, newLeftPosition, top);
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    if (top > 0)
                    {
                        newLeftPosition = left - ship.Length - 1;
                        newTopPosition = top - 1;
                    }
                    else
                    {
                        newLeftPosition = left - ship.Length - 1;
                        newTopPosition = 0;
                    }

                    Draw(ship, newLeftPosition, newTopPosition);
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    if (Console.WindowHeight > top + 2) // Dejo una fila para que se vea la explosión
                    {
                        newLeftPosition = left - ship.Length - 1;
                        newTopPosition = top + 1;
                    }
                    else
                    {
                        newLeftPosition = left - ship.Length - 1;
                        newTopPosition = Console.WindowHeight - 2;
                    }
                    
                    Draw(ship, newLeftPosition, newTopPosition);
                    break;
                default:
                    newLeftPosition = left;
                    if (left > ship.Length)
                    {
                        newLeftPosition = left - ship.Length - 1;
                    }

                    Draw(ship, newLeftPosition, top);
                    break;
            }
        }

        /// <summary>
        /// Lanza la bomba si la tecla que se presionó es la B 
        /// </summary>
        /// <param name="keyPressed">
        /// keyPressed: Es la tecla presionada
        /// </param>
        public static void DropBomb(ConsoleKey keyPressed)
        {
            if (ConsoleKey.B == keyPressed)
            {
                Tuple<int, int> currentPosition = GetCurrentPosition();
                int left = currentPosition.Item1;
                int shipPositionTop = currentPosition.Item2;
                int bombTop = shipPositionTop;
                
                while (bombTop + 1 < Console.WindowHeight - 1)
                {
                    Draw(ShipWithoutBomb, (left - ShipWithoutBomb.Length), shipPositionTop);
                    //FIXME: Modificar para cuando el ancho de la nave es par
                    Draw(Bomb, (left - (ShipWithoutBomb.Length + 1) / 2), bombTop + 1);
                    
                    bombTop = GetCurrentPosition().Item2;
                    Thread.Sleep(300);
                    
                    Console.Clear();
                    
                }
                
                Draw(ShipWithBomb, left - ShipWithBomb.Length, shipPositionTop);
            }
        }

        /// <summary>
        /// Obtiene la posición actual del cursor en la consola
        /// </summary>
        /// <returns>
        ///     Una tupla que corresponde al valor en X (Item1) y al valor en Y (Item2) de la posición actual del
        ///     cursor
        /// </returns>
        private static Tuple<int, int> GetCurrentPosition()
        {
            return Console.GetCursorPosition().ToTuple();
        }
        
        /// <summary>
        /// Calcula el centro de la consola respecto al largo
        /// </summary>
        /// <returns>
        /// Valor entero correspondiente al centro de la consola
        /// </returns>
        static int GetWindowHeightMiddle()
        {
            return Console.WindowHeight / 2;
        }
        /// <summary>
        /// Calcula el centro de la consola respecto al ancho
        /// </summary>
        /// <returns>
        /// Valor entero correspondiente al centro de la consola
        /// </returns>
        static int GetWindowWidthMiddle()
        {
            return Console.WindowWidth / 2;
        }

        /// <summary>
        /// Limpia la consola, muestra un mensaje y espera a que se presione una tecla 
        /// </summary>
        private static void ShowMessage(String message)
        {
            Console.Clear();
            Console.SetCursorPosition(GetWindowWidthMiddle() - message.Length/2, GetWindowHeightMiddle());
            Console.Write(message);
            Console.ReadKey(true);
        }
        
        // Main
        /// <summary>
        /// Hilo principal de ejecución
        /// </summary>
        /// <param name="args">Not in use</param>
        static void Main(string[] args)
        {
            ConsoleKey keyPressed = ConsoleKey.A;
            
            Initialize();

            do
            {
                if (Console.KeyAvailable && KeyPressedIsNotEscape(keyPressed = Console.ReadKey().Key))
                {
                    MoveShip(ShipWithBomb, keyPressed);
                    DropBomb(keyPressed);
                }

            } while (KeyPressedIsNotEscape(keyPressed));

            ShowMessage("Presione una tecla para salir...");
        }
    }
}