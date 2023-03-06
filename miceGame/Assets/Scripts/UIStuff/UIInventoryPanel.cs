using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryPanel : MonoBehaviour
{
    public static UIInventoryPanel instance;
    public Image cheese;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
        UpdateInventory();
    }

    public void UpdateInventory ()
    {
        if (PlayerData.hasCheese)
        {
            cheese.enabled = true;
        }
        else
        {
            cheese.enabled = false;
        }
    }
}
