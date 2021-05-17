using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemConfirmation : MonoBehaviour
{
    public Text username;

    public AdminManager aManager;

    
    public void ApproveItem()
    {
        aManager = FindObjectOfType<AdminManager>();
        aManager.ApproveItemForUser(username.text);
        
        Destroy(gameObject);
    }
    // Start is called before the first frame update

}
