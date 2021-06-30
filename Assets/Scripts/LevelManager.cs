using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Gem gemPrefab;
    public List<Gem> gemsSpawned = new List<Gem>();
    public List<Transform> gemPositions = new List<Transform>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        foreach (Transform gemPosition in gemPositions)
        {
            Gem spawnedGem = Instantiate(gemPrefab, gemPosition.position, gemPosition.rotation);
            gemsSpawned.Add(spawnedGem);
        }
    }

    public void RemoveGem(Gem currentGem)
    {
        int i = 0;

        foreach (Gem gem in gemsSpawned)
        {
            if (gem.Equals(currentGem))
            {
                gemsSpawned.RemoveAt(i);
                gemPositions.RemoveAt(i);
                break;
            }

            i++;
        }
    }
}
