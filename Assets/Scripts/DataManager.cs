using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public UserData data;
    
    // Start is called before the first frame update
    public string file = "userdata.txt";
    
    public string marketFile = "marketFile.txt";

    public string user;
    
    string confirmationPath = "Assets/OnayBekleyenKullanicilar.txt";
    string itemConfirmationPath = "Assets/ItemOnayBekleyenKullanicilar.txt";

    public Text userMoneyField;


    public void Save()
    {
        SetUser();
        
        string json = JsonUtility.ToJson(data);
        
        WriteToFile(file,json);
    }

    public void SaveMarketData(string json)
    {
        WriteToFile(marketFile,json);
    }

    public string LoadMarketData()
    {
        return ReadFromFile(marketFile);
    }

    public void Load()
    {
        SetUser();

        data = new UserData();
        
        string json = ReadFromFile(file);
        
        JsonUtility.FromJsonOverwrite(json,data);
    }

    public void SetUser()
    {
        file = user + ".txt";
    }

    private void WriteToFile(string fileName,string json)
    {
        SetUser();
        
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string ReadFromFile(string fileName)
    {
        SetUser();
        
        string path = GetFilePath(fileName);

        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.Log("File not found!");
        }

        return "";
    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    public void SendConfirmationForMoney(Text moneyField)
    {
        userMoneyField.text = moneyField.text + "TL ONAYLANMADI";
        PopupManager.Instance.ShowText("Onay İsteği Yollandı!");
        data.money = Int32.Parse(moneyField.text);
        Save();
        
        File.AppendAllText(confirmationPath, 
            user + Environment.NewLine);
    }
    
    public void SendConfirmationForItem()
    {
        PopupManager.Instance.ShowText("Onay İsteği Yollandı!");
        File.AppendAllText(itemConfirmationPath, 
            user + Environment.NewLine);
    }

}
