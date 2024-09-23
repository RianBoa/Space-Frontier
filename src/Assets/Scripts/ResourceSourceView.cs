using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceSourceView : MonoBehaviour, IResourceSourceView
{
    private TextMeshProUGUI statusText;
    private Transform spaceShip;
    private string resourceName;
    public string Id {  get; set; }
    
    public event Action OnEnterResourceSource;
    public event Action OnExitResourceSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpaceShip"))
        {
            OnEnterResourceSource?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SpaceShip"))
        {
            OnExitResourceSource?.Invoke();
        }
    }


    public void DisplayResourceInfo(string resourceName, int oreAmount)
    {
        statusText.text = $"planet id : {Id}";
    }


    public void StartRechargeTimer(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
    public void SetShipTransform(Transform spaceShip)
    {
        this.spaceShip = spaceShip;
    }

    public void UpdateResourceNameAndDistance(string resourceName, float distance)
    {
       
    }
    public void SetStatusText(TextMeshProUGUI statusText)
    {
        this.statusText = statusText;
    }

    
}

