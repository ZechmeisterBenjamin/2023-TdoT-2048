﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Main_Window.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            AddRandomNumbers(slots, 2);
        }
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            MoveCube(e.Key);
        }

        private void MoveCube(Key direction)
        {
            bool moveAgain = true;
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
                            MoveUp(slots, slot, j, i, cnt, moveAgain);
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
                            MoveDown(slots, slot, j, i, cnt, moveAgain);
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
                            MoveLeft(slots, slot, j, i, cnt, moveAgain);
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
                            MoveRight(slots, slot, j, i, cnt, moveAgain);
                        }

                    }
                }
            }
        }

        private void MoveUp(List<List<Button>> slots, int slot, int j, int i, int cnt, bool moveAgain)
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
                    else if (slots[slot][j].Content == slots[slot + 1][j].Content)
                    {
                        int slotInt = int.Parse(slots[slot][j].Content.ToString());
                        slots[slot][j].Content = (slotInt * 2).ToString();
                        slots[slot + 1][j].Content = "-";
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


        private void MoveDown(List<List<Button>> slots, int slot, int j, int i, int cnt, bool moveAgain)
        {
            //throw new NotImplementedException();
        }
        private void MoveLeft(List<List<Button>> slots, int slot, int j, int i, int cnt, bool moveAgain)
        {
            //throw new NotImplementedException();
        }
        private void MoveRight(List<List<Button>> slots, int slot, int j, int i, int cnt, bool moveAgain)
        {
            //throw new NotImplementedException();
        }

        private void SetScore(List<List<Button>> slots, int slot, int j)
        {
            score += int.Parse(slots[slot][j].Content.ToString());
        }
        private void AddRandomNumbers(List<List<Button>> buttons, int amount)
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
    }
}

