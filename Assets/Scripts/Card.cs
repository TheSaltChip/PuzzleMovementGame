using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private string suit;
        [SerializeField] private int number;
        [SerializeField] private int id;

        public void Flip()
        {
            CardManager.Instance.Compare(suit,number,id);
        }

        public void Deactivate(int[] ids)
        {
            if (id == ids[0] || id == ids[1])
            {
                this.GameObject().SetActive(false);
            }
        }
    }
}