using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrbitalStationController : MonoBehaviour
{
    [SerializeField] private PanelManager panelManager;
    bool playerNearStation;
    [SerializeField] TextMeshProUGUI pressButtonText;
    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpaceShip") || collision.CompareTag("Player"))
        {
            playerNearStation = true;

            pressButtonText.gameObject.SetActive(true);
            pressButtonText.text = "Press E to interact with Station";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SpaceShip") || collision.CompareTag("Player"))
        {
            playerNearStation = false;
            pressButtonText.gameObject.SetActive(false);  // Скрыть текст
        }
    }

    private void Update()
    {
        if(playerNearStation && Input.GetKeyDown(KeyCode.E))
        {
            panelManager.OpenMainPanelAtStart();
        }
    }
}
