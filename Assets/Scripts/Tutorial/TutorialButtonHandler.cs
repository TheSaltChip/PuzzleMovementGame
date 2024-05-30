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

namespace Tutorial
{
    public class TutorialButtonHandler : MonoBehaviour
    {
        [SerializeField] private GameObject prev;
        [SerializeField] private GameObject next;
        [SerializeField] private GameObject start;
        [SerializeField] private GameObject end;

        public void First()
        {
            next.SetActive(false);
            prev.SetActive(false);
            start.SetActive(true);
        }

        public void Last()
        {
            next.SetActive(false);
            prev.SetActive(true);
            end.SetActive(true);
        }

        public void StartTut()
        {
            next.SetActive(true);
            prev.SetActive(true);
            start.SetActive(false);
        }

        public void End()
        {
            prev.SetActive(false);
            end.SetActive(false);
        }

        public void Middle()
        {
            if(!prev.activeSelf)
                prev.SetActive(true);
            if(!next.activeSelf)
                next.SetActive(true);
            if(start.activeSelf)
                start.SetActive(false);
            if(end.activeSelf)
                end.SetActive(false);
        }
    }
}
