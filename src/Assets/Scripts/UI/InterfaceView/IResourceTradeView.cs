using System;

public enum TradeAction
{
    Buy,
    Sell
}



public interface IResourceTradeView
{
    event Action<ResourceType, int> OnResourceChanged;  // ������� ��� ��������� �������
    event Action<ResourceType, int, TradeAction> OnTradeButtonClicked;  // ������� ��� �������/�������

    void UpdatePriceText(float pricePerUnit, int quantity);  // �������� ����
    void UpdateErrorMessage(string message);  // �������� ��������� �� ������
    void ShowBuyButton();  // �������� ������ "������"
    void ShowSellButton();  // �������� ������ "�������"
}