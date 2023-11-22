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
        Button[,] slots = new Button[4, 4];
        public MainWindow()
        {
            InitializeComponent();
            FillSlots();
            Main_Window.KeyDown += new KeyEventHandler(OnButtonKeyDown);
        }
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            MoveCube(e.Key); 
            }

        private void MoveCube(Key direction)
        {
            throw new NotImplementedException();
        }
        private void FillSlots()
        {
            slots[0, 0] = TL;
            slots[1, 0] = TML;
            slots[2, 0] = TMR;
            slots[3, 0] = TR;
            slots[0, 1] = TML;
            slots[1, 1] = TMLM;
            slots[2, 1] = TMRM;
            slots[3, 1] = TMR;
            slots[0, 2] = BML;
            slots[1, 2] = BMLM;
            slots[2, 2] = BMRM;
            slots[3, 2] = BMR;
            slots[0, 3] = BL;
            slots[1, 3] = BLM;
            slots[2, 3] = BRM;
            slots[3, 3] = BR;

        }
    }
}

