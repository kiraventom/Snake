using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SnakeClassLibrary;

namespace SnakeGame
{
    static class Program
    {
        static Program()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
        }

        static Game _game { get; set; }
        static int CurrentScore { get; set; }
        static double CurrentSpeed { get; set; }

        static void Main(string[] args)
        {
            while (true)
            {
                bool shouldQuit = StartGame();
                if (shouldQuit)
                    break;
            }
        }

        private static bool StartGame()
        {
            const int size = 20;
            _game = new Game(size, size);
            CurrentScore = 2;
            CurrentSpeed = 2.0;
            var direction = Snake.Direction.Up;
            ConsoleKey? lastKeyPressed = null;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    lastKeyPressed = Console.ReadKey(true).Key;
                }
                switch (lastKeyPressed)
                {
                    case null:
                        break;
                    case ConsoleKey.W:
                        if (direction != Snake.Direction.Down)
                            direction = Snake.Direction.Up;
                        break;
                    case ConsoleKey.A:
                        if (direction != Snake.Direction.Right)
                            direction = Snake.Direction.Left;
                        break;
                    case ConsoleKey.S:
                        if (direction != Snake.Direction.Up)
                            direction = Snake.Direction.Down;
                        break;
                    case ConsoleKey.D:
                        if (direction != Snake.Direction.Left)
                            direction = Snake.Direction.Right;
                        break;
                    case ConsoleKey.Escape:
                        return true;
                }
                switch (_game.Tick(direction))
                {
                    case Game.GameEvent.Lose:
                        Console.SetCursorPosition(_game.Field.Width / 2, _game.Field.Height / 2);
                        Console.Write($"Game Over! Your score:{CurrentScore}");
                        Console.ReadKey();
                        Console.Clear();
                        return false;
                    case Game.GameEvent.Upscore:
                        CurrentScore++;
                        CurrentSpeed += 0.1;
                        if (CurrentScore > Properties.Settings.Default.HighScore)
                        {
                            Properties.Settings.Default.HighScore = CurrentScore;
                            Properties.Settings.Default.Save();
                        }
                        break;
                    default:
                        break;
                }
                Update(_game.Field);
                Thread.Sleep((int)(500 / CurrentSpeed));
            }
        }

        private static void Update(Field field)
        {
            DrawBorders(field.Height, field.Width * 2);
            Console.Write($"\nHigh Score: {Properties.Settings.Default.HighScore}");
            Console.Write($"\nYour Score: {CurrentScore}");
            DrawSnake(field.Snake);
            DrawFood(field);
            Console.SetCursorPosition(0, 0);
        }

        private static void DrawFood(Field field)
        {
            var food = field.Blocks.Cast<Block>().Single(block => block is FoodBlock);
            Console.SetCursorPosition((food.Coords.Column * 2) + 1, food.Coords.Row + 1);
            Console.Write(Constants.Chars.Extra.Food);
        }

        private static void DrawSnake(Snake snake)
        {
            for (int i = 0; i < snake.Blocks.Count; ++i)
            {
                Console.SetCursorPosition((snake.Blocks[i].Coords.Column * 2) + 1, snake.Blocks[i].Coords.Row + 1);
                string s = "";
                if (i == 0)
                {
                    switch (snake.CurrentHeadDirection)
                    {
                        case Snake.Direction.Up:
                            s = Constants.Chars.Snake.Head.N.ToString();
                            break;
                        case Snake.Direction.Down:
                            s = Constants.Chars.Snake.Head.S.ToString();
                            break;
                        case Snake.Direction.Left:
                            s = Constants.Chars.Snake.Body.WE.ToString() + Constants.Chars.Snake.Head.W.ToString();
                            break;
                        case Snake.Direction.Right:
                            s = Constants.Chars.Snake.Head.E.ToString() + Constants.Chars.Snake.Body.WE.ToString();
                            break;
                    }
                }
                else
                if (i == snake.Blocks.Count - 1)
                {
                    switch (snake.CurrentTailDirection.Value)
                    {
                        case Snake.Direction.Up:
                            s = Constants.Chars.Snake.Tail.N.ToString();
                            break;
                        case Snake.Direction.Down:
                            s = Constants.Chars.Snake.Tail.S.ToString();
                            break;
                        case Snake.Direction.Left:
                            s = Constants.Chars.Snake.Tail.W.ToString() + Constants.Chars.Snake.Body.WE.ToString();
                            break;
                        case Snake.Direction.Right:
                            s = Constants.Chars.Snake.Body.WE.ToString() + Constants.Chars.Snake.Tail.E.ToString();
                            break;
                    }
                }
                else
                if (snake.Blocks[i].Coords.Row == snake.Blocks[i - 1].Coords.Row &&
                    snake.Blocks[i].Coords.Row == snake.Blocks[i + 1].Coords.Row)
                {
                    s = Constants.Chars.Snake.Body.WE.ToString() + Constants.Chars.Snake.Body.WE.ToString();
                }
                else
                if (snake.Blocks[i].Coords.Column == snake.Blocks[i - 1].Coords.Column &&
                    snake.Blocks[i].Coords.Column == snake.Blocks[i + 1].Coords.Column)
                {
                    s = Constants.Chars.Snake.Body.NS.ToString();
                }
                else
                if (snake.Blocks[i].Coords.Column > snake.Blocks[i - 1].Coords.Column &&
                    snake.Blocks[i].Coords.Row > snake.Blocks[i + 1].Coords.Row ||
                    snake.Blocks[i].Coords.Column > snake.Blocks[i + 1].Coords.Column &&
                    snake.Blocks[i].Coords.Row > snake.Blocks[i - 1].Coords.Row)
                {
                    s = Constants.Chars.Snake.Body.NW.ToString();
                }
                else
                if (snake.Blocks[i].Coords.Column < snake.Blocks[i - 1].Coords.Column &&
                    snake.Blocks[i].Coords.Row > snake.Blocks[i + 1].Coords.Row ||
                    snake.Blocks[i].Coords.Column < snake.Blocks[i + 1].Coords.Column &&
                    snake.Blocks[i].Coords.Row > snake.Blocks[i - 1].Coords.Row)
                {
                    s = Constants.Chars.Snake.Body.NE.ToString() + Constants.Chars.Snake.Body.WE.ToString();
                }
                else
                if (snake.Blocks[i].Coords.Column > snake.Blocks[i - 1].Coords.Column &&
                    snake.Blocks[i].Coords.Row < snake.Blocks[i + 1].Coords.Row ||
                    snake.Blocks[i].Coords.Column > snake.Blocks[i + 1].Coords.Column &&
                    snake.Blocks[i].Coords.Row < snake.Blocks[i - 1].Coords.Row)
                {
                    s = Constants.Chars.Snake.Body.SW.ToString();
                }
                else
                {
                    s = Constants.Chars.Snake.Body.SE.ToString() + Constants.Chars.Snake.Body.WE.ToString();
                }

                Console.Write(s);
            }
        }

        private static void DrawBorders(int height, int width)
        {
            string borders = GetBorders(height, width);
            Console.Write(borders);
        }

        static string GetBorders(int height, int width)
        {
            var lines = new StringBuilder();
            var line = new StringBuilder();

            line.Append(Constants.Chars.Walls.Angles.LeftUpAngle);
            line.Append(Constants.Chars.Walls.HorizontalWall, width);
            line.Append(Constants.Chars.Walls.Angles.RightUpAngle);

            lines.AppendLine(line.ToString());

            line.Clear();

            line.Append(Constants.Chars.Walls.VerticalWall);
            line.Append(Constants.Chars.Empty, width);
            line.Append(Constants.Chars.Walls.VerticalWall);

            for (int i = 0; i < height; ++i)
            {
                lines.AppendLine(line.ToString());
            }

            line.Clear();
            line.Append(Constants.Chars.Walls.Angles.LeftBottomAngle);
            line.Append(Constants.Chars.Walls.HorizontalWall, width);
            line.Append(Constants.Chars.Walls.Angles.RightBottomAngle);

            lines.AppendLine(line.ToString());

            return lines.ToString();
        }
    }
}
