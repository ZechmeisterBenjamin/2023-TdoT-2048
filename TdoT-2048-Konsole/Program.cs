using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;

namespace _2048_Spiel
{
    internal class Program
    {
        static bool addNmb = false;
        static int score = 0;
        static int highscore = 0;

        static void Main()
        {
            /*string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = path.Remove(path.LastIndexOf((char)92));
            StreamReader fileSr = new StreamReader(path + "/highscore.txt");
            string line = fileSr.ReadLine();
            if (line != null) highscore = int.Parse(line);
            else highscore = 0;
            fileSr.Close();
            StreamWriter fileSw = new StreamWriter(path + "/highscore.txt");*/
            Console.CursorVisible = false;
            string[,] slots = new string[4, 4];
            for (int i = 0; i < slots.GetLength(0); i++)
                for (int j = 0; j < slots.GetLength(1); j++)
                    slots[i, j] = "-";
            bool again = true;
            AddNumber(slots, 2);
            do
            {
                PrintSlots(slots);
                MoveCubes(slots);
                if (addNmb)
                    AddNumber(slots);
                if (IsGameOver(slots))
                    again = false;
            } while (again);
            PrintSlots(slots);
            Console.Clear();
            highscore = GetHighscore(score, highscore);
            Console.WriteLine($"Game Over\nScore: {score} \t highscore: {highscore}");
            /*fileSw.Write(highscore.ToString());
            fileSw.Close();*/
            Console.ReadLine();
        }

        private static int GetHighscore(int score, int oldHighscore)
        {
            int highscore;
            if (score > oldHighscore)
            {
                highscore = score;
            }
            else
            {
                highscore = oldHighscore;
            }
            return highscore;
        }

        private static bool IsGameOver(string[,] slots)
        {
            bool gameOver = true;
            for (int i = 0; i < slots.GetLength(0); i++)
                for (int j = 0; j < slots.GetLength(1); j++)
                    if (slots[i, j] == "-")
                    {
                        gameOver = false;
                    }
            if (gameOver)
            {
                for (int i = 0; i < slots.GetLength(0); i++)
                    for (int j = 0; j < slots.GetLength(1); j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            if (slots[i, j] != slots[i + 1, j] && slots[i, j] != slots[i, j + 1]) gameOver = true;
                        }
                        else if (i == 0 && j == 3)
                        {
                            if (slots[i, j] != slots[i + 1, j] && slots[i, j] != slots[i, j - 1]) gameOver = true;
                        }
                        else if (i == 3 && j == 0)
                        {
                            if (slots[i, j] != slots[i - 1, j] && slots[i, j] != slots[i, j + 1]) gameOver = true;
                        }
                        else if (i == 3 && j == 3)
                        {
                            if (slots[i, j] != slots[i - 1, j] && slots[i, j] != slots[i, j - 1]) gameOver = true;
                        }
                        else if (i == 0)
                        {
                            if (slots[i, j] != slots[i + 1, j] && slots[i, j] != slots[j - 1, j] && slots[i, j] != slots[i, j + 1]) gameOver = true;
                        }
                        else if (i == 3)
                        {
                            if (slots[i, j] != slots[i - 1, j] && slots[i, j] != slots[j - 1, j] && slots[i, j] != slots[i, j + 1]) gameOver = true;
                        }
                        else if (j == 0)
                        {
                            if (slots[i, j] != slots[i - 1, j] && slots[i, j] != slots[i + 1, j] && slots[i, j] != slots[i, j + 1]) gameOver = true;
                        }
                        else if (j == 3)
                        {
                            if (slots[i, j] != slots[i - 1, j] && slots[i, j] != slots[i + 1, j] && slots[i, j] != slots[i, j - 1]) gameOver = true;
                        }
                        else
                        {
                            if (slots[i, j] != slots[i - 1, j] && slots[i, j] != slots[i + 1, j] && slots[i, j] != slots[j - 1, j] && slots[i, j] != slots[i, j + 1]) gameOver = true;
                        }
                    }
            }
            return gameOver;
        }

        private static void AddNumber(string[,] slots, int amount = 1)
        {
            Random rnd = new Random();
            bool again;
            int i = 0, firstValue = 0;
            int slot = 0;
            while (i < amount)
            {
                if (i == 1) firstValue = slot;
                do
                {
                    slot = rnd.Next(0, 11);
                    int slot1 = rnd.Next(0, slots.GetLength(0));
                    int slot2 = rnd.Next(0, slots.GetLength(1));
                    if (slots[slot1, slot2] == "-")
                    {
                        if (slot != 10 || firstValue == 4)
                        {
                            slots[slot1, slot2] = 2.ToString();
                        }
                        else
                        {
                            slots[slot1, slot2] = 4.ToString();
                        }
                        again = false;
                    }
                    else again = true;
                } while (again);
                i++;
            }
            addNmb = false;
        }
        private static void MoveCubes(string[,] slots)
        {
            ConsoleKeyInfo key;
            bool moveAgain = true;
            key = Console.ReadKey();
            int slot;
            int cnt = 0;
            if (key.Key == ConsoleKey.UpArrow)
            {
                for (int i = 0; i < slots.GetLength(0); i++)
                {
                    for (int j = 0; j < slots.GetLength(1); j++)
                    {
                        if (slots[i, j] != "-")
                        {
                            slot = i;
                            MoveUp(slots, slot, j, i, cnt, moveAgain);
                        }
                    }
                }
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                for (int i = slots.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = slots.GetLength(1) - 1; j >= 0; j--)
                    {
                        if (slots[i, j] != "-")
                        {
                            slot = i;
                            MoveDown(slots, slot, j, i, cnt, moveAgain);
                        }
                    }
                }
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                for (int i = 0; i < slots.GetLength(0); i++)
                {
                    for (int j = 0; j < slots.GetLength(1); j++)
                    {
                        if (slots[i, j] != "-")
                        {
                            slot = j;
                            MoveLeft(slots, slot, j, i, cnt, moveAgain);
                        }

                    }
                }
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                for (int i = slots.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = slots.GetLength(1) - 1; j >= 0; j--)
                    {
                        if (slots[i, j] != "-")
                        {
                            slot = j;
                            MoveRight(slots, slot, j, i, cnt, moveAgain);
                        }

                    }
                }
            }
        }

        private static void MoveRight(string[,] slots, int slot, int j, int i, int cnt, bool moveAgain)
        {
            while (slot > -1 && cnt < 4)
            {
                if (slot < slots.GetLength(0) && slot > 0 && slot != j)
                {
                    if (slots[i, slot] == "-")
                    {
                        slots[i, slot] = slots[i, slot - 1];
                        slots[i, slot - 1] = "-";
                        addNmb = true;
                    }
                    else if (slots[i, slot] == slots[i, slot - 1])
                    {
                        int slotInt = int.Parse(slots[i, slot]);
                        slots[i, slot] = (slotInt * 2).ToString();
                        slots[i, slot - 1] = "-";
                        SetScore(slots, i, slot);
                        addNmb = true;
                        moveAgain = false;
                    }
                    if (!moveAgain)
                    {
                        break;
                    }
                }
                slot++;
                cnt++;
            }
        }

        private static void MoveLeft(string[,] slots, int slot, int j, int i, int cnt, bool moveAgain)
        {
            while (slot > -1 && cnt < 4)
            {
                if (slot != 3 && slot >= 0 & slot != j)
                {
                    if (slots[i, slot] == "-")
                    {
                        slots[i, slot] = slots[i, slot + 1];
                        slots[i, slot + 1] = "-";
                        addNmb = true;
                    }
                    else if (slots[i, slot] == slots[i, slot + 1])
                    {
                        int slotInt = int.Parse(slots[i, slot]);
                        slots[i, slot] = (slotInt * 2).ToString();
                        slots[i, slot + 1] = "-";
                        SetScore(slots, i, slot);
                        addNmb = true;
                        moveAgain = false;
                    }
                    if (!moveAgain)
                    {
                        break;
                    }
                }
                slot--;
                cnt++;
            }
        }

        private static void MoveDown(string[,] slots, int slot, int j, int i, int cnt, bool moveAgain)
        {
            while (slot > -1 && cnt < 4)
            {
                if (slot < 4 && slot >= 0 && slot != i)
                {
                    if (slots[slot, j] == "-")
                    {
                        slots[slot, j] = slots[slot - 1, j];
                        slots[slot - 1, j] = "-";
                        addNmb = true;
                    }
                    else if (slots[slot, j] == slots[slot - 1, j])
                    {
                        int slotInt = int.Parse(slots[slot, j]);
                        slots[slot, j] = (slotInt * 2).ToString();
                        slots[slot - 1, j] = "-";
                        SetScore(slots, slot, j);
                        addNmb = true;
                        moveAgain = false;
                    }
                    if (!moveAgain)
                    {
                        break;
                    }
                }
                slot++;
                cnt++;
            }
        }

        private static void MoveUp(string[,] slots, int slot, int j, int i, int cnt, bool moveAgain)
        {
            while (slot > -1 && cnt < 4)
            {
                if (slot != 3 && slot >= 0 & slot != i)
                {
                    if (slots[slot, j] == "-")
                    {
                        slots[slot, j] = slots[slot + 1, j];
                        slots[slot + 1, j] = "-";
                        addNmb = true;
                    }
                    else if (slots[slot, j] == slots[slot + 1, j])
                    {
                        int slotInt = int.Parse(slots[slot, j]);
                        slots[slot, j] = (slotInt * 2).ToString();
                        slots[slot + 1, j] = "-";
                        SetScore(slots, slot, j);
                        addNmb = true;
                        moveAgain = false;
                    }
                    if (!moveAgain)
                    {
                        break;
                    }
                }
                slot--;
                cnt++;
            }
        }

        private static void SetScore(string[,] slots, int i, int j)
        {
            score += int.Parse(slots[i, j]);
        }

        private static void PrintSlots(string[,] slots)
        {
            Console.Clear();
            for (int i = 0; i < slots.GetLength(0); i++)
            {
                for (int j = 0; j < slots.GetLength(1); j++)
                {
                    Console.Write((char)124);
                    if (slots[i, j] != "-")
                    {
                        if (slots[i, j] == 2.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if (slots[i, j] == 4.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                        }
                        else if (slots[i, j] == 8.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else if (slots[i, j] == 16.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }
                        else if (slots[i, j] == 32.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (slots[i, j] == 64.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                        }
                        else if (slots[i, j] == 128.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else if (slots[i, j] == 256.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else if (slots[i, j] == 512.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                        }
                        else if (slots[i, j] == 1024.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (slots[i, j] == 2048.ToString())
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        }
                        Console.Write(slots[i, j].PadLeft(4));
                    }
                    else
                    {
                        Console.Write(slots[i, j].PadLeft(4, '-'));
                    }
                    Console.ResetColor();
                }
                Console.WriteLine((char)124);
            }
            Console.WriteLine($"Score: {score}");
        }
    }
}