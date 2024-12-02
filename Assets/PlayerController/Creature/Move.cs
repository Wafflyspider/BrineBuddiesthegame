using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
   public Moveset Base  { get; set; }
   public int PP { get; set; }
   public  Move(Moveset pBase)
   {
    Base = pBase;
    PP = pBase.PP;
   }
}
