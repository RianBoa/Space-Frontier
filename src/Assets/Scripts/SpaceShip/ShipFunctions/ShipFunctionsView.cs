using UnityEngine;
using System.Collections;
using TMPro;
using System.Runtime.CompilerServices;

public class ShipFunctionsView : MonoBehaviour, IShipFunctionsView
{
    public event System.Action<IResourceSource> OnEnterResourceSource;
    public event System.Action OnExitResourceSource;
    public event System.Action OnExitPlanetCollider;
    public event System.Action OnStartOreCollection;
    public event System.Action OnStartRepair;

    [SerializeField] TextMeshProUGUI textOnEnterCollider;
    [SerializeField] TextMeshProUGUI textOnError;

    private void Update()
    {
        // ���������, ������ �� ������� "E" ��� ����� �������
        if (Input.GetKeyDown(KeyCode.E))
        {
            CollectOre();
        }

        // ���������, ������ �� ������� "F" ��� �������
        if (Input.GetKeyDown(KeyCode.F))
        {
            Repair();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var resourceSource = other.GetComponent<IResourceSource>();
        if (resourceSource != null && !resourceSource.IsDepleted())
        {
            // �������� ����������� (��������, ����������), ��� ������� ����� � ���� � ���������
            textOnEnterCollider.gameObject.SetActive(true);
          
            OnEnterResourceSource?.Invoke(resourceSource);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Planet"))
        {
            // �������� � ������ �� ���� �������
            OnExitPlanetCollider?.Invoke();
        }

        var resourceSource = other.GetComponent<IResourceSource>();
        if (resourceSource != null)
        {
            textOnEnterCollider.gameObject.SetActive(false);
            // �������� � ������ �� ���� � ���������
            OnExitResourceSource?.Invoke();
        }
    }

    public void CollectOre()
    {
        // ��� ����� ���� ������� ������� � ���������� ������������ ��� �������� ������� "E"
        OnStartOreCollection?.Invoke();
    }

    public void Repair()
    {
        // ��� ����� ���� ������� ������� � ���������� ������������ ��� �������� ������� "F"
        OnStartRepair?.Invoke();
    }

    // ����� ��� ������� ������� ����� View
    public void StartCustomCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine); // ������ �������� ����� MonoBehaviour
    }

    // ����� ��������� �� ������ ��� ������
    public void ShowMessage(string message)
    {
        Debug.Log(message); 
    }

    public void ShowError(string error)
    {
       textOnError.text = error;
       textOnError.gameObject.SetActive(true);
       StartCoroutine(HideErrorAfterDelay(5));
    }
    private IEnumerator HideErrorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ���� �������� �����
        textOnError.gameObject.SetActive(false); // �������� ����� ������
    }


}
