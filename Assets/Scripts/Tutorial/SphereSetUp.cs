using Lobby;
using UnityEngine;

namespace Tutorial
{
    public class SphereSetUp : MonoBehaviour
    {
        [SerializeField] private TutorialData data;
        [SerializeField] private GameObject sphere;
        
        public void ShowSphere()
        {
            sphere.SetActive(data.button == VRControllerButtons.Grip);
            print("Active");
        }
    }
}