using System.Collections;

public interface IResourceSourceView
{
    event System.Action OnEnterResourceSource;
    event System.Action OnExitResourceSource;


    void DisplayResourceInfo(string resourceName, int oreAmount);
    void UpdateResourceNameAndDistance(string resourceName, float distance);
    void SetShipTransform(UnityEngine.Transform shipTransform);
    void StartRechargeTimer(IEnumerator coroutine);
    void SetStatusText(TMPro.TextMeshProUGUI statusText);
}
