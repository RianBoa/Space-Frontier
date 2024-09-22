using System;
using System.Collections.Generic;
public interface IModuleSelectionView 
{
  public  void ShowModuleSelectionPanel(Hull hull, int i);
  public  void SetupModuleButtons(List<ModuleType> buttonModuleMap);
  public event Action <ModuleType, Hull, int> ModuleSelected;
}
