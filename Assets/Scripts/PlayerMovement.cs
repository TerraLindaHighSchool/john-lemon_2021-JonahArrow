using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public GameObject ScaryLemon;
    public Canvas canvas;
    public GameObject MainCamera;
    public GameObject Camera;
    public GameObject Static1;
    private float count;

    private bool isStatic = false;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;


    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        Camera.gameObject.SetActive(false);
        count = 1;
        Static1.gameObject.SetActive(false);
    }

    void Update()
    {
        if(isStatic)
        {
            count++;
            Static1.gameObject.SetActive(true);
        }
        if(count == 20)
        {
            isStatic = false;
            count = 1;
            MainCamera.gameObject.SetActive(true);
            Camera.gameObject.SetActive(false);
            Static1.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger1"))
        {
            MainCamera.gameObject.SetActive(false);
            Camera.gameObject.SetActive(true);
            isStatic = true;
            other.gameObject.SetActive(false);
        }
    }
}

