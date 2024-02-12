using UnityEngine;

namespace Util.PRS 
{
    public static class PosRotSclUtil
    {
        public static PosRotScl ToPosRotScl(this Transform transform)
        {
            return new PosRotScl
            {
                position = transform.position,
                rotation = transform.localEulerAngles,
                scale = transform.localScale
            };
        }
    }
}