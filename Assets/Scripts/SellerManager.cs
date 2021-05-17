using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SellerManager : MonoBehaviour
{
    public DataManager dataManager;
    public Dropdown itemDropdown;
    public InputField itemAmount;

    public GameObject productPrefab;
    public Transform productTransform;
    public Text priceField;
    public Text moneyField;

    private void Awake()
    {
        FillItems();
        moneyField.text = dataManager.data.money.ToString() + " TL";
    }

    public void AddItem()
    {
        var selectedValue = itemDropdown.value;
        var selectedItem = itemDropdown.options[selectedValue].text;
        var price = 0;

        switch (itemDropdown.options[selectedValue].text)
        {
            case "Pamuk":
                dataManager.data.pamukFiyat = Int32.Parse(priceField.text);
                price = dataManager.data.pamukFiyat;
                break;
            case "Buğday":
                dataManager.data.bugdayFiyat = Int32.Parse(priceField.text);
                price = dataManager.data.bugdayFiyat;
                break;
            case "Arpa":
                dataManager.data.arpaFiyat = Int32.Parse(priceField.text);
                price = dataManager.data.arpaFiyat;
                break;
        }

        string marketJson = dataManager.LoadMarketData();
        if (marketJson != "")
        {
            MarketItem[] existingItems = JsonHelper.FromJson<MarketItem>(dataManager.LoadMarketData());

            MarketItem[] newMarketArray = new MarketItem[existingItems.Length + 1];

            for (int i = 0; i < existingItems.Length; i++)
            {
                newMarketArray[i] = existingItems[i];
            }
            
            newMarketArray[newMarketArray.Length - 1] = new MarketItem();
            newMarketArray[newMarketArray.Length - 1].owner = dataManager.data.username;
            newMarketArray[newMarketArray.Length - 1].productName = selectedItem;
            newMarketArray[newMarketArray.Length - 1].amount = Int32.Parse(itemAmount.text);
            newMarketArray[newMarketArray.Length - 1].price = price;

            string marketToJson = JsonHelper.ToJson(newMarketArray, true);
            dataManager.SaveMarketData(marketToJson);
        }
        else
        {
            MarketItem[] newMarketArray = new MarketItem[1];
            newMarketArray[0] = new MarketItem();
            newMarketArray[0].owner = dataManager.data.username;
            newMarketArray[0].productName = selectedItem;
            newMarketArray[0].amount = Int32.Parse(itemAmount.text);
            newMarketArray[0].price = price;
            string marketToJson = JsonHelper.ToJson(newMarketArray, true);
            Debug.Log(marketJson);
            dataManager.SaveMarketData(marketToJson);
        }
        
        selectedItem += itemAmount.text;
        
        dataManager.data.items.Add(selectedItem);
        dataManager.data.isItemsApproved = 0;
        dataManager.Save();
        
        FillItems();
    }

    public void FillItems()
    {
        for (int i = 0; i < productTransform.childCount; i++)
        {
            Destroy(productTransform.GetChild(i).gameObject);
        }
        
        var itemList = dataManager.data.items;

        for (int i = 0; i < itemList.Count; i++)
        {
            var input = itemList[i];
            var amount = string.Concat(input.ToArray().Reverse().TakeWhile(char.IsNumber).Reverse());
            var type = new String(input.Where(Char.IsLetter).ToArray());

            var tempGO = Instantiate(productPrefab, productTransform);
            tempGO.GetComponent<Product>().productName.text = type;
            tempGO.GetComponent<Product>().productAmount.text = amount + " KG";
            switch (type)
            {
                case "Pamuk":
                    tempGO.GetComponent<Product>().productPrice.text = dataManager.data.pamukFiyat.ToString();
                    break;
                case "Buğday":
                    tempGO.GetComponent<Product>().productPrice.text = dataManager.data.bugdayFiyat.ToString();
                    break;
                case "Arpa":
                    tempGO.GetComponent<Product>().productPrice.text = dataManager.data.arpaFiyat.ToString();
                    break;
            }
            if (dataManager.data.isItemsApproved == 0)
            {
                tempGO.GetComponent<Product>().productConfirmation.text = "ONAYLANMADI";
            }
            else
            {
                tempGO.GetComponent<Product>().productConfirmation.text = "";
            }

        }
        
    }

}
