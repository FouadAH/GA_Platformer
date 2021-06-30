using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public TMP_Text gemText;
    public Image healthSprite;

    public GameObject healthParent;

    public void UpdateGem(int gemCount)
    {
        gemText.text = gemCount.ToString();
    }

    public void UpdateHealthUI(int changeAmount)
    {
        if(changeAmount < 0)
        {
            Destroy(healthParent.transform.GetChild(healthParent.transform.childCount - 1).gameObject);
        }
        else
        {
            Instantiate(healthSprite, healthParent.transform);
        }
    }
}
