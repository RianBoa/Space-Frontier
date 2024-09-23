using System;
using System.Linq;
public class CommandCenterPresenter
{
    private readonly ICommandCenterView c_view;
    private readonly IModuleHandler m_handle;
    private readonly HullPanelPresenter hullPanelPresenter;
    private CommandCenter com;
    public CommandCenterPresenter(ICommandCenterView c_view, IModuleHandler m_handle, HullPanelPresenter hullPanelPresenter)
    {
        this.c_view = c_view;
        this.m_handle = m_handle;
        this.hullPanelPresenter = hullPanelPresenter;

        this.c_view.BuyCCenterClicked += OnTryPurchaseCommandCenter;
        this.c_view.BuyHullClicked += OnTryPurchaseHull;
        this.c_view.UpgradeCCenterClicked += TryUpgradeCommandCenter;
        this.c_view.UpgradeHullClicked += TryUpgradeHull;
        this.c_view.SellHullClicked += TrySellHull;
        this.c_view.AutoBuyClicked += TryBuyRequiredModules;
        
    }

    // Метод для обновления кнопок корпуса через представление
    public void OnTryPurchaseHull()
    {
        if (m_handle.HandleHullPurchase())
        {   
            UpdateHullButtons(com);
            c_view.UpdateHullButtonsState(com);
        }
    }


    public void UpdateHullButtons(CommandCenter commandCenter)
    {
        int hullCount = commandCenter.Hulls.Count;

        for (int i = 0; i < c_view.GetHullButtonCount(); i++)
        {
            if (i < hullCount)
            {
                var hullButton = c_view.GetHullButtonAtIndex(i);
                if (hullButton != null)
                {
                    hullButton.AssignHull(commandCenter.Hulls[i]); 
                    hullPanelPresenter.SubToHullButton(hullButton); 
                }
                c_view.SetHullButtonActive(i, true); 
            }
            else
            {
                c_view.SetHullButtonActive(i, false); 
            }
        }
    }

    public void OnTryPurchaseCommandCenter()
    {
        if (m_handle.HandleCenterPurchase())
        {
            com = m_handle.GetCommandCenter();

            c_view.UpdateUIAfterPurchase(com);
            c_view.UpdateCenterLevelText(com);
            c_view.UpdateHullButtonsState(com);
        }
    }
    public void TryUpgradeCommandCenter()
    {
        m_handle.HandleModuleUpgrade(com);
        c_view.UpdateCenterLevelText(com);
        c_view.UpdateUIAfterPurchase(com);
        c_view.UpdateHullButtonsState(com);
    }
    public void TryUpgradeHull()
    {
        if (com == null) return;

        int minHullLevel = com.Hulls.Min(hull => hull.Level);

        foreach (var hull in com.Hulls)
        {
            if (hull.Level == minHullLevel)
            {
                if (m_handle.HandleModuleUpgrade(hull))
                {
                    // Обновляем интерфейс после улучшения корпуса
                    UpdateHullButtons(com);
                    c_view.UpdateHullButtonsState(com);
                    break;
                }
            }
        }
    }

    // Метод для продажи последнего корпуса
    public void TrySellHull()
    {
        var hull = com.Hulls.LastOrDefault();
        if (hull != null && m_handle.HandleHullSold(hull))
        {
            UpdateHullButtons(com);
            c_view.UpdateHullButtonsState(com);
        }
    }
    public void TryBuyRequiredModules()
    {
       if(m_handle.TryPurchaseAllRequiresModules())
        {
            com = m_handle.GetCommandCenter();
            UpdateHullButtons(com);
            c_view.UpdateCenterLevelText(com);
            c_view.UpdateHullButtonsState(com);
            c_view.UpdateUIAfterPurchase(com);
        }
    }
}

