using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] CreatureBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public Creatures Creatures { get; set; }
    public void Setup()
    {
        Creatures = new Creatures(_base, level);
        if (isPlayerUnit)
            GetComponent<Image>().sprite = Creatures.Base.BackSprite;
        else 
            GetComponent<Image>().sprite = Creatures.Base.FrontSprite;
    }
}
