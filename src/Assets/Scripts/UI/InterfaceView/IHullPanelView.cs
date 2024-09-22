public interface IHullPanelView
{
 public void SetSlotButtonActive(int index, bool isActive);  
 public void OpenHullPanel(Hull hull); 
 public void ShowModuleSoldMessage(int slotIndex); 
 public void ShowModuleUpgradedMessage(IModule module);
 public void SetLevelText(Hull hull);
 
}
