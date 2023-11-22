using System;
using System.IO;
using ThrowingOnTargets.ScriptableObjects;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

namespace ThrowingOnTargets
{
    [ExecuteInEditMode]
    public class StageSaver : MonoBehaviour
    {
        [SerializeField] private GameObject parent;
        [SerializeField] private StagesSO stages;

        [SerializeField] private bool save;
        [SerializeField] private int stageNumber;

        private StreamWriter _writer;
        private FileStream _fileStream;

        private void SaveStage()
        {
            var pointTransforms = parent.GetComponentsInChildren<Transform>();

            if (stageNumber < 1 || stageNumber > stages.stages.Length)
            {
                Debug.LogError($"{stageNumber} is not valid range is 1 - {stages.stages.Length}");
                return;
            }
            
            var stage = stages.stages[stageNumber - 1];
            
            stage.posRots = new PosRotScl[pointTransforms.Length - 1];

            for (var i = 1; i < pointTransforms.Length; i++)
            {
                var t = pointTransforms[i];
                stage.posRots[i - 1].location = t.localPosition;
                stage.posRots[i - 1].rotation = t.localRotation.eulerAngles;
                stage.posRots[i - 1].scale = t.localScale;
            }

            var dest = Application.dataPath + "/Stages/" + "test.dat";

            print(dest);
            
            _fileStream = File.Exists(dest) ? File.OpenWrite(dest) : File.Create(dest);
            
            print(stages);
            
            var data = JsonUtility.ToJson(stages);
            JsonUtility.FromJsonOverwrite(data, stages);
            
            print(stages);

            _writer = new StreamWriter(_fileStream);
            
            _writer.Write(data);
            _writer.Close();
            _fileStream.Close();
        }

        private void Update()
        {
            if (!save) return;

            save = false;
            SaveStage();
        }
    }
}