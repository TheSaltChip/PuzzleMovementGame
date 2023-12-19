using UnityEngine;

public class ActiveColorTracker : MonoBehaviour
{
    [SerializeField] private Color _color;
    
    public void ChangeActive(Color c)
    {
        gameObject.SetActive(_color.Equals(c));
    }
}