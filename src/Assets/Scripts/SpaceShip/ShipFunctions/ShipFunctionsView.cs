using UnityEngine;
using System.Collections;
using TMPro;
using System.Runtime.CompilerServices;

public class ShipFunctionsView : MonoBehaviour, IShipFunctionsView
{
    public event System.Action<string> OnEnterResourceSource;
    public event System.Action OnExitResourceSource;
    public event System.Action OnExitPlanetCollider;
    public event System.Action OnStartOreCollection;
    public event System.Action OnStartRepair;

    private bool isInResourceZone = false;

    [SerializeField] TextMeshProUGUI textOnEnterCollider;
    [SerializeField] TextMeshProUGUI textOnError;

    private void Update()
    {
        // Проверяем, нажата ли клавиша "E" для сбора ресурса
        if (Input.GetKeyDown(KeyCode.E) && isInResourceZone)
        {
            CollectOre();
        }

        // Проверяем, нажата ли клавиша "F" для починки
        if (Input.GetKeyDown(KeyCode.F))
        {
            Repair();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var resourceSource = other.GetComponent<ResourceSourceView>();

        if (resourceSource != null)
        {
            // Сообщаем подписчикам (например, презентеру), что корабль вошел в зону с ресурсами
            textOnEnterCollider.gameObject.SetActive(true);
            isInResourceZone = true;
            textOnEnterCollider.text = resourceSource.Id;
            OnEnterResourceSource?.Invoke(resourceSource.Id);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInResourceZone = false;
        if (other.CompareTag("Planet"))
        {
            // Сообщаем о выходе из зоны планеты
            OnExitPlanetCollider?.Invoke();
        }

        var resourceSource = other.GetComponent<IResourceSource>();
        if (resourceSource != null)
        {
            textOnEnterCollider.gameObject.SetActive(false);
            // Сообщаем о выходе из зоны с ресурсами
            OnExitResourceSource?.Invoke();
        }
    }

    public void CollectOre()
    {
        // Это может быть вызвано кнопкой в интерфейсе пользователя или нажатием клавиши "E"
        OnStartOreCollection?.Invoke();
    }

    public void Repair()
    {
        // Это может быть вызвано кнопкой в интерфейсе пользователя или нажатием клавиши "F"
        OnStartRepair?.Invoke();
    }

    // Метод для запуска корутин через View
    public void StartCustomCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine); // Запуск корутины через MonoBehaviour
    }

    // Показ сообщения об ошибке или успехе
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
        yield return new WaitForSeconds(delay); // Ждем заданное время
        textOnError.gameObject.SetActive(false); // Скрываем текст ошибки
    }


}
