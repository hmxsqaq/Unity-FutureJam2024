// -------------------------------------------------
// Copyright@ LiJianhao
// Author : LiJianhao
// Date: 2024_11_30
// License: MIT
// -------------------------------------------------

namespace Pditine.Scripts.GamePlay
{
    /// <summary>
    /// 用于可以为天平提供重力的物体
    /// </summary>
    public interface IHasGravity
    {
        /// <summary>
        /// 获得重力信息
        /// </summary>
        /// <returns>isLeft, gravity</returns>
        public (bool, float) GetGravityInfo(Balance balance);
    }
}