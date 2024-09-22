using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotButtonView : MonoBehaviour, ISlotButtonView
{
    [SerializeField] private Button mainButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button sellButton;

    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;

    public event Action OnUpgradeButtonClicked;
    public event Action OnSellButtonClicked;
    public event Action OnSlotButtonClicked;
    public event Action OnLeftArrowClicked;
    public event Action OnRightArrowClicked;

    private void Awake()
    {
        upgradeButton.onClick.AddListener(() => OnUpgradeButtonClicked?.Invoke());
        sellButton.onClick.AddListener(() => OnSellButtonClicked?.Invoke());
        mainButton.onClick.AddListener(() => OnSlotButtonClicked?.Invoke());
    }

    public void SetSlotInfo(string moduleName, int moduleLevel)
    {
        mainButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{moduleName} Lv. {moduleLevel}";
        mainButton.interactable = string.IsNullOrEmpty(moduleName) ? false : true;
    }

    public void SetUpgradeButtonState(bool isActive, int upgradeCost)
    {
        upgradeButton.gameObject.SetActive(isActive);
        if (isActive)
        {
            var priceText = upgradeButton.GetComponentInChildren<TextMeshProUGUI>();
            priceText.text = $"Upgrade ₡{upgradeCost}";
        }
    }

    public void SetSellButtonState(bool isActive, int sellPrice)
    {
        sellButton.gameObject.SetActive(isActive);
        if (isActive)
        {
            var priceText = sellButton.GetComponentInChildren<TextMeshProUGUI>();
            priceText.text = $"Sell ₡{sellPrice}";
        }
    }


    // Метод для настройки стрелок через события
    public void SetupArrowButtons(Hull hull, int slotIndex)
    {
        if (hull == null || hull.slots == null || slotIndex < 0 || slotIndex >= hull.slots.Count)
        {
            Debug.LogError($"Invalid slot or hull data. SlotIndex: {slotIndex}, Hull is null: {hull == null}");
            return;
        }

        // Проверка, занят ли текущий слот
        var currentSlot = hull.slots[slotIndex];
        if (!currentSlot.IsOccupied)
        {
            ConfigureArrow(leftArrow, false, OnLeftArrowClicked);
            ConfigureArrow(rightArrow, false, OnRightArrowClicked);
            Debug.Log($"Slot {slotIndex} is empty, disabling arrows.");
            return;
        }

        // Если слот занят, проверяем возможность перемещения
        bool canMoveLeft = slotIndex > 0 && hull.slots[slotIndex - 1] != null && !hull.slots[slotIndex - 1].IsOccupied;
        bool canMoveRight = slotIndex < hull.slots.Count - 1 && hull.slots[slotIndex + 1] != null && !hull.slots[slotIndex + 1].IsOccupied;

        Debug.Log($"Slot {slotIndex} | Can move left: {canMoveLeft} | Can move right: {canMoveRight}");

        ConfigureArrow(leftArrow, canMoveLeft, OnLeftArrowClicked);
        ConfigureArrow(rightArrow, canMoveRight, OnRightArrowClicked);
    }

        private void ConfigureArrow(Button arrowButton, bool isEnabled, Action onClickAction)
    {
        if (arrowButton == null)
        {
            Debug.LogError("Arrow button is null.");
            return;
        }

        Debug.Log($"Arrow {arrowButton.name} active state: {isEnabled}");
        arrowButton.gameObject.SetActive(isEnabled);

        if (isEnabled)
        {
            arrowButton.onClick.RemoveAllListeners();
            arrowButton.onClick.AddListener(() => onClickAction?.Invoke());
        }
    }
}