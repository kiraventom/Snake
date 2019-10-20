namespace SnakeClassLibrary
{
    public class SnakeBlock : Block
    {
        public SnakeBlock(Coordinates coords, int index) : base(coords)
        {
            Index = index;
        }

        public int Index { get; }
    }
}
