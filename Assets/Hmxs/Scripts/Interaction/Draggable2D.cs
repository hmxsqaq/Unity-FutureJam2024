using System;
using UnityEngine;

namespace Hmxs.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class Draggable2D : MonoBehaviour
    {
        [SerializeField] private Transform entity;

        private Vector2 _offset;
        private bool _isDragging;

        private void OnMouseDown()
        {
            _offset = (Vector2)entity.position - Utility.Instance.GetMouseWorldPosition();
            _isDragging = true;
        }

        private void OnMouseUp()
        {
            _isDragging = false;
        }

        private void Update()
        {
            if (_isDragging) entity.position = Utility.Instance.GetMouseWorldPosition() + _offset;
        }
    }
}