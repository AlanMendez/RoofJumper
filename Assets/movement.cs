﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class movement : MonoBehaviour
{
    /*
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    
    }

    private void Update()
    {
        float verticalInput = Input.GetAxis(verticalInputName) * movementSpeed * Time.deltaTime;
        float horizontalInput = Input.GetAxis(horizontalInputName) * movementSpeed * Time.deltaTime;
        Vector3 forwardMovement = transform.forward * verticalInput;
        Vector3 rightMovement = transform.right * horizontalInput;

        characterController.SimpleMove(forwardMovement + rightMovement);

    }
    */
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rayCastDistance;
    private Rigidbody rb;
    public Text countText;
    public Text winString;
    private int count;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        jump();
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontalAxis, 0, verticalAxis) * speed;
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(newPosition);
    }
    private void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
        }
    }
    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayCastDistance, Color.blue);
        return(Physics.Raycast(transform.position, Vector3.down, rayCastDistance));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickUps"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
    void SetCountText()
    {
        float theTime = 3;
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winString.text = "You Win!!!!!";
            StartCoroutine(ExecuteAfterTime(theTime));
        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
