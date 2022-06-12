using DiagramDesigner.Persistence.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace DiagramDesigner
{

    /// <summary>
    /// Simple service interface
    /// </summary>
    public interface IServiceProvider
    {
        IUIVisualizerService VisualizerService { get; }
        IMessageBoxService MessageBoxService { get; }
    }


    /// <summary>
    /// Simple service locator
    /// </summary>
    public class ServiceProvider : IServiceProvider
    {
        private IUIVisualizerService visualizerService = new WPFUIVisualizerService();
        private IMessageBoxService messageBoxService = new WPFMessageBoxService();

        public IUIVisualizerService VisualizerService
        {
            get { return visualizerService; }
        }

        public IMessageBoxService MessageBoxService
        {
            get { return messageBoxService; }
        }
    }



    /// <summary>
    /// Simple service locator helper
    /// </summary>
    public class ApplicationServicesProvider
    {
        private static Lazy<ApplicationServicesProvider> instance = new Lazy<ApplicationServicesProvider>(() => new ApplicationServicesProvider());
        private IServiceProvider serviceProvider = new ServiceProvider();

        private ApplicationServicesProvider()
        {

        }

        static ApplicationServicesProvider()
        {

        }

        public void SetNewServiceProvider(IServiceProvider provider)
        {
            serviceProvider = provider;
        }

        public IServiceProvider Provider
        {
            get { return serviceProvider; }
        }

        public static ApplicationServicesProvider Instance
        {
            get { return instance.Value;  }
        }
    }
}
