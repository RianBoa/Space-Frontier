using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    private Stack<GameObject> panelHistory = new Stack<GameObject>(); // Стек для збереження історії панелей

    [SerializeField] private Button closeButton;  // Кнопка "Закрити"
    [SerializeField] private Button backButton;   // Кнопка "Назад"
    [SerializeField] private GameObject mainPanel; // Головна панель
    [SerializeField] private GameObject hullPanel;
    [SerializeField] private GameObject commandCenterPanel;

    private void Start()
    {
        backButton.onClick.AddListener(GoBack);     // Обробник кнопки "Назад"
        closeButton.onClick.AddListener(CloseAllPanels); // Обробник кнопки "Закрити"
        UpdateButtonStates(); // Оновлюємо стан кнопок при старті
    }

    // Метод для відкриття нової панелі
    public void OpenPanel(GameObject panel)
    {
        if (panelHistory.Count > 0)
        {
            panelHistory.Peek().SetActive(false); // Приховуємо поточну панель
        }

        panelHistory.Push(panel); // Додаємо нову панель до стека
        Debug.Log("Відкриття нової панелі: " + panel.name);
        panel.SetActive(true);    // Показуємо нову панель
        UpdateButtonStates();     // Оновлюємо стан кнопок
    }

    // Метод для натискання кнопки "Назад"
    public void GoBack()
    {
        if (panelHistory.Peek() == hullPanel)
        {
            CloseAllHullPanels(); // Закриваємо всі панелі, якщо поточна — HullPanel
        }
        else if (panelHistory.Count > 1)
        {
            GameObject currentPanel = panelHistory.Pop(); // Видаляємо поточну панель зі стека
            Debug.Log("Видалення панелі: " + currentPanel.name);
            currentPanel.SetActive(false); // Приховуємо поточну панель

            GameObject previousPanel = panelHistory.Peek(); // Отримуємо попередню панель
            Debug.Log("Показуємо попередню панель: " + previousPanel.name);
            previousPanel.SetActive(true); // Показуємо попередню панель
            StartCoroutine(EnsurePanelSwitch(previousPanel));
        }
        else
        {
            Debug.Log("Залишилась лише головна панель, показуємо головну панель");
            mainPanel.SetActive(true); // Показуємо головну панель, якщо більше немає інших
        }

        UpdateButtonStates(); // Оновлюємо стан кнопок
        Debug.Log("Виклик UpdateButtonStates, стан кнопки Назад: " + backButton.gameObject.activeSelf);
    }

    // Метод для відкриття головної панелі, якщо стек історії порожній
    public void OpenMainPanelAtStart()
    {
        if (panelHistory.Count == 0)
        {
            panelHistory.Push(mainPanel); // Додаємо головну панель до стека
            mainPanel.SetActive(true);    // Показуємо головну панель
        }

        UpdateButtonStates(); // Оновлюємо стан кнопок
    }

    // Метод для закриття всіх панелей і повернення до головної панелі
    public void CloseAllPanels()
    {
        while (panelHistory.Count > 0)
        {
            panelHistory.Pop().SetActive(false); // Приховуємо всі панелі
        }

        mainPanel.SetActive(false); // Приховуємо головну панель (за потреби)
        UpdateButtonStates(); // Оновлюємо стан кнопок
    }

    // Метод для оновлення стану кнопок "Назад" і "Закрити"
    public void UpdateButtonStates()
    {
        // Кнопка "Назад" активна, якщо в стеці історії більше 1 панелі (головна + хоча б одна інша)
        if (panelHistory.Count > 1)
            backButton.gameObject.SetActive(true);
        else
            backButton.gameObject.SetActive(false);

        // Кнопка "Закрити" активна, якщо відкрита хоча б одна панель (окрім головної)
        if (panelHistory.Count > 0)
        {
            closeButton.gameObject.SetActive(true);
        }
        else
        {
            closeButton.gameObject.SetActive(false);
        }
    }

    // Гарантуємо перемикання панелей
    private IEnumerator EnsurePanelSwitch(GameObject panel)
    {
        yield return new WaitForEndOfFrame();
        panel.SetActive(true);
    }

    // Метод для закриття всіх панелей, пов'язаних з HullPanel, і повернення до панелі командного центру
    private void CloseAllHullPanels()
    {
        // Зупиняємось, коли залишається лише одна панель (попередня)
        while (panelHistory.Count > 1)
        {
            GameObject currentPanel = panelHistory.Pop();
            currentPanel.SetActive(false); // Деактивуємо поточну панель
        }

        // Перевіряємо, якщо остання панель не є панеллю командного центру, відкриваємо її
        if (panelHistory.Peek() != commandCenterPanel)
        {
            panelHistory.Push(commandCenterPanel); // Додаємо командний центр до стека
            commandCenterPanel.SetActive(true);    // Активуємо командний центр
        }

        UpdateButtonStates(); // Оновлюємо стан кнопок
    }
}
