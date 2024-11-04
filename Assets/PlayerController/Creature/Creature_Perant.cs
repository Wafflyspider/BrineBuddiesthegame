using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName= "Creature/Create new Creature")]
public class CreatureParent : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;

    [SerializeField] private CreatureType type1;
    [SerializeField] private CreatureType type2;

    // Base Stats
    [SerializeField] private int maxHP;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField] private int spAttack;
    [SerializeField] private int spDefense;
    [SerializeField] private int speed;

    public string Name {
        get { return name; }
    }

    public string description{
        return {return description; }
    }

        public Sprite frontSprite{
        return {return frontSprite; }
    }

          public Sprite backSprite{
        return {return backSprite; }
    }

          public CreatureType type1{
        return {return type1; }
    }

          public CreatureType type2{
        return {return type2; }
    }

          public int MaxHP {
        return {return maxHP; }
    }

       public int attack {
        return {return attack; }
    }
     public int defense {
        return {return defense; }
    }

     public int spAttack {
        return {return spAttack; }
    }

     public int spDefense {
        return {return spDefense; }
    }

     public int speed {
        return {return speed; }
    }
}

public enum CreatureType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon,
    Fairy
}