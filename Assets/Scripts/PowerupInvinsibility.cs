using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupInvinsibility : Powerup
    {
        [SerializeField] private float TimeBonusActive;

        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.Invincibility(TimeBonusActive);
        }
    }
}
