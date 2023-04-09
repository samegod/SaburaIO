using System;
using UnityEngine;

namespace Test
{
    public class TestSlashingVisual : MonoBehaviour
    {
        [SerializeField] private float gizmosLinesLength;
        [SerializeField] private float bulletGizmosLength;
        [SerializeField] private Transform target;
        [SerializeField] private Transform targetPointer;

        [SerializeField] private float angle;
        [SerializeField] private float rotatedAngle;
        

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {

            Vector3 forward = Quaternion.AngleAxis(0, transform.up) * transform.forward * gizmosLinesLength;
            Vector3 right = Quaternion.AngleAxis(-90, transform.up) * transform.forward * gizmosLinesLength;
            Vector3 left = Quaternion.AngleAxis(90, transform.up) * transform.forward * gizmosLinesLength;
            Vector3 back = Quaternion.AngleAxis(180, transform.up) * transform.forward * gizmosLinesLength;
            
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, forward + transform.position);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, right + transform.position);
            Gizmos.DrawLine(transform.position, left + transform.position);
            Gizmos.DrawLine(transform.position, back + transform.position);
            
            CalculateAngle();
            DrawBulletGizmos();
        }
        
        private void CalculateAngle()
        {
            Vector3 newDirection = transform.InverseTransformPoint(target.position);
            angle = Quaternion.LookRotation(newDirection).eulerAngles.y;

            
            if (angle > 180)
            {
                angle -= 180;
            }

            if (angle > 90)
            {
                angle = 180 - angle;
            }
            
        }

        private void DrawBulletGizmos()
        {
            Gizmos.color = Color.green;
            
            Vector3 forward = Quaternion.AngleAxis(angle, target.up) * target.forward * bulletGizmosLength;
            Gizmos.DrawLine(target.position, forward + target.position);
            
            Gizmos.color = Color.red;
            
            forward = Quaternion.AngleAxis(rotatedAngle, target.up) * target.forward * bulletGizmosLength;
            Gizmos.DrawLine(target.position, forward + target.position);
        }
#endif
    }
}
