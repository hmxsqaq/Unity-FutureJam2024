using System;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    [RequireComponent(typeof(Collider2D))]
    public class AngryBird : Drop
    {
        [SerializeField] private Sprite triggeredSprite;
        [SerializeField] private GameObject destroyVfx;

        private bool _isTriggered;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_isTriggered && other.gameObject.TryGetComponent(out Drop drop))
            {
                drop.DestroySelf();
                _isTriggered = true;
                GetComponent<SpriteRenderer>().sprite = triggeredSprite;
                Instantiate(destroyVfx, other.transform.position, Quaternion.identity);
            }
        }
    }
}