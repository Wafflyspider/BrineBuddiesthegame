using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName= "Creature/Create new Creature")]
public class CreatureBase : ScriptableObject
{
    [SerializeField] string creaturename;

    [TextArea]
    [SerializeField]  string description;

    [SerializeField]  Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField]  CreatureType type1;
    [SerializeField]  CreatureType type2;

    // Base Stats
    [SerializeField]  int maxHP;
    [SerializeField] int attack;
    [SerializeField]  int defense;
    [SerializeField]  int spAttack;
    [SerializeField]  int spDefense;
    [SerializeField]  int speed;

    [SerializeField] List<LearnableMoves> learnableMoves;

     public string creaturenameName {
        get { return creaturename; }
    }

    public string Description{
        get {return description; }
    }

        public Sprite FrontSprite {
        get {return frontSprite; }
    }

         public Sprite BackSprite {
        get {return backSprite; }
    }

          public CreatureType Type1{
        get {return type1; }
    }

          public CreatureType Type2{
        get {return type2; }
    }

          public int MaxHP {
        get {return maxHP; }
    }

       public int Attack {
        get{return attack; }
    }
     public int Defense {
       get {return defense; }
    }

     public int SpAttack {
        get {return spAttack; }
    }

     public int SpDefense {
        get {return spDefense; }
    }

     public int Speed {
       get {return speed; }
    }

    public List<LearnableMoves> LearnableMoves{
        get {return learnableMoves; }
    }

}

[System.Serializable]
public class LearnableMoves
{
    [SerializeField] Moveset moveSet;
    [SerializeField] int level;

      public Moveset Base{
        get {return moveSet; }
    }

     public int Level {
       get {return level; }
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