using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float minAnimationTime = 2.0f;
    [SerializeField] float maxAnimationTime = 5.0f;
    float animationTime = 0;
    float time = 0;
    Vector3 moveDirection;

    Animator animator;
    bool isIdle = true;

	void Start () {
        animator = GetComponent<Animator>();
        SetAnimationTime();
    }
	
	void FixedUpdate () {
        time += Time.deltaTime;

        if (time >= animationTime) // Change animation
        {
            
            time = 0;
            SetAnimationTime();

            if (isIdle)
            {
                SetDirection();
                animator.SetTrigger("Walk");
                transform.Rotate(moveDirection);
            }
            else
            {
                animator.SetTrigger("Idle");
            }
            isIdle = !isIdle;
        }

        if(!isIdle)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
	}

    void SetAnimationTime()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        animationTime = Random.Range(minAnimationTime, maxAnimationTime);
    } 

    void SetDirection()
    {
        moveDirection = new Vector3(0, Random.value * 360, 0);
    }
}
