using UnityEngine;

namespace Additions.Utils
{
	public class RotationLocker : MonoBehaviour
	{
		[SerializeField] private Vector3 targetRotation;

		private void Update()
		{
			transform.eulerAngles = targetRotation;
		}
	}
}
