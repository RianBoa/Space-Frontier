using System.Collections;

public interface IShipFunctionsView
{
    public event System.Action<string> OnEnterResourceSource;
    public event System.Action OnExitResourceSource;
    public event System.Action OnExitPlanetCollider;
    public event System.Action OnStartOreCollection;
    public event System.Action OnStartRepair;

    public void StartCustomCoroutine(IEnumerator coroutine);
    public void ShowError(string message); 
    public void ShowMessage(string message);
}
