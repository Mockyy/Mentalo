using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("State")]
    public PlayerState state;

    [SerializeField]
    public bool canMove;    //Si le joueur peut se déplacer

    [Header("Jump settings")]
    [SerializeField]
    private float jumpForce = 5f;   //Force du saut par défaut
    [SerializeField]
    private float gravity = -9.81f; //Gravité
    [Tooltip("Max height of the jump")]
    [SerializeField]
    private float maxJump = 10f;    //Hauteur max du saut
    private bool isGrounded;    //Si le joueur est au sol
    private float jumpHeight;   //Force du saut calculé


    [Header("Movement settings")]
    [SerializeField]
    private float speed = 10f;  //Vitesse   

    private Vector3 velocity;   //Velocité
    protected float horizontal; //Direction horizontale
    protected float vertical;   //Direction verticale
    protected Vector3 direction;

    [Header("Sounds settings")]
    [SerializeField]
    private GameObject soundJump = default;   //Son de saut

    protected CharacterController cc;  

    //Animation variables
    private Animator animator;  //Animation

    //Camera variables
    private Transform cam;  //Camera

    private PlayerPush PlayerPush;  //Script de poussée
    protected bool isPushing;   //Si le joueur est en train de pousser quelque chose

    private ParticleSystem dust;    //Particules d'attérissage

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
        PlayerPush = GetComponent<PlayerPush>();
        dust = GetComponent<ParticleSystem>();
        state = GetComponent<PlayerState>();

        canMove = true;
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
            if (Input.GetButton("Jump") && canMove)
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
            if (Input.GetButtonUp("Jump") && canMove)
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
        if (canMove)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }

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
