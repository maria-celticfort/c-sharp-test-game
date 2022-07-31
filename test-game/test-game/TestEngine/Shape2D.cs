using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_game.TestEngine
{
    public class Shape2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = "";
        public string Tag = "";

        public Shape2D (Vector2 Position, Vector2 Scale,string Directory ,string tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = tag;

            

            Log.Info($"[SHAPE2D]({Tag}) - Has been registered!");
            TestEngineClass.RegisterShape(this);
        }

        public void DestroySelf()
        {
            Log.Info($"[SHAPE2D]({Tag}) - Has been destroyed!");
            TestEngineClass.UnRegisterShape(this);
        }
    }
}
