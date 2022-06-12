using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DiagramDesigner
{
    public abstract class DesignerItemViewModelBase : SelectableDesignerItemViewModelBase
    {
        private double left;
        private double top;
        private bool showConnectors = false;
        private string _imageUrl;
        private List<FullyCreatedConnectorInfo> connectors = new List<FullyCreatedConnectorInfo>();

        private double itemWidth = 80;
        private double itemHeight = 80;

        public DesignerItemViewModelBase(int id, IDiagramViewModel parent, double left, double top) 
            : base(id, parent)
        {
            this.left = left;
            this.top = top;
            Init();
        }

        public DesignerItemViewModelBase(int id, IDiagramViewModel parent, double left, double top, double itemWidth, double itemHeight) 
            : base(id, parent)
        {
            this.left = left;
            this.top = top;
            this.itemWidth = itemWidth;
            this.itemHeight = itemHeight;
            Init();
        }

        public DesignerItemViewModelBase(): base()
        {
            Init();
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }
        public double ItemWidth
        {
            get
            {
                return itemWidth;
            }
            set
            {
                itemWidth = value;
                OnPropertyChanged("ItemWidth");
            }
        }

        public double ItemHeight
        {
            get
            {
                return itemHeight;
            }
            set
            {
                itemHeight = value;
                OnPropertyChanged("ItemHeight");
            }
        }

        #region Connectors
        public FullyCreatedConnectorInfo TopConnector
        {
            get { return connectors[0]; }
        }


        public FullyCreatedConnectorInfo BottomConnector
        {
            get { return connectors[1]; }
        }


        public FullyCreatedConnectorInfo LeftConnector
        {
            get { return connectors[2]; }
        }


        public FullyCreatedConnectorInfo RightConnector
        {
            get { return connectors[3]; }
        }


        public bool ShowConnectors
        {
            get
            {
                return showConnectors;
            }
            set
            {
                if (showConnectors != value)
                {
                    showConnectors = value;
                    TopConnector.ShowConnectors = value;
                    BottomConnector.ShowConnectors = value;
                    RightConnector.ShowConnectors = value;
                    LeftConnector.ShowConnectors = value;
                    OnPropertyChanged("ShowConnectors");
                }
            }
        }
        #endregion

        public double Left
        {
            get
            {
                return left;
            }
            set
            {
                if (left != value)
                {
                    left = value;
                    OnPropertyChanged("Left");
                }
            }
        }

        public double Top
        {
            get
            {
                return top;
            }
            set
            {
                if (top != value)
                {
                    top = value;
                    OnPropertyChanged("Top");
                }
            }
        }


        private void Init()
        {
            connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Top));
            connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Bottom));
            connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Left));
            connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Right));
        }

    }
}
