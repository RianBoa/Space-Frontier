using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HullPanelView : MonoBehaviour, IHullPanelView
{
    public event Action<Hull> HullSelected;
    public event Action<Hull, int> SlotSelected;

    [SerializeField] GameObject hullPanel;
    [SerializeField] private SlotButtonView[] slotButtons;
    [SerializeField] private PanelManager panelManager;
    [SerializeField] private TextMeshProUGUI textLvl;
    private Hull selectedHull;
    private int selectedSlot;


    public void OpenHullPanel(Hull hull)
    {
        selectedHull = hull;
        panelManager.OpenPanel(hullPanel);
        
        HullSelected?.Invoke(hull);
        Debug.Log("Открытие панели корпуса для корпуса с ID: " + hull.ModuleId);
    }
    
    private void SlotSelect(Hull hull, int slotIndex)
    {
        selectedSlot = slotIndex;
        SlotSelected?.Invoke(hull, slotIndex);
    }
    public void SetSlotButtonActive(int index, bool isActive)
    {
        slotButtons[index].gameObject.SetActive(isActive);
    }


    public void ShowModuleSoldMessage(int index)
    {
        throw new NotImplementedException();
    }

    public void ShowModuleUpgradedMessage(IModule module)
    {
        throw new NotImplementedException();
    }
    public void SetLevelText(Hull hull)
    {
        textLvl.text = "Hull Lv. " + hull.Level;
    }
    
}

