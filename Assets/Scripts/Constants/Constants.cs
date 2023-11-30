using System.IO;

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
        public const string Levels = "Levels";

        public static readonly string Throwable = Path.Combine(Levels, "Throwable");
    }
}