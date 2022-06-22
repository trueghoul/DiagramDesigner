using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramDesigner.Persistence.Commons
{
    public class TextBoxDesignerItem : DesignerItemBase
    {
        public TextBoxDesignerItem(Guid id, double left, double top, double itemWidth, double itemHeight, string text, int fontSize, string textAlign) : 
            base(id, left, top, itemWidth, itemHeight, null)
        {
            Text = text;
            FontSize = fontSize;
            TextAlign = textAlign;
        }
        public string Text { get; set; }
        public int FontSize { get; set; }
        public string TextAlign { get; set; }
    }
}
