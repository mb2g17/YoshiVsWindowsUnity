using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes Yoshi keep running at the final boss
/// </summary>
public class YoshiRunner : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Run", true);
    }
}
