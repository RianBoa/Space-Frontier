using System;
using System.Collections.Generic;

public class HullPanelPresenter
{
    private readonly IHullPanelView h_view;
    private readonly IModuleHandler moduleHandler;
    private readonly SlotMovementManager slotMovementManager;
    private readonly SlotButtonPresenter[] slotPresenters;
    private readonly ModuleSelectionPresenter moduleSelectionPresenter;

    public HullPanelPresenter(IHullPanelView h_view, SlotButtonPresenter[] slotPresenters, ModuleSelectionPresenter moduleSelectionPresenter)
    {
        this.h_view = h_view;
        this.slotPresenters = slotPresenters;

        for (int i = 0; i < slotPresenters.Length; i++)
        {
            var slotPresenter = slotPresenters[i];
            slotPresenter.ModuleUpgraded += OnModuleUpgraded;
            slotPresenter.ModuleMoved += OnModuleMoved;
            
        }
        moduleSelectionPresenter.ModuleBought += OnModuleBought;
    }

    // Метод для обновления панели корпуса
    private void UpdateHullPanel(Hull hull)
    {
        for (int i = 0; i < slotPresenters.Length; i++)
        {
            if (i < hull.slots.Count)
            {
                h_view.SetSlotButtonActive(i, true);
                slotPresenters[i].SetupSlot(hull, i);
            }
            else
            {
                h_view.SetSlotButtonActive(i, false);
            }
        }
    }

    public void SubToHullButton(HullButton hullButton)
    {
        hullButton.HullButtonClicked += OnHullButtonClicked;
    }

    private void OnHullButtonClicked(Hull hull)
    {
        UpdateHullPanel(hull);
        h_view.OpenHullPanel(hull);
        h_view.SetLevelText(hull);
    }


    private void OnModuleSold(Hull hull, int slotIndex)
    {
        UpdateHullPanel(hull);
        h_view.ShowModuleSoldMessage(slotIndex);
    }

    private void OnModuleUpgraded(IModule module, Hull hull)
    {
        UpdateHullPanel(hull);
        h_view.ShowModuleUpgradedMessage(module);
    }
    private void OnModuleBought(Hull hull)
    {
        UpdateHullPanel(hull);
        h_view.OpenHullPanel(hull);
    }

    private void OnModuleMoved(Hull hull)
    {
        UpdateHullPanel(hull);
    }
}