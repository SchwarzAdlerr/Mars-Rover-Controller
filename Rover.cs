using System;
using System.Collections.Generic;
using System.Text;

namespace TermProject_Part2_MarsRover
{
    enum Direction { N, E, S, W };

    class Rover : Program
    {
        public static int roverCount = 0;
        public Direction direction;
        public int x, y;
        public string name;

        public Rover(int roverX, int roverY, char roverDir)
        {
            ++roverCount;
            name = roverCount.ToString();
            this.x = roverX;
            this.y = roverY;
            if (roverDir == 'N' || roverDir == 'n')
                this.direction = Direction.N;
            else if (roverDir == 'E' || roverDir == 'e')
                this.direction = Direction.E;
            else if (roverDir == 'S' || roverDir == 's')
                this.direction = Direction.S;
            else if (roverDir == 'W' || roverDir == 'w')
                this.direction = Direction.W;
            Console.WriteLine();
            Console.WriteLine($">>>Rover {name} deployed at {x} {y} {direction}");
        }
    }

}
