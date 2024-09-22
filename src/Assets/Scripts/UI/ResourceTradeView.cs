using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceTradeView : MonoBehaviour, IResourceTradeView
{
    [SerializeField] public TMP_Dropdown resourceDropdown;
    [SerializeField] public Button buyButton;
    [SerializeField] public Button sellButton;
    [SerializeField] public TMP_Text priceText;
    [SerializeField] public TMP_InputField quantityInput;
    [SerializeField] public TMP_Text errorMessage;

    public event Action<ResourceType, int> OnResourceChanged;
    public event Action<ResourceType, int, TradeAction> OnTradeButtonClicked;

    private void Start()
    {

        resourceDropdown.ClearOptions();
        resourceDropdown.AddOptions(new System.Collections.Generic.List<string> { "Ore", "Energy" });

    
        resourceDropdown.onValueChanged.AddListener(OnResourceSelectionChanged);

 
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        sellButton.onClick.AddListener(OnSellButtonClicked);

      
        ShowBuyButton();
    }

    private void OnResourceSelectionChanged(int selectedIndex)
    {
        if (int.TryParse(quantityInput.text, out int quantity) && quantity > 0)
        {
            var selectedResource = GetSelectedResource();
            OnResourceChanged?.Invoke(selectedResource, quantity);
        }
        else
        {
            UpdateErrorMessage("Invalid quantity.");
        }
    }

    private void OnBuyButtonClicked()
    {
        if (int.TryParse(quantityInput.text, out int quantity) && quantity > 0)
        {
            var selectedResource = GetSelectedResource();
            OnTradeButtonClicked?.Invoke(selectedResource, quantity, TradeAction.Buy);
        }
        else
        {
            UpdateErrorMessage("Invalid quantity.");
        }
    }

    private void OnSellButtonClicked()
    {
        if (int.TryParse(quantityInput.text, out int quantity) && quantity > 0)
        {
            var selectedResource = GetSelectedResource();
            OnTradeButtonClicked?.Invoke(selectedResource, quantity, TradeAction.Sell);
        }
        else
        {
            UpdateErrorMessage("Invalid quantity.");
        }
    }

    private ResourceType GetSelectedResource()
    {
        return resourceDropdown.value == 0 ? ResourceType.Ore : ResourceType.Energy;
    }

    public void UpdatePriceText(float pricePerUnit, int quantity)
    {
        priceText.text = $"Total Price: ₡{pricePerUnit * quantity}";
    }

    public void UpdateErrorMessage(string message)
    {
        errorMessage.text = message;
    }

    public void ShowBuyButton()
    {
        buyButton.gameObject.SetActive(true);
        sellButton.gameObject.SetActive(false);
    }

    public void ShowSellButton()
    {
        buyButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(true);
    }
}
