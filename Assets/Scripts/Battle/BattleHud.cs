using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;

    public void SetData(Creatures creatures)
    {
        nameText.text = creatures.Base.name;
        levelText.text = "Lvl" + creatures.Level;
        hpBar.SetHP((float)creatures.HP / creatures.MaxHP);
    }
}
