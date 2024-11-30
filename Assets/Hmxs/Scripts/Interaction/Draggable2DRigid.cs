using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs.Scripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Draggable2DRigid : MonoBehaviour
    {
        // [MinMaxSlider(0, 10, true)]
        // [SerializeField] private Vector2 dragImpulseRange;
        // [MinMaxSlider(0, 10, true)]
        // [SerializeField] private Vector2 dragForceRange;
        // [MinMaxSlider(0, 5, true)]
        // [SerializeField] private Vector2 dragDistanceRange;

        [SerializeField] private AnimationCurve dragForceCurve;
        [SerializeField] private AnimationCurve dragImpulseCurve;

        private Vector2 StartWorldPoint => (Vector2)transform.position + _offsetWorld;
        private Vector2 StartCanvasPoint => Utility.GetCanvasPosition(transform.position) + _offsetCanvas;

        private Vector2 _offsetWorld;
        private Vector2 _offsetCanvas;
        private bool _isDragging;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _dragDirection;
        private float _dragDistance;

        private void Start() => _rigidbody2D = GetComponent<Rigidbody2D>();

        private void OnMouseDown()
        {
            var worldPoint = Utility.GetMouseWorldPosition();
            _offsetWorld = worldPoint - (Vector2)transform.position;
            var canvasPoint = Utility.GetMouseCanvasPosition();
            _offsetCanvas = canvasPoint - Utility.GetCanvasPosition(transform.position);
            _isDragging = true;
            Arrow.Instance.Generate(worldPoint);
        }

        private void OnMouseUp()
        {
            _isDragging = false;
            Arrow.Instance.Clear();
            var dragImpulse = dragImpulseCurve.Evaluate(_dragDistance);
            _rigidbody2D.AddForce(_dragDirection * dragImpulse, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            if (_isDragging)
            {
                Arrow.Instance.Set(StartWorldPoint, StartCanvasPoint, Utility.GetMouseCanvasPosition());
                var worldPoint = Utility.GetMouseWorldPosition();
                _dragDistance = Vector2.Distance(StartWorldPoint, worldPoint);
                _dragDirection = (worldPoint - StartWorldPoint).normalized;
                var dragForce = dragForceCurve.Evaluate(_dragDistance);
                _rigidbody2D.AddForce(dragForce * _dragDirection, ForceMode2D.Force);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(100, 0, 0, 0.5f);
            Gizmos.DrawWireSphere(transform.position, dragForceCurve.keys[0].time);
            Gizmos.DrawWireSphere(transform.position, dragForceCurve.keys[dragForceCurve.length - 1].time);
            Gizmos.color = new Color(0, 100, 0, 0.5f);
            Gizmos.DrawWireSphere(transform.position, dragImpulseCurve.keys[0].time);
            Gizmos.DrawWireSphere(transform.position, dragImpulseCurve.keys[dragImpulseCurve.length - 1].time);
        }
    }
}