using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using System.Media;

namespace SnakeGame
{
    public class Game
    {
        private static Map map = new Map();
        private Snake snake;
        private Food Food;
        private int score;
        private Menu mainMenu = new Menu();
        private SoundPlayer eatSound = new SoundPlayer(@"Resourses\\food.wav");

        private const int frameMs= 200;

        public Game()
        {
            Title = "Snake Game";
            ConsoleSetup();
            mainMenu.SelectedIndex = MenuSelect();
        }

        public void GameLoop()
        {
            while (true)
            {
                StartGame();
                Thread.Sleep(1000);
                ReadKey();
            }
        }

        public void StartGame()
        {
            mainMenu.MenuMusic.Stop();
            score = 0;
            Clear();
            DrawBorder();
            snake = new Snake(10, 5);
            Direction currentMovement = Direction.Right;
            InitialFood();
            Food = InitialFood();
            Food.GenerateFood();
            Food.Draw();

            Stopwatch sw = new Stopwatch();
            //Main Game loop
            while (true)
            {
                sw.Restart();
                Direction oldMovement = currentMovement;
                while (sw.ElapsedMilliseconds <= frameMs)
                {
                    if (currentMovement == oldMovement) currentMovement = ReadMovement(currentMovement);
                }
                eatSound.Load();

                if(snake.Head.X==Food.FoodPixel.X && snake.Head.Y == Food.FoodPixel.Y)
                {
                    eatSound.Play();
                    score++;
                    snake.Move(currentMovement, true);
                    Food = InitialFood();
                    Food.GenerateFood();
                    Food.Draw();
                }
                else
                {
                    snake.Move(currentMovement);
                }
                
                if (GameOverCondition(snake))
                {
                    mainMenu.MenuMusic.Load();
                    mainMenu.MenuMusic.PlayLooping();
                    break;
                }
            }
            snake.Clear();
            SetCursorPosition(map.MapHeight / 3, map.MapHeight / 3);
            GameOverMessage();
            DisplayMyScore();
        }
        static Direction ReadMovement(Direction currentDirection)
        {
            if (!KeyAvailable) return currentDirection;

            ConsoleKey key = ReadKey(true).Key;

            currentDirection = key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                _ => currentDirection
            };

            return currentDirection;
        }

        //Seting up Console size
        public void ConsoleSetup()
        {
            SetWindowSize(map.MapWidth, map.MapHeight);
            SetBufferSize(map.MapWidth, map.MapHeight);
            CursorVisible = false;
        }

        //Draw Map border
        public void DrawBorder()
        {
            for (int i = 0; i < map.MapWidth; i++)
            {
                new Pixel(i, 0, map.BorderColor).Draw();
                new Pixel(i, map.MapHeight-1, map.BorderColor).Draw();
            }

            for (int i = 0; i < map.MapHeight; i++)
            {
                new Pixel(0, i, map.BorderColor).Draw();
                new Pixel(map.MapWidth-1, i, map.BorderColor).Draw();
            }
        }

        public Food InitialFood()
        {
            return new Food(snake, map);
        }


        public int MenuSelect()
        {
            switch (mainMenu.SelectedIndex)
            {
                case 0:
                    GameLoop();
                    break;
                case 1:
                    mainMenu.About();
                    break;
                case 2:
                    mainMenu.ExitGame();
                    break;
            }
            return mainMenu.SelectedIndex;
        }

        //Chek the game over condition
        public bool GameOverCondition(Snake snake)
        {
            if (snake.Head.X == map.MapWidth - 1
                || snake.Head.X == 0
                || snake.Head.Y == map.MapHeight - 1
                || snake.Head.Y == 0
                || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
            {
                
                return true;
            }

            return false;
        }

        //Simple GameOver Message
        public void GameOverMessage()
        {
            
            Console.WriteLine("The Game is over");
        }

        public void DisplayMyScore()
        {
            SetCursorPosition(map.MapHeight / 3, map.MapHeight / 3 + 2);
            ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"Your score is {score}");
        }
    }
}
