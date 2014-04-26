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

            using (GameWindow game = new GameWindow()) {

                // Always start by loading the things needed for the game.
                game.Load += (sender, e) => {
                    // setup settings, load textures, sounds
                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
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
                };

                game.RenderFrame += (sender, e) => {
                    // render graphics
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                    //Draw stuff here

                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}
