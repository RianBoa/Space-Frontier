public class ResourceTradePresenter
{
    private readonly IResourceTradeView view;
    private readonly IResourceManager resourceManager;

    public ResourceTradePresenter(IResourceTradeView view, IResourceManager resourceManager)
    {
        this.view = view;
        this.resourceManager = resourceManager;

        // Подписываемся на изменение ресурса
        this.view.OnResourceChanged += HandleResourceChanged;

        // Подписываемся на покупку/продажу
        this.view.OnTradeButtonClicked += HandleTradeAction;
    }

    // Обработка изменения ресурса
    private void HandleResourceChanged(ResourceType resourceType, int quantity)
    {
        float pricePerUnit = GetPricePerUnit(resourceType, quantity);

        // Обновляем цену
        view.UpdatePriceText(pricePerUnit, quantity);

        // Отображаем соответствующую кнопку (купить или продать)
        if (resourceType == ResourceType.Ore)
        {
            view.ShowSellButton();
        }
        else if (resourceType == ResourceType.Energy)
        {
            view.ShowBuyButton();
        }
    }

    // Обрабатываем покупку или продажу
    private void HandleTradeAction(ResourceType resourceType, int quantity, TradeAction action)
    {
        float pricePerUnit = GetPricePerUnit(resourceType, quantity);
        float totalPrice = pricePerUnit * quantity;

        bool isActionSuccessful = action switch
        {
            TradeAction.Buy => resourceManager.SpendResource(ResourceType.Crypto, (int)totalPrice), // Покупаем ресурс
            TradeAction.Sell => resourceManager.GetResourceAmount(resourceType) >= quantity && resourceManager.SpendResource(resourceType, quantity), // Продаем ресурс
            _ => false
        };

        if (isActionSuccessful)
        {
            // Обновляем ресурсы игрока
            if (action == TradeAction.Buy)
            {
                resourceManager.AddResource(resourceType, quantity);
            }
            else if (action == TradeAction.Sell)
            {
                resourceManager.AddResource(ResourceType.Crypto, (int)totalPrice);
            }

            // Обновляем отображение цены
            view.UpdatePriceText(pricePerUnit, quantity);
        }
        else
        {
            view.UpdateErrorMessage("Not enough resources or currency.");
        }
    }

    // Метод для получения цены за единицу ресурса
    private float GetPricePerUnit(ResourceType resourceType, int quantity)
    {
        if (resourceType == ResourceType.Ore)
        {
            if (quantity < 100) return 0.12f;
            if (quantity < 500) return 0.10f;
            if (quantity < 1500) return 0.08f;
            return 0.06f;
        }
        else if (resourceType == ResourceType.Energy)
        {
            if (quantity < 100) return 0.5f;
            if (quantity < 500) return 0.4f;
            if (quantity < 1500) return 0.3f;
            return 0.1f;
        }

        return 0f; // Если не удалось определить цену, возвращаем 0
    }
}
