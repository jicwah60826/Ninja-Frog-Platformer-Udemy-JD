using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager instance;

    public int collectibleCount,
        extraLifeThreshold;

    private void Awake()
    {
        instance = this;
    }

    public void GetCollectible(int amount)
    {
        collectibleCount += amount;
        UIManager.instance.UpdateFruitCountUI(collectibleCount);
        if (collectibleCount >= LifeManager.instance.extraLifeThreshold)
        {
            // reduce collectibles
            collectibleCount -= extraLifeThreshold;
            // add life to player
            LifeManager.instance.AddLife();
        }
    }
}
