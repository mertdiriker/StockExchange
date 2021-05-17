using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("Parent Menu Objects")] 
    public GameObject LoginPage;
    public GameObject RegisterPage;
    public GameObject InformationPage;
    
    [Header("Login Page")]
    [SerializeField] private InputField username;
    [SerializeField] private InputField password;

    [Header("Register Page")] 
    [SerializeField] private InputField rUsername;
    [SerializeField] private InputField rPassword;
    [SerializeField] private InputField uname;
    [SerializeField] private InputField surname;
    [SerializeField] private InputField idNo;
    [SerializeField] private InputField address;
    [SerializeField] private InputField phone;
    [SerializeField] private InputField email;
    [SerializeField] private Toggle isAdmin;
    [SerializeField] private Toggle isSeller;

    [Header("Information Page")] 
    [SerializeField] private Text nameSurnameField;
    [SerializeField] private Text roleField;
    [SerializeField] private Text moneyField;
    [SerializeField] private GameObject buyerParent;
    [SerializeField] private GameObject adminParent;
    [SerializeField] private GameObject sellerParent;


    private void Awake()
    {
        if (PlayerPrefs.GetString("Admin", "0") == "0")
        {
            PlayerPrefs.SetString("Admin","123");
            RegisterAdminAccount();
        }
    }

    public DataManager dataManager;

    // Start is called before the first frame upda
    public void Login()
    {
        if (PlayerPrefs.GetString(username.text, "0") != "0")
        {
            if (password.text == PlayerPrefs.GetString(username.text))
            {
                //Login Success
                PopupManager.Instance.ShowText("Giriş Başarılı!");
                dataManager.user = username.text;
                dataManager.Load();
                nameSurnameField.text = dataManager.data.name + " " + dataManager.data.surname;
                if (dataManager.data.isAdmin == 1)
                {
                    roleField.text = "Admin";
                    adminParent.SetActive(true);
                }
                else
                {
                    if (dataManager.data.isSeller == 1)
                    {
                        roleField.text = "Satıcı";
                        sellerParent.SetActive(true);
                    }
                    else
                    {
                        roleField.text = "Alıcı";
                        buyerParent.SetActive(true);
                        moneyField.text = dataManager.data.money.ToString()+"TL";
                        if (dataManager.data.isMoneyApproved == 0)
                        {
                            moneyField.text += "TL ONAYLANMADI";
                        }
                    }
                }
                InformationPage.SetActive(true);
                LoginPage.SetActive(false);
                RegisterPage.SetActive(false);
            }
            else
            {
                PopupManager.Instance.ShowText("Giriş Başarısız!");
                //Login Fail
            }
        }
        else
        {
            //Login Fail
            PopupManager.Instance.ShowText("Giriş Başarısız!");
        }
    }

    public void Register()
    {
        dataManager.user = rUsername.text;
        PlayerPrefs.SetString(rUsername.text,rPassword.text);
        dataManager.Load();
        dataManager.data.name = uname.text;
        dataManager.data.username = rUsername.text;
        dataManager.data.address = address.text;
        dataManager.data.surname = surname.text;
        dataManager.data.password = rPassword.text;
        dataManager.data.idNo = idNo.text;
        dataManager.data.phoneNo = phone.text;
        dataManager.data.email = email.text;
        dataManager.data.isMoneyApproved = 0;
        
        if (isAdmin.isOn)
        {
            dataManager.data.isAdmin = 1;
            dataManager.data.isSeller = 0;
        }
        else
        {
            dataManager.data.isAdmin = 0;
            if (isSeller.isOn)
            {
                dataManager.data.isSeller = 1;
            }
            else
            {
                dataManager.data.isSeller = 0;
            }
        }
        
        dataManager.Save();
        PopupManager.Instance.ShowText("Kayıt tamamlandı!");
    }

    public void RegisterAdminAccount()
    {
        dataManager.user = "Admin";
        dataManager.Load();
        dataManager.data.name = "Mert";
        dataManager.data.surname = "Diriker";
        dataManager.data.address = "Filler";
        dataManager.data.password = "123";
        dataManager.data.idNo = "Filler";
        dataManager.data.phoneNo = "1235646";
        dataManager.data.email = "Filler";
        dataManager.data.isMoneyApproved = 0;
        dataManager.data.isAdmin = 1;
        dataManager.data.isSeller = 0;
        dataManager.Save();
    }
}
