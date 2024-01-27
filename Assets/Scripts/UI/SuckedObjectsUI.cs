using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuckedObjectsUI : MonoBehaviour
{
    [SerializeField] private List<Image> suckedObjects;

    private void Awake()
    {
        SuckableBase.ObjectSucked += UpdateObjectUI;
    }

    private void OnDisable()
    {
        SuckableBase.ObjectSucked -= UpdateObjectUI;
    }

    private void UpdateObjectUI(SuckableObjectStateManager obj)
    {
        suckedObjects[0].sprite = obj.GetSprite();
    }
}
