using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.Persistence.Commons
{
    public class GroupDesignerItem : DesignerItemBase
    {
        public GroupDesignerItem(Guid id, double left, double top, double itemWidth, double itemHeight) 
            : base(id, left, top, itemWidth, itemHeight, null)
        {
            //this.DesignerItems = new List<DiagramItemData>();
            this.ConnectionIds = new List<Guid>();
        }

        //public List<DiagramItemData> DesignerItems { get; set; }
        public List<Guid> ConnectionIds { get; set; }
    }
}
