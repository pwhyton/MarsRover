namespace MarsRover.Models
{
    public abstract class Direction
    {
        public abstract Direction MoveLeft();
        public abstract Direction MoveRight();
        public abstract string DirectionSymbol { get; }
        public abstract (int, int) MoveOneSpace(int x, int y);
    }

    public class North : Direction
    {
        const string _symbol = "N";
        public override string DirectionSymbol => _symbol;
        public override Direction MoveLeft()
        {
            return new West();
        }

        public override (int, int) MoveOneSpace(int x, int y)
        {
            return (x, y + 1);
        }

        public override Direction MoveRight()
        {
            return new East();
        }
    }

    public class South : Direction
    {
        const string _symbol = "S";
        public override string DirectionSymbol => _symbol;
        public override Direction MoveLeft()
        {
            return new East();
        }

        public override Direction MoveRight()
        {
            return new West();
        }

        public override (int, int) MoveOneSpace(int x, int y)
        {
            return (x, y - 1);
        }
    }

    public class East : Direction
    {
        const string _symbol = "E";

        public override string DirectionSymbol => _symbol;

        public override Direction MoveLeft()
        {
            return new North();
        }

        public override Direction MoveRight()
        {
            return new South();
        }

        public override (int, int) MoveOneSpace(int x, int y)
        {
            return (x + 1, y);
        }
    }

    public class West : Direction
    {
        const string _symbol = "W";

        public override string DirectionSymbol => _symbol;

        public override Direction MoveLeft()
        {
            return new South();
        }

        public override Direction MoveRight()
        {
            return new North();
        }

        public override (int, int) MoveOneSpace(int x, int y)
        {
            return (x - 1, y);
        }
    }
}
