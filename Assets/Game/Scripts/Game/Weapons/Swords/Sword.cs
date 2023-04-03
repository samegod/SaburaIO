using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons.Swords
{
    public class Sword : Weapon
    {
        [SerializeField] private Transform hitPoint;
        [SerializeField] private LayerMask interactableLayer; // Set this to the layer you created earlier
        [SerializeField] private float hitRadius; // Set this to the radius of the sword slash area
        [SerializeField] private float slashAngle; // Set this to the maximum angle for the sword slash

        public override void Attack()
        {
            Collider[] colliders = Physics.OverlapSphere(hitPoint.position, hitRadius, interactableLayer);

            List<Collider> hits = new List<Collider>();

            // Filter the results based on the angle between the sword's forward direction and the direction to each hit object
            foreach (Collider collider in colliders)
            {
                Vector3 directionToCollider = (collider.transform.position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, directionToCollider);

                if (angle <= slashAngle * 0.5f)
                {
                    hits.Add(collider);
                }
            }

            // Iterate through the hits and perform your desired action on each object
            foreach (Collider hit in hits)
            {
                Debug.Log("Sword slashed: " + hit.gameObject.name); // Replace this with your desired action
            }
        }

        public override void Reload()
        {
            throw new System.NotImplementedException();
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            // Draw the sphere
            Gizmos.DrawWireSphere(transform.position, hitRadius);

            // Draw the bounds
            Vector3 rightBound = Quaternion.AngleAxis(slashAngle * 0.5f, transform.up) * transform.forward * hitRadius;
            Vector3 leftBound = Quaternion.AngleAxis(-slashAngle * 0.5f, transform.up) * transform.forward * hitRadius;

            Gizmos.DrawLine(transform.position, transform.position + rightBound);
            Gizmos.DrawLine(transform.position, transform.position + leftBound);
        }

#endif
    }
}