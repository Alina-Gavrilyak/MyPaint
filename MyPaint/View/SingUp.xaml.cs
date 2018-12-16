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
using System.Windows.Shapes;

namespace MyPaint.View
{
    /// <summary>
    /// Логика взаимодействия для SingUp.xaml
    /// </summary>
    public partial class SingUp : Window
    {
        public SingUp()
        {
            InitializeComponent();
        }

        private void btn_SingIn_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow mw = new MenuWindow();
            mw.Show();
            this.Close();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            SingIn s = new SingIn();
            s.Show();
            this.Close();
        }
    }
}
