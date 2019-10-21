using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeClassLibrary
{
    public sealed class Game
    {
        public Game(int height, int width)
        {
            Field = new Field(height, width);
        }

        //public Task Start(IProgress<Field> progress)
        //{
        //    while (true)
        //    {
        //        Tick();
        //        progress.Report(this.Field);
        //    }
        //}

        public GameEvent Tick(Snake.Direction direction)
        {
            return Field.Update(direction);
        }

        public enum GameEvent { Lose, Upscore, None}

        public Field Field { get; }
    }
}
