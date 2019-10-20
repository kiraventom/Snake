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
            public struct Snake
            {
                public struct Head
                {
                    public const char N = '\u2542';
                    public const char S = '\u2542';
                    public const char W = '\u253f';
                    public const char E = '\u253f';
                }
                public struct Tail
                {
                    public const char N = '\u257d';
                    public const char S = '\u257f';
                    public const char W = '\u257c';
                    public const char E = '\u257e';
                }
                public struct Body
                {
                    public const char NS = '\u2503';
                    public const char WE = '\u2501';
                    public const char NW = '\u251b';
                    public const char NE = '\u2517';
                    public const char SW = '\u2513';
                    public const char SE = '\u250f';
                }
            }
            
            public const char Empty = ' ';
        }
    }
}
