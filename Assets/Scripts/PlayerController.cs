using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement Component
    private CharacterController controller;
    private Animator animator;

    private float moveSpeed = 4f;

    [Header("Movement System")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;

    private float gravity = 9.81f;

    PlayerInteraction playerInteraction;
    // Start is called before the first frame update
    void Start()
    {
        //Get movement Components
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Interact();

        if (Input.GetKey(KeyCode.RightBracket))
        {
            TimeManager.Instance.Tick();
        }
    }

    public void Interact()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            playerInteraction.Interact();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            playerInteraction.ItemInteract();
        }
    }

    //
    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 velocity = moveSpeed * Time.deltaTime * dir;
        if (controller.isGrounded)
        {
            velocity.y = 0;
        }
        velocity.y -= Time.deltaTime * gravity;

        //Is the sprint key pressed down?
        if(Input.GetButton("Sprint"))
        {
            moveSpeed = runSpeed;
            animator.SetBool("Running", true);
        }
        else
        {
            moveSpeed = walkSpeed;
            animator.SetBool("Running", false);
        }

        if(dir.magnitude >= 0.1f)
        {
            //look towards that direction
            transform.rotation = Quaternion.LookRotation(dir);
            //Move
            controller.Move(velocity);

        }

        animator.SetFloat("Speed", dir.magnitude);
    }
}
