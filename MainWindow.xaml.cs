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

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();

            _mainWindowViewModel = new MainWindowViewModel();
            this.DataContext = _mainWindowViewModel;
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }


        /// <summary>
        /// This shows you how you can create diagram items in code, which means you can 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
