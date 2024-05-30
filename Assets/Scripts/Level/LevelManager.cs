﻿#region License
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

using Compass;
using Completables;
using NaughtyAttributes;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private bool noTask;
        [SerializeReference, HideIf("noTask")] private Completable task;

        public static LevelManager Instance { get; private set; }

        private GameObject[] _pointsOfInterest;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning(
                    $"Invalid configuration. Duplicate Instances found! First one: {Instance.name} Second one: {name}. Destroying second one.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }


        private void Start()
        {
            _pointsOfInterest = GameObject.FindGameObjectsWithTag("Interest");
            CompassSystem.Instance.SetTarget(_pointsOfInterest[0].transform);
        }

        private void Update()
        {
            if (!noTask && task.IsDone)
            {
                print("Done");
            }
        }
    }
}