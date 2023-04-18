using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public Vector3 jump;
    public bool onGround;
    public int numJumps;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    public Rigidbody rb;
    private int count;
    public float movementX;
    public float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        jump = new Vector3(0.0f, 5.0f, 0.0f);
        onGround = true;
        numJumps = 0;
    }

    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if (count >= 12) {
            winTextObject.gameObject.SetActive(true);
        }
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && numJumps < 2) {
            rb.AddForce(jump * 2.0f, ForceMode.Impulse);
            onGround = false;
            numJumps += 1;
        } 
        if (transform.position.y == 0) {
            Debug.Log("Y position is 0");
        } 
    }

    public void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Ground")) {
            numJumps = 0;
            onGround = true;
        }
    }

}