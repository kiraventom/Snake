using System;
using System.Collections.Generic;

namespace SnakeClassLibrary
{
    public sealed class Snake
    {
        public Snake(Coordinates headCoords)
        {
            Blocks = new List<SnakeBlock>
            {
                new SnakeBlock(headCoords, 0),
                new SnakeBlock(new Coordinates(headCoords.Row + 1, headCoords.Column), 1)
            };
        }

        public enum Direction { Up, Down, Left, Right }

        public void Move()
        {
            var oldBlockCoords = Head.Coords;
            var newBlockCoords = new Coordinates();
            switch (CurrentHeadDirection)
            {
                case Direction.Up:
                    newBlockCoords = new Coordinates(oldBlockCoords.Row - 1, oldBlockCoords.Column);
                    break;
                case Direction.Down:
                    newBlockCoords = new Coordinates(oldBlockCoords.Row + 1, oldBlockCoords.Column);
                    break;
                case Direction.Left:
                    newBlockCoords = new Coordinates(oldBlockCoords.Row, oldBlockCoords.Column - 1);
                    break;
                case Direction.Right:
                    newBlockCoords = new Coordinates(oldBlockCoords.Row, oldBlockCoords.Column + 1);
                    break;
            }
            Head = new SnakeBlock(newBlockCoords, Head.Index);

            for (int i = 1; i < this.Blocks.Count; ++i)
            {
                newBlockCoords = oldBlockCoords;
                oldBlockCoords = this.Blocks[i].Coords;
                this.Blocks[i] = new SnakeBlock(newBlockCoords, this.Blocks[i].Index);
            }
        }

        public void MoveTo(Block block)
        {
            var oldBlockCoords = Head.Coords;
            var newBlockCoords = block.Coords;
            Head = new SnakeBlock(newBlockCoords, Head.Index);
            for (int i = 1; i < this.Blocks.Count; ++i)
            {
                newBlockCoords = oldBlockCoords;
                oldBlockCoords = this.Blocks[i].Coords;
                this.Blocks[i] = new SnakeBlock(newBlockCoords, this.Blocks[i].Index);
            }
        }

        public void Grow()
        {
            var oldTailCoords = Tail.Coords;
            var newTailCoords = new Coordinates();
            switch (CurrentTailDirection)
            {
                case Direction.Up:
                    newTailCoords = new Coordinates(oldTailCoords.Row - 1, oldTailCoords.Column);
                    break;
                case Direction.Down:
                    newTailCoords = new Coordinates(oldTailCoords.Row + 1, oldTailCoords.Column);
                    break;
                case Direction.Left:
                    newTailCoords = new Coordinates(oldTailCoords.Row, oldTailCoords.Column - 1);
                    break;
                case Direction.Right:
                    newTailCoords = new Coordinates(oldTailCoords.Row, oldTailCoords.Column + 1);
                    break;
            }
            var newBlock = new SnakeBlock(newTailCoords, Tail.Index + 1);
            Blocks.Add(newBlock);
        }

        public int Length => Blocks.Count;
        public List<SnakeBlock> Blocks { get; }
        public SnakeBlock Head
        {
            get => Blocks[0];
            private set => Blocks[0] = value;
        }
        public SnakeBlock Tail
        {
            get => Blocks[Blocks.Count - 1];
            private set => Blocks[Blocks.Count - 1] = value;
        }
        public Direction CurrentHeadDirection;
        public Direction? CurrentTailDirection
        {
            get
            {
                var blockBeforeTail = Blocks[Blocks.Count - 2];
                if (Tail.Coords.Row > blockBeforeTail.Coords.Row)
                {
                    return Direction.Down;
                }
                else
                if (Tail.Coords.Row < blockBeforeTail.Coords.Row)
                {
                    return Direction.Up;
                }
                else
                if (Tail.Coords.Column > blockBeforeTail.Coords.Column)
                {
                    return Direction.Right;
                }
                else
                if (Tail.Coords.Column < blockBeforeTail.Coords.Column)
                {
                    return Direction.Left;
                }
                return null; // never
            }
        }
    }
}
