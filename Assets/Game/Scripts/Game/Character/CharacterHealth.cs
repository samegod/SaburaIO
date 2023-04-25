using UnityEngine;

namespace Game.Character
{
    public class CharacterHealth
    {
        private Character _thisCharacter;
        private float _currentHealth;

        public float CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public Character ThisCharacter
        {
            get => _thisCharacter;
            set => _thisCharacter = value;
        }

        public void TakeDamage(float damage)
        {
            Debug.Log("damage taken: " + damage);
            CurrentHealth -= damage;

            if (CurrentHealth <= 0f)
            {
                ThisCharacter.Die();
            }
        }
    }
}
