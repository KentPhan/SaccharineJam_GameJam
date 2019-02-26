using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public RectTransform m_WinScreen;

    public void Awake()
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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowWin()
    {
        m_WinScreen.gameObject.SetActive(true);
    }
}
