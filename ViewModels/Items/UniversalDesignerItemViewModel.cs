using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner;
using System.Windows.Input;

namespace DiagramDesigner
{
    public class UniversalDesignerItemViewModel : DesignerItemViewModelBase, ISupportDataChanges
    {
        private IUIVisualizerService visualiserService;

        public UniversalDesignerItemViewModel(int id, IDiagramViewModel parent, double left, double top, string text, int fontSize) 
            : base(id,parent, left,top)
        {
            FontSize = fontSize;
            Text = text;
            Init();
        }
        public UniversalDesignerItemViewModel(int id, IDiagramViewModel parent, double left, double top, double itemWidth, double itemHeight, string text, int fontSize) 
            : base(id, parent, left, top, itemWidth, itemHeight)
        {
            FontSize = fontSize;
            Text = text;
            Init();
        }

        public UniversalDesignerItemViewModel() : base()
        {
            FontSize = 12;
            OnPropertyChanged("FontSize");

            Init();
        }

        public string Text { get; set; }
        public int FontSize { get; set; }
        public ICommand ShowDataChangeWindowCommand { get; private set; }

        public void ExecuteShowDataChangeWindowCommand(object parameter)
        {
            UniversalDesignerItemData data = new UniversalDesignerItemData(Text, ItemHeight, ItemWidth, FontSize);
            if (visualiserService.ShowDialog(data) == true)
            {
                Text = data.Text;
                OnPropertyChanged("Text");

                ItemHeight = data.ItemHeight;
                ItemWidth = data.ItemWidth;

                FontSize = data.FontSize;
                OnPropertyChanged("FontSize");
            }
            
        }
        private void Init()
        {
            visualiserService = ApplicationServicesProvider.Instance.Provider.VisualizerService;
            ShowDataChangeWindowCommand = new SimpleCommand(ExecuteShowDataChangeWindowCommand);
            this.ShowConnectors = false;
        }
    }
}
