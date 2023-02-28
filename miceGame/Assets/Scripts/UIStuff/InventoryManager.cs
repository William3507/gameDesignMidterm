using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    Image cheese;

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
        cheese.enabled = false;
    }

    void UpdateImage()
    {
        if (PlayerData.hasCheese)
        {
            cheese.enabled = true;
        }
    }
}
