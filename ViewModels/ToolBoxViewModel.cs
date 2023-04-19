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
        private ObservableCollection<ToolBoxData> _blockDiagram = new ObservableCollection<ToolBoxData>();
        private ObservableCollection<ToolBoxData> _cisco = new ObservableCollection<ToolBoxData>();
        private ObservableCollection<ToolBoxData> _customElements = new ObservableCollection<ToolBoxData>();

        private const string Directory = "/Resources/Images/";
        public CommandBase AddItemCommand { get; private set; }

        public ToolBoxViewModel()
        {
            AddItemCommand = new CommandBase(ExecuteAddItemCommand);

            _blockDiagram.Add(new ToolBoxData(Directory + "Text.png", typeof(TextBoxDesignerItemViewModel), null));
            _blockDiagram.Add(new ToolBoxData(Directory + "Rectangle.png", typeof(RectangleDesignerItemViewModel), null));
            _blockDiagram.Add(new ToolBoxData(Directory + "Parallelo.png", typeof(ParallelogramDesignerItemViewModel), null));
            _blockDiagram.Add(new ToolBoxData(Directory + "roundedRec.png", typeof(RoundedRectangleDesignerItemViewModel), 20));
            _blockDiagram.Add(new ToolBoxData(Directory + "Ellipse.png", typeof(EllipseDesignerItemViewModel), 40));
            _blockDiagram.Add(new ToolBoxData(Directory + "Circle.png", typeof(EllipseDesignerItemViewModel), 9999));
            _blockDiagram.Add(new ToolBoxData(Directory + "Rhombus.png", typeof(UniversalDesignerItemViewModel), null));
            _blockDiagram.Add(new ToolBoxData(Directory + "cicleup.png", typeof(UniversalDesignerItemViewModel), null));
            _blockDiagram.Add(new ToolBoxData(Directory + "cicledown.png", typeof(UniversalDesignerItemViewModel), null));

            _cisco.Add(new ToolBoxData(Directory + "router.png", typeof(UniversalDesignerItemViewModel), null));
            _cisco.Add(new ToolBoxData(Directory + "switch.png", typeof(UniversalDesignerItemViewModel), null));
            _cisco.Add(new ToolBoxData(Directory + "web-browser.png", typeof(UniversalDesignerItemViewModel), null));
            _cisco.Add(new ToolBoxData(Directory + "workstation.png", typeof(UniversalDesignerItemViewModel), null));

        }
        private void ExecuteAddItemCommand(object parametr)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;",
                Title = "Выбор файла"
            };
            if (openFileDialog.ShowDialog() != true) return;
            var filepath = openFileDialog.FileName;
            var filename = openFileDialog.SafeFileName;
            try
            {
                System.IO.Directory.CreateDirectory("../../bin/Debug/Resources/Images/");
                File.Copy(filepath,
                    Path.Combine("../../bin/Debug/Resources/Images/" + filename), true);
            }
            catch
            {
                throw new Exception();
            }
            _customElements.Add(new ToolBoxData("pack://siteoforigin:,,,/Resources/Images/" + filename,
                typeof(UniversalDesignerItemViewModel),  null));
        }
        public ObservableCollection<ToolBoxData> BlockDiagram
        {
            get { return _blockDiagram; }
        }
        public ObservableCollection<ToolBoxData> CustomElements
        {
            get { return _customElements; }
        }
        public ObservableCollection<ToolBoxData> Cisco
        {
            get { return _cisco; }
        }
    }
}
