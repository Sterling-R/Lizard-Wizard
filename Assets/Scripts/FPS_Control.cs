using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPS_Control : MonoBehaviour
{

    [SerializeField] float speed = 5.0f;
    
    [SerializeField] float m_lookSensitivity = 3.0f;

    private float m_MovX;
    private float m_MovY;
    private Vector3 m_moveHorizontal;
    private Vector3 m_movVertical;
    private Vector3 m_velocity;
    private Rigidbody m_Rigid;
    private float m_yRot;
    private float m_xRot;
    private Vector3 m_rotation;
    private Vector3 m_cameraRotation;
    public Camera m_Camera;

    // Use this for initialization
    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if(!gameObject.GetComponent<PlayerController>().isDead)
        {
            m_MovX = Input.GetAxis("Horizontal");
            m_MovY = Input.GetAxis("Vertical");

            m_moveHorizontal = transform.right * m_MovX;
            m_movVertical = transform.forward * m_MovY;

            m_velocity = (m_moveHorizontal + m_movVertical).normalized * speed;

            //mouse movement 
            m_yRot = Input.GetAxisRaw("Mouse X");
            m_rotation = new Vector3(0, m_yRot, 0) * m_lookSensitivity;

            m_xRot = Input.GetAxisRaw("Mouse Y");
            m_cameraRotation = new Vector3(m_xRot, 0, 0) * m_lookSensitivity;

            //apply camera rotation

            //move the actual player here
            if (m_velocity != Vector3.zero)
            {
                m_Rigid.MovePosition(m_Rigid.position + m_velocity * Time.fixedDeltaTime);
            }

            if (m_rotation != Vector3.zero)
            {
                //rotate the camera of the player
                m_Rigid.MoveRotation(m_Rigid.rotation * Quaternion.Euler(m_rotation));
            }

            if (m_Camera != null)
            {
                //negate this value so it rotates like a FPS
                m_Camera.transform.Rotate(-m_cameraRotation);

                //clamp the camera
                if(m_Camera.transform.localRotation.x > 0.69 || m_Camera.transform.localRotation.x < -0.69)
                {
                    m_Camera.transform.Rotate(m_cameraRotation);
                }
            }
        }

    }
}
