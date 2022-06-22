using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace DiagramDesigner
{
    public interface IDiagramViewModel
    {
        CommandBase AddItemCommand { get; }
        CommandBase RemoveItemCommand { get;  }
        CommandBase ClearSelectedItemsCommand { get;  }
        List<SelectableDesignerItemViewModelBase> SelectedItems { get; }
        ObservableCollection<SelectableDesignerItemViewModelBase> Items { get; }
    }
}
