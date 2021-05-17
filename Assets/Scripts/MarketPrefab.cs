using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketPrefab : MonoBehaviour
{
    public Text price;

    public Text owner;

    public Text productType;

    public Text amount;

    public DataManager dataManager;

    public BuyerManager buyerManager;

    public int amountInt;

    public int priceInt;

    private int priceTotal;

    // Start is called before the first frame update

    public void BuyProduct()
    {
        dataManager = FindObjectOfType<DataManager>();
        buyerManager = FindObjectOfType<BuyerManager>();

        if (dataManager.data.isMoneyApproved == 0)
        {
            PopupManager.Instance.ShowText("Paranız onaylanmadı!");
            return;
        }

        priceTotal = amountInt * priceInt;
        
        if (priceTotal > dataManager.data.money)
        {
            PopupManager.Instance.ShowText("Yeterli Paranız Yok!");
        }
        else
        {
            dataManager.data.money -= priceTotal;
            dataManager.Save();
            
            
            TransferMoney();
            
            ReorganizeMarket();
            
            buyerManager.UpdateMoneyField();
            Destroy(gameObject);
        }
        
        
    }

    public void ReorganizeMarket()
    {
        var jsonString = dataManager.LoadMarketData();

        MarketItem[] marketItems = JsonHelper.FromJson<MarketItem>(jsonString);

        List<MarketItem> marketItemList = marketItems.ToList();

        for (int i = 0; i < marketItemList.Count; i++)
        {
            if (marketItemList[i].owner == owner.text && marketItemList[i].productName == productType.text && marketItemList[i].price == priceInt && marketItemList[i].amount == amountInt)
            {
                marketItemList.Remove(marketItemList[i]);
                break;
            }
        }

        MarketItem[] newMarketItems = marketItemList.ToArray();
        string newMarketJson = JsonHelper.ToJson(newMarketItems, true);
        dataManager.SaveMarketData(newMarketJson);
    }

    public void TransferMoney()
    {
        var prevUser = dataManager.data.username;
        
        dataManager.Save();
        
        dataManager.user = owner.text;
  
        dataManager.SetUser();
        dataManager.Load();
        for (int i = 0; i < dataManager.data.items.Count; i++)
        {
            var itemAmount = string.Concat(dataManager.data.items[i].ToArray().Reverse().TakeWhile(char.IsNumber).Reverse());
            var type = new String(dataManager.data.items[i].Where(Char.IsLetter).ToArray());
            if (itemAmount == amountInt.ToString() && type == productType.text)
            {
                dataManager.data.items.Remove(dataManager.data.items[i]);
                break;
            }
        }
        dataManager.data.money += priceTotal;
        dataManager.Save();

        dataManager.user = prevUser;
        dataManager.SetUser();
        dataManager.Load();
        

    }
}
