using System;

namespace Variables
{
    [Serializable]
    public class IntReference
    {
        public bool UseConstant = true;
        public int ConstantValue;
        public IntVariable Variable;

        public int Value => UseConstant ? ConstantValue : Variable.value;
    }
}