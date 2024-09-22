using System;
using System.Collections.Generic;
public interface ICommandCenterView
{
    int GetHullButtonCount();
    HullButton GetHullButtonAtIndex(int index);
    public  void UpdateHullButtonsState(CommandCenter commandCenter);
  public void UpdateUIAfterPurchase(CommandCenter com);
  public void SetHullButtonActive(int index, bool isActive);
    public void UpdateCenterLevelText(CommandCenter com);


    public event Action BuyCCenterClicked;
    public event Action BuyHullClicked;
    public event Action UpgradeCCenterClicked;
    public event Action UpgradeHullClicked;
    public event Action SellHullClicked;


}
