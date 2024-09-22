using System;

public enum TradeAction
{
    Buy,
    Sell
}



public interface IResourceTradeView
{
    event Action<ResourceType, int> OnResourceChanged;  // Событие для изменения ресурса
    event Action<ResourceType, int, TradeAction> OnTradeButtonClicked;  // Событие для покупки/продажи

    void UpdatePriceText(float pricePerUnit, int quantity);  // Обновить цену
    void UpdateErrorMessage(string message);  // Показать сообщение об ошибке
    void ShowBuyButton();  // Показать кнопку "Купить"
    void ShowSellButton();  // Показать кнопку "Продать"
}