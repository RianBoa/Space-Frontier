using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    private Stack<GameObject> panelHistory = new Stack<GameObject>(); // ���� ��� ���������� ����� �������

    [SerializeField] private Button closeButton;  // ������ "�������"
    [SerializeField] private Button backButton;   // ������ "�����"
    [SerializeField] private GameObject mainPanel; // ������� ������
    [SerializeField] private GameObject hullPanel;
    [SerializeField] private GameObject commandCenterPanel;

    private void Start()
    {
        backButton.onClick.AddListener(GoBack);     // �������� ������ "�����"
        closeButton.onClick.AddListener(CloseAllPanels); // �������� ������ "�������"
        UpdateButtonStates(); // ��������� ���� ������ ��� �����
    }

    // ����� ��� �������� ���� �����
    public void OpenPanel(GameObject panel)
    {
        if (panelHistory.Count > 0)
        {
            panelHistory.Peek().SetActive(false); // ��������� ������� ������
        }

        panelHistory.Push(panel); // ������ ���� ������ �� �����
        Debug.Log("³������� ���� �����: " + panel.name);
        panel.SetActive(true);    // �������� ���� ������
        UpdateButtonStates();     // ��������� ���� ������
    }

    // ����� ��� ���������� ������ "�����"
    public void GoBack()
    {
        if (panelHistory.Peek() == hullPanel)
        {
            CloseAllHullPanels(); // ��������� �� �����, ���� ������� � HullPanel
        }
        else if (panelHistory.Count > 1)
        {
            GameObject currentPanel = panelHistory.Pop(); // ��������� ������� ������ � �����
            Debug.Log("��������� �����: " + currentPanel.name);
            currentPanel.SetActive(false); // ��������� ������� ������

            GameObject previousPanel = panelHistory.Peek(); // �������� ��������� ������
            Debug.Log("�������� ��������� ������: " + previousPanel.name);
            previousPanel.SetActive(true); // �������� ��������� ������
            StartCoroutine(EnsurePanelSwitch(previousPanel));
        }
        else
        {
            Debug.Log("���������� ���� ������� ������, �������� ������� ������");
            mainPanel.SetActive(true); // �������� ������� ������, ���� ����� ���� �����
        }

        UpdateButtonStates(); // ��������� ���� ������
        Debug.Log("������ UpdateButtonStates, ���� ������ �����: " + backButton.gameObject.activeSelf);
    }

    // ����� ��� �������� ������� �����, ���� ���� ����� �������
    public void OpenMainPanelAtStart()
    {
        if (panelHistory.Count == 0)
        {
            panelHistory.Push(mainPanel); // ������ ������� ������ �� �����
            mainPanel.SetActive(true);    // �������� ������� ������
        }

        UpdateButtonStates(); // ��������� ���� ������
    }

    // ����� ��� �������� ��� ������� � ���������� �� ������� �����
    public void CloseAllPanels()
    {
        while (panelHistory.Count > 0)
        {
            panelHistory.Pop().SetActive(false); // ��������� �� �����
        }

        mainPanel.SetActive(false); // ��������� ������� ������ (�� �������)
        UpdateButtonStates(); // ��������� ���� ������
    }

    // ����� ��� ��������� ����� ������ "�����" � "�������"
    public void UpdateButtonStates()
    {
        // ������ "�����" �������, ���� � ����� ����� ����� 1 ����� (������� + ���� � ���� ����)
        if (panelHistory.Count > 1)
            backButton.gameObject.SetActive(true);
        else
            backButton.gameObject.SetActive(false);

        // ������ "�������" �������, ���� ������� ���� � ���� ������ (���� �������)
        if (panelHistory.Count > 0)
        {
            closeButton.gameObject.SetActive(true);
        }
        else
        {
            closeButton.gameObject.SetActive(false);
        }
    }

    // ��������� ����������� �������
    private IEnumerator EnsurePanelSwitch(GameObject panel)
    {
        yield return new WaitForEndOfFrame();
        panel.SetActive(true);
    }

    // ����� ��� �������� ��� �������, ���'������ � HullPanel, � ���������� �� ����� ���������� ������
    private void CloseAllHullPanels()
    {
        // �����������, ���� ���������� ���� ���� ������ (���������)
        while (panelHistory.Count > 1)
        {
            GameObject currentPanel = panelHistory.Pop();
            currentPanel.SetActive(false); // ���������� ������� ������
        }

        // ����������, ���� ������� ������ �� � ������� ���������� ������, ��������� ��
        if (panelHistory.Peek() != commandCenterPanel)
        {
            panelHistory.Push(commandCenterPanel); // ������ ��������� ����� �� �����
            commandCenterPanel.SetActive(true);    // �������� ��������� �����
        }

        UpdateButtonStates(); // ��������� ���� ������
    }
}
