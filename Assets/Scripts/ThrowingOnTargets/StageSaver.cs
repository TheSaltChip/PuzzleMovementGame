using ThrowingOnTargets.ScriptableObjects;
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
        }

        private void Update()
        {
            if (!save) return;

            save = false;
            SaveStage();
        }
    }
}