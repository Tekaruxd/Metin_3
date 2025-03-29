using UnityEngine;

public class attack_script : MonoBehaviour
{
    public Animator Sword_Animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Attack()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Sword_Animator.SetTrigger("Swing");
            Debug.Log("whooosh");
        }

    }
}
