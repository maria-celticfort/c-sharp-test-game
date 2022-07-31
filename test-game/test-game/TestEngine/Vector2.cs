using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_game.TestEngine
{
    public class Vector2
    {
        public float x {get; set;}
        public float y {get; set;}

        public Vector2()
        {
            x = Zero().x;
            y = Zero().y;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Returns x and y as 0
        /// </summary>
        /// <returns></returns>
        public static Vector2 Zero()
        {
            return new Vector2(0,0);
        }
    }
}
