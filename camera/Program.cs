using System;
using System.Numerics;
using Raylib_cs;

namespace camera
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialization
            //--------------------------------------------------------------------------------------
            int MAX_BUILDINGS = 100;
            Random r = new Random();
            const int screenWidth = 800;
            const int screenHeight = 450;
            Raylib.InitWindow(screenWidth, screenHeight, "raylib [core] example - 2d camera");
            Rectangle player = new Rectangle(400, 280, 40, 40);
            Rectangle[] buildings = new Rectangle[MAX_BUILDINGS];
            Color[] buildColors = new Color[MAX_BUILDINGS];
            float spacing = 0;
            for (int i = 0; i < MAX_BUILDINGS; i++)
            {
                buildings[i].width = r.Next(50, 200);
                buildings[i].height = r.Next(100, 800);
                buildings[i].y = screenHeight - 130 - buildings[i].height;
                buildings[i].x = -6000 + spacing;
                spacing += buildings[i].width;
                buildColors[i] = new Color(r.Next(200, 240), r.Next(200, 240), r.Next(200, 250), 255);
            }
            Camera2D camera = new Camera2D();
            camera.target = new Vector2(player.x + 20, player.y + 20);
            camera.offset = new Vector2(screenWidth / 2, screenHeight / 2);
            camera.rotation = 0.0f;
            camera.zoom = 1.0f;
            Raylib.SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second
                                                       //--------------------------------------------------------------------------------------
                                                       // Main game loop
            while (!Raylib.WindowShouldClose())        // Detect window close button or ESC key
            {
                // Update
                //----------------------------------------------------------------------------------

                // Player movement
                if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                {
                    player.x += 2;
                }
                else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                {
                    player.x -= 2;
                }
                // Camera target follows player
                camera.target = new Vector2(player.x + 20, player.y + 20);
                // Camera rotation controls
                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                {
                    camera.rotation--;
                }
                else if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                {
                    camera.rotation++;
                }
                // Limit camera rotation to 80 degrees (-40 to 40)
                if (camera.rotation > 40)
                {
                    camera.rotation = 40;
                }
                else if (camera.rotation < -40)
                {
                    camera.rotation = -40;
                }
                // Camera zoom controls
                camera.zoom += Raylib.GetMouseWheelMove() * 0.05f;
                if (camera.zoom > 3.0f)
                {
                    camera.zoom = 3.0f;
                }
                else if (camera.zoom < 0.1f)
                {
                    camera.zoom = 0.1f;
                }
                // Camera reset (zoom and rotation)
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
                {
                    camera.zoom = 1.0f;
                    camera.rotation = 0.0f;
                }
                //----------------------------------------------------------------------------------
                // Draw
                //----------------------------------------------------------------------------------
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RAYWHITE);
                Raylib.BeginMode2D(camera);
                Raylib.DrawRectangle(-6000, 320, 13000, 8000, Color.DARKGRAY);
                for (int i = 0; i < MAX_BUILDINGS; i++)
                {
                    Raylib.DrawRectangleRec(buildings[i], buildColors[i]);
                }
                Raylib.DrawRectangleRec(player, Color.RED);
                Raylib.DrawLine((int)camera.target.X, -screenHeight * 10, (int)camera.target.X, screenHeight * 10, Color.GREEN);
                Raylib.DrawLine(-screenWidth * 10, (int)camera.target.Y, screenWidth * 10, (int)camera.target.Y, Color.GREEN);
                Raylib.EndMode2D();
                Raylib.DrawText("SCREEN AREA", 640, 10, 20, Color.RED);
                Raylib.DrawRectangle(0, 0, screenWidth, 5, Color.RED);
                Raylib.DrawRectangle(0, 5, 5, screenHeight - 10, Color.RED);
                Raylib.DrawRectangle(screenWidth - 5, 5, 5, screenHeight - 10, Color.RED);
                Raylib.DrawRectangle(0, screenHeight - 5, screenWidth, 5, Color.RED);
                Raylib.DrawRectangle(10, 10, 250, 113, Raylib.Fade(Color.SKYBLUE, 0.5f));
                Raylib.DrawRectangleLines(10, 10, 250, 113, Color.BLUE);
                Raylib.DrawText("Free 2d camera controls:", 20, 20, 10, Color.BLACK);
                Raylib.DrawText("- Right/Left to move Offset", 40, 40, 10, Color.DARKGRAY);
                Raylib.DrawText("- Mouse Wheel to Zoom in-out", 40, 60, 10, Color.DARKGRAY);
                Raylib.DrawText("- A / S to Rotate", 40, 80, 10, Color.DARKGRAY);
                Raylib.DrawText("- R to reset Zoom and Rotation", 40, 100, 10, Color.DARKGRAY);
                Raylib.EndDrawing();
                //----------------------------------------------------------------------------------
            }
            // De-Initialization
            //--------------------------------------------------------------------------------------
        }
    }
}
