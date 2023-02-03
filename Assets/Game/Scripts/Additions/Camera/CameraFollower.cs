using UnityEngine;

namespace Game.Scripts.Additions.Camera
{
	public class CameraFollower : MonoBehaviour
	{
		[SerializeField] private Transform target;
		[SerializeField] private float rotationAngleX;
		[SerializeField] private float distance;
		[SerializeField] private float offsetY;

		private void LateUpdate()
		{
			if (target == null) return;

			var rotation = Quaternion.Euler(rotationAngleX, 0, 0);
			var position = rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();

			transform.rotation = rotation;
			transform.position = position;
		}

		public void Follow(GameObject following) =>
			target = following.transform;

		private Vector3 FollowingPointPosition()
		{
			var followingPosition = target.position;
			followingPosition.y += offsetY;

			return followingPosition;
		}
	}
}
