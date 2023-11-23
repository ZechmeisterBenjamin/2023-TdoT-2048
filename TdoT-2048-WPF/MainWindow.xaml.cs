using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TdoT_2048_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool addNmb = false;
        static int score = 0;
        List<List<Button>> slots = new List<List<Button>>();
        public MainWindow()
        {
            InitializeComponent();
            FillSlots();
            AddRandomNumbers(slots, 2);
            Main_Window.KeyDown += new KeyEventHandler(OnButtonKeyDown);
        }
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Down ||e.Key == Key.Up ||e.Key == Key.Left || e.Key == Key.Right)
            {
            if (IsGameOver(slots)) MessageBox.Show("Its Joever :(");
            else
                {
            MoveNumbers(e.Key);
            if(addNmb)
            AddRandomNumbers(slots, 1);
            addNmb = false;
                }
            }
            else if(e.Key == Key.R)
            {
                ClearGame();
                AddRandomNumbers(slots, 2);
            }
        }
        private void MoveNumbers(Key direction)
        {
            int slot;
            int cnt = 0;
            if (direction == Key.Up)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    for (int j = 0; j < slots[i].Count; j++)
                    {
                        if (slots[i][j].Content.ToString() != "-")
                        {
                            slot = i;
                            MoveUp(slots, slot, j, i, cnt);
                        }
                    }
                }
            }
            else if (direction == Key.Down)
            {
                for (int i = slots.Count - 1; i >= 0; i--)
                {
                    for (int j = slots[i].Count - 1; j >= 0; j--)
                    {
                        if (slots[i][j].Content.ToString() != "-")
                        {
                            slot = i;
                            MoveDown(slots, slot, j, i, cnt);
                        }
                    }
                }
            }
            else if (direction == Key.Left)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    for (int j = 0; j < slots[i].Count; j++)
                    {
                        if (slots[i][j].Content.ToString() != "-")
                        {
                            slot = j;
                            MoveLeft(slots, slot, j, i, cnt);
                        }

                    }
                }
            }
            else if (direction == Key.Right)
            {
                for (int i = slots.Count - 1; i >= 0; i--)
                {
                    for (int j = slots[i].Count - 1; j >= 0; j--)
                    {
                        if (slots[i][j].Content.ToString() != "-")
                        {
                            slot = j;
                            MoveRight(slots, slot, j, i, cnt);
                        }

                    }
                }
            }
        }
        private void MoveUp(List<List<Button>> slots, int slot, int j, int i, int cnt)
        {
            while (slot > -1 && cnt < 4)
            {
                if (slot != 3 && slot >= 0 && slot != i)
                {
                    if (slots[slot][j].Content.ToString() == "-")
                    {
                        slots[slot][j].Content = slots[slot + 1][j].Content;
                        slots[slot + 1][j].Content = "-";
                        addNmb = true;
                    }
                    else if (slots[slot][j].Content.ToString() == slots[slot + 1][j].Content.ToString())
                    {
                        int slotInt = int.Parse(slots[slot][j].Content.ToString());
                        slots[slot][j].Content = (slotInt * 2).ToString();
                        slots[slot + 1][j].Content = "-";
                        SetScore(slots, slot, j);
                        addNmb = true;
                    }
                }
                slot--;
                cnt++;
            }
        }
        private void MoveDown(List<List<Button>> slots, int slot, int j, int i, int cnt)
        {
            while (slot > -1 && cnt < 4)
            {
                if (slot < 4 && slot >= 0 && slot != i)
                {
                    if (slots[slot][j].Content.ToString() == "-")
                    {
                        slots[slot][j].Content = slots[slot - 1][j].Content;
                        slots[slot - 1][j].Content = "-";
                        addNmb = true;
                    }
                    else if (slots[slot][j].Content.ToString() == slots[slot - 1][j].Content.ToString())
                    {
                        int slotInt = int.Parse(slots[slot][j].Content.ToString());
                        slots[slot][j].Content = (slotInt * 2).ToString();
                        slots[slot - 1][j].Content = "-";
                        SetScore(slots, slot, j);
                        addNmb = true;
                    }
                }
                slot++;
                cnt++;
            }
        }
        private void MoveLeft(List<List<Button>> slots, int slot, int j, int i, int cnt)
        {
            while (slot > -1 && cnt < 4)
            {
                if (slot != 3 && slot >= 0 & slot != j)
                {
                    if (slots[i][slot].Content.ToString() == "-")
                    {
                        slots[i][slot].Content = slots[i][slot + 1].Content;
                        slots[i][slot + 1].Content = "-";
                        addNmb = true;
                    }
                    else if ( slots[i][slot].Content.ToString() == slots[i][slot + 1].Content.ToString())
                    {
                        int slotInt = int.Parse(slots[i][slot].Content.ToString());
                        slots[i][slot].Content = (slotInt * 2).ToString();
                        slots[i][slot + 1].Content = "-";
                        SetScore(slots, i, slot);
                        addNmb = true;
                    }
                }
                slot--;
                cnt++;
            }
        }
        private void MoveRight(List<List<Button>> slots, int slot, int j, int i, int cnt)
        {
            while (slot > -1 && cnt < 4)
            {
                if (slot < slots.Count && slot > 0 && slot != j)
                {
                    if (slots[i][slot].Content.ToString() == "-")
                    {
                        slots[i][slot].Content = slots[i][slot - 1].Content;
                        slots[i][slot - 1].Content = "-";
                        addNmb = true;
                    }
                    else if (slots[i][slot].Content.ToString() == slots[i][slot - 1].Content.ToString())
                    {
                        int slotInt = int.Parse(slots[i][slot].Content.ToString());
                        slots[i][slot].Content = (slotInt * 2).ToString();
                        slots[i][slot - 1].Content = "-";
                        SetScore(slots, i, slot);
                        addNmb = true;
                    }
                }
                slot++;
                cnt++;
            }
        }
        private void SetScore(List<List<Button>> slots, int slot, int j)
        {
            score += int.Parse(slots[slot][j].Content.ToString());
            score_txtbk.Text = "Score: " + score.ToString();
        }
        private void AddRandomNumbers(List<List<Button>> buttons, int amount)
        {
            ResetColor();
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
                    int slot1 = rnd.Next(0, slots.Count);
                    int slot2 = rnd.Next(0, slots[slot1].Count);
                    if (slots[slot1][slot2].Content.ToString() == "-")
                    {
                        if (slot != 10 || firstValue == 4)
                        {
                            slots[slot1][slot2].Content = "2";
                        }
                        else
                        {
                            slots[slot1][slot2].Content = "4";
                        }
                        again = false;
                    }
                    else again = true;
                } while (again);
                i++;
            }
            addNmb = false;
            ColorNumbers();
        }


        private bool IsGameOver(List<List<Button>> slots)
        {
            bool gameOver = true;
            for (int i = 0; i < slots.Count; i++)
                for (int j = 0; j < slots[i].Count; j++)
                    if (slots[i][j].Content.ToString() == "-")
                    {
                        gameOver = false;
                    }
            if (gameOver)
            {
                for (int i = 0; i < slots.Count && gameOver; i++)
                    for (int j = 0; j < slots[i].Count && gameOver; j++)
                    {
                        if(i < slots.Count - 1 && j < slots[i].Count - 1)
                        {
                        if (slots[i][j].Content.ToString() == slots[i + 1][j].Content.ToString()) gameOver = false;
                        else if (slots[i][j].Content.ToString() == slots[i][j + 1].Content.ToString()) gameOver = false;
                        }
                        else if (i == slots.Count - 1 && j != slots[i].Count)
                        {
                            if (slots[i][j].Content.ToString() == slots[i][j+1].Content.ToString()) gameOver = false;
                        }
                        else if (j == slots[i].Count - 1 && i != slots.Count)
                        {
                            if (slots[i][j].Content.ToString() == slots[i+1][j].Content.ToString()) gameOver = false;
                        }
                    }
            }
            return gameOver;
        }
        private void FillSlots()
        {
            slots.Add(new List<Button>());
            slots.Add(new List<Button>());
            slots.Add(new List<Button>());
            slots.Add(new List<Button>());
            slots[0].Add(TL);
            slots[0].Add(TLM);
            slots[0].Add(TRM);
            slots[0].Add(TR);
            slots[1].Add(TML);
            slots[1].Add(TMLM);
            slots[1].Add(TMRM);
            slots[1].Add(TMR);
            slots[2].Add(BML);
            slots[2].Add(BMLM);
            slots[2].Add(BMRM);
            slots[2].Add(BMR);
            slots[3].Add(BL);
            slots[3].Add(BLM);
            slots[3].Add(BRM);
            slots[3].Add(BR);

        }
        private void ClearGame()
        {
            for (int i = 0; i < slots.Count; i++)
                for (int j = 0; j < slots[i].Count; j++)
                    slots[i][j].Content = "-";
            score = 0;

        }
        private void ColorNumbers()
        {
            for(int i = 0; i < slots.Count; i++)
                for(int j = 0; j < slots[i].Count; j++)
                {
            if (slots[i][j].Content.ToString() == "2")
            {
                slots[i][j].Background = Brushes.Cyan;
            }
            else if (slots[i][j].Content.ToString() == "4")
            {
                slots[i][j].Background = Brushes.DarkCyan;
            }
            else if (slots[i][j].Content.ToString() == "8")
            {
                slots[i][j].Background = Brushes.Green;
            }
            else if (slots[i][j].Content.ToString() == "16")
            {
                slots[i][j].Background = Brushes.DarkGreen;
            }
            else if (slots[i][j].Content.ToString() == "32")
            {
                slots[i][j].Background = Brushes.Red;
            }
            else if (slots[i][j].Content.ToString() == "64")
            {
                slots[i][j].Background = Brushes.DarkRed;
            }
            else if (slots[i][j].Content.ToString() == "128")
            {
                slots[i][j].Background = Brushes.Yellow;
            }
            else if (slots[i][j].Content.ToString() == "256")
            {
                slots[i][j].Background = Brushes.Blue;
            }
            else if (slots[i][j].Content.ToString() == "512")
            {
                slots[i][j].Background = Brushes.DarkBlue;
            }
            else if (slots[i][j].Content.ToString() == "1024")
            {
                        slots[i][j].Background = Brushes.White;
            }
            else if (slots[i][j].Content.ToString() == "2048")
            {
                slots[i][j].Background = Brushes.Magenta;
            }
                }
        }
        private void ResetColor()
        {
            for(int i = 0; i < slots.Count; i++)
                for(int j = 0; j < slots[i].Count; j++)
                    slots[i][j].Background = Brushes.Transparent;
        }
    }
}

