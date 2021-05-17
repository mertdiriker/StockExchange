using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyerManager : MonoBehaviour
{
    public MarketItem[] marketItems;

    public DataManager dataManager;

    public GameObject marketPrefab;

    public Transform marketParent;

    public GameObject tempGO;

    public Text moneyField;
    // Start is called before the first frame update
    void Start()
    {
        string jsonString = dataManager.LoadMarketData();

        marketItems = JsonHelper.FromJson<MarketItem>(jsonString);

        for (int i = 0; i < marketItems.Length; i++)
        {
            tempGO = Instantiate(marketPrefab, marketParent);
            tempGO.GetComponent<MarketPrefab>().owner.text = marketItems[i].owner;
            tempGO.GetComponent<MarketPrefab>().price.text = marketItems[i].price.ToString() + " TL";
            tempGO.GetComponent<MarketPrefab>().amount.text = marketItems[i].amount.ToString() + " KG";
            tempGO.GetComponent<MarketPrefab>().productType.text = marketItems[i].productName;
            tempGO.GetComponent<MarketPrefab>().priceInt = marketItems[i].price;
            tempGO.GetComponent<MarketPrefab>().amountInt = marketItems[i].amount;

        }
    }

    public void UpdateMoneyField()
    {
        moneyField.text = dataManager.data.money.ToString() + " TL";

    }

    // Update is called once per frame
}
