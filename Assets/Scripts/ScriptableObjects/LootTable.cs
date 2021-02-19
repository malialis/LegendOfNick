using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public PowerUP thisLoot;
    public int lootChance;
}


[CreateAssetMenu(menuName = "Scriptable/Loot")]
public class LootTable : ScriptableObject
{

    public Loot[] loots;

    public PowerUP LootPowerUp()
    {
        int cumulativeProbability = 0;
        int currentProbability = Random.Range(0, 100);
        for (int i = 0; i < loots.Length; i++)
        {
            cumulativeProbability += loots[i].lootChance;
            if(currentProbability <= cumulativeProbability)
            {
                return loots[i].thisLoot;
            }
        }

        return null;
    }




}
