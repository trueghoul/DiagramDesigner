using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.Persistence.Commons
{
    public abstract class DesignerItemBase : PersistableItemBase
    {
        public DesignerItemBase(int id, double left, double top, double itemWidth, double itemHeight, string imgUrl) : base(id)
        {
            this.Left = left;
            this.Top = top;
            this.ItemWidth = itemWidth;
            this.ItemHeight = itemHeight;
            this.ImgUrl = imgUrl;
        }

        public double ItemHeight { get; private set; }
        public double ItemWidth { get; private set; }
        public double Left { get; private set; }
        public double Top { get; private set; }
        public string ImgUrl { get; private set; }

    }
}
