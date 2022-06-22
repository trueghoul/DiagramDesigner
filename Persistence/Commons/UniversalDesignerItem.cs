using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.Persistence.Commons
{
    public class UniversalDesignerItem : DesignerItemBase
    {
        public UniversalDesignerItem(Guid id, double left, double top, double itemWidth, double itemHeight, string text, int fontSize, string imgUrl) 
            : base(id, left, top, itemWidth, itemHeight, imgUrl)
        {
            Text = text;
            FontSize = fontSize;
        }

        public string Text { get; set; }
        public int FontSize { get; set; }


    }
}
