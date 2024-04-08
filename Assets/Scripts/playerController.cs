using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    
    private float movementX;
    private float movementY;
    public float speed = 10;

    private int jumpCount;
    public float jumpForce = 5f;

    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        count = 0;
        jumpCount = 2;
        SetCountText();
        winTextObject.SetActive(false);

    }


    void OnMove(InputValue movementValue) {

        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    
    }

    void OnJump() {
        if (jumpCount > 1)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount--;
        }
    }

    // Call this method when the player touches the ground
    void ResetJumpCount()
    {
        jumpCount = 2; // Reset jumpCount to initial value
    }


    private void FixedUpdate() {

        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (transform.position.y <= 0.5f) {
            jumpCount = 2;
        }
    }

   void OnTriggerEnter (Collider other) 
   {
       if (other.gameObject.CompareTag("PickUp")) 
       {
           other.gameObject.SetActive(false);
           count += 1;
           SetCountText();
       }
   }

    void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();

        if (count >= 8)
        {
            winTextObject.SetActive(true);
        }
   }
}
