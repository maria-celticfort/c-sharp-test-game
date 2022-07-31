using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_game.TestEngine;
using System.Drawing;
using System.Windows.Forms;

namespace test_game.TestEngine
{
    class DemoGame : TestEngine.TestEngineClass
    {
        Sprite2D player;
        bool left;
        bool right;
        bool up;
        bool down;

        Vector2 lastPos = Vector2.Zero();

        string[,] Map =
        {
            { "g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g" },
            { "g","c","c","g",".",".","c",".",".",".","c",".",".",".",".","c",".","g" },
            { "g","c","c","g",".","c",".","c",".","c",".","c",".",".",".",".","c","g" },
            { "g","c","c","g","c",".",".",".","c",".",".",".","c",".",".","c",".","g" },
            { "g","c","c","g","g","g","g","g","g","g","g","g","g",".",".","g","g","g" },
            { "g",".",".",".","c","c","g","c","c",".","c","c","g",".",".","c",".","g" },
            { "g",".",".",".",".",".","g",".",".",".",".",".","g",".",".",".","c","g" },
            { "g",".",".",".",".",".","g",".",".",".",".",".","g",".",".","c",".","g" },
            { "g",".",".","g",".",".","g",".",".","g",".",".","g",".",".",".","c","g" },
            { "g",".",".","g",".",".",".",".",".","g",".",".",".",".",".","c",".","g" },
            { "g",".",".","g",".",".",".",".",".","g",".",".",".",".",".",".","c","g" },
            { "g",".",".","g","c","c",".","c","c","g","c","c",".",".",".","c",".","g" },
            { "g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g" },
        };

        public DemoGame() : base(new Vector2(740,560), "Lil' guy goes for snails")
        {
        }

        public override void OnLoad()
        {

            BackgroundColor = Color.DarkGray;
            //CameraZoom is doing nothing rn, i just wanted to test it out
            CameraZoom = new Vector2(1f,1f);
            Sprite2D floorRef = new Sprite2D("stone");
            Sprite2D snailRef = new Sprite2D("snail1");

            for (int i = 0; i <Map.GetLength(1); i++)
            {
                for (int j=0; j <Map.GetLength(0); j++)
                {
                    if (Map[j, i] == "g")
                    {
                        new Sprite2D(new Vector2(i * 40, j * 40), new Vector2(40, 40), floorRef, "Floor");
                    }
                    if (Map[j, i] == "c")
                    {
                        new Sprite2D(new Vector2(i * 40, j * 40), new Vector2(36,18), snailRef, "Snail");
                    }

                }
            }

            player = new Sprite2D(new Vector2(45, 390), new Vector2(64, 84), "player_walk_1", "Player");
        }

        public override void OnDraw()
        {
           
        }

        public override void OnUpdate()
        {
            if (player == null)
            {
                return;
            }
            if (up)
            {
                player.Position.y -= 5f;
            }

            if (down)
            {
                player.Position.y += 5f;
            }

            if (right)
            {
                player.Position.x += 5f;
            }

            if (left)
            {
                player.Position.x -= 5f;
            }

            Sprite2D snail = player.IsColliding("Snail");
            if (snail != null)
            {
                snail.DestroySelf();
            }


            if (player.IsColliding("Floor") != null)
            {
                player.Position.x = lastPos.x;
                player.Position.y = lastPos.y;
            }
            else
            {
                lastPos.x = player.Position.x;
                lastPos.y = player.Position.y;
            }

        }


        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = true; }
            if (e.KeyCode == Keys.A) { left = true; }
            if (e.KeyCode == Keys.S) { down = true; }
            if (e.KeyCode == Keys.D) { right = true; }

        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = false; }
            if (e.KeyCode == Keys.A) { left = false; }
            if (e.KeyCode == Keys.S) { down = false; }
            if (e.KeyCode == Keys.D) { right = false; }
        }
    }
}
