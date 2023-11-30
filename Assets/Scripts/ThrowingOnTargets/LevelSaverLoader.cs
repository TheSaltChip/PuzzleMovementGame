using System;
using System.IO;
using Constants;
using ThrowingOnTargets.Saveable;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace ThrowingOnTargets
{
    [ExecuteInEditMode]
    public class LevelSaverLoader : MonoBehaviour
    {
        [SerializeField] private GameObject parent;
        [SerializeField] private StagesSO stagesSO;
        [SerializeField] private IntVariable currentStageVariable;
        [SerializeField] private StringVariable levelName;

        [Space]
        
        [SerializeField, Min(1)] private int currentStage;
        [SerializeField] private bool save;
        [SerializeField] private bool load;

        public UnityEvent stageLoaded;

        private StagesSaveable _stagesSaveable;

        private StreamWriter _writer;
        private FileStream _fileStream;

        public void SaveStage()
        {
            var dest = Path.Combine(Application.dataPath, PathNames.Throwable, $"{levelName.value}.dat");

            var level = LoadLevel(dest);

            var pointTransforms = parent.GetComponentsInChildren<Transform>();

            var stage = new Stage
            {
                posRots = new PosRotScl[pointTransforms.Length - 1]
            };

            for (var i = 1; i < pointTransforms.Length; i++)
            {
                var t = pointTransforms[i];
                stage.posRots[i - 1].location = t.localPosition;
                stage.posRots[i - 1].rotation = t.localRotation.eulerAngles;
                stage.posRots[i - 1].scale = t.localScale;
            }

            if (currentStage - 1 >= level.stages.Length)
            {
                var temp = level.stages;

                level.stages = new Stage[currentStage];

                Array.Copy(temp, level.stages, temp.Length);
            }

            level.stages[currentStage-1] = stage;

            print(level);

            using var fileStream = File.Exists(dest)
                ? File.Open(dest, FileMode.Truncate, FileAccess.Write)
                : File.OpenWrite(dest);
            using var writer = new StreamWriter(fileStream);

            var buffer = JsonUtility.ToJson(level);

            print(buffer);

            writer.WriteLine(buffer);
        }

        private StagesSaveable LoadLevel(string dest)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dest)!);

            var level = new StagesSaveable
            {
                name = levelName.value,
                stages = new Stage[5]
            };

            if (!File.Exists(dest)) return level;

            using var fileStream = File.OpenRead(dest);
            using var fileReader = new StreamReader(fileStream);
            var readToEnd = fileReader.ReadToEnd();

            print(readToEnd);

            JsonUtility.FromJsonOverwrite(readToEnd, level);

            return level;
        }

        public void LoadStage()
        {
            var dest = Path.Combine(Application.dataPath, "ThrowableLevels", $"{levelName.value}.dat");

            Directory.CreateDirectory(Path.GetDirectoryName(dest)!);

            var stagesSaveable = new StagesSaveable
            {
                stages = new Stage[5]
            };

            if (!File.Exists(dest))
            {
                Debug.LogError("File not found");
                return;
            }

            using var filestream = File.OpenRead(dest);
            using var fileReader = new StreamReader(filestream);

            JsonUtility.FromJsonOverwrite(fileReader.ReadToEnd(), stagesSaveable);

            stagesSO.stages = stagesSaveable.stages;
            stagesSO.levelName = levelName.value;

            currentStageVariable.value = currentStage-1;
            
            stageLoaded.Invoke();
        }

        private void Update()
        {
            if (load)
            {
                LoadStage();
                load = false;
                return;
            }

            if (!save) return;

            save = false;
            SaveStage();
        }
    }
}