using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupStats : Powerup
    {
        public enum EffectType
        {
            AddAmmo,
            AddEnergy,
            AddVelocity
        }

        [SerializeField] private EffectType m_EffectType;

        [SerializeField] private float m_Value;

        [SerializeField] private float TimeBonusActive;

        protected override void OnPickedUp(SpaceShip ship)
        {
            if (m_EffectType == EffectType.AddEnergy)
            {
                ship.AddEnergy((int)m_Value);
            }

            if (m_EffectType == EffectType.AddAmmo)
            {
                ship.AddAmmo((int)m_Value);
            }

            if (m_EffectType == EffectType.AddVelocity)
            {
                ship.AddVelocity((int)m_Value, TimeBonusActive);
            }
        }
    }
}
