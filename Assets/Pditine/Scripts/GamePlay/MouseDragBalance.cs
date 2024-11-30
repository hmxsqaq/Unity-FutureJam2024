// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_11_30
// // License: MIT
// // -------------------------------------------------

using System;
using PurpleFlowerCore;
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
        private bool _isDragging;
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
            _isDragging = true;
        }

        private void OnMouseDown()
        {
            PFCLog.Info(123);
            ctrlPoint.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        private void OnMouseUp()
        {
            _isDragging = false;
        }
    }
}