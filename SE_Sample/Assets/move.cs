using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed = 3f;
    float moveX = 0f;
    float moveZ = 0f;
    Rigidbody rb;
    AudioSource audioSource;
    public AudioClip sound1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();


    }
    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * speed;
        moveZ = Input.GetAxis("Vertical") * speed;
        Vector3 direction = new Vector3(moveX, 0, moveZ);
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveX, 0, moveZ);
    }
    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(sound1);
    }
}
