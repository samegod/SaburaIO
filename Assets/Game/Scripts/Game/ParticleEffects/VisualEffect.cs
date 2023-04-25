using Additions.Pool;
using UnityEngine;

namespace Game.ParticleEffects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class VisualEffect : MonoBehaviourPoolObject
    {
        [SerializeField, HideInInspector] private ParticleSystem particles;
        
        public override void OnPop()
        {
            base.OnPop();
            
            particles.Play();
        }

        public override void Push()
        {
            ParticleEffectsPool.Instance.Push(this);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            particles = GetComponent<ParticleSystem>();
        }
#endif
    }
}
