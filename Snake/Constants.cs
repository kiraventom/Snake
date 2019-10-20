using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public struct Constants
    {
        public struct Chars
        {
            public struct Walls
            {
                public struct Angles
                {
                    public const char LeftUpAngle = '\u250c';
                    public const char RightUpAngle = '\u2510';
                    public const char LeftBottomAngle = '\u2514';
                    public const char RightBottomAngle = '\u2518';
                }
                public const char HorizontalWall = '\u2500';
                public const char VerticalWall = '\u2502';
            }
            public static class Extra
            {
                static Extra()
                {
                    Rnd = new Random();
                }

                private static Random Rnd { get; }

                private const string Apple = ".";
                private const string Pear = "=";
                private const string Cherry = "+";
                private const string Pizza = "o";
                private readonly static string[] FoodArray = { Apple, Pear, Cherry, Pizza };
                public static string Food => FoodArray[Rnd.Next(FoodArray.Length)];
                public const string Bonus = "\U0001F396";
            }
            public const char Snake = '\u2588';
            public const char Empty = ' ';
        }
    }
}
