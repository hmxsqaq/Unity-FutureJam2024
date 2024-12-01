// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_12_1
// // License: MIT
// // -------------------------------------------------

using PurpleFlowerCore;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.Scripts.GamePlay
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Text TimeText;
        private float _time;
        public float Time => _time;
        public bool isOn;
        
        private void FixedUpdate()
        {
            if(!isOn) return;
            _time += UnityEngine.Time.deltaTime;
            SetTime();
        }

        private void SetTime()
        {
            int time = (int) _time;
            TimeText.text = $"{time / 60:D2}:{time % 60:D2}";
        }
    }
}