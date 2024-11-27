using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

namespace Game.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 20f;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints =  Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0)
            {
                Die();
            }
            print(healthPoints);
        }

        private void Die()
        {
            if(isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
