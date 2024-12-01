// -------------------------------------------------
// Copyright@ LiJianhao
// Author : LiJianhao
// Date: 2024_11_30
// License: MIT
// -------------------------------------------------

using System.Collections.Generic;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace Pditine.Scripts.GamePlay
{
    public class Balance : MonoBehaviour
    {
        [SerializeField] private Transform board;
        [Header("平衡系数,当天平倾斜时的回复速度")]
        [SerializeField] private float balanceCoeff;
        [Header("平衡阈值,当重力差小于此值时认为平衡")]
        [SerializeField] private float balanceThreshold;
        [Header("空气阻力")]
        [SerializeField] private float airResistance;
        [Header("最大重力差")]
        [SerializeField] private float forceLimit;
        private readonly HashSet<IHasGravity> _objects = new();
        private readonly Dictionary<IHasGravity, float> _removeBuffer = new();
        private Collider2D _theCollider;
        [Inspectable]private float _force; // 左正右负
        [Inspectable]private float _forceAdd;
        [Inspectable]private float _speed;
        [Inspectable]private float _angle; // -90~90

        private void Start()
        {
            _theCollider = GetComponent<Collider2D>();
        }

        public float Angle
        {
            set => _angle = Mathf.Clamp(value, -90, 90);
            get => _angle;
        }

        public Vector2 Normal => board.transform.up;
        public void Update()
        {
            Debug.DrawLine(board.position, board.position + (Vector3)Normal*10, Color.red);
            UpdateRemoveBuffer();
            UpdateForce();
            UpdateSpeed();
            UpdateAngle();
            ApplyAngle();
        }
        
        /// <summary>
        /// 用于向天平注册持续施加重力的物体
        /// </summary>
        public void AddObject(IHasGravity obj)
        {
            _objects.Add(obj);
            if(_removeBuffer.ContainsKey(obj))
                _removeBuffer.Remove(obj);
        }
        
        public void RemoveObject(IHasGravity obj)
        {
            _removeBuffer[obj] = 0.1f;
        }
        
        /// <summary>
        /// 用于向天平施加瞬间的力
        /// </summary>
        public void AddForce(bool isLeft, float force)
        {
            force*= 50;
            if (isLeft)
            {
                _forceAdd += force;
            }
            else
            {
                _forceAdd -= force;
            }
        }
        
        private void UpdateRemoveBuffer()
        {
            var keysToRemove = new List<IHasGravity>();
            var keysToUpdate = new List<IHasGravity>();
            
            foreach (var kv in _removeBuffer)
            {
                if (kv.Value < 0)
                {
                    keysToRemove.Add(kv.Key);
                }
                else
                {
                    keysToUpdate.Add(kv.Key);
                }
            }

            foreach (var key in keysToUpdate)
            {
                _removeBuffer[key] -= Time.deltaTime;
            }

            foreach (var key in keysToRemove)
            {
                _objects.Remove(key);
                _removeBuffer.Remove(key);
            }
        }
        
        /// <summary>
        /// 1.更新重力，核心思想是天平内部不关心物体的重力如何计算，而是由外部提供
        /// 预期的重力提供者包括但不限于：天平上的物体，环境, 拖拽
        /// </summary>
        private void UpdateForce()
        {
            foreach (var obj in _objects)
            {
                var (isLeft, force) = obj.GetGravityInfo(this);
                if (isLeft)
                {
                    _force += force;
                }
                else
                {
                    _force -= force;
                }
            }
            if (Mathf.Abs(_force) < balanceThreshold)
            {
                _force = 0;
            }else
            {
                _force = _force > 0 ? _force - balanceThreshold : _force + balanceThreshold;
            }
            _force *= 0.5f;
            _force = Mathf.Clamp(_force, -forceLimit, forceLimit);
            _force += _forceAdd;
            _forceAdd = 0;
        }
        
        /// <summary>
        /// 2.通过左右重力差及当前角度计算加速度,并更新速度
        /// </summary>
        private void UpdateSpeed()
        {
            float acceleration;
            if (Mathf.Abs(_force) < balanceThreshold)
            {
                _speed = 0;
                acceleration = 0;
            }
            else
            {
                acceleration = 10 * _force;
            }
            acceleration += -_angle * Mathf.Abs(_angle) * balanceCoeff * 0.1f - Mathf.Sign(_angle) * 10;
            _speed += acceleration * Time.deltaTime;
            _speed *= Mathf.Clamp(1 - airResistance * Time.deltaTime, 0, 1);
        }
        
        /// <summary>
        /// 3.通过当前速度更新角度
        /// </summary>
        private void UpdateAngle()
        {
            float delta = _speed * Time.deltaTime;
            delta = Mathf.Clamp(delta, -1,1);
            _angle += delta;
            _angle = Mathf.Clamp(_angle, -90, 90);
        }

        /// <summary>
        /// 4.应用角度
        /// </summary>
        private void ApplyAngle()
        {
            //非常好代码,使我的天平旋转,爱来自插值
            //board.localEulerAngles = Vector3.Lerp(board.localEulerAngles, new Vector3(0, 0, _angle), 0.1f); 
            board.localEulerAngles = new Vector3(0, 0, _angle);
        }
    }
}