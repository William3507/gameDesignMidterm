using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void GotCheese()
    {
        AudioManager.instance.playSound(AudioManager.instance.cheeseGet);
        PlayerData.hasCheese = true;
        UIInventoryPanel.instance.UpdateInventory();
    }
}
