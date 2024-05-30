#region License
// Copyright (C) 2024 Sebastian Misje Jonassen & Mathias Nupen
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Commons Clause License version 1.0 with GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Commons Clause License and GNU General Public License for more details.
// 
// You should have received a copy of the Commons Clause License and GNU General Public License
// along with this program.  If not, see <https://commonsclause.com/> and <https://www.gnu.org/licenses/>.
#endregion

using System;
using System.IO;
using Constants;
using ThrowingOnTargets.Saveable;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Util.PRS;
using Variables;

namespace ThrowingOnTargets
{
    [ExecuteInEditMode]
    public class LevelSaverLoader : MonoBehaviour
    {
        [SerializeField] private GameObject parent;
        [SerializeField] private ThrowLevelSO throwLevelSO;
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
                stage.posRots[i - 1].position = t.localPosition;
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

            throwLevelSO.stages = stagesSaveable.stages;
            throwLevelSO.levelName = levelName.value;

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