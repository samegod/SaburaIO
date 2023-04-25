using UnityEngine;

namespace Game.ParticleEffects
{
    public class VisualEffectsContainer : MonoBehaviour, IVisualEffectsContainer
    {
        [SerializeField] private VisualEffect effect;

        public VisualEffect PopEffect()
        {
            return ParticleEffectsPool.Instance.Pop(effect);
        }
    }
}
