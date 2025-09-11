using UnityEngine;

public class SaveGameObject : MonoBehaviour
{
    public static SaveGameObject Instance;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(GameObject.FindWithTag("KeepObject"));
    }
}
