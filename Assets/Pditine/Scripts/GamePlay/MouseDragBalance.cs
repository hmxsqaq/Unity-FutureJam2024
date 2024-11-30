// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_11_30
// // License: MIT
// // -------------------------------------------------

using System;
using Hmxs.Scripts;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pditine.Scripts.GamePlay
{
    public class MouseDragBalance : MonoBehaviour, IHasGravity
    {
        [SerializeField] private float mouseForce;
        [SerializeField] private Balance balance;
        private Vector3 _endPos;
        [SerializeField] private Transform ctrlPoint;
        [RO]private bool _isDragging;
        private Vector2 StartCanvasPoint => Utility.GetCanvasPosition(transform.position) + _offsetCanvas;
        private Vector2 _offsetWorld;
        private Vector2 _offsetCanvas;
        private void Start()
        {
            balance.AddObject(this);
        }

        private void Update()
        {
            Debug.DrawLine(ctrlPoint.position, _endPos, Color.red);
        }

        public (bool, float) GetGravityInfo(Balance balance)
        {
            if (!_isDragging) return (false, 0);
            Vector3 force = _endPos - ctrlPoint.position;
            float forceValue = -Vector3.Dot(force, balance.Normal) * mouseForce;
            float dic = transform.position.x - ctrlPoint.position.x;
            forceValue *= Mathf.Abs(dic);
            return (dic > 0, forceValue);
        }

        private void OnMouseDrag()
        {
            _endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Arrow.Instance.Set(ctrlPoint.position,
                StartCanvasPoint, Utility.GetMouseCanvasPosition());
        }

        private void OnMouseDown()
        {
            _isDragging = true;
            Arrow.Instance.Generate(Utility.GetMouseWorldPosition());
            _offsetCanvas = Utility.GetMouseCanvasPosition() - Utility.GetCanvasPosition(transform.position);
            ctrlPoint.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        private void OnMouseUp()
        {
            _isDragging = false;
            Arrow.Instance.Clear();
        }
    }
}