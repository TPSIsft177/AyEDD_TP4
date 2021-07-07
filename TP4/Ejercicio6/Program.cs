using System;
using System.Threading;

namespace Ejercicio6
{
    class Program
    {
        private const string ShipWithBomb    = "[I]";
        private const string ShipWithoutBomb = "[ ]";
        private const string Bomb            =  "I";
        
        static bool KeyPressedIsNotEscape(ConsoleKey keyPressed)
        {
            return ConsoleKey.Escape != keyPressed;
        }
        
        static void Draw(string objectToDraw)
        {
            Console.Write(objectToDraw);
        }

        static void MoveShip(string ship, ConsoleKey keyPressed)
        {
            Tuple<int, int> currentPosition = GetCurrentPosition();
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
                    
                    Draw(ship);
                    break;
                case ConsoleKey.RightArrow:
                    if(Console.WindowWidth > left+1) //No llegó al tope derecho
                        Console.SetCursorPosition(left - (ship.Length-2)-2, top);
                    else //Llegó al tope derecho por lo que no avanzo
                        Console.SetCursorPosition(Console.WindowWidth-ship.Length-2, top);
                    
                    Draw(ship);
                    break;
                case ConsoleKey.UpArrow:
                    if(top > 0)
                        Console.SetCursorPosition(left-ship.Length-1, top - 1);
                    else
                        Console.SetCursorPosition(left-ship.Length-1, 0);
                    
                    Draw(ship);
                    break;
                case ConsoleKey.DownArrow:
                    if(Console.WindowHeight > top + 2) // Dejo una fila para que se vea por lo menos la explosión
                        Console.SetCursorPosition(left-ship.Length-1, top + 1);
                    else
                        Console.SetCursorPosition(left-ship.Length-1, Console.WindowHeight - 2);
                    
                    Draw(ship);
                    break;
                default:
                    if(left > ship.Length)
                        Console.SetCursorPosition(left-ship.Length-1, top);

                    Draw(ship);
                    break;
            }
            
            //Draw(ship);
        }

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
                    Console.SetCursorPosition(left - ShipWithoutBomb.Length, shipPositionTop);
                    Draw(ShipWithoutBomb);
                    Console.SetCursorPosition(left - ShipWithoutBomb.Length + 1, bombTop + 1);
                    Draw(Bomb);
                    
                    bombTop = GetCurrentPosition().Item2;
                    Thread.Sleep(300);
                    
                    Console.Clear();
                    
                }
                
                Console.SetCursorPosition(left - ShipWithBomb.Length, shipPositionTop);
                Draw(ShipWithBomb);
            }
        }

        private static Tuple<int, int> GetCurrentPosition()
        {
            return Console.GetCursorPosition().ToTuple();
        }
        
        static int GetWindowHeightMiddle()
        {
            return Console.WindowHeight / 2;
        }
        
        static int GetWindowWidthMiddle()
        {
            return Console.WindowWidth / 2;
        }

        private static void ShowExitMessage()
        {
            string exitMessage = "Presione una tecla para salir...";
            
            Console.Clear();
            
            Console.SetCursorPosition(GetWindowWidthMiddle() - exitMessage.Length/2, GetWindowHeightMiddle());
            Console.Write(exitMessage);
            Console.ReadKey(true);
        }
        
        static void Main(string[] args)
        {
            ConsoleKey keyPressed = ConsoleKey.A;
            Console.CursorVisible = false;
            Console.Clear();
            Console.SetCursorPosition(0, GetWindowHeightMiddle());
            Draw(ShipWithBomb);
            
            do
            {
                if (Console.KeyAvailable && KeyPressedIsNotEscape(keyPressed = Console.ReadKey().Key))
                {
                    MoveShip(ShipWithBomb, keyPressed);
                    DropBomb(keyPressed);
                }

            } while (KeyPressedIsNotEscape(keyPressed));

            ShowExitMessage();
        }
    }
}