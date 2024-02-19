using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Lobby
{
    public class DebugLogg : MonoBehaviour
    {
        [SerializeField] private TMP_Text debugText;

        private Dictionary<string, string> debugLogs = new();
        
        private void OnEnable()
        {
            Application.logMessageReceived += HandleException;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleException;
        }

        private void HandleException(string condition, string stacktrace, LogType type)
        {
            if (type is LogType.Exception or LogType.Log)
            {
                var splitString = condition.Split(':');
                var debugKey = splitString[0];
                var debugValue = splitString.Length > 1 ? splitString[1] + "\n" + stacktrace : "";

                debugLogs[debugKey] = debugValue;
            }

            var sb = new StringBuilder();

            foreach (var log in debugLogs)
            {
                sb.AppendLine(log.Value == "" ? log.Key : $"{log.Key}: {log.Value}");
            }

            debugText.text = sb.ToString();
        }
    }
}