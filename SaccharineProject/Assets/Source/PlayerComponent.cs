using System.Linq;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{

    [SerializeField] private float m_Speed = 1.0f;
    [SerializeField] private float m_RotationSpeed = 1.0f;
    [SerializeField] private float m_AirRotationSpeed = 1.0f;
    [SerializeField] private float m_RotationDampening = 10.0f;
    [SerializeField] private float m_JumpForce = 1.0f;


    private bool m_IsGrounded = false;
    [SerializeField]
    private GameObject m_CurrentWhale;


    [SerializeField] private Material[] m_FaceMaterials;
    private Material m_BodyMaterial;



    private SkinnedMeshRenderer m_Renderer;
    private Rigidbody m_RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        m_BodyMaterial = m_Renderer.materials.ToList().Find((item) => item.mainTexture.name.Contains("Whale"));
    }

    // Update is called once per frame
    void Update()
    {
        float l_deltaTime = Time.deltaTime;

        float l_desiredRotation = 0.0f;
        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            l_desiredRotation = gameObject.transform.rotation.eulerAngles.y + (m_RotationSpeed * l_deltaTime);
            Quaternion l_desiredRotationQ = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x, l_desiredRotation, gameObject.transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, l_desiredRotationQ, l_deltaTime * m_RotationDampening);
        }
        else if (Input.GetAxis("Horizontal") < 0.0f)
        {
            l_desiredRotation = gameObject.transform.rotation.eulerAngles.y + (m_RotationSpeed * l_deltaTime * -1.0f);
            Quaternion l_desiredRotationQ = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x, l_desiredRotation, gameObject.transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, l_desiredRotationQ, l_deltaTime * m_RotationDampening);
        }


        if (Input.GetAxis("Vertical") > 0.0f)
        {
            m_RigidBody.MovePosition(gameObject.transform.position + (gameObject.transform.forward * m_Speed * l_deltaTime));
        }
        else if (Input.GetAxis("Vertical") < 0.0f)
        {
            m_RigidBody.MovePosition(gameObject.transform.position + (gameObject.transform.forward * m_Speed * l_deltaTime * -1.0f));
        }

        if (m_IsGrounded)
        {

        }
        else
        {
            //if (Input.GetAxis("Vertical") > 0.0f)
            //{
            //    l_desiredRotation = l_originalRotation.eulerAngles.x + (m_AirRotationSpeed);
            //    l_desiredRotationQ = Quaternion.Euler(l_desiredRotation, l_originalRotation.eulerAngles.y, l_originalRotation.eulerAngles.z);
            //    transform.rotation = Quaternion.Lerp(transform.rotation, l_desiredRotationQ, l_deltaTime * m_RotationDampening);
            //}
            //else if (Input.GetAxis("Vertical") < 0.0f)
            //{
            //    l_desiredRotation = l_originalRotation.eulerAngles.x + (m_AirRotationSpeed * -1.0f);
            //    Debug.Log(l_desiredRotation);
            //    l_desiredRotationQ = Quaternion.Euler(l_desiredRotation, l_originalRotation.eulerAngles.y, l_originalRotation.eulerAngles.z);
            //    transform.rotation = Quaternion.Lerp(transform.rotation, l_desiredRotationQ, l_deltaTime * m_RotationDampening);
            //}
        }


        if (m_IsGrounded && Input.GetAxis("Jump") > 0.0f)
        {
            m_RigidBody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            m_IsGrounded = false;
        }


        if (Input.GetButtonDown("Submit"))
        {
            GameObject l_newWhale = GameManager.Instance.GetNewWhale();

            Vector3 m_localPosition = m_CurrentWhale.transform.localPosition;
            m_CurrentWhale.transform.SetParent(GameManager.Instance.gameObject.transform);
            m_CurrentWhale.AddComponent<Rigidbody>();


            gameObject.transform.position = l_newWhale.transform.position;
            gameObject.transform.rotation = l_newWhale.transform.rotation;
            l_newWhale.transform.SetParent(this.gameObject.transform);
            l_newWhale.transform.localPosition = m_localPosition;
            m_CurrentWhale = l_newWhale;


        }
    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.point.y <= transform.position.y)
                m_IsGrounded = true;

        }


        m_Renderer.materials = new Material[] { m_FaceMaterials[Random.Range(0, m_FaceMaterials.Length - 1)], m_BodyMaterial };
    }
}

