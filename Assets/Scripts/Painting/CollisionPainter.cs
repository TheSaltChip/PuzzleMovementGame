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
using UnityEngine.Events;

public class CollisionPainter : MonoBehaviour
{
    [SerializeField] private Color paintColor;
    [SerializeField] private float strength = 1f;
    [SerializeField] private float hardness = 0.5f;
    [SerializeField] private float radius = 0.25f;
    public UnityEvent collided;

    private void OnCollisionEnter(Collision other)
    {
        Paintable p = other.collider.GetComponent<Paintable>();
        if (p != null)
        {
            print("collided");
            Vector3 pos = other.contacts[0].point;
            PaintManager.Instance.paint(p,pos,radius,hardness,strength,paintColor);
        }
        collided.Invoke();
    }

    public void SetColor(Color color)
    {
        paintColor = color;
    }
}
