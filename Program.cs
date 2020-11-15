using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;

namespace TermProject_Part2_MarsRover
{
    class Program
    {
        static bool state;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Rover Controller 2.1");
            Console.WriteLine("First you must declare boundaries for plateu...");
            CreatePlateu(out int[,] plateu);

            List<Rover> rovers = new List<Rover>();
            string action=null;
            while (action != "4")
            {
                Console.WriteLine();
                Console.WriteLine("Actions you can take now are listed below:");
                Console.WriteLine("1. Deploy a Rover");
                if (rovers.Count > 0)
                {
                    Console.WriteLine("2. Control a Rover");
                    Console.WriteLine("3. Learn the location of all Rovers");
                }
                Console.WriteLine("4. Exit");
                Console.Write("Type the number of the action you want to take: ");
                action = Console.ReadLine();

                switch (action)
                {
                    case "1": //DEPLOYING
                        do
                        {
                            Console.Write("\nEnter deployment coordinates: ");
                            Input(out int deployX, out int deployY, out char deployDir);
                            if (plateu[deployX, deployY] != 0)
                            {
                                state = false;
                                Console.WriteLine();
                                Console.Beep();
                                Console.WriteLine("!!! WARNING !!!");
                                Console.WriteLine("There is already a rover exists on the deployment coordinates!!");
                                Console.WriteLine("Please enter another coordinate for deployment...");
                            }
                            else
                            {
                                state = true;
                                rovers.Add(new Rover(deployX, deployY, deployDir));
                                plateu[deployX, deployY] = Convert.ToInt32(rovers[rovers.Count-1].name);
                                if (rovers.Count > 1)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine($"There are currently {rovers.Count} rovers on the map..");
                                    foreach (Rover rover in rovers)
                                    {
                                        Console.WriteLine($"{rover.name} : {rover.x} {rover.y} {rover.direction}");
                                    }
                                }
                            }
                        } while (state != true);
                        break;
                    case "2": //MOVING
                        if (rovers.Count < 2)
                        {
                            Move(rovers[0],plateu);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine();
                            foreach (Rover rover in rovers)
                            {
                                Console.WriteLine($"{rover.name} : {rover.x} {rover.y} {rover.direction}");
                            }
                            Console.Write("Which rover you want to control: ");
                            int roverControlNumber = Convert.ToInt32(Console.ReadLine()) - 1;
                            Move(rovers[roverControlNumber], plateu);
                        }
                        break;
                    case "3": //LOCATION OUTPUT WITH MAP
                        Console.WriteLine();
                        for (int yIndex = (plateu.GetLength(1) - 1); yIndex >= 0; yIndex--)
                        {
                            for (int xIndex = 0; xIndex < plateu.GetLength(0); xIndex++)
                            {
                                if (plateu[xIndex, yIndex] == 0)
                                    Console.Write("- ");
                                else
                                    Console.Write(plateu[xIndex, yIndex] + " ");
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                        foreach (Rover rover in rovers)
                        {
                            Console.WriteLine($"{rover.name} : {rover.x} {rover.y} {rover.direction}");
                        }
                        break;
                    case "4": //END PROGRAM
                        break;
                    default:
                        Console.WriteLine("Please choose an action within the parameters!");
                        break;
                }
            }
        }
        //CREATES THE MOVING AREA CALLED PLATEU
        public static void CreatePlateu(out int[,] plateuDef)
        {
            int xBound, yBound;

            Console.Write("Please enter plateu boundary point: ");
            string boundaryCoordinates = Console.ReadLine();
            string[] boundaries = boundaryCoordinates.Split(" ", StringSplitOptions.RemoveEmptyEntries); //CLEARS OUT SPACES IN THE ENTRY
            Console.WriteLine();
            Console.WriteLine(">>>Plateu created with boundary point: " + boundaries[0] + " " + boundaries[1]);

            xBound = Convert.ToInt32(boundaries[0]) + 1;
            yBound = Convert.ToInt32(boundaries[1]) + 1;

            plateuDef = new int[xBound, yBound];

            for (int yIndex = (yBound-1); yIndex >= 0; yIndex--) //PRINTS OUT PLATEU AS COORDINATES
            {
                for (int xIndex = 0; xIndex < xBound; xIndex++)
                {
                    Console.Write("[" + xIndex + "," + yIndex + "]");
                }
                Console.WriteLine();
            }

        }
        //CLEARS SPACES OUT OF INPUTS
        public static void Input(out int inputX, out int inputY, out char inputDir)
        {
            string Inputs = Console.ReadLine();
            string[] input = Inputs.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            inputX = Convert.ToInt32(input[0]);
            inputY = Convert.ToInt32(input[1]);
            inputDir = Convert.ToChar(input[2]);
        }
        //FUNCTION TO MOVE THE ROVER
        public static void Move(Rover roverMoving, int[,] area)
        {
            Console.WriteLine();
            Console.Write("Please enter directives for controlling: ");
            char[] directives = Console.ReadLine().ToUpper().ToCharArray();

            foreach (char dir in directives)
            {
                if (state == false)
                    break;
                
                try
                {
                    if (dir == 'L')
                    {
                        switch (roverMoving.direction)
                        {
                            case Direction.N:
                                roverMoving.direction = Direction.W;
                                break;
                            case Direction.E:
                                roverMoving.direction = Direction.N;
                                break;
                            case Direction.S:
                                roverMoving.direction = Direction.E;
                                break;
                            case Direction.W:
                                roverMoving.direction = Direction.S;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (dir == 'R')
                    {
                        switch (roverMoving.direction)
                        {
                            case Direction.N:
                                roverMoving.direction = Direction.E;
                                break;
                            case Direction.E:
                                roverMoving.direction = Direction.S;
                                break;
                            case Direction.S:
                                roverMoving.direction = Direction.W;
                                break;
                            case Direction.W:
                                roverMoving.direction = Direction.N;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (dir == 'M')
                    {
                        switch (roverMoving.direction)
                        {
                            case Direction.N:
                                if (area[roverMoving.x, roverMoving.y + 1] != 0)
                                {
                                    state = false;
                                    Console.WriteLine();
                                    Console.Beep();
                                    Console.WriteLine("!!! WARNING !!!");
                                    Console.WriteLine($"Rover {roverMoving.name} was about to collide with Rover {area[roverMoving.x, roverMoving.y + 1]} so action stopped!!!");
                                    Console.WriteLine("Please try a different command");
                                }
                                else
                                {
                                    state = true;
                                    area[roverMoving.x, roverMoving.y] = 0;
                                    roverMoving.y += 1;
                                    area[roverMoving.x, roverMoving.y] = Convert.ToInt32(roverMoving.name);
                                }
                                break;
                            case Direction.E:
                                if (area[roverMoving.x + 1, roverMoving.y] != 0)
                                {
                                    state = false;
                                    Console.WriteLine();
                                    Console.Beep();
                                    Console.WriteLine("!!! WARNING !!!");
                                    Console.WriteLine($"Rover {roverMoving.name} was about to collide with Rover {area[roverMoving.x + 1, roverMoving.y]} so action stopped!!!");
                                    Console.WriteLine("Please try a different command");
                                }
                                else
                                {
                                    state = true;
                                    area[roverMoving.x, roverMoving.y] = 0;
                                    roverMoving.x += 1;
                                    area[roverMoving.x, roverMoving.y] = Convert.ToInt32(roverMoving.name);
                                }
                                break;
                            case Direction.S:
                                if (area[roverMoving.x, roverMoving.y - 1] != 0)
                                {
                                    state = false;
                                    Console.WriteLine();
                                    Console.Beep();
                                    Console.WriteLine("!!! WARNING !!!");
                                    Console.WriteLine($"Rover {roverMoving.name} was about to collide with Rover {area[roverMoving.x, roverMoving.y - 1]} so action stopped!!!");
                                    Console.WriteLine("Please try a different command");
                                }
                                else
                                {
                                    state = true;
                                    area[roverMoving.x, roverMoving.y] = 0;
                                    roverMoving.y -= 1;
                                    area[roverMoving.x, roverMoving.y] = Convert.ToInt32(roverMoving.name);
                                }
                                break;
                            case Direction.W:
                                if (area[roverMoving.x - 1, roverMoving.y] != 0)
                                {
                                    state = false;
                                    Console.WriteLine();
                                    Console.Beep();
                                    Console.WriteLine("!!! WARNING !!!");
                                    Console.WriteLine($"Rover {roverMoving.name} was about to collide with Rover {area[roverMoving.x - 1, roverMoving.y]} so action stopped!!!");
                                    Console.WriteLine("Please try a different command");
                                }
                                else
                                {
                                    state = true;
                                    area[roverMoving.x, roverMoving.y] = 0;
                                    roverMoving.x -= 1;
                                    area[roverMoving.x, roverMoving.y] = Convert.ToInt32(roverMoving.name);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine();
                    Console.Beep();
                    Console.WriteLine("!!! WARNING !!!");
                    Console.WriteLine("Rover tried walk on the edge of the plateu");
                    Console.WriteLine("It stopped before falling over!!!");
                    break;
                }
                
            }
            Console.WriteLine($"\n>>>Rover {roverMoving.name}'s last location is {roverMoving.x} {roverMoving.y} {roverMoving.direction}");
            state = true;
        }

    }
}
