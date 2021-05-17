using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string name;
    public string surname;
    public string username;
    public string password;
    public string idNo;
    public string phoneNo;
    public string email;
    public string address;
    public int isAdmin;
    public int isSeller;

    public int money;
    public int isMoneyApproved;

    public List<string> items = new List<string>();
    public int isItemsApproved;

    public int pamukFiyat;
    public int bugdayFiyat;
    public int arpaFiyat;
}
