using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.Persistence.Commons
{
    public class Connection : PersistableItemBase
    {
        public Connection(Guid id, Guid sourceId, Orientation sourceOrientation, 
            string sourceType, Guid sinkId, Orientation sinkOrientation, string sinkType) : base(id)
        {
            this.SourceId = sourceId;
            this.SourceOrientation = sourceOrientation;
            this.SourceType = sourceType;
            this.SinkId = sinkId;
            this.SinkOrientation = sinkOrientation;
            this.SinkType = sinkType;
        }

        public Guid SourceId { get; private set; }
        public Orientation SourceOrientation { get; private set; }
        public string SourceType { get; private set; }
        public Guid SinkId { get; private set; }
        public Orientation SinkOrientation { get; private set; }
        public string SinkType { get; private set; }
    }

}
