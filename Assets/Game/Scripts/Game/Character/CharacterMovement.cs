using UnityEngine;

namespace Game.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float movementSpeed;
        [SerializeField, HideInInspector] private CharacterController characterController;

        private Vector3 _targetLookRotation;
        private Camera _camera;

        private void Awake() =>
            _camera = Camera.main;

        public void Move(Vector3 axis)
        {
            var movementVector = Vector3.zero;

            if (axis.sqrMagnitude > Mathf.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(axis);
                movementVector.y = 0;
                movementVector.Normalize();

                _targetLookRotation = movementVector;
            }

            movementVector += Physics.gravity;

            characterController.Move(movementVector * (movementSpeed * Time.deltaTime));
        }

        private void Update()
        {
            if (_targetLookRotation != transform.localEulerAngles)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_targetLookRotation);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            }
        }

        #region Editor

        private void OnValidate()
        {
            if (characterController == null)
                characterController = GetComponent<CharacterController>();
        }

        #endregion
    }
}