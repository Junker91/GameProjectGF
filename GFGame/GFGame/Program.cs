using System;
using System.Drawing;
//using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Example {
    class MyApplication {
        [STAThread]
        public static void Main() {

            double beginX = 300.0, beginY = 300.0, QHeight = 50.0, QWidth = 50.0;
            int acceleration = 1, accelerationCount = 0, accelerationCountTarget = 10;

            using (GameWindow game = new GameWindow()) {

                // Always start by loading the things needed for the game.
                game.Load += (sender, e) => {
                    // setup settings, load textures, sounds
                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    game.Width = 1024;
                    game.Height = 768;
                    game.X = 400;
                    game.Y = 200;
                    GL.Ortho(0.0, (double)game.Width, (double)game.Height, 0.0, 0.0, 4.0);
                    //       left,       right,             bottom,       top, zNear, zFar  <-- all double
                    game.VSync = VSyncMode.On;

                    game.WindowBorder = WindowBorder.Fixed;
                };

                //Resize
                game.Resize += (sender, e) => {
                    GL.Viewport(0, 0, game.Width, game.Height);
                };

                game.UpdateFrame += (sender, e) => {
                    // add game logic, input handling
                    if (game.Keyboard[Key.Escape]) {
                        game.Exit();
                    }

                    if (game.Keyboard[Key.Down]) {
                        beginY+= acceleration;
                    }
                    else if (game.Keyboard[Key.Up]) {
                        beginY-= acceleration;
                    }

                    if (game.Keyboard[Key.Right]) {
                        beginX+= acceleration;
                        accelerationCount++;
                        if (accelerationCount == accelerationCountTarget) {
                            acceleration++;
                            accelerationCount = 0;
                        }
                    }
                    else if (game.Keyboard[Key.Left]) {
                        beginX-= acceleration;
                        accelerationCount++;
                        if (accelerationCount == accelerationCountTarget) {
                            acceleration++;
                            accelerationCount = 0;
                        }
                    }
                    else {
                        accelerationCount = 0;
                        acceleration = 1;
                    }

                    if (game.Keyboard[Key.K]) {
                        beginX = 300.0;
                        beginY = 300.0;
                    }
                };

                game.RenderFrame += (sender, e) => {
                    // render graphics
                    GL.ClearColor(0.1f, 0.8f, 1.0f, 1.0f);
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    GL.Begin(PrimitiveType.Quads);

                    GL.Color3(Color.Cyan);
                    GL.Vertex2(beginX, QHeight + beginY);

                    GL.Color3(Color.Black);
                    GL.Vertex2(beginX, beginY);

                    GL.Color3(Color.Green);
                    GL.Vertex2(QWidth + beginX, beginY);

                    GL.Color3(Color.Red);
                    GL.Vertex2(QWidth + beginX, QHeight + beginY);

                    GL.End();
                    //Draw stuff here

                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}
