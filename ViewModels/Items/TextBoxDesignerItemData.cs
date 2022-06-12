using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DiagramDesigner
{
    public class TextBoxDesignerItemData : ViewModelBase
    {
        private string _text;
        private int _fontSize;
        private double _itemWidth;
        private double _itemHeight;
        private string _textAlign;

        private ObservableCollection<string> _textAlignments;
        public TextBoxDesignerItemData(string text, double itemHeight, double itemWidth, int fontSize, string textAlign)
        {
            _text = text;
            _itemHeight = itemHeight;
            _itemWidth = itemWidth;
            _fontSize = fontSize;
            _textAlign = textAlign;

            List<string> list = new List<string>();
            list.Add("Center");
            list.Add("Left");
            list.Add("Right");
            _textAlignments = new ObservableCollection<string>(list);
        }
        public ObservableCollection<string> TextAlignments
        {
            get { return _textAlignments; }
        }
        public string TextAlign
        {
            get
            {
                return _textAlign;
            }
            set
            {
                if (_textAlign == value) return;
                _textAlign = value;
                OnPropertyChanged(nameof(TextAlign));
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
                OnPropertyChanged(nameof(FontSize));
            }
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
                OnPropertyChanged(nameof(Text));
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
    }
}
