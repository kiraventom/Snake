using System.Linq;

namespace SnakeClassLibrary
{
    public sealed class Field 
    {
        public Field(int height = 20, int width = 20)
        {
            Rnd = new System.Random();

            Height = height;
            Width = width;
            Blocks = new Block[height, width];
            CreateBlocks();
            Snake = CreateSnake();
            UpdateSnake();
            CreateFood();
        }

        public Game.GameEvent Update(Snake.Direction direction)
        {
            FoodBlock food = this.Blocks.Cast<Block>().Single(b => b is FoodBlock) as FoodBlock;
            bool foodEaten = false;

            Snake.CurrentHeadDirection = direction;
            
            if (Snake.Blocks.Any(block => block.Coords.Equals(Snake.Head.Coords) && block.Index != 0))
            {
                return Game.GameEvent.Lose;
            }
            else
            if (Snake.Head.Coords.Row == 0 && this.Snake.CurrentHeadDirection == Snake.Direction.Up)
            {
                Snake.MoveTo(this.Blocks[this.Height - 1, Snake.Head.Coords.Column]);
            }
            else
            if (Snake.Head.Coords.Row == this.Height - 1 && this.Snake.CurrentHeadDirection == Snake.Direction.Down)
            {
                Snake.MoveTo(this.Blocks[0, Snake.Head.Coords.Column]);
            }
            else
            if (Snake.Head.Coords.Column == 0 && this.Snake.CurrentHeadDirection == Snake.Direction.Left)
            {
                Snake.MoveTo(this.Blocks[Snake.Head.Coords.Row, this.Width - 1]);
            }
            else
            if (Snake.Head.Coords.Column == this.Width - 1 && this.Snake.CurrentHeadDirection == Snake.Direction.Right)
            {
                Snake.MoveTo(this.Blocks[Snake.Head.Coords.Row, 0]);
            }
            else
            {
                Snake.Move();
            }

            // if snake ate food, it grows
            if (Snake.Head.Coords.Equals(food.Coords))
            {
                Snake.Grow();
                foodEaten = true;
                
                for (int i = 0; i < Snake.Blocks.Count; ++i)
                {
                    if (Snake.Blocks[i].Coords.Row < 0)
                    {
                        Snake.Blocks[i] = 
                            new SnakeBlock(new Coordinates(this.Height - 1, Snake.Blocks[i].Coords.Column), i);
                    }
                    else
                    if (Snake.Blocks[i].Coords.Row == Height)
                    {
                        Snake.Blocks[i] =
                            new SnakeBlock(new Coordinates(0, Snake.Blocks[i].Coords.Column), i);
                    }
                    else
                    if (Snake.Blocks[i].Coords.Column < 0)
                    {
                        Snake.Blocks[i] =
                            new SnakeBlock(new Coordinates(Snake.Blocks[i].Coords.Row, this.Width - 1), i);
                    }
                    else
                    if (Snake.Blocks[i].Coords.Column == Width)
                    {
                        Snake.Blocks[i] =
                            new SnakeBlock(new Coordinates(Snake.Blocks[i].Coords.Row, 0), i);
                    }
                }
            }

            // temporary: recreating all blocks
            CreateBlocks();

            // updating snake
            UpdateSnake();

            // recreating food if eaten
            if (foodEaten)
            {
                CreateFood();
                return Game.GameEvent.Upscore;
            }
            else // just updating
            {
                Blocks[food.Coords.Row, food.Coords.Column] = food;
                return Game.GameEvent.None;
            }
        }

        private Snake CreateSnake()
        {
            Coordinates snakeHeadCoords = new Coordinates(this.Height / 2, this.Width / 2);
            return new Snake(snakeHeadCoords);
        }
        
        private void UpdateSnake()
        {
            foreach (var sb in Snake.Blocks)
            {
                Blocks[sb.Coords.Row, sb.Coords.Column] = sb;
            }
        }

        private void CreateBlocks()
        {
            for (int i = 0; i < Height; ++i)
            {
                for (int j = 0; j < Width; ++j)
                {
                    Blocks[i, j] = new FieldBlock(new Coordinates(i, j));
                }
            }
        }

        private Coordinates CreateFood()
        {
            int row;
            int column;
            do
            {
                row = Rnd.Next(Height);
                column = Rnd.Next(Width);
            }
            while (!(this.Blocks[row, column] is FieldBlock));
            this.Blocks[row, column] = new FoodBlock(new Coordinates(row, column));
            return new Coordinates(row, column);
        }

        public Block[,] Blocks { get; }
        public Snake Snake { get; }
        public int Height { get; }
        public int Width { get; }

        private System.Random Rnd { get; }
    }
}
