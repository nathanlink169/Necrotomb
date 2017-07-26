using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LoadPoint : int
{
    AreaMRoom1,
    AreaMRoom4,
    AreaMRoom11,
    Area1Room1,
    Area1Room2,
    Area1Room8,
    Area1Room15,
}

public class SavePoint : BaseBehaviour
{
    public void Save()
    {
        GSaveManager.Save(GPlayerManager.Instance.PlayerData);
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO: Do this properly
        if (other.CompareTag("Player"))
        {
            Save();
        }
    }
}
