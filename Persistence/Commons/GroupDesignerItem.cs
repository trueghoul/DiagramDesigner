using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.Persistence.Commons
{
    public class GroupDesignerItem : DesignerItemBase, IDiagramItem
    {
        public GroupDesignerItem(int id, double left, double top, double itemWidth, double itemHeight) 
            : base(id, left, top, itemWidth, itemHeight)
        {
            this.DesignerItems = new List<DiagramItemData>();
            this.ConnectionIds = new List<int>();
        }

        public List<DiagramItemData> DesignerItems { get; set; }
        public List<int> ConnectionIds { get; set; }

    }
}
