using UnityEngine;

namespace Game.Input
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Game.Character.Character character;

        private const string attackButton = "attack";
        
        private void Update()
        {
            float horizontal = SimpleInput.GetAxis("Horizontal");
            float vertical = SimpleInput.GetAxis("Vertical");
            
            if (horizontal != 0 || vertical != 0)
            {
                Vector3 axis = new Vector3(horizontal, 0, vertical);
                Debug.Log(axis);
                character.Move(axis);
            }

            if (SimpleInput.GetButtonUp(attackButton))
            {
                character.Attack();
            }
        }
    }
}
