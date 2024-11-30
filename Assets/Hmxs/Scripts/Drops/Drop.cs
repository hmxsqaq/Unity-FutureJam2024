using System;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Drop : MonoBehaviour
    {
        protected Rigidbody2D Rb { get; private set; }

        protected virtual void Start() => Rb = GetComponent<Rigidbody2D>();

        public virtual void DestroySelf() => Destroy(gameObject);
    }
}