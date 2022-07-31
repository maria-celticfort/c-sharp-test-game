using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace test_game.TestEngine
{
    /// <summary>
    /// This forces Windows SO to refresh the window
    /// </summary>
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }

    public abstract class TestEngineClass
    {
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "New Game";
        private Canvas Window = null;
        private Thread GameLoopThread = null;

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();

        public Color BackgroundColor = Color.Black;

        public Vector2 CameraZoom = new Vector2(1,1);
        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0f;

        public TestEngineClass(Vector2 ScreenSize, string Title)
        {
            Log.Info("Game is starting...");
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)ScreenSize.x, (int)ScreenSize.y);
            Window.Text = this.Title;
            Window.Paint += Renderer;

            Window.KeyUp += Window_KeyUp;
            Window.KeyDown += Window_KeyDown;

            Window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Window.FormClosing += Window_FormClosing;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        private void Window_FormClosing(object sewnder, FormClosingEventArgs e)
        {
            GameLoopThread.Abort();
        }


        public void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }

        public static void UnRegisterShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }

        public static void RegisterSprite(Sprite2D sprite)
        {
            AllSprites.Add(sprite);
        }

        public static void UnRegisterSprite(Sprite2D sprite)
        {
            AllSprites.Remove(sprite);
        }

        void GameLoop()
        {
            OnLoad();
            while (GameLoopThread.IsAlive)
            {
                //This try catch is waiting the game to load
                try 
                { 
                OnDraw();
                Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                OnUpdate();
                //Windows needs some rest after every refresh or I´ll get a frozen windows
                Thread.Sleep(2);
                }catch
                {
                    Log.Error("Game has not being found...");
                }
            }
        }

        public void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackgroundColor);

            g.TranslateTransform(CameraPosition.x, CameraPosition.y);
            g.RotateTransform(CameraAngle);
            g.ScaleTransform(CameraZoom.x, CameraZoom.y);

            try {
                foreach (Shape2D shape in AllShapes)
                {
                    g.FillRectangle(new SolidBrush(Color.Red), shape.Position.x, shape.Position.y, shape.Scale.x, shape.Scale.y);
                }

                foreach (Sprite2D sprite in AllSprites)
                {
                    if (!sprite.IsReference)
                    {
                        if ((sprite.Position == null) || (sprite.Scale == null))
                        {
                            sprite.Position = Vector2.Zero();
                            sprite.Scale = Vector2.Zero();
                        }
                        g.DrawImage(sprite.Sprite, sprite.Position.x, sprite.Position.y, sprite.Scale.x, sprite.Scale.y);
                    }
                }
            }
            catch
            {

            }
           
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);

    }
}
