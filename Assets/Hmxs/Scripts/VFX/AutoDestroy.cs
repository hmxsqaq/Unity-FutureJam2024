using UnityEngine;

namespace Hmxs.Scripts.VFX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class AutoDestroy : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Start() => _particleSystem = GetComponent<ParticleSystem>();

        private void Update()
        {
            if (_particleSystem.isPlaying) return;
            Destroy(gameObject);
        }
    }
}