using System;

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