using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyConfirmation : MonoBehaviour
{
    public Text username;
    public Text moneyField;

    public AdminManager aManager;

    public void ApproveMoney()
    {
        aManager = FindObjectOfType<AdminManager>();
        aManager.ApproveMoneyForUser(username.text);
        
        Destroy(gameObject);
    }
    // Start is called before the first frame update

}
