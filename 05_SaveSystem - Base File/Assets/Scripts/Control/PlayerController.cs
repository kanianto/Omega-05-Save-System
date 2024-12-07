using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Movement;
using Game.Combat;
using Game.Attributes;

namespace Game.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if(health.IsDead()) return;
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
            print("nothing to do");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target  == null) continue;

                if(!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if(Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit whereToGo;
            bool hasHit = Physics.Raycast(GetMouseRay(), out whereToGo);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(whereToGo.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
