using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    public static int HealthCurrent;  
    public static int HealthMax;
    public TextMeshProUGUI TextMeshProUGUI;

   

    // Start is called before the first frame update
    void Start()
    {
        healthBar= GetComponent<Image>();   
        //HealthCurrent = HealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)HealthCurrent / (float)HealthMax;
        TextMeshProUGUI.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();
    }
}
