using System;
using UnityEngine;

namespace Game.ParticleEffects
{
    public class ParticleEffectsContainer : MonoBehaviour
    {
        public static ParticleEffectsContainer Instance;

        [SerializeField] private ParticleEffect effect;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public ParticleEffect PopEffect()
        {
            return ParticleEffectsPool.Instance.Pop(effect);
        }
    }
}
