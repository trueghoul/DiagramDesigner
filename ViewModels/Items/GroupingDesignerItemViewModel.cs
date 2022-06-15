using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace DiagramDesigner
{
    public class GroupingDesignerItemViewModel : DesignerItemViewModelBase, IDiagramViewModel, ISupportDataChanges
    {
        private IUIVisualizerService visualiserService;
        private ObservableCollection<SelectableDesignerItemViewModelBase> items = new ObservableCollection<SelectableDesignerItemViewModelBase>();

        public GroupingDesignerItemViewModel(int id, IDiagramViewModel parent, double left, double top, Brush stroke, double strokeThickness)
            : base(id, parent, left, top)
        {
            Stroke  = stroke;
            OnPropertyChanged("Stroke");
            StrokeThickness = strokeThickness;
            OnPropertyChanged("StrokeThickness");
            Init();
        }

        public GroupingDesignerItemViewModel()
        {
            Stroke = new SolidColorBrush(Colors.Black);
            OnPropertyChanged("Stroke");
            StrokeThickness = 1;
            OnPropertyChanged("StrokeThickness");
            Init();
        }

        public GroupingDesignerItemViewModel(int id, IDiagramViewModel parent, double left, double top, double itemWidth, double itemHeight, Brush stroke, double strokeThickness)
            : base(id, parent, left, top, itemWidth, itemHeight)
        {
            Stroke = stroke;
            OnPropertyChanged("Stroke");
            StrokeThickness = strokeThickness;
            OnPropertyChanged("StrokeThickness");
            Init();
        }
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; }

        public SimpleCommand AddItemCommand { get; private set; }
        public SimpleCommand RemoveItemCommand { get; private set; }
        public SimpleCommand ClearSelectedItemsCommand { get; private set; }
        public SimpleCommand CreateNewDiagramCommand { get; private set; }

        public ICommand ShowDataChangeWindowCommand { get; private set; }

        public void ExecuteShowDataChangeWindowCommand(object parameter)
        {
            GroupingDesignerItemData data = new GroupingDesignerItemData(StrokeThickness, Stroke);
            if (visualiserService.ShowDialog(data) == true)
            {
                StrokeThickness = data.StrokeThickness;
                OnPropertyChanged("StrokeThickness");
                Stroke = data.Stroke;
                OnPropertyChanged("Stroke");
            }

        }

        public ObservableCollection<SelectableDesignerItemViewModelBase> Items
        {
            get { return items; }
        }

        new public List<SelectableDesignerItemViewModelBase> SelectedItems
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
                item.IsSelected = false;
                if (item is DesignerItemViewModelBase) 
                {
                    (item as DesignerItemViewModelBase).ShowConnectors = false;
                }
            }
        }

        private void ExecuteCreateNewDiagramCommand(object parameter)
        {
            Items.Clear();
        }


        private void Init()
        {
            visualiserService = ApplicationServicesProvider.Instance.Provider.VisualizerService;
            AddItemCommand = new SimpleCommand(ExecuteAddItemCommand);
            RemoveItemCommand = new SimpleCommand(ExecuteRemoveItemCommand);
            ClearSelectedItemsCommand = new SimpleCommand(ExecuteClearSelectedItemsCommand);
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);

            ShowDataChangeWindowCommand = new SimpleCommand(ExecuteShowDataChangeWindowCommand);

            this.ShowConnectors = false;
        }
    }
}
