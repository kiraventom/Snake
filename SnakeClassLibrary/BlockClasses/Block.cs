namespace SnakeClassLibrary
{
    public abstract class Block
    {
        public Block(Coordinates coords)
        {
            Coords = coords;
        }

        public Coordinates Coords { get; }
    }
}
