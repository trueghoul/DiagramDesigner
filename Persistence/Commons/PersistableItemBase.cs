using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.Persistence.Commons
{
    public abstract class PersistableItemBase
    {
        public PersistableItemBase()
        {

        }

        public PersistableItemBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}
