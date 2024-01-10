// Attach to Camera object
// Attach Player object to Target(Transform) slot  
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
  // Lovely Variables
  [SerializeField] private Transform target;
  [SerializeField] private float height = 10f;
  [SerializeField] private float distance = 10f;
  [SerializeField] private float orbitSpeed = 5f;
  [SerializeField] private float verticalOrbitSpeed = 2.5f;
  [SerializeField] private float maxVerticalAngle = 80f;
  [SerializeField] private float minVerticalAngle = 5f;

  private Vector3 _offset;

  //----------------------------------------------//

  private void Awake()
  {
    // Calculate initial offset
    _offset = new Vector3(0, height, -distance);
  }

  private void Start()
  {
      if (!target)
      {
          Debug.LogError("Player Transform not set on CameraFollowController script!");
      }  
  }

   private void Update()
   {
       
   }

  private void LateUpdate()
  {
      FollowPlayer();
  }

    ///Lovely Functions///

    void FollowPlayer()
    {
        // Follow player position, maintaining offset
        transform.position = target.position + _offset; 
        
        // Horizontal orbit (left/right mouse movement)
        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * orbitSpeed);
            _offset = transform.position - target.position;
        }

        // Vertical orbit (up/down mouse movement)
        if (Input.GetAxis("Mouse Y") != 0)
        {
            // Calculate the current angle between the camera's position relative to the target and the up vector
            float angle = Vector3.Angle(transform.position - target.position, Vector3.up);

            // Determine whether the vertical movement is allowed based on the angle limits
            bool isIncreasingAngle = Input.GetAxis("Mouse Y") > 0 && angle < maxVerticalAngle;
            bool isDecreasingAngle = Input.GetAxis("Mouse Y") < 0 && angle > minVerticalAngle;

            if (isIncreasingAngle || isDecreasingAngle)
            {
                // Rotate around the target's right axis
                transform.RotateAround(target.position, transform.right, -Input.GetAxis("Mouse Y") * verticalOrbitSpeed);

                // Update the offset after rotation
                _offset = transform.position - target.position;
            }
        }

        transform.LookAt(target);
    }
}///--END-OF-PROGRAM--///
