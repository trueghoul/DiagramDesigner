using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.Persistence.Commons
{
    public interface IDatabaseAccessService
    {
        //delete methods
        void DeleteConnection(int connectionId);
        void DeletePersistDesignerItem(int persistDesignerId);
        void DeleteSettingDesignerItem(int settingsDesignerItemId);
        void DeleteRhombusDesignerItem(int rhombusDesignerItemId);

        //save methods
        int SaveDiagram(DiagramItem diagram);
        //PersistDesignerItem is pecific to the DemoApp example
        int SavePersistDesignerItem(UniversalDesignerItem persistDesignerItemToSave);
        //SettingsDesignerItem is pecific to the DemoApp example
        int SaveConnection(Connection connectionToSave);
        int SaveGroupingDesignerItem(GroupDesignerItem groupDesignerItem);
        //Fetch methods
        IEnumerable<DiagramItem> FetchAllDiagram();
        DiagramItem FetchDiagram(int diagramId);
        //PersistDesignerItem is pecific to the DemoApp example
        UniversalDesignerItem FetchPersistDesignerItem(int settingsDesignerItemId);
        //SettingsDesignerItem is pecific to the DemoApp example
        Connection FetchConnection(int connectionId);
        GroupDesignerItem FetchGroupingDesignerItem(int itemId);
    }
}
