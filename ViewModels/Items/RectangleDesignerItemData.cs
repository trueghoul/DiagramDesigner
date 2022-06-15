using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DiagramDesigner
{
    public class RectangleDesignerItemData : UniversalDesignerItemData
    {
        private double _strokeThickness;
        private Brush _stroke;
        public Brush Stroke
        {
            get
            {
                return _stroke;
            }
            set
            {
                _stroke = value;
                OnPropertyChanged(nameof(Stroke));
            }
        }
        public double StrokeThickness
        {
            get
            {
                return _strokeThickness;
            }
            set
            {
                _strokeThickness = value;
                OnPropertyChanged(nameof(StrokeThickness));
            }
        }
        public RectangleDesignerItemData(double strokeThickness, Brush stroke, string text, double itemHeight, double itemWidth, int fontSize) 
            : base (text, itemHeight, itemWidth, fontSize)
        {
            _strokeThickness = strokeThickness;
            _stroke = stroke;
        }
    }
}
