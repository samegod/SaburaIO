using UnityEngine;

namespace Game.CharacterBase
{
    public class CharacterRigPoints : MonoBehaviour
    {
        [SerializeField] private Transform meleeRightHand;
        [SerializeField] private Transform firearmRightHand;

        public Transform MeleeRightHand => meleeRightHand;
        public Transform FirearmRightHand => firearmRightHand;
    }
}
