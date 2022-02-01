using System;
using static System.Console;
using System.Media;

namespace SnakeGame
{

    public class Menu
    {
        private int selectedIndex;
        private string[] options;
        private Map map = new Map();
        private ConsoleColor borderColor = ConsoleColor.Green;
        private SoundPlayer menuMusic = new SoundPlayer(@"Resourses\\menu.wav");


        public SoundPlayer MenuMusic { get { return menuMusic; } }
        public int SelectedIndex { get => selectedIndex; set =>selectedIndex=value; }


        public Menu()
        {
            menuMusic.LoadAsync();
            menuMusic.PlayLooping();
            options = new string[]{ "Play", "About", "Exit" };
            selectedIndex = CalculateSelectedMenu();
        }

        private void DisplayMenu()
        {
            StartMenu();

            for (int i = 0; i < options.Length; i++)
            {
                SettingUpConsole(i);
                string currentOptions = options[i];
                string prefix;
                if (i == selectedIndex)
                {
                    prefix = "*";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " "; 
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($"{prefix}<< {currentOptions} >>");
            }
            ResetColor();
        }

        private int CalculateSelectedMenu()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                DisplayMenu();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                //Update selectedIndex based on arrow keys

                if(keyPressed == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex == -1) selectedIndex = options.Length - 1;
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex == options.Length) selectedIndex = 0;
                }

            } while (keyPressed!=ConsoleKey.Enter);
            return selectedIndex;
        }
       
        private void StartMenu()
        {
            DrawBorder();
            SettingUpConsole(-2);
            ForegroundColor = ConsoleColor.Green;

            Write("Snake Game");
        }

        private void DrawBorder()
        {
           
            for (int i = 0; i < map.MapWidth; i++)
            {
                new Pixel(i, 0, borderColor).Draw();
                new Pixel(i, map.MapHeight - 1, borderColor).Draw();
            }

            for (int i = 0; i < map.MapHeight; i++)
            {
                new Pixel(0, i, borderColor).Draw();
                new Pixel(map.MapWidth  - 1, i, borderColor).Draw();
            }
        }

        private void SettingUpConsole(int i)
        {
            SetCursorPosition(map.MapWidth/3, map.MapHeight/3+i);
            CursorVisible = false;
        }

        public void About()
        {
            Clear();
            Title = "About";

            WriteLine("My first Console Game");
            WriteLine("Date: 01.02.2022");

            WriteLine("<< Exit >>");
            ReadKey();
        }
        public void ExitGame()
        {
            Environment.Exit(0);
        }
    }
}
