using UnityEngine;

public class PlayerComponent : MonoBehaviour
{

    [SerializeField] private float m_Speed = 1.0f;
    [SerializeField] private float m_RotationSpeed = 1.0f;
    [SerializeField] private float m_RotationDampening = 10.0f;
    [SerializeField] private float m_JumpForce = 1.0f;

    private Rigidbody m_RigidBody;
    private bool m_IsGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float l_deltaTime = Time.deltaTime;
        Vector3 l_originalPosition = gameObject.transform.position;
        Quaternion l_originalRotation = gameObject.transform.rotation;

        float l_desiredRotation = 0.0f;
        Quaternion l_desiredRotationQ = Quaternion.identity;
        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            l_desiredRotation = l_originalRotation.eulerAngles.y + (m_RotationSpeed);
            l_desiredRotationQ = Quaternion.Euler(l_originalRotation.eulerAngles.x, l_desiredRotation, l_originalRotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, l_desiredRotationQ, l_deltaTime * m_RotationDampening);
        }
        else if (Input.GetAxis("Horizontal") < 0.0f)
        {
            l_desiredRotation = l_originalRotation.eulerAngles.y + (m_RotationSpeed * -1.0f);
            l_desiredRotationQ = Quaternion.Euler(l_originalRotation.eulerAngles.x, l_desiredRotation, l_originalRotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, l_desiredRotationQ, l_deltaTime * m_RotationDampening);
        }



        if (Input.GetAxis("Vertical") > 0.0f)
        {
            m_RigidBody.MovePosition(l_originalPosition + (gameObject.transform.forward * m_Speed * l_deltaTime));
        }
        else if (Input.GetAxis("Vertical") < 0.0f)
        {
            m_RigidBody.MovePosition(l_originalPosition + (gameObject.transform.forward * m_Speed * l_deltaTime * -1.0f));
        }

        if (m_IsGrounded && Input.GetAxis("Jump") > 0.0f)
        {
            m_RigidBody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            m_IsGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.point.y <= transform.position.y)
                m_IsGrounded = true;

        }
    }
}

