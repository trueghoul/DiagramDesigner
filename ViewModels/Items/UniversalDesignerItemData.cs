using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner;

namespace DiagramDesigner
{
    public class UniversalDesignerItemData : ViewModelBase
    {
        private string _text;
        private int _fontSize;
        private double _itemWidth;
        private double _itemHeight;

        public UniversalDesignerItemData(string text, double itemHeight, double itemWidth, int fontSize)
        {
            _text = text;
            _itemHeight = itemHeight;
            _itemWidth = itemWidth;
            _fontSize = fontSize;
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        public double ItemWidth
        {
            get
            {
                return _itemWidth;
            }
            set
            {
                _itemWidth = value;
                OnPropertyChanged("ItemWidth");
            }
        }
        public double ItemHeight
        {
            get
            {
                return _itemHeight;
            }
            set
            {
                _itemHeight = value;
                OnPropertyChanged("ItemHeight");
            }
        }
        public int FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                OnPropertyChanged("FontSize");
            }
        }
    }
}
