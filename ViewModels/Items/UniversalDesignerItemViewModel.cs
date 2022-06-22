using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;

namespace DiagramDesigner
{
    public class UniversalDesignerItemViewModel : DesignerItemViewModelBase, ISupportDataChanges
    {
        private IUIVisualizerService visualiserService;

        public UniversalDesignerItemViewModel(Guid id, IDiagramViewModel parent, double left, double top, string text, int fontSize) 
            : base(id,parent, left,top)
        {
            FontSize = fontSize;
            OnPropertyChanged("FontSize");
            Text = text;
            OnPropertyChanged("Text");

            Init();
        }
        public UniversalDesignerItemViewModel(Guid id, IDiagramViewModel parent, double left, double top, double itemWidth, double itemHeight, string text, int fontSize) 
            : base(id, parent, left, top, itemWidth, itemHeight)
        {
            FontSize = fontSize;
            OnPropertyChanged("FontSize");
            Text = text;
            OnPropertyChanged("Text");

            Init();
        }

        public UniversalDesignerItemViewModel() : base()
        {
            Id = Guid.NewGuid();
            FontSize = 14;
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
            ShowDataChangeWindowCommand = new CommandBase(ExecuteShowDataChangeWindowCommand);
            this.ShowConnectors = false;
        }
    }
}
