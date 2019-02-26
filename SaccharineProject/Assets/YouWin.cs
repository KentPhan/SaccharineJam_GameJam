using UnityEngine;

public class YouWin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider collision)
    {
        GameManager.Instance.TriggerWin();
    }
}
