using System;
using Hmxs.Toolkit.Plugins.Fungus.FungusTools;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Drop : MonoBehaviour
    {
        public string dropName;
        public Rigidbody2D Rb { get; private set; }

        protected virtual void Start() => Rb = GetComponent<Rigidbody2D>();

        public virtual void DestroySelf() => Destroy(gameObject);

        private void OnMouseDown()
        {
            switch (dropName)
            {
                case "Shark": FlowchartManager.ExecuteBlock("Click_Shark"); break;
                case "Earth": FlowchartManager.ExecuteBlock("Click_Earth"); break;
                case "Train": FlowchartManager.ExecuteBlock("Click_Train"); break;
                case "Brick": FlowchartManager.ExecuteBlock("Click_Brick"); break;
                case "Skull": FlowchartManager.ExecuteBlock("Click_Skull"); break;
                case "Heart": FlowchartManager.ExecuteBlock("Click_Heart"); break;
                case "Strawberry": FlowchartManager.ExecuteBlock("Click_Strawberry"); break;
                case "Poise": FlowchartManager.ExecuteBlock("Click_Poise"); break;
                case "TV": FlowchartManager.ExecuteBlock("Click_TV"); break;
                case "Whale": FlowchartManager.ExecuteBlock("Click_Whale"); break;
            }
        }
    }
}