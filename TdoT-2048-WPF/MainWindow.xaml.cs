using System;
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
        }
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            MoveCube(e.Key);
            for(int i = 0; i < slots.Count; i++)
            {
                for(int j = 0; j < slots[i].Count; j++)
                {
                    MessageBox.Show(slots[i][j].Name);
                }
            }
        }

        private void MoveCube(Key direction)
        {
            //throw new NotImplementedException();
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

