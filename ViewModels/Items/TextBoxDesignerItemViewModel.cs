using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace DiagramDesigner
{
    public class TextBoxDesignerItemViewModel : DesignerItemViewModelBase, ISupportDataChanges
    {
        private IUIVisualizerService visualiserService;
        public string Text { get; set; }
        public int FontSize { get; set; }
        public string TextAlign { get; set; }
        public ICommand ShowDataChangeWindowCommand { get; private set; }
        public TextBoxDesignerItemViewModel(Guid id, IDiagramViewModel parent, double left, double top, string text, int fontSize, string textAlign)
            : base(id, parent, left, top)
        {
            TextAlign = textAlign;
            OnPropertyChanged("TextAlign");
            ItemHeight = 100;
            ItemWidth = 100;
            FontSize = fontSize;
            OnPropertyChanged("FontSize");
            Text = text;
            OnPropertyChanged("Text");

            Init();
        }
        public TextBoxDesignerItemViewModel(Guid id, IDiagramViewModel parent, double left, double top, double itemWidth, double itemHeight, string text, int fontSize, string textAlign)
            : base(id, parent, left, top, itemWidth, itemHeight)
        {
            TextAlign = textAlign;
            OnPropertyChanged("TextAlign");
            ItemHeight = itemHeight;
            ItemWidth = itemWidth;
            FontSize = fontSize;
            OnPropertyChanged("FontSize");
            Text = text;
            OnPropertyChanged("Text");

            Init();
        }

        public TextBoxDesignerItemViewModel() : base()
        {
            TextAlign = "Center";
            OnPropertyChanged("TextAlign");

            ItemHeight = 100;
            ItemWidth = 100;

            Text = "text";
            OnPropertyChanged("Text");

            FontSize = 14;
            OnPropertyChanged("FontSize");
            Init();
        }

        public void ExecuteShowDataChangeWindowCommand(object parameter)
        {
            TextBoxDesignerItemData data = new TextBoxDesignerItemData(Text, ItemHeight, ItemWidth, FontSize, TextAlign);
            if (visualiserService.ShowDialog(data) == true)
            {
                Text = data.Text;
                OnPropertyChanged("Text");

                ItemHeight = data.ItemHeight;
                ItemWidth = data.ItemWidth;

                FontSize = data.FontSize;
                OnPropertyChanged("FontSize");

                TextAlign = data.TextAlign;
                OnPropertyChanged("TextAlign");
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
