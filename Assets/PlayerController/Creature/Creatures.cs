using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creatures
{
  CreaturesBase _base;
  int level;

  public Creatures(CreaturesBase pBase, int pLevel)
  {
    _base = pBase;
    level= pLevel;
  }

  public int Attack {
    get {return Mathf.FloorToInt((_base.Attack *  level) / 100f) + 5; }
  }
  public int defense {
    get {return Mathf.FloorToInt((_base.defense *  level) / 100f) + 5; }
  }

  public int Attack {
    get {return Mathf.FloorToInt((_base.Attack *  level) / 100f) + 5; }
  }

  public int spAttackAttack {
    get {return Mathf.FloorToInt((_base.spAttack *  level) / 100f) + 5; }
  }

  public int spDefense {
    get {return Mathf.FloorToInt((_base.spDefense *  level) / 100f) + 5; }
  }

  public int speed {
    get {return Mathf.FloorToInt((_base.speed *  level) / 100f) + 5; }
  }
  public int maxHP {
    get {return Mathf.FloorToInt((_base.maxHP *  level) / 100f) + 10; }
  }
}
