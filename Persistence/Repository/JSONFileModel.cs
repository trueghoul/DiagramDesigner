using DiagramDesigner.Persistence.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramDesigner.Persistence.Repository
{
    public class JSONFileModel
    {
        public List<UniversalDesignerItem> UniversalDesignerItems { get; set; }
        public List<Connection> Connections { get; set; }
        public JSONFileModel()
        {
            Connections = new List<Connection>();
            UniversalDesignerItems = new List<UniversalDesignerItem>();
        }
    }
}
