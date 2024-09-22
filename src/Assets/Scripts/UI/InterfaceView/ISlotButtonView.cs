using System;

public interface ISlotButtonView 
{
   public void SetSlotInfo(string moduleName, int moduleLevel);
   public void SetUpgradeButtonState(bool isActive, int upgradeCost);
   public void SetSellButtonState(bool isActive, int sellPrice);
    public void SetupArrowButtons(Hull hull, int slotIndex);
   public event Action OnUpgradeButtonClicked;
   public event Action OnSellButtonClicked;
   public event Action OnSlotButtonClicked;
    public event Action OnLeftArrowClicked;
    public event Action OnRightArrowClicked;
}
