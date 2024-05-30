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

using Tutorial;
using UnityEngine;

public class AnimationAndButtonSetUp : MonoBehaviour
{
    [SerializeField] private GameObject ControllerL;
    [SerializeField] private GameObject ControllerR;
    [SerializeField] private TutorialData tutorialData;

    private ControllerButtonSetUp l;
    private ControllerButtonSetUp r;

    private void Start()
    {
        l = ControllerL.GetComponent<ControllerButtonSetUp>();
        l.ActivateAnimationAndGlow();
        r = ControllerR.GetComponent<ControllerButtonSetUp>();
        r.ActivateAnimationAndGlow();
    }

    public void Activate()
    {
        l.ResetAnimationAndMaterial();
        r.ResetAnimationAndMaterial();
        
        switch (tutorialData.selectedHand)
        {
            case SelectedHand.Left:
                l.ActivateAnimationAndGlow();
                break;
            case SelectedHand.Right:
                r.ActivateAnimationAndGlow();
                break;
            case SelectedHand.Both:
            default:
                l.ActivateAnimationAndGlow();
                r.ActivateAnimationAndGlow();
                break;
        }
    }

    public void End()
    {
        ControllerL.SetActive(false);
        ControllerR.SetActive(false);
    }
}
