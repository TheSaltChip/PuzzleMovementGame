using System.IO;
using UnityEngine.Device;

namespace Constants
{
    public static class PlayerPrefsNames
    {
        public const string Turn = "turn";
        public const string TurnSpeed = "turnSpeed";
        public const string SnapTurnAngle = "snapTurnAngle";
    }

    public static class PathNames
    {
        public static readonly string Levels = Path.Combine(Application.dataPath, "Levels");
        public static readonly string Throwable = Path.Combine(Levels, "Throwable");
        public static readonly string CustomThrowable = Path.Combine(Throwable, "Custom");
    }
}