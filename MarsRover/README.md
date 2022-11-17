Welcome to MarsRoverSolution, This solution needs to be loaded in VS2022, VS2019 gives some errors.

Ive made the plateau size configurable by adding an options class and a setting in appsettings.json

The plateau is loaded and the users must select a movements file to load.
The file is loaded and posted back to the server which uses an instance of an IFormFileReader to translate the instructions
into a format that the ISpaceVehicleInstructionParser instance can use to translate into a collection of SpaceVehicleInstructions
These instructions are in turn a collection of moves or positions, with the starting position being the first move.
Each position has an x and y coordinate and a direction that indicates which way the rover will be facing.

Direction is an abstract class of which there are 4 instances, North, South, East and West.
Each of these instances implements the abstract methods, MoveLeft, MoveRight and MoveOneSpace.
MoveLeft and MoveRight return a new Direction and MoveOneSpace returns a new set of coordinates;

The MarsRoverInstructionParser (instance of ISpaceVehicleInstructionParser) uses the methods on the Direction classes
to determine the list of positions (i.e. moves)

I have added tests for the MarsRoverInstructionParser and if I had more time would have added tests for the Direction classes and
the InstructionsFormFileReader.

For the front end I have used an ajax call to the MarsRoverController UploadFile post that returns the set of moves
for each rover.

These instructions are then iterated in an each loop that uses setTimeout to display the moves in sequence
There is a class for each direction. I could probably have done this better but my css skills are not great.
I'm sure a parameterised sass file wouild have been cleaner.