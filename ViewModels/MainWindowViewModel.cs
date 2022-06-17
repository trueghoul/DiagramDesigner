using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DiagramDesigner;
using System.ComponentModel;
using System.Windows.Data;
using DiagramDesigner.Persistence.Commons;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Text.Json;

namespace DiagramDesigner
{
    public class MainWindowViewModel : ViewModelBase
    {

        private List<int> savedDiagrams = new List<int>();

        private int? savedDiagramId;

        private List<SelectableDesignerItemViewModelBase> itemsToRemove;

        private IMessageBoxService messageBoxService;

        private DiagramViewModel diagramViewModel = new DiagramViewModel();

        private bool isBusy = false;


        public MainWindowViewModel()
        {
            messageBoxService = ApplicationServicesProvider.Instance.Provider.MessageBoxService;

            ToolBoxViewModel = new ToolBoxViewModel();
            DiagramViewModel = new DiagramViewModel();

            DeleteSelectedItemsCommand = new SimpleCommand(ExecuteDeleteSelectedItemsCommand);
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);
            SaveDiagramCommand = new SimpleCommand(ExecuteSaveDiagramCommand);
            //LoadDiagramCommand = new SimpleCommand(ExecuteLoadDiagramCommand);
            GroupCommand = new SimpleCommand(ExecuteGroupCommand);

            ConnectorViewModel.PathFinder = new OrthogonalPathFinder();

        }


        public SimpleCommand DeleteSelectedItemsCommand { get; private set; }
        public SimpleCommand CreateNewDiagramCommand { get; private set; }
        public SimpleCommand SaveDiagramCommand { get; private set; }
        public SimpleCommand GroupCommand { get; private set; }
        //public SimpleCommand LoadDiagramCommand { get; private set; }
        public ToolBoxViewModel ToolBoxViewModel { get; private set; }


        public DiagramViewModel DiagramViewModel
        {
            get
            {
                return diagramViewModel;
            }
            set
            {
                if (diagramViewModel != value)
                {
                    diagramViewModel = value;
                    OnPropertyChanged("DiagramViewModel");
                }
            }
        }

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }


        public List<int> SavedDiagrams
        {
            get
            {
                return savedDiagrams;
            }
            set
            {
                if (savedDiagrams != value)
                {
                    savedDiagrams = value;
                    OnPropertyChanged("SavedDiagrams");
                }
            }
        }

        public int? SavedDiagramId
        {
            get
            {
                return savedDiagramId;
            }
            set
            {
                if (savedDiagramId != value)
                {
                    savedDiagramId = value;
                    OnPropertyChanged("SavedDiagramId");
                }
            }
        }



        private void ExecuteDeleteSelectedItemsCommand(object parameter)
        {
            itemsToRemove = DiagramViewModel.SelectedItems;
            List<SelectableDesignerItemViewModelBase> connectionsToAlsoRemove = new List<SelectableDesignerItemViewModelBase>();

            foreach (var connector in DiagramViewModel.Items.OfType<ConnectorViewModel>())
            {
                if (ItemsToDeleteHasConnector(itemsToRemove, connector.SourceConnectorInfo))
                {
                    connectionsToAlsoRemove.Add(connector);
                }

                if (ItemsToDeleteHasConnector(itemsToRemove, (FullyCreatedConnectorInfo)connector.SinkConnectorInfo))
                {
                    connectionsToAlsoRemove.Add(connector);
                }
            }
            itemsToRemove.AddRange(connectionsToAlsoRemove);
            foreach (var selectedItem in itemsToRemove)
            {
                DiagramViewModel.RemoveItemCommand.Execute(selectedItem);
            }
        }
        private void ExecuteGroupCommand(object parameter)
        {
            if (diagramViewModel.SelectedItems.Count > 0)
            {
                // if only one selected item is a Grouping item -> ungroup
                if (diagramViewModel.SelectedItems[0] is GroupingDesignerItemViewModel && diagramViewModel.SelectedItems.Count == 1)
                {
                    GroupingDesignerItemViewModel groupObject = diagramViewModel.SelectedItems[0] as GroupingDesignerItemViewModel;
                    foreach (var item in groupObject.Items)
                    {

                        if (item is DesignerItemViewModelBase)
                        {
                            DesignerItemViewModelBase tmp = (DesignerItemViewModelBase)item;
                            tmp.Top += groupObject.Top;
                            tmp.Left += groupObject.Left;
                        }
                        diagramViewModel.AddItemCommand.Execute(item);
                        item.Parent = DiagramViewModel;
                    }

                    // "cut" connections between DiagramItems and the Group
                    List<SelectableDesignerItemViewModelBase> GroupedItemsToRemove = new List<SelectableDesignerItemViewModelBase>();
                    foreach (var connector in DiagramViewModel.Items.OfType<ConnectorViewModel>())
                    {
                        if (groupObject == connector.SourceConnectorInfo.DataItem)
                        {
                            GroupedItemsToRemove.Add(connector);
                        }

                        if (groupObject == ((FullyCreatedConnectorInfo)connector.SinkConnectorInfo).DataItem)
                        {
                            GroupedItemsToRemove.Add(connector);
                        }
                    }
                    GroupedItemsToRemove.Add(groupObject);
                    foreach (var selectedItem in GroupedItemsToRemove)
                    {
                        DiagramViewModel.RemoveItemCommand.Execute(selectedItem);
                    }

                }
                else if (diagramViewModel.SelectedItems.Count > 1)
                {
                    double margin = 15;
                    Rect rekt = GetBoundingRectangle(diagramViewModel.SelectedItems, margin);

                    GroupingDesignerItemViewModel groupItem = new GroupingDesignerItemViewModel(0, this.diagramViewModel, rekt.Left, rekt.Top, new SolidColorBrush(Colors.Black), 1);
                    groupItem.ItemWidth = rekt.Width;
                    groupItem.ItemHeight = rekt.Height;
                    foreach (var item in diagramViewModel.SelectedItems)
                    {
                        if (item is DesignerItemViewModelBase)
                        {
                            DesignerItemViewModelBase tmp = (DesignerItemViewModelBase)item;
                            tmp.Top -= rekt.Top;
                            tmp.Left -= rekt.Left;
                        }
                        groupItem.Items.Add(item);
                        item.Parent = groupItem;

                    }

                    // "cut" connections between DiagramItems which are going to be grouped and
                    // Diagramitems which are not going to be grouped
                    List<SelectableDesignerItemViewModelBase> GroupedItemsToRemove = DiagramViewModel.SelectedItems;
                    List<SelectableDesignerItemViewModelBase> connectionsToAlsoRemove = new List<SelectableDesignerItemViewModelBase>();

                    foreach (var connector in DiagramViewModel.Items.OfType<ConnectorViewModel>())
                    {
                        if (ItemsToDeleteHasConnector(GroupedItemsToRemove, connector.SourceConnectorInfo))
                        {
                            connectionsToAlsoRemove.Add(connector);
                        }

                        if (ItemsToDeleteHasConnector(GroupedItemsToRemove, (FullyCreatedConnectorInfo)connector.SinkConnectorInfo))
                        {
                            connectionsToAlsoRemove.Add(connector);
                        }

                    }
                    GroupedItemsToRemove.AddRange(connectionsToAlsoRemove);
                    foreach (var selectedItem in GroupedItemsToRemove)
                    {
                        DiagramViewModel.RemoveItemCommand.Execute(selectedItem);
                    }

                    diagramViewModel.SelectedItems.Clear();
                    this.diagramViewModel.Items.Add(groupItem);
                }
            }

        }

        private void ExecuteCreateNewDiagramCommand(object parameter)
        {
            //ensure that itemsToRemove is cleared ready for any new changes within a session
            itemsToRemove = new List<SelectableDesignerItemViewModelBase>();
            SavedDiagramId = null;
            DiagramViewModel.CreateNewDiagramCommand.Execute(null);
        }
        private static Rect GetBoundingRectangle(IEnumerable<SelectableDesignerItemViewModelBase> items, double margin)
        {
            double x1 = Double.MaxValue;
            double y1 = Double.MaxValue;
            double x2 = Double.MinValue;
            double y2 = Double.MinValue;

            foreach (DesignerItemViewModelBase item in items.OfType<DesignerItemViewModelBase>())
            {
                x1 = Math.Min(item.Left - margin, x1);
                y1 = Math.Min(item.Top - margin, y1);

                x2 = Math.Max(item.Left + item.ItemWidth + margin, x2);
                y2 = Math.Max(item.Top + item.ItemHeight + margin, y2);
            }

            return new Rect(new Point(x1, y1), new Point(x2, y2));
        }
        private Orientation GetOrientationFromConnector(ConnectorOrientation connectorOrientation)
        {
            Orientation result = Orientation.None;
            switch (connectorOrientation)
            {
                case ConnectorOrientation.Bottom:
                    result = Orientation.Bottom;
                    break;
                case ConnectorOrientation.Left:
                    result = Orientation.Left;
                    break;
                case ConnectorOrientation.Top:
                    result = Orientation.Top;
                    break;
                case ConnectorOrientation.Right:
                    result = Orientation.Right;
                    break;
            }
            return result;
        }
        private bool ItemsToDeleteHasConnector(List<SelectableDesignerItemViewModelBase> itemsToRemove, FullyCreatedConnectorInfo connector)
        {
            return itemsToRemove.Contains(connector.DataItem);
        }
        private void ExecuteSaveDiagramCommand(object parameter)
        {
            var univer = DiagramViewModel.Items.First();
            if (univer is UniversalDesignerItemViewModel)
            {
                UniversalDesignerItemViewModel _univer = univer as UniversalDesignerItemViewModel;
                UniversalDesignerItem item = new UniversalDesignerItem(_univer.Id, _univer.Left, _univer.Top, _univer.ItemWidth,
                    _univer.ItemHeight, _univer.Text, _univer.FontSize, _univer.ImageUrl);
                string jsonString = JsonSerializer.Serialize(item);
            }
        }

        /*
        private void ExecuteSaveDiagramCommand(object parameter)
        {
            if (!DiagramViewModel.Items.Any())
            {
                messageBoxService.ShowError("There must be at least one item in order save a diagram");
                return;
            }

            IsBusy = true;
            DiagramItem wholeDiagramToSave = null;

            Task<int> task = Task.Factory.StartNew<int>(() =>
            {

                if (SavedDiagramId != null)
                {
                    int currentSavedDiagramId = (int)SavedDiagramId.Value;
                    wholeDiagramToSave = databaseAccessService.FetchDiagram(currentSavedDiagramId);

                    //If we have a saved diagram, we need to make sure we clear out all the removed items that
                    //the user deleted as part of this work sesssion
                    foreach (var itemToRemove in itemsToRemove)
                    {
                        DeleteFromDatabase(wholeDiagramToSave, itemToRemove);
                    }
                    //start with empty collections of connections and items, which will be populated based on current diagram
                    wholeDiagramToSave.ConnectionIds = new List<int>();
                    wholeDiagramToSave.DesignerItems = new List<DiagramItemData>();
                }
                else
                {
                    wholeDiagramToSave = new DiagramItem();
                }

                //ensure that itemsToRemove is cleared ready for any new changes within a session
                itemsToRemove = new List<SelectableDesignerItemViewModelBase>();

                SavePersistDesignerItem(wholeDiagramToSave, DiagramViewModel);

                wholeDiagramToSave.Id = databaseAccessService.SaveDiagram(wholeDiagramToSave);
                return wholeDiagramToSave.Id;
            });
            task.ContinueWith((ant) =>
            {
                int wholeDiagramToSaveId = ant.Result;
                if (!savedDiagrams.Contains(wholeDiagramToSaveId))
                {
                    List<int> newDiagrams = new List<int>(savedDiagrams);
                    newDiagrams.Add(wholeDiagramToSaveId);
                    SavedDiagrams = newDiagrams;

                }
                IsBusy = false;
                messageBoxService.ShowInformation(string.Format("Finished saving Diagram Id : {0}", wholeDiagramToSaveId));

            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private void SavePersistDesignerItem(IDiagramItem wholeDiagramToSave, IDiagramViewModel diagramViewModel)
        {
            //Save all RhombusDesignerItemViewModel
            foreach (var universalItemVM in diagramViewModel.Items.OfType<UniversalDesignerItemViewModel>())
            {
                UniversalDesignerItem universalDesignerItem = new UniversalDesignerItem(universalItemVM.Id, universalItemVM.Left, universalItemVM.Top,
                    universalItemVM.ItemWidth, universalItemVM.ItemHeight, universalItemVM.Text, universalItemVM.FontSize);

                universalItemVM.Id = databaseAccessService.SaveRhombusDesignerItem(rhombusDesignerItem);
                wholeDiagramToSave.DesignerItems.Add(new DiagramItemData(rhombusDesignerItem.Id, typeof(SettingsDesignerItem)));
            }
            //Save all GroupingDesignerItemViewModel
            foreach (var groupingItemVM in diagramViewModel.Items.OfType<GroupingDesignerItemViewModel>())
            {
                GroupDesignerItem groupDesignerItem = new GroupDesignerItem(groupingItemVM.Id, groupingItemVM.Left, groupingItemVM.Top, groupingItemVM.ItemWidth, groupingItemVM.ItemHeight);
                if(groupingItemVM.Items != null && groupingItemVM.Items.Count > 0)
                {
                    SavePersistDesignerItem(groupDesignerItem, groupingItemVM);
                }
                groupingItemVM.Id = databaseAccessService.SaveGroupingDesignerItem(groupDesignerItem);
                wholeDiagramToSave.DesignerItems.Add(new DiagramItemData(groupDesignerItem.Id, typeof(GroupDesignerItem)));
            }
            //Save all connections which should now have their Connection.DataItems filled in with correct Ids
            foreach (var connectionVM in diagramViewModel.Items.OfType<ConnectorViewModel>())
            {
                FullyCreatedConnectorInfo sinkConnector = connectionVM.SinkConnectorInfo as FullyCreatedConnectorInfo;

                Connection connection = new Connection(
                    connectionVM.Id,
                    connectionVM.SourceConnectorInfo.DataItem.Id,
                    GetOrientationFromConnector(connectionVM.SourceConnectorInfo.Orientation),
                    GetTypeOfDiagramItem(connectionVM.SourceConnectorInfo.DataItem),
                    sinkConnector.DataItem.Id,
                    GetOrientationFromConnector(sinkConnector.Orientation),
                    GetTypeOfDiagramItem(sinkConnector.DataItem));

                connectionVM.Id = databaseAccessService.SaveConnection(connection);
                wholeDiagramToSave.ConnectionIds.Add(connectionVM.Id);
            }
        }
        private void ExecuteLoadDiagramCommand(object parameter)
        {
            IsBusy = true;
            DiagramItem wholeDiagramToLoad = null;
            if (SavedDiagramId == null)
            {
                messageBoxService.ShowError("You need to select a diagram to load");
                return;
            }

            Task<DiagramViewModel> task = Task.Factory.StartNew<DiagramViewModel>(() =>
            {
                //ensure that itemsToRemove is cleared ready for any new changes within a session
                itemsToRemove = new List<SelectableDesignerItemViewModelBase>();
                DiagramViewModel diagramViewModel = new DiagramViewModel();

                wholeDiagramToLoad = databaseAccessService.FetchDiagram((int)SavedDiagramId.Value);

                LoadPerstistDesignerItems(wholeDiagramToLoad, diagramViewModel);

                return diagramViewModel;
            });
            task.ContinueWith((ant) =>
            {
                this.DiagramViewModel = ant.Result;
                IsBusy = false;
                messageBoxService.ShowInformation(string.Format("Finished loading Diagram Id : {0}", wholeDiagramToLoad.Id));

            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
        private void LoadPerstistDesignerItems(IDiagramItem wholeDiagramToLoad, IDiagramViewModel diagramViewModel)
        {
            //load diagram items
            foreach (DiagramItemData diagramItemData in wholeDiagramToLoad.DesignerItems)
            {
                if (diagramItemData.ItemType == typeof(RhombusDesignerItem))
                {
                    RhombusDesignerItem rhombusDesignerItem = databaseAccessService.FetchRhombusDesignerItem(diagramItemData.ItemId);
                    RhombusDesignerItemViewModel rhombusDesignerItemViewModel =
                        new RhombusDesignerItemViewModel(rhombusDesignerItem.Id, diagramViewModel, rhombusDesignerItem.Left, rhombusDesignerItem.Top, rhombusDesignerItem.ItemWidth, rhombusDesignerItem.ItemHeight, rhombusDesignerItem.Text);
                    diagramViewModel.Items.Add(rhombusDesignerItemViewModel);
                }
                if (diagramItemData.ItemType == typeof(GroupDesignerItem))
                {
                    GroupDesignerItem groupDesignerItem = databaseAccessService.FetchGroupingDesignerItem(diagramItemData.ItemId);
                    GroupingDesignerItemViewModel groupingDesignerItemViewModel =
                        new GroupingDesignerItemViewModel(groupDesignerItem.Id, diagramViewModel, groupDesignerItem.Left, groupDesignerItem.Top, groupDesignerItem.ItemWidth, groupDesignerItem.ItemHeight);
                    if(groupDesignerItem.DesignerItems != null && groupDesignerItem.DesignerItems.Count > 0)
                    {
                        LoadPerstistDesignerItems(groupDesignerItem, groupingDesignerItemViewModel);
                    }
                    diagramViewModel.Items.Add(groupingDesignerItemViewModel);
                }
            }
            //load connection items
            foreach (int connectionId in wholeDiagramToLoad.ConnectionIds)
            {
                Connection connection = databaseAccessService.FetchConnection(connectionId);

                DesignerItemViewModelBase sourceItem = GetConnectorDataItem(diagramViewModel, connection.SourceId, connection.SourceType);
                ConnectorOrientation sourceConnectorOrientation = GetOrientationForConnector(connection.SourceOrientation);
                FullyCreatedConnectorInfo sourceConnectorInfo = GetFullConnectorInfo(connection.Id, sourceItem, sourceConnectorOrientation);

                DesignerItemViewModelBase sinkItem = GetConnectorDataItem(diagramViewModel, connection.SinkId, connection.SinkType);
                ConnectorOrientation sinkConnectorOrientation = GetOrientationForConnector(connection.SinkOrientation);
                FullyCreatedConnectorInfo sinkConnectorInfo = GetFullConnectorInfo(connection.Id, sinkItem, sinkConnectorOrientation);

                ConnectorViewModel connectionVM = new ConnectorViewModel(connection.Id, diagramViewModel, sourceConnectorInfo, sinkConnectorInfo);
                diagramViewModel.Items.Add(connectionVM);
            }
        }

        private FullyCreatedConnectorInfo GetFullConnectorInfo(int connectorId, DesignerItemViewModelBase dataItem, ConnectorOrientation connectorOrientation)
        {
            switch (connectorOrientation)
            {
                case ConnectorOrientation.Top:
                    return dataItem.TopConnector;
                case ConnectorOrientation.Left:
                    return dataItem.LeftConnector;
                case ConnectorOrientation.Right:
                    return dataItem.RightConnector;
                case ConnectorOrientation.Bottom:
                    return dataItem.BottomConnector;

                default:
                    throw new InvalidOperationException(
                        string.Format("Обнаружена ошибочная ореинтация коннектора с Id: {0}", connectorId));
            }
        }

        private Type GetTypeOfDiagramItem(DesignerItemViewModelBase vmType)
        {
            if (vmType is UniversalDesignerItemViewModel)
                return typeof(UniversalDesignerItem);
            if (vmType is GroupingDesignerItemViewModel)
                return typeof(GroupDesignerItem);
            throw new InvalidOperationException(string.Format("Элемент неизвестного типа."));

        }

        private DesignerItemViewModelBase GetConnectorDataItem(IDiagramViewModel diagramViewModel, int conectorDataItemId, Type connectorDataItemType)
        {
            DesignerItemViewModelBase dataItem = null;

            if (connectorDataItemType == typeof(UniversalDesignerItem))
            {
                dataItem = diagramViewModel.Items.OfType<UniversalDesignerItemViewModel>().Single(x => x.Id == conectorDataItemId);
            }

            if (connectorDataItemType == typeof(GroupDesignerItem))
            {
                dataItem = diagramViewModel.Items.OfType<GroupingDesignerItemViewModel>().Single(x => x.Id == conectorDataItemId);
            }
            return dataItem;
        }

        private ConnectorOrientation GetOrientationForConnector(Orientation persistedOrientation)
        {
            ConnectorOrientation result = ConnectorOrientation.None;
            switch (persistedOrientation)
            {
                case Orientation.Bottom:
                    result = ConnectorOrientation.Bottom;
                    break;
                case Orientation.Left:
                    result = ConnectorOrientation.Left;
                    break;
                case Orientation.Top:
                    result = ConnectorOrientation.Top;
                    break;
                case Orientation.Right:
                    result = ConnectorOrientation.Right;
                    break;
            }
            return result;
        }
        */
    }
}
