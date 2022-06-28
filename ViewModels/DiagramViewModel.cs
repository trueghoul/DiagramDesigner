using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;


namespace DiagramDesigner
{
    public class DiagramViewModel : ViewModelBase, IDiagramViewModel
    {
        private ObservableCollection<SelectableDesignerItemViewModelBase> items = new ObservableCollection<SelectableDesignerItemViewModelBase>();
        private double _latticeThickness;
        public double LatticeThickness
        {
            get
            {
                return _latticeThickness;
            }
            set
            {
                _latticeThickness = value;
                OnPropertyChanged(nameof(LatticeThickness));
            }
        }

        public DiagramViewModel()
        {
            LatticeThickness = 0.4;
            AddItemCommand = new CommandBase(ExecuteAddItemCommand);
            RemoveItemCommand = new CommandBase(ExecuteRemoveItemCommand);
            ClearSelectedItemsCommand = new CommandBase(ExecuteClearSelectedItemsCommand);
            CreateNewDiagramCommand = new CommandBase(ExecuteCreateNewDiagramCommand);
            GridVisibilityChangeCommand = new CommandBase(ExecuteGridVisibilityChangeCommand);

            Mediator.Instance.Register(this);
        }



        [MediatorMessageSink("DoneDrawingMessage")]
        public void OnDoneDrawingMessage(bool dummy)
        {
            foreach (var item in Items.OfType<DesignerItemViewModelBase>())
            {
                item.ShowConnectors = false;
            }
        }


        public CommandBase AddItemCommand { get; private set; }
        public CommandBase RemoveItemCommand { get; private set; }
        public CommandBase ClearSelectedItemsCommand { get; private set; }
        public CommandBase CreateNewDiagramCommand { get; private set; }
        public CommandBase GridVisibilityChangeCommand { get; private set; }

        private void ExecuteGridVisibilityChangeCommand(object parameter)
        {
            if (LatticeThickness == 0.4)
                LatticeThickness = 0;
            else LatticeThickness = 0.4;
        }

        public ObservableCollection<SelectableDesignerItemViewModelBase> Items
        {
            get { return items; }
        }

        public List<SelectableDesignerItemViewModelBase> SelectedItems
        {
            get { return Items.Where(x => x.IsSelected).ToList(); }
        }

        private void ExecuteAddItemCommand(object parameter)
        {
            if (parameter is SelectableDesignerItemViewModelBase)
            {
                SelectableDesignerItemViewModelBase item = (SelectableDesignerItemViewModelBase)parameter;
                item.Parent = this;
                items.Add(item);
            }
        }

        private void ExecuteRemoveItemCommand(object parameter)
        {
            if (parameter is SelectableDesignerItemViewModelBase)
            {
                SelectableDesignerItemViewModelBase item = (SelectableDesignerItemViewModelBase)parameter;
                items.Remove(item);
            }
        }

        private void ExecuteClearSelectedItemsCommand(object parameter)
        {
            foreach (SelectableDesignerItemViewModelBase item in Items)
            {
                if (item is GroupingDesignerItemViewModel)
                {
                    (item as GroupingDesignerItemViewModel).ClearSelectedItemsCommand.Execute(null);
                }
                item.IsSelected = false;
            }
        }

        private void ExecuteCreateNewDiagramCommand(object parameter)
        {
            Items.Clear();
        }
    }
}
