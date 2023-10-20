using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class LevelConditionsPosition : MonoBehaviour, ILevelCondition
    {
        private bool m_ReachedZone;
        private bool m_Reached;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destructible PlayerShip = collision.transform.root.GetComponent<Destructible>();
            
            if (PlayerShip != null && PlayerShip.Nickname == "Player")
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    m_Reached = true;
                }
            }            
        }
        
        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (m_Reached == true)
                {
                    m_ReachedZone = true;
                }
                return m_ReachedZone;
            }
        }
    }
}
