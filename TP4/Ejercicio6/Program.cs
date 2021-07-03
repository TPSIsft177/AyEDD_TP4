using System;
using System.Threading;

namespace Ejercicio6
{
    class Program
    {
        static void Draw(string objectToDraw)
        {
            Console.Write(objectToDraw);
        }

        static bool KeyPressedIsNotEscape(ConsoleKey keyPressed)
        {
            return ConsoleKey.Escape != keyPressed;
        }

        static void MoveShip(string ship, ConsoleKey keyPressed)
        {
            Tuple<int, int> currentPosition = Console.GetCursorPosition().ToTuple();
            int left = currentPosition.Item1;
            int top = currentPosition.Item2;

            Console.Clear();
            
            switch (keyPressed)
            {
                case ConsoleKey.LeftArrow:
                    if (left - ship.Length - 2 >= 0)
                        Console.SetCursorPosition((left - ship.Length) - 2, top);
                    else
                        Console.SetCursorPosition(0, top);
                    break;
                case ConsoleKey.RightArrow:
                    if(Console.WindowWidth > left+1) //No llegó al tope derecho
                        Console.SetCursorPosition(left - (ship.Length-2)-2, top);
                    else //Llegó al tope derecho por lo que no avanzo
                        Console.SetCursorPosition(Console.WindowWidth-ship.Length-2, top);
                    break;
                case ConsoleKey.UpArrow:
                    if(top > 0)
                        Console.SetCursorPosition(left-ship.Length-1, top - 1);
                    else
                        Console.SetCursorPosition(left-ship.Length-1, 0);
                    break;
                case ConsoleKey.DownArrow:
                    if(Console.WindowHeight > top + 2) // Dejo una fila para que se vea por lo menos la explosión
                        Console.SetCursorPosition(left-ship.Length-1, top + 1);
                    else
                        Console.SetCursorPosition(left-ship.Length-1, Console.WindowHeight - 2);
                    break;
            }
            
            Draw(ship);
        }

        static int GetWindowHeightMiddle()
        {
            return Console.WindowHeight / 2;
        }
        
        static void Main(string[] args)
        {
            ConsoleKey keyPressed = ConsoleKey.A;
            Console.CursorVisible = false;
            Console.Clear();
            Console.SetCursorPosition(0, GetWindowHeightMiddle());
            Draw("[I]");
            
            do
            {
                if (Console.KeyAvailable)
                {
                    keyPressed = Console.ReadKey().Key;
                    MoveShip("[I]", keyPressed);
                }
                

            } while (KeyPressedIsNotEscape(keyPressed));

            Console.Write("Presione una tecla para salir...");
            Console.ReadKey(true);
        }
    }
}