using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace DiagramDesigner
{
    public class RoundedRectangleDesignerItemViewModel : DesignerItemViewModelBase, ISupportDataChanges
    {
        private IUIVisualizerService visualiserService;

        public RoundedRectangleDesignerItemViewModel(double radius, Guid id, IDiagramViewModel parent, double left, double top, string text, int fontSize, Brush stroke, double strokeThickness)
            : base(id, parent, left, top)
        {
            Radius = radius;
            OnPropertyChanged("Radius");
            Stroke = stroke;
            OnPropertyChanged("Stroke");
            StrokeThickness = strokeThickness;
            OnPropertyChanged("StrokeThickness");
            FontSize = fontSize;
            OnPropertyChanged("FontSize");
            Text = text;
            OnPropertyChanged("Text");

            Init();
        }
        public RoundedRectangleDesignerItemViewModel(double radius, Guid id, IDiagramViewModel parent, double left, double top, double itemWidth, double itemHeight, string text, int fontSize, Brush stroke, double strokeThickness)
            : base(id, parent, left, top, itemWidth, itemHeight)
        {
            Radius = radius;
            OnPropertyChanged("Radius");
            Stroke = stroke;
            OnPropertyChanged("Stroke");
            StrokeThickness = strokeThickness;
            OnPropertyChanged("StrokeThickness");
            FontSize = fontSize;
            OnPropertyChanged("FontSize");
            Text = text;
            OnPropertyChanged("Text");

            Init();
        }

        public RoundedRectangleDesignerItemViewModel() : base()
        {
            Radius = 20;
            OnPropertyChanged("Radius");
            FontSize = 14;
            OnPropertyChanged("FontSize");
            Stroke = new SolidColorBrush(Colors.Black);
            OnPropertyChanged("Stroke");
            StrokeThickness = 1;
            OnPropertyChanged("StrokeThickness");

            Init();
        }

        public string Text { get; set; }
        public int FontSize { get; set; }
        public double StrokeThickness { get; set; }
        public Brush Stroke { get; set; }
        public double Radius { get; set; }
        public ICommand ShowDataChangeWindowCommand { get; private set; }

        public void ExecuteShowDataChangeWindowCommand(object parameter)
        {
            RoundedRectangleDesignerItemData data = new RoundedRectangleDesignerItemData(Radius, StrokeThickness, Stroke, Text, ItemHeight, ItemWidth, FontSize);
            if (visualiserService.ShowDialog(data) == true)
            {
                Text = data.Text;
                OnPropertyChanged("Text");

                ItemHeight = data.ItemHeight;
                ItemWidth = data.ItemWidth;

                FontSize = data.FontSize;
                OnPropertyChanged("FontSize");

                StrokeThickness = data.StrokeThickness;
                OnPropertyChanged("StrokeThickness");

                Stroke = data.Stroke;
                OnPropertyChanged("Stroke");

                Radius = data.Radius;
                OnPropertyChanged("Radius");
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
