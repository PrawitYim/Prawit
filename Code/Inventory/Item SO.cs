using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributesToChange attributeToChange = new AttributesToChange();
    public int amountToChangeAttrinut;


    public bool UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            PlayerHP playerHealth = GameObject.Find("HealthManager").GetComponent<PlayerHP>();
            if(playerHealth.health == playerHealth.maxHealth)
            {
                return false;
            }
            else
            {
                playerHealth.RestoreHealth(amountToChangeStat);
                return true;

            }

        }
        return false;
    }

    public enum StatToChange
    {
        none,
        health,
        mana,
        stamina,

    };

    public enum AttributesToChange
    {
        none,
        Str,
        def,
        agi,

    };
}
