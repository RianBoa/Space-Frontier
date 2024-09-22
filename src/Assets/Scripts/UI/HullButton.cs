using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HullButton : MonoBehaviour
{
    public Hull AssignedHull { get; private set; }
    [SerializeField] private Button button;

    public event Action<Hull> HullButtonClicked;

    public void AssignHull(Hull hull)
    {
       AssignedHull = hull;
        button.onClick.AddListener(() => Debug.Log("Кнопка корпуса с ID " + hull.ModuleId + " нажата"));
        button.onClick.AddListener(NotifyHullSelected);
    }
    private void NotifyHullSelected()
    {
        HullButtonClicked?.Invoke(AssignedHull);
    }

}
