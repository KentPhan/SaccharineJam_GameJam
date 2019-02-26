using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance;


    void Awake()
    {
        if (Instance == this)
            return;

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
