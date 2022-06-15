using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DiagramDesigner;
using Microsoft.Win32;
using System.IO;

namespace DiagramDesigner
{
    public class ToolBoxViewModel
    {
        private ObservableCollection<ToolBoxData> _toolBoxItems = new ObservableCollection<ToolBoxData>();
        private const string Directory = "/Resources/Images/";
        public SimpleCommand AddItemCommand { get; private set; }

        public ToolBoxViewModel()
        {
            AddItemCommand = new SimpleCommand(ExecuteAddItemCommand);

            _toolBoxItems.Add(new ToolBoxData(Directory + "Text.png", typeof(TextBoxDesignerItemViewModel)));
            _toolBoxItems.Add(new ToolBoxData(Directory + "Ellipse.png", typeof(UniversalDesignerItemViewModel)));
            _toolBoxItems.Add(new ToolBoxData(Directory + "Rhombus.png", typeof(UniversalDesignerItemViewModel)));
            _toolBoxItems.Add(new ToolBoxData(Directory + "Rectangle.png", typeof(RectangleDesignerItemViewModel)));

        }
        private void ExecuteAddItemCommand(object parametr)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            openFileDialog.Title = "Выбор файла";
            string _filepath;
            string _filename;
            if (openFileDialog.ShowDialog() == true)
            {
                _filepath = openFileDialog.FileName;
                _filename = openFileDialog.SafeFileName;
                try
                {
                    //File.Copy(_filepath, Path.Combine("../../" + Directory, _filename), true);

                    System.IO.Directory.CreateDirectory("../../bin/Debug/Resources/Images/");
                    File.Copy(_filepath, Path.Combine("../../bin/Debug/Resources/Images/" + _filename), true);
                }
                catch { new Exception(); }
                _toolBoxItems.Add(new ToolBoxData("pack://siteoforigin:,,,/Resources/Images/" + _filename, typeof(UniversalDesignerItemViewModel)));
            }
        }
        public ObservableCollection<ToolBoxData> ToolBoxItems
        {
            get { return _toolBoxItems; }
        }
    }
}
