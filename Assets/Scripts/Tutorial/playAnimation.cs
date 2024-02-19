using UnityEngine;

public class playAnimation : MonoBehaviour
{
    private Animation anim;
    
    void Start()
    {
        anim = this.GetComponent<Animation>();
        anim.Play("Cylinder");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
