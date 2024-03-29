﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace DiagramDesigner
{
    public class DragThumb : Thumb
    {
        public DragThumb()
        {
            base.DragDelta += new DragDeltaEventHandler(DragThumb_DragDelta);
        }

        void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            DesignerItemViewModelBase designerItem = this.DataContext as DesignerItemViewModelBase;

            if (designerItem != null && designerItem.IsSelected)
            {
                double minLeft = double.MaxValue;
                double minTop = double.MaxValue;

                var designerItems = designerItem.SelectedItems;

                foreach (DesignerItemViewModelBase item in designerItems.OfType<DesignerItemViewModelBase>())
                {
                    double left = item.Left;
                    double top = item.Top;
                    minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
                    minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);

                    double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
                    double deltaVertical = Math.Max(-minTop, e.VerticalChange);
                    item.Left += deltaHorizontal;
                    item.Top += deltaVertical;

                    if (item.Parent is IDiagramViewModel && item.Parent is DesignerItemViewModelBase)
                    {
                        DesignerItemViewModelBase groupItem = (DesignerItemViewModelBase)item.Parent;
                        if (item.Left + item.ItemWidth >= groupItem.ItemWidth) item.Left = groupItem.ItemWidth - item.ItemWidth;
                        if (item.Top + item.ItemHeight >= groupItem.ItemHeight) item.Top = groupItem.ItemHeight - item.ItemHeight;
                    }

                }
                e.Handled = true;
            }
        }
    }
}
