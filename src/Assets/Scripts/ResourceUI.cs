
using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
  private ResourceManager resourceManager;

  [SerializeField] private TextMeshProUGUI oreText;
  [SerializeField] private TextMeshProUGUI energyText;
  [SerializeField] private TextMeshProUGUI cryptoText;

    void Update()
    {
        oreText.text = resourceManager.GetResourceAmount(ResourceType.Ore).ToString();
        energyText.text = resourceManager.GetResourceAmount(ResourceType.Energy).ToString();
        cryptoText.text = resourceManager.GetResourceAmount(ResourceType.Crypto).ToString();
    }
    public void SetResourceManager(ResourceManager resourceManager)
    {
    this.resourceManager = resourceManager; 
    }
}
