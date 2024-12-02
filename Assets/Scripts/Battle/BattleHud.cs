using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
[SerializeField] Text nameText;
[SerializeField] Text levelText;
[SerializeField] HPBar hpBar;

Creatures _creatures;

public void SetData(Creatures creatures)
{
    if (creatures == null)
    {
        Debug.LogWarning("Creatures data is null. Cannot set data.");
        return;
    }

    _creatures = creatures;

    if (nameText != null)
        nameText.text = _creatures.Base.name;
    else
        Debug.LogWarning("nameText is not assigned.");

    if (levelText != null)
        levelText.text = $"Lvl {_creatures.Level}";
    else
        Debug.LogWarning("levelText is not assigned.");

    if (hpBar != null)
        hpBar.SetHP((float)_creatures.HP / _creatures.MaxHP);
    else
        Debug.LogWarning("hpBar is not assigned.");
}

public IEnumerator UpdateHP()
{
    if (hpBar == null || _creatures == null)
    {
        Debug.LogWarning("hpBar or _creatures is not assigned. Cannot update HP.");
        yield break;
    }

    yield return hpBar.SetHPSmooth((float)_creatures.HP / _creatures.MaxHP);
}
}
