using System;
using System.Collections.Generic;
using Game.Interfaces;
using UnityEngine;

namespace Game.Weapons.Swords
{
    public class Sword : Weapon
    {
        [SerializeField] private Transform hitPoint;
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private float hitRadius;
        [SerializeField] private float slashAngle;
        [SerializeField] private float damage;

        public override void Attack()
        {
            List<Collider> hits = GetTargetsInRange();

            foreach (Collider hit in hits)
            {
                IHitable hitTarget = hit.GetComponent<IHitable>();

                if (hitTarget != null)
                {
                    hitTarget.Slash(hitPoint, damage);
                }
            }
        }

        public override void Reload()
        {
        }

        private void OnCollisionEnter(Collision collision)
        {
            throw new NotImplementedException();
        }

        private List<Collider> GetTargetsInRange()
        {
            Collider[] colliders = Physics.OverlapSphere(hitPoint.position, hitRadius, interactableLayer);
            List<Collider> hits = new List<Collider>();

            foreach (Collider item in colliders)
            {
                Vector3 directionToCollider = (item.transform.position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, directionToCollider);

                if (angle <= slashAngle * 0.5f)
                {
                    hits.Add(item);
                }
            }

            return hits;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, hitRadius);

            Vector3 rightBound = Quaternion.AngleAxis(slashAngle * 0.5f, transform.up) * transform.forward * hitRadius;
            Vector3 leftBound = Quaternion.AngleAxis(-slashAngle * 0.5f, transform.up) * transform.forward * hitRadius;

            Gizmos.DrawLine(transform.position, transform.position + rightBound);
            Gizmos.DrawLine(transform.position, transform.position + leftBound);
        }

#endif
    }
}