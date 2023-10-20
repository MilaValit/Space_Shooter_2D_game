using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Explosion : MonoBehaviour
    {
        public void ActivateExplosion()
        {
            ParticleSystem exp = GetComponent<ParticleSystem>();
            exp.Play();
            Destroy(gameObject, exp.main.duration);
        }
    }
}
