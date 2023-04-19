using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner
{
    public class WPFUIVisualizerService : IUIVisualizerService
    {
        public bool? ShowDialog(object dataContextForPopup)
        {
            Window win = new PopupWindow()
            {
                DataContext = dataContextForPopup,
                Owner = Application.Current.MainWindow
            };
            return win.ShowDialog();
        }
    }
}
