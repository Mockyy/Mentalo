using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("State")]
    public PlayerState state;

    [Header("Jump settings")]
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float gravity = -9.81f;
    [Tooltip("Max height of the jump")]
    [SerializeField]
    private float maxJump = 10f;
    private bool isGrounded;
    private float jumpHeight;


    [Header("Movement settings")]
    [SerializeField]
    private float speed = 10f;

    private Vector3 velocity;
    protected float horizontal;
    protected float vertical;
    protected Vector3 direction;

    [Header("Sounds settings")]
    [SerializeField]
    private GameObject soundJump;

    protected CharacterController cc;

    private float turnSmoothVelocity;

    //Animation variables
    private Animator animator;

    //Camera variables
    private Transform cam;

    private PlayerPush PlayerPush;
    protected bool isPushing;

    private ParticleSystem dust;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
        PlayerPush = GetComponent<PlayerPush>();
        dust = GetComponent<ParticleSystem>();
        state = GetComponent<PlayerState>();

        jumpHeight = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        state.actualState = PlayerState.state.idle;
        //On applique la gravité
        velocity.y -= gravity * Time.deltaTime;

        //On bouge le player avec la gravité
        cc.Move(velocity * Time.deltaTime);

        if (cc.isGrounded)
        {
            //Si le player touche le sol, on l'y maintient
            velocity.y = -2f;

            //Quand le joueur appuie sur la touche de saut
            if (Input.GetButton("Jump"))
            {
                state.actualState = PlayerState.state.jumping;
                //On incrémente la hauteur du saut tant qu'il maintient
                jumpHeight += Time.deltaTime * 10;

                //Si la hauteur du saut est trop grande
                if (jumpHeight >= maxJump)
                {
                    //On la plafonne
                    jumpHeight = maxJump;
                }
            }
            
            //Quand on relache la touche de saut
            if (Input.GetButtonUp("Jump"))
            {
                //Joue le son de saut
                soundJump.GetComponent<AudioSource>().Play();
                //Joue les particules de saut
                dust.Play();
                //Saut
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * -gravity);
                //Réassignation de la hauteur de saut à sa valeur de base
                jumpHeight = jumpForce;
            }
        }
        else
        {
            
        }

        //Inputs de mouvement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //On multiplie la direction par la vitesse
        direction = new Vector3(horizontal, 0.0f, vertical);
        direction *= speed;

        //La direction du player prend celle de la camera
        direction = cam.TransformDirection(direction);
        direction.y = 0;

        //On déplace le player une première fois pour le mouvement
        cc.Move(direction * Time.deltaTime);

        //On anime le player selon l'input
        animator.SetFloat("Speed", direction.magnitude);

        //Rotation du player selon l'input
        if (direction.magnitude > 0)
        {
            state.actualState = PlayerState.state.moving;
            Quaternion newRota = Quaternion.LookRotation(direction);
            newRota.x = 0f;     //On fixe la rotation en x et z
            newRota.z = 0f;     //pour que seule l'axe y rotate
            transform.rotation = newRota;
        }
    }
}
