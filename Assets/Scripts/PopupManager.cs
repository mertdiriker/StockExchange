using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    public GameObject popupObject;

    public Text popupText;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    // Update is called once per frame
    public void ShowText(string targetText)
    {
        popupObject.SetActive(true);
        popupText.text = targetText;
    }
}
