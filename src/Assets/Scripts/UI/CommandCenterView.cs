using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandCenterView : MonoBehaviour, ICommandCenterView
{

    [SerializeField] List<HullButton> hullButtons = new List<HullButton>();

    [SerializeField] private Button buyCommandCenterButton;
    [SerializeField] private Button upgradeCommandCenterButton;
    [SerializeField] private Button buyHullButton;
    [SerializeField] private Button sellHullButton;
    [SerializeField] private Button upgradeHullButton;
    [SerializeField] private Button autoBuyButton;

    [SerializeField] private TextMeshProUGUI CenterLevelText;
    [SerializeField] private TextMeshProUGUI HullMaxInfo;
    [SerializeField] private TextMeshProUGUI UpgradeCenterPriceText;
    [SerializeField] private TextMeshProUGUI BuyHullPriceText;
    [SerializeField] private TextMeshProUGUI UpgradeHullPriceText;
    [SerializeField] private TextMeshProUGUI SellHullPriceText;


    public event Action BuyCCenterClicked;
    public event Action BuyHullClicked;
    public event Action UpgradeCCenterClicked; 
    public event Action UpgradeHullClicked;
    public event Action SellHullClicked;
    public event Action AutoBuyClicked;


    private void Start()
    {
        buyCommandCenterButton.onClick.AddListener(OnBuyCommandCenterClicked);
        upgradeCommandCenterButton.onClick.AddListener(OnUpgradeCommandCenterClicked);
        buyHullButton.onClick.AddListener(OnBuyHullClicked);
        upgradeHullButton.onClick.AddListener(OnHullUpgradeClicked);
        sellHullButton.onClick.AddListener(OnSellHullClicked);
        autoBuyButton.onClick.AddListener(OnAutoBuyClicked);
    }


    public void SetHullButtonActive(int index, bool isActive)
    {
        if (index >= 0 && index < hullButtons.Count)
        {
            hullButtons[index].gameObject.SetActive(isActive);
            var hullButton = hullButtons[index].GetComponent<Button>();

            if (hullButtons[index].AssignedHull != null)
            {
                hullButton.GetComponentInChildren<TextMeshProUGUI>().text = "Hull " + (index + 1) + " Lv." + hullButtons[index].AssignedHull.Level;

                UpgradeHullPriceText.text = "Upgrade ₡" + hullButtons[index].AssignedHull.LevelPrices[hullButtons[index].AssignedHull.Level];

                SellHullPriceText.text = "Sell ₡" + (hullButtons[index].AssignedHull.LevelPrices[hullButtons[index].AssignedHull.Level - 1] / 2);
            }
            else
            {
                hullButton.GetComponentInChildren<TextMeshProUGUI>().text = "Hull " + (index + 1) + " - No Hull";
            }
        }
    }

    public void UpdateHullButtonsState(CommandCenter com)
    {   if(com != null && com.Hulls.Count != com.MaxHulls[com.Level-1])
        {
            buyHullButton.gameObject.SetActive(true);
            BuyHullPriceText.text = "Buy Hull ₡" + ModuleInfo.GetPriceForLevel(ModuleType.Hull, 1);
        }
        else
        {
            buyHullButton.gameObject.SetActive(false);
        }
        if (com != null && com.Hulls.Count > 0)
        {

            var hullToUpgrade = com.Hulls.OrderBy(h => h.Level).FirstOrDefault(h => h.Level < h.MaxLevel);

            if (hullToUpgrade != null)
            {
        
                UpgradeHullPriceText.text = "Upgrade ₡" + hullToUpgrade.LevelPrices[hullToUpgrade.Level];
                upgradeHullButton.gameObject.SetActive(true);
            }
            else
            {
         
                upgradeHullButton.gameObject.SetActive(false);
            }


            var lastHull = com.Hulls.LastOrDefault();
            if (lastHull != null)
            {
                SellHullPriceText.text = "Sell ₡" + (lastHull.LevelPrices[lastHull.Level - 1] / 2);
                sellHullButton.gameObject.SetActive(true);
            }
            else
            {
                sellHullButton.gameObject.SetActive(false);
            }
        }
    }
    public int GetHullButtonCount()
    {
        return hullButtons.Count;
    }
    public HullButton GetHullButtonAtIndex(int index)
    {
        if (index >= 0 && index < hullButtons.Count)
        {
            return hullButtons[index];
        }
        return null;
    }
    public void UpdateCenterLevelText(CommandCenter com)
    {
        CenterLevelText.text = "CommandCenter Lv. " + com.Level;
        HullMaxInfo.text = "Hulls Max " + com.MaxHulls[com.Level -1];
    }
 

    public void UpdateUIAfterPurchase(CommandCenter com)
    {
        if (com == null)
        buyCommandCenterButton.gameObject.SetActive(true);
        if (com != null)
        {
            buyCommandCenterButton.gameObject.SetActive(false);
            autoBuyButton.gameObject.SetActive(false);
            upgradeCommandCenterButton.gameObject.SetActive(true);
            if (com.Level != 3)
                UpgradeCenterPriceText.text = "Upgrade\n Command Center\n  ₡" + com.LevelPrices[com.Level];
            else upgradeCommandCenterButton.gameObject.SetActive(false);
        }
    }

    public void OnBuyCommandCenterClicked()
    {
        BuyCCenterClicked?.Invoke();
    }
    public void OnUpgradeCommandCenterClicked()
    {
        UpgradeCCenterClicked?.Invoke();
    }
    public void OnHullUpgradeClicked()
    {
        UpgradeHullClicked?.Invoke();
    }
    public void OnSellHullClicked()
    {
        SellHullClicked?.Invoke();
    }
    public void OnBuyHullClicked()
    {
        BuyHullClicked?.Invoke();
    }
    public void OnAutoBuyClicked()
    {
        AutoBuyClicked?.Invoke();
    }
}
