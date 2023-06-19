using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : PowerupEffects
{
    public float speedAmount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerMovement>().speed += speedAmount;
        
    }
}
