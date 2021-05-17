using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class AdminManager : MonoBehaviour
{
    public TextAsset moneyApprovelFile;

    string confirmationPath = "Assets/OnayBekleyenKullanicilar.txt";
    
    string itemConfirmationPath = "Assets/ItemOnayBekleyenKullanicilar.txt";

    public GameObject moneyConfirmationPrefab;
    
    public GameObject itemConfirmationPrefab;


    public Transform gridParent;

    private GameObject tempGO;

    public DataManager dataManager;

    private List<string> addedElements = new List<string>();
    
    private List<string> itemAddedElements = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        string[] lines = System.IO.File.ReadAllLines(confirmationPath);

        for (int i = 0; i < lines.Length; i++)
        {
            if (!addedElements.Contains(lines[i]))
            {
                tempGO = Instantiate(moneyConfirmationPrefab, gridParent);
                tempGO.GetComponent<MoneyConfirmation>().username.text = lines[i].ToString();
                tempGO.GetComponent<MoneyConfirmation>().moneyField.text = GetMoneyOfUser(lines[i].ToString()).ToString();
                addedElements.Add(lines[i]);
            }
        }
        
        string[] linesItem = System.IO.File.ReadAllLines(itemConfirmationPath);

        for (int i = 0; i < linesItem.Length; i++)
        {
            if (!itemAddedElements.Contains(linesItem[i]))
            {
                tempGO = Instantiate(itemConfirmationPrefab, gridParent);
                tempGO.GetComponent<ItemConfirmation>().username.text = linesItem[i].ToString();
                itemAddedElements.Add(linesItem[i]);
            }
        }
    }

    public int GetMoneyOfUser(string username)
    {
        dataManager.user = username;
        dataManager.Load();
        return dataManager.data.money;
    }


    public void ApproveMoneyForUser(string username)
    {
        DeleteLinesFromFile(username,confirmationPath);
        dataManager.user = username;
        dataManager.Load();
        dataManager.data.isMoneyApproved = 1;
        dataManager.Save();
    }
    
    public void ApproveItemForUser(string username)
    {
        DeleteLinesFromFile(username,itemConfirmationPath);
        dataManager.user = username;
        dataManager.Load();
        dataManager.data.isItemsApproved = 1;
        dataManager.Save();
    }
    
    public void DeleteLinesFromFile(string strLineToDelete,string path)
    {
        string strSearchText = strLineToDelete;
        string strOldText;
        string n = "";
        StreamReader sr = File.OpenText(path);
        while ((strOldText = sr.ReadLine()) != null)
        {
            if (!strOldText.Contains(strSearchText))
            {
                n += strOldText + Environment.NewLine;
            }
        }
        sr.Close();
        File.WriteAllText(path, n);
    }
    
    
}
