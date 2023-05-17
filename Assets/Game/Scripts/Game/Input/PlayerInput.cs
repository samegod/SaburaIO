using UnityEngine;

namespace Game.Input
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private CharacterBase.Character character;
        [SerializeField] private float multiplier;
        [SerializeField] private float minStep;
        
        [SerializeField] private Vector2 _axis;
        
        private const string attackButton = "attack";
        
        private void Update()
        {
            float horizontal = SimpleInput.GetAxis("Horizontal");
            float vertical = SimpleInput.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                _axis = new Vector2(horizontal, vertical);
                MoveCharacter(horizontal, vertical);
            }
            else if (_axis != Vector2.zero)
            {
                if (Mathf.Abs(_axis.x) + Mathf.Abs(_axis.y) <= minStep )
                {
                    _axis = Vector2.zero;
                }
                
                _axis = Vector2.Lerp(_axis, Vector2.zero, Time.deltaTime * multiplier);
                MoveCharacter(_axis.x, _axis.y);
            }

            if (SimpleInput.GetButton(attackButton) || UnityEngine.Input.GetKey(KeyCode.Space))
            {
                character.Attack();
            }
        }

        private void MoveCharacter(float horizontal, float vertical)
        {
            Vector3 axis = new Vector3(horizontal, 0, vertical);
            
            if (axis.sqrMagnitude > Mathf.Epsilon)
            {
                float speed = Mathf.Sqrt(axis.x * axis.x + axis.z * axis.z);
                character.Move(axis, speed);
                _axis = new Vector2(horizontal, vertical);
            }
        }
    }
}
