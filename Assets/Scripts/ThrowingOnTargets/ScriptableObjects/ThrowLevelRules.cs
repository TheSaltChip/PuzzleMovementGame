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

using UnityEngine;
using Variables;

namespace ThrowingOnTargets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ThrowLevelRules", menuName = "ThrowAtTargets/ThrowLevelRules")]
    public class ThrowLevelRules : ScriptableObject
    {
        public IntVariable stagesVariable;
        public IntVariable targetsPerStageVariable;
        public IntVariable xSizeVariable;
        public IntVariable ySizeVariable;
        public FloatVariable chanceBigTargetVariable;
        public FloatVariable distBetweenStagesVariable;

        public int Stages
        {
            get => stagesVariable.value;
            set => stagesVariable.value = value;
        }

        public int TargetsPerStage
        {
            get => targetsPerStageVariable.value;
            set => targetsPerStageVariable.value = value;
        }

        public int XSize
        {
            get => xSizeVariable.value;
            set => xSizeVariable.value = value;
        }

        public int YSize
        {
            get => ySizeVariable.value;
            set => ySizeVariable.value = value;
        }

        public float ChanceBigTarget
        {
            get => chanceBigTargetVariable.value;
            set => chanceBigTargetVariable.value = value;
        }

        public float DistBetweenStages
        {
            get => distBetweenStagesVariable.value;
            set => distBetweenStagesVariable.value = value;
        }
    }
}