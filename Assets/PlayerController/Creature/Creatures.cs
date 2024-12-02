using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creatures
{
    public CreatureBase Base { get; set; }
    public int Level { get; set; }

    public int HP { get; set; }

    public List<Move> Moves { get; set; }

    public Creatures(CreatureBase pBase, int pLevel)
    {
        Base = pBase;
        Level = pLevel;
        HP = MaxHP;

        //Generate Moves
        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
                Moves.Add(new Move(move.Base));

                if (Moves.Count >= 4)
                    break;
        }
    }

    public int Attack {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5;}
    }

     public int Defense {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5;}
    }

     public int SpAttack {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5;}
    }

     public int SpDefense {
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5;}
    }

     public int Speed {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5;}
    }

     public int MaxHP {
        get { return Mathf.FloorToInt((Base.MaxHP * Level) / 100f) + 10;}
    }

   public bool TakeDamage(Move move, Creatures attacker)
{
    float modifiers = Random.Range(0.85f, 1f);
    float a = (2f * attacker.Level + 10) / 250f;
    float d = a * move.Base.Power * ((float)attacker.Attack / (Defense > 0 ? Defense : 1)) + 2;
    int damage = Mathf.FloorToInt(d * modifiers);

    HP -= damage;
    if (HP <= 0)
    {
        HP = 0;
        return true; // Creature has fainted
    }

    return false;
}

public Move GetRandomMove()
{
    if (Moves == null || Moves.Count == 0)
    {
        Debug.LogWarning("No moves available to select.");
        return null; // Or handle this case as appropriate in your game
    }

    int r = Random.Range(0, Moves.Count);
    return Moves[r];
}
}
