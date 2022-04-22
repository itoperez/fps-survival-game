using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private GameObject health_UI, stamina_UI;
    private Image health_Image, stamina_Image;

    private void Awake()
    {
        health_UI = GameObject.Find(UITags.PLAYER_HEALTH_UI);
        stamina_UI = GameObject.Find(UITags.PLAYER_STAMINA_UI);
        health_Image = health_UI.GetComponent<Image>();
        stamina_Image = stamina_UI.GetComponent<Image>();
    }

    public void Display_HealthStats(float healthValue)
    {
        healthValue /= 100f;

        health_Image.fillAmount = healthValue;
    }

    public void Display_StaminaStats(float staminaValue)
    {
        staminaValue /= 100f;

        stamina_Image.fillAmount = staminaValue;
    }

}
