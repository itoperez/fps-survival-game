using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public static EnemyStats instance;

    private Image cannibal_Health_Background, boar_Health_Background;
    private Image cannibal_Health_Icon, boar_Health_Icon;
    private Image cannibal_Health_Foreground, boar_Health_Foreground;

    public bool is_Current_Cannibal, is_Current_Boar;

    private void Awake()
    {
        MakeInstance();

        cannibal_Health_Background = GameObject.Find(UITags.CANNIBAL_HEALTH_BACKGROUND).GetComponent<Image>();
        boar_Health_Background = GameObject.Find(UITags.BOAR_HEALTH_BACKGROUND).GetComponent<Image>(); 

        cannibal_Health_Icon = GameObject.Find(UITags.CANNIBAL_HEALTH_ICON).GetComponent<Image>();
        boar_Health_Icon = GameObject.Find(UITags.BOAR_HEALTH_ICON).GetComponent<Image>();

        cannibal_Health_Foreground = GameObject.Find(UITags.CANNIBAL_HEALTH_FOREGROUND).GetComponent<Image>();
        boar_Health_Foreground = GameObject.Find(UITags.BOAR_HEALTH_FOREGROUND).GetComponent<Image>();

        is_Current_Cannibal = false;
        is_Current_Boar = false ;

        cannibal_Health_Background.enabled = false;
        cannibal_Health_Icon.enabled = false;
        cannibal_Health_Foreground.enabled = false;

        boar_Health_Background.enabled = false;
        boar_Health_Icon.enabled = false;
        boar_Health_Foreground.enabled = false;
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void TurnOnEnemyDisplayHealth(float enemyHealthValue, bool is_Cannibal, bool is_Boar)
    {
        if (!is_Current_Cannibal && !is_Current_Boar)
        {
            return;
        }

        enemyHealthValue /= 100f;

        if (is_Cannibal)
        {
            cannibal_Health_Background.enabled = true;
            cannibal_Health_Icon.enabled = true;
            cannibal_Health_Foreground.enabled = true;

            cannibal_Health_Foreground.fillAmount = enemyHealthValue;
        }
        if (is_Boar)
        {
            boar_Health_Background.enabled = true;
            boar_Health_Icon.enabled = true;
            boar_Health_Foreground.enabled = true;

            boar_Health_Foreground.fillAmount = enemyHealthValue;
        }
        
    }

    public void TurnOffEnemyDisplayHealth(bool is_Cannibal, bool is_Boar)
    {
        if (!is_Current_Cannibal && !is_Current_Boar)
        {
            return;
        }

        if (is_Cannibal)
        {
            cannibal_Health_Background.enabled = false;
            cannibal_Health_Icon.enabled = false;
            cannibal_Health_Foreground.enabled = false;
        }
        if (is_Boar)
        {
            boar_Health_Background.enabled = false;
            boar_Health_Icon.enabled = false;
            boar_Health_Foreground.enabled = false;
        }
    }

    public void SetCurrentEnemy(bool is_Cannibal)
    {
        if (is_Cannibal)
        {
            is_Current_Cannibal = true;
        } else
        {
            is_Current_Boar = true;
        }
    }

}
