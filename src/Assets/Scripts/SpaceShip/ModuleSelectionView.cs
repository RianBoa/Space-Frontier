using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleSelectionView : MonoBehaviour, IModuleSelectionView
{
    public event Action<ModuleType, Hull, int> ModuleSelected; // Событие выбора модуля

    [SerializeField] private GameObject moduleSelectionPanel;
    [SerializeField] private List<Button> moduleButtons;

    [SerializeField] private PanelManager panelManager;

    private Hull selectedHull;
    private int selectedSlotIndex;


    // Метод для отображения панели выбора модулей
    public void ShowModuleSelectionPanel(Hull hull, int slotIndex)
    {
        selectedHull = hull;
        selectedSlotIndex = slotIndex;
        panelManager.OpenPanel(moduleSelectionPanel);
    }

    // Метод для настройки кнопок модулей
    public void SetupModuleButtons(List<ModuleType> moduleTypes)
    {
        for (int i = 0; i < moduleButtons.Count && i < moduleTypes.Count; i++)
        {
            if (moduleTypes[i] != ModuleType.CommandCenter && moduleTypes[i] != ModuleType.Hull)
            {
                var moduleType = moduleTypes[i];
                var button = moduleButtons[i];

                // Настраиваем текст на кнопке
                button.GetComponentInChildren<TextMeshProUGUI>().text = ModuleInfo.GetModuleName(moduleType) + "\n₡" + ModuleInfo.GetPriceForLevel(moduleType, 1);
                button.onClick.RemoveAllListeners();

                // Сообщаем о выборе модуля через событие
                int currentIndex = i;
                button.onClick.AddListener(() => ModuleSelected?.Invoke(moduleTypes[currentIndex], selectedHull, selectedSlotIndex));
            }
        }
    }
}