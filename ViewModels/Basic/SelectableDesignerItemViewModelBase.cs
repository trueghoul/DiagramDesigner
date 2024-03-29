﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DiagramDesigner
{

    public interface ISelectItems
    {
        CommandBase SelectItemCommand { get;  }
    }


    public abstract class SelectableDesignerItemViewModelBase : ViewModelBase, ISelectItems
    {
        private bool isSelected;

        public SelectableDesignerItemViewModelBase(Guid id, IDiagramViewModel parent)
        {
            this.Id = id;
            this.Parent = parent;
            Init();
        }

        public SelectableDesignerItemViewModelBase()
        {
            Init();
        }

        public List<SelectableDesignerItemViewModelBase> SelectedItems
        {
            get { return Parent.SelectedItems; }
        }

        public IDiagramViewModel Parent { get; set; }
        public CommandBase SelectItemCommand { get; private set; }
        public Guid Id { get; set; }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        private void ExecuteSelectItemCommand(object param)
        {
            SelectItem((bool)param, !IsSelected);
        }
        
        private void SelectItem(bool newselect, bool select)
        {
            if (newselect)
            {
                foreach (var designerItemViewModelBase in Parent.SelectedItems.ToList())
                {
                    designerItemViewModelBase.isSelected = false;
                }
            }

            IsSelected = select;
        }
    
        private void Init()
        {
            SelectItemCommand = new CommandBase(ExecuteSelectItemCommand);
        }
    }
}
