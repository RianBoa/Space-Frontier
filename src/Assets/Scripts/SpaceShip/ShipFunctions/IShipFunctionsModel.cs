using System;
using System.Collections;
public interface IShipFunctionsModel
{
    public IResourceSource CurrentResourceSource { get; set; }
    public IEnumerator RepairCoroutine(Action<int> onRepairProgress, Action<string> onError);
    public IEnumerator CollectOreCoroutine(IResourceSource resourceSource, Action<int> onOreCollected);
    public void SetCurrentResourceSource(IResourceSource resourceSource);
    public void ClearCurrentResourceSource(IResourceSource resourceSource);
    void OnLeaveCollider();

    public event Action<string> onError;
}
