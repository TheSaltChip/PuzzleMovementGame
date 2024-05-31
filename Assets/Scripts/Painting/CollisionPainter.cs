#region License
// Copyright (c) 2021 Mix and Jam

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
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
