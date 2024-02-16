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

        public static void SetFromPosRotScl(this Transform transform, PosRotScl posRotScl)
        {
            transform.position = posRotScl.position;
            transform.localEulerAngles = posRotScl.rotation;
            transform.localScale = posRotScl.scale;
        }
    }
}