using System;
using UnityEngine;

namespace Additions.Utils
{
	[RequireComponent(typeof(Collider))]
	public class ColliderHandler : MonoBehaviour
	{
		public event Action<Collision> CollisionEnter;
		public event Action<Collision> CollisionExit;
		public event Action<Collision> CollisionStay;
		
		
		private void OnCollisionEnter (Collision collision)
		{
			CollisionEnter?.Invoke(collision);
		}

		private void OnCollisionExit (Collision other)
		{
			CollisionExit?.Invoke(other);
		}

		private void OnCollisionStay (Collision collisionInfo)
		{
			CollisionStay?.Invoke(collisionInfo);
		}
	}
}
