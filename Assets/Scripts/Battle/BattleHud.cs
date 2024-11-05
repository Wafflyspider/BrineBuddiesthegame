using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;

    public void SetData(Creatures Creatures)
    {
        nameText.text = Creatures.Base.name;
        levelText.text = "Lvl" + Creatures.Level;
        hpBar.SetHP((float)Creatures.HP / Creatures.MaxHP);
    }
}
