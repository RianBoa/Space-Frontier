using System;
using System.Linq;
public class ModuleSelectionPresenter
{
    private readonly IModuleSelectionView m_view;
    private readonly ModuleFactory moduleFactory;
    private readonly IModuleHandler moduleHandler;

    public event Action<Hull> ModuleBought;

    public ModuleSelectionPresenter(IModuleSelectionView m_view, ModuleFactory moduleFactory, IModuleHandler moduleHandler)
    {
        this.m_view = m_view;
        this.moduleFactory = moduleFactory;
        this.moduleHandler = moduleHandler;

        m_view.ModuleSelected += OnModuleSelected;

        
        SetupModuleSelection();
    }

    public void OpenModuleSelection(Hull hull, int slotIndex)
    {
       
        m_view.ShowModuleSelectionPanel(hull, slotIndex);   
    }

    public void ShowModuleSelectionPanel(Hull hull, int slot)
    {

    }
    public void HideModuleSelectionPanel()
    {
        throw new System.NotImplementedException();
    }

    public void SetupModuleSelection()
    {
        // Получаем список доступных модулей
        var moduleTypes = moduleFactory.GetAllModuleKeys().ToList();


        m_view.SetupModuleButtons(moduleTypes);
    }
    public void OnModuleSelected(ModuleType moduleType, Hull hull, int slotIndex)
    {
        if(moduleHandler.HandleModuleSelection(moduleType, hull, slotIndex))
        {
            ModuleBought?.Invoke(hull);
        }
    }
}
