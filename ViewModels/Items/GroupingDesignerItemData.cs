using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DiagramDesigner
{
    public class GroupingDesignerItemData : ViewModelBase
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
        public GroupingDesignerItemData(double strokeThickness, Brush stroke)
        {
            _strokeThickness = strokeThickness;
            _stroke = stroke;
        }
    }
}
