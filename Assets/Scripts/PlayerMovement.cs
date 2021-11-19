using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class PlayerMovement : MonoBehaviour
{

    Animator animator; 

    bool alive = true;

    public bool isGrounded = false;

    public float speed = 5;
    [SerializeField] Rigidbody rb;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    public float speedIncreasePerPoint = 0.1f;

    public Vector3 jump;
    public float jumpForce = 2.0f;

    


    private void Start()
    {
        animator = GetComponent<Animator>(); 
    }
    private void FixedUpdate()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);

       
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (transform.position.y < -5)
        {
            Die();
        }

        if (Input.GetKeyDown("space")&& isGrounded == true)
        {
            //Vector3 forwardJump= transform.up * speed * Time.fixedDeltaTime;
            Debug.Log("Jump");
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            animator.SetBool("IsJumping", true); 
        }
    }


    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Plane")
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
    }

    //consider when character is jumping .. it will exit collision.
    void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Plane")
        {
            isGrounded = false;
            animator.SetBool("IsJumping", true);
        }
    }

    public void Die()
    {
        alive = false;
        // Restart the game
        Invoke("Restart", 1);

        animator.SetBool("IsDead", true);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

  


}