namespace SnakeClassLibrary
{
    public struct Coordinates
    {
        public Coordinates(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }
        public int Column { get; }

        public override bool Equals(object obj)
        {
            return obj is Coordinates coordinates && this.Row == coordinates.Row && this.Column == coordinates.Column;
        }
    }
}
