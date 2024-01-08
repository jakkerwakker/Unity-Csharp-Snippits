using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class StarterPlayerController : MonoBehaviour
{
    /**/

    [Range(0f, 8f)]
    public float walkSpeed = 5f;
    
    [Range(8f, 15f)]
    public float runSpeed = 8f;

    [Range(0f, 20f)]
    public float jumpForce = 10f;
    public float maxJumpHeight = 10f;

    public float gravityScale = 1;
    public float fallingGravityScale = 2;

    public bool isGrounded;

    public Vector3 initPosition;

    private Rigidbody rb;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    /*----------------------------------------------------------*/

    private void Awake()
    {
        
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }


    void Update()
    {
        EnableMovement();

        SweetJumpCapabilities();

    }

    /*---------------------------------------------------------*/

    //CUSTOM FUNCTIONS--

    void EnableMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;// + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += moveDir * runSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += moveDir * walkSpeed * Time.deltaTime;
            }
        }
    }

    void SweetJumpCapabilities()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        if (rb.velocity.y >= 1)
        {
            rb.AddForce(Physics.gravity * gravityScale * rb.mass);
        }
        else if (rb.velocity.y < 2)
        {
            rb.AddForce(Physics.gravity * fallingGravityScale * rb.mass);
        }
    }

    //--//

}///END OF PROGRAM///
