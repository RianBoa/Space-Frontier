using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [SerializeField] Button moduleShopButton;
    [SerializeField] Button buysellresourceButton;

    [SerializeField] GameObject commandCenterPanel;
    [SerializeField] PanelManager panelManager;
    [SerializeField] GameObject TradePanel;

    private void Start()
    {
        moduleShopButton.onClick.AddListener(() => panelManager.OpenPanel(commandCenterPanel));
        buysellresourceButton.onClick.AddListener(() => panelManager.OpenPanel(TradePanel));
    }
}
