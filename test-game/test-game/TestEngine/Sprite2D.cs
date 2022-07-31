using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace test_game.TestEngine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Tag = "";
        public string Directory = "";
        public Bitmap Sprite = null;
        public bool IsReference = false;

        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;
            this.Directory = Directory;

            Image tmp = Image.FromFile($"assets/sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp);

            Sprite = sprite;

            Log.Info($"[SPRITE2D]({Tag}) - Has been registered!");
            TestEngineClass.RegisterSprite(this);
        }


        public Sprite2D(string Directory)
        {
            this.Directory = Directory;

            Image tmp = Image.FromFile($"assets/sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp);

            Sprite = sprite;

            Log.Info($"[SPRITE2D]({Tag}) - Has been registered!");
            TestEngineClass.RegisterSprite(this);
        }

        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D reference, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            Sprite = reference.Sprite;

            Log.Info($"[REFERENCE2D]({Tag}) - Has been registered!");
            TestEngineClass.RegisterSprite(this);
        }

        public Sprite2D IsColliding(string tag)
        {

            foreach (Sprite2D b in TestEngineClass.AllSprites)
            {
                if (b.Tag == tag)
                {
                    if (Position.x < b.Position.x + b.Scale.x &&
                        Position.x + Scale.x > b.Position.x &&
                        Position.y < b.Position.y + b.Scale.y &&
                        Position.y + Scale.y > b.Position.y)
                    {
                        return b;
                    }
   
                }
            }

            return null;

        }

        /// <summary>
        /// I need a new collision 1:55:45
        /// </summary>


        public void DestroySelf()
        {
            TestEngineClass.UnRegisterSprite(this);
        }
    }
}
