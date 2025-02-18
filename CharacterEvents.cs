using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CharacterEvents
{

    //Character Damaged and Damage Value
    public static UnityAction<GameObject, int> characterDamaged;
    //Character healed and amount healed
    public static UnityAction<GameObject, int> characterHealed;

    

}
