using System;

namespace Variables
{
    [Serializable]
    public class IntReference
    {
        public bool UseConstant = true;
        public int ConstantValue;
        public IntVariable Variable;

        public int Value
        {
            get =>
                UseConstant ? ConstantValue : Variable.value;
            set

            {
                if (UseConstant)
                {
                    ConstantValue = value;
                    return;
                }

                Variable.value = value;
            }
        }
    }
}