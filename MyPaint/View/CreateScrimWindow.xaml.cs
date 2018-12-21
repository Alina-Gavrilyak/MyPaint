using MyPaint.ViewModel;
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
using static MyPaint.MenuWindow;

namespace MyPaint.View
{
    /// <summary>
    /// Логика взаимодействия для CreateScrim.xaml
    /// </summary>
    public partial class CreateScrimWindow : Window
    {
        CreateScrimVM context;
        CreateScrimDelegate delegat;
        MenuRoomVM roomVM;
        public CreateScrimWindow(CreateScrimDelegate delegat,MenuRoomVM roomVM)
        {
            InitializeComponent();
            context = new CreateScrimVM();
            DataContext = context;
            this.delegat = delegat;
            this.roomVM = roomVM;
        }

        private void btn_CreateScrim_Click(object sender, RoutedEventArgs e)
        {
            delegat(context.Name, roomVM);
            this.Close();
        }
    }
}
