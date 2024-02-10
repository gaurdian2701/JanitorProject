using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class for handling UI related to objects that the player has sucked so that the player knows what objects they have sucked inside
public class SuckedObjectsUI
{
    [SerializeField] private List<Image> suckedObjects;
    private int UIIndex;

    public SuckedObjectsUI(List<Image> _suckedObjects)
    {
        suckedObjects = _suckedObjects;
        UIIndex = 0;
        EventService.Instance.OnObjectSucked.AddEventListener(AddObjectToUI);
        EventService.Instance.OnObjectShot.AddEventListener(RemoveObjectFromUI);
    }

    public void Cleanup()
    {
        EventService.Instance.OnObjectSucked.RemoveEventListener(AddObjectToUI);
        EventService.Instance.OnObjectShot.RemoveEventListener(RemoveObjectFromUI);
        suckedObjects.Clear();
    }

    private void RemoveObjectFromUI()
    {
        if (UIIndex > 0)
            UIIndex--;

        suckedObjects[UIIndex].sprite = null;
    }

    private void AddObjectToUI(SuckableObjectStateManager obj)
    {
        if (obj == null)
            return;

        suckedObjects[UIIndex].sprite = obj.GetSprite();
        UIIndex++;
    }
}
