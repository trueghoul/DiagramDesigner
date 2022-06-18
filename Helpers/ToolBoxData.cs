using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner
{
    public class ToolBoxData
    {
        public string ImageUrl { get; private set; }
        public Type Type { get; private set; }
        public object Parametr { get ; private set; }

        public ToolBoxData(string imageUrl, Type type, object parametr)
        {
            this.ImageUrl = imageUrl;
            this.Type = type;
            this.Parametr = parametr;
        }
    }
}
