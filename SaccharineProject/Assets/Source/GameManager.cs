using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance;

    [SerializeField]
    private GameObject[] m_Whales;

    [SerializeField] private GameObject m_WhaleSpawnPosition;

    [SerializeField] private Material[] m_FaceMaterials;
    [SerializeField] private Material[] m_BodyMaterials;
    [SerializeField] private CinemachineVirtualCamera m_Camera;

    private PlayerComponent m_CurrentPlayer;

    void Awake()
    {
        if (Instance == this)
            return;

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);



    }

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public GameObject GetNewWhale()
    {
        GameObject l_newWhale = Instantiate(m_Whales[Random.Range(0, m_Whales.Length - 1)], m_WhaleSpawnPosition.transform.position,
            Quaternion.identity);

        SkinnedMeshRenderer l_renderer = l_newWhale.GetComponentInChildren<SkinnedMeshRenderer>();
        l_renderer.materials = new Material[] { m_FaceMaterials[Random.Range(0, m_FaceMaterials.Length - 1)], m_BodyMaterials[Random.Range(0, m_BodyMaterials.Length - 1)] };

        m_Camera.LookAt = l_newWhale.transform;
        return l_newWhale;
    }


    public void TriggerWin()
    {
        CanvasManager.Instance.ShowWin();
    }
}
