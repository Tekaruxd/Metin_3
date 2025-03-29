using UnityEngine;

public class bloks_test_script : MonoBehaviour
{
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("animate", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void animate()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("animate");
    }
}
