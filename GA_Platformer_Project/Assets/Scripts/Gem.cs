using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerController>().gemCount+= value;
            collision.GetComponent<PlayerController>().UpdateGemCount();
            LevelManager.instance.RemoveGem(this);
            Destroy(gameObject);
        }
    }
}
