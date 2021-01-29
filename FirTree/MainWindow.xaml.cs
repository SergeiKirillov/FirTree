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
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace FirTree
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// <example>
    /// http://www.techweek.ru/yolochka-na-rabochij-stol-sozdaj-sam/
    /// http://www.techweek.ru/dobavit-v-avtozagruzku/
    /// </example>
    /// 


    public partial class MainWindow : Window
    {
        Point old;

        private IntPtr Handle
        {
            get
            {
                return new WindowInteropHelper(this).Handle;
            }
        }

        [DllImport("user32.dll")]
        private static extern int setWindowLong(IntPtr window, int index, int value);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr window, int index);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public const int HWND_BOTTON = 0x1;
        public const uint SWP_NOSIZE = 0x1;
        public const uint SWP_NOMOVE = 0x2;
        public const uint SWP_SHOWWINDOW = 0x40;

        private void ShoveToBackground()
        {
            SetWindowPos((int)this.Handle, HWND_BOTTON, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
        }




        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        public static void HideFromAltTab(IntPtr Handle)
        {
            setWindowLong(Handle, GWL_EXSTYLE, GetWindowLong(Handle, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
        }


        

        public MainWindow()
        {

            InitializeComponent();

        }

        private void Image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            

        }

        private void Image1_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HideFromAltTab(Handle);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ShoveToBackground();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            old = e.GetPosition(null);
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point cur = e.GetPosition(null);
                this.Left += cur.X - old.X;
                this.Top += cur.Y - old.Y;

            }
        }
    }
}
