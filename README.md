# Mars Rover
**THIS IS AN OBJECT ORIENTED PROGRAMMING LESSON TERM PROJECT**<br/>

A squad of robotic rovers are to be landed by NASA on a plateau on Mars. This plateau, which is curiously rectangular, must be navigated by the rovers so that their on board cameras can get a complete view of the surrounding terrain to send back to Earth.

A rover's position and location is represented by a combination of x and y coordinates and a letter representing one of the four cardinal compass points. The plateau is divided up into a grid to simplify navigation. An example position might be `0, 0, N` which means the rover is in the bottom left corner and facing North.

In order to control a rover, NASA sends a simple string of letters. The possible letters are `L`, `R`, and `M`. `L` and `R` makes the rover spin 90 degrees left or right respectively, without moving from its current spot. `M` means move forward one grid point, and maintain the same heading.

Assume that the square directly North from `(x, y)` is `(x, y+1)`.

### Input:
The first line of input is the upper-right coordinates of the plateau, the lower-left coordinates are assumed to be `0, 0`.

The position is made up of two integers and a letter separated by spaces, corresponding to the x and y coordinates and the rover's orientation.

Each rover will be finished sequentially, which means that the second rover won't start to move until the first one has finished moving.

### Output

The output for each rover should be its final coordinates and heading.

### Test

#### Test Input:
```
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
```

#### Expected Output:
```
1 3 N
5 1 E
```

Program will also detect when a movement command will result in

1. The rover to go off the bounds of the plateau
2. The rover to collide with an already existing rover

And if one of these is the case, the program will tell the user that the command is invalid, and ask the user for the input again.

Program accepts an arbitrary number of rovers, each consisting of 2 lines and stops taking input if the user types 'exit'.

## Tests

### Out of bounds:

#### Input:
```
5 5
1 3 N
MMM
```

#### Output:
```
Out of bounds at (1, 4). Please try a different command.
```

### Rover collision:

#### Input:
```
5 5
1 2 N
LM
0 3 E
RMM
```

#### Output:
```
There is already a rover at (0, 2). Please try a different command.
```
