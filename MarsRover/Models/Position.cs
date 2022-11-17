namespace MarsRover.Models
{
    public class Position
    {
        public Position(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public void MoveOneSpace()
        {

        }
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
    }
}
