using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DiagramDesigner
{
    public class EllipseDesignerItemData : RectangleDesignerItemData
    {
        public EllipseDesignerItemData(double strokeThickness, Brush stroke, string text, double itemHeight, double itemWidth, int fontSize) 
            : base(strokeThickness, stroke, text, itemHeight, itemWidth, fontSize)
        {

        }
    }
}
