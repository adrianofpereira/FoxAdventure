using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private  Rigidbody2D rb2d;

    public Animator playerAnim;

    [SerializeField] private LayerMask groundMask;

    private float moveInput;

    public float moveSpeed;

    public float jumpForce;

    [SerializeField] private bool onGround;

    private bool wasOnGround;

    private bool isJump;

    //Ground Circle Colider

    private Collider2D[] colliders_1, colliders_2;

    private float groundCheckRadius = 0.036f; //tamanho do objeto que checa a colisao com o chao

    public Transform[] groundCheck; //posisao do objeto que chec a colisao com o chao
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem();
        checkGround();
        Animations();
    }

    private void FixedUpdate(){
        Move();
    }

    private void Move(){
        rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
    }

    private void InputSystem(){
        moveInput = Input.GetAxisRaw("Horizontal");

        //Virar player na direcao que esta andando---
        if(moveInput != 0f){
            transform.localScale = new Vector3(moveInput, 1f, 1f);
        }
        if(Input.GetKeyDown(KeyCode.Space) && onGround==true){
            Jump();
        }
    }
    void checkGround(){
        colliders_1 = Physics2D.OverlapCircleAll(groundCheck[0].position, groundCheckRadius, groundMask);
        colliders_2 = Physics2D.OverlapCircleAll(groundCheck[1].position, groundCheckRadius, groundMask);

        if(colliders_1.Length>0 || colliders_2.Length>0){
            onGround = true;
        }else{
        onGround = false;
        }
    }
    private void Jump(){
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }
    private void Animations(){
        playerAnim.SetFloat("Speedx", Mathf.Abs(moveInput));
        playerAnim.SetFloat("Speedy", rb2d.velocity.y);
        playerAnim.SetBool("onGround", onGround);

    }

}
