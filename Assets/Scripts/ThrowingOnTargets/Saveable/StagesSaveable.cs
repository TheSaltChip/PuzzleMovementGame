using System;
using ThrowingOnTargets.ScriptableObjects;
using Variables;

namespace ThrowingOnTargets.Saveable
{
    [Serializable]
    public struct StagesSaveable
    {
        public string name;
        public Stage[] stages;

        public override string ToString()
        {
            return $"{nameof(name)}: {name}, {nameof(stages)}: {stages}";
        }
    }
}