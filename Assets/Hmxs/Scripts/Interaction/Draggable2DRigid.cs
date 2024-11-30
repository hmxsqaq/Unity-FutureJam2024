using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class Draggable2DRigid : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D objectRb;
        [SerializeField] private AnimationCurve dragForceCurve;
        [SerializeField] private AnimationCurve dragImpulseCurve;

        private Vector2 StartWorldPoint => (Vector2)transform.position + Utility.RotateVector(_offsetWorld, transform.eulerAngles.z - _startAngle);
        private Vector2 StartCanvasPoint => Utility.GetCanvasPosition(StartWorldPoint);

        private Vector2 _offsetWorld;
        private float _startAngle;
        private bool _isDragging;
        private Vector2 _dragDirection;
        private float _dragDistance;

        private void Start()
        {
            if (objectRb == null) objectRb = GetComponent<Rigidbody2D>();
            if (objectRb != null) return;
            Debug.LogError("Rigidbody2D not found in Draggable2DRigid component or in the GameObject.");
            enabled = false;
        }

        private void OnMouseDown()
        {
            var worldPoint = Utility.GetMouseWorldPosition();
            _offsetWorld = worldPoint - (Vector2)transform.position;
            _startAngle = transform.eulerAngles.z;
            _isDragging = true;
            Arrow.Instance.Generate(worldPoint);
        }

        private void OnMouseUp()
        {
            _isDragging = false;
            Arrow.Instance.Clear();
            var dragImpulse = dragImpulseCurve.Evaluate(_dragDistance);
            objectRb.AddForce(_dragDirection * dragImpulse, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            if (!_isDragging) return;
            Arrow.Instance.Set(StartWorldPoint, StartCanvasPoint, Utility.GetMouseCanvasPosition());
            var worldPoint = Utility.GetMouseWorldPosition();
            _dragDistance = Vector2.Distance(StartWorldPoint, worldPoint);
            _dragDirection = (worldPoint - StartWorldPoint).normalized;
            var dragForce = dragForceCurve.Evaluate(_dragDistance);
            objectRb.AddForce(dragForce * _dragDirection, ForceMode2D.Force);
        }

        private void OnDestroy() => Arrow.Instance.Clear();

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