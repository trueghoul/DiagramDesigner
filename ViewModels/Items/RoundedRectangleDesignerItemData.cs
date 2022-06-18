using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DiagramDesigner
{
    public class RoundedRectangleDesignerItemData : RectangleDesignerItemData
    {
        public RoundedRectangleDesignerItemData(double radius, double strokeThickness, Brush stroke, string text, double itemHeight, double itemWidth, int fontSize) 
            : base(strokeThickness, stroke, text, itemHeight, itemWidth, fontSize)
        {
            Radius = radius;
        }

        private double _radius;
        public double Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
                OnPropertyChanged(nameof(Radius));
            }
        }
    }
}
