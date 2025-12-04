using UnityEngine;

public class SelectedHolder : MonoBehaviour
{
    public static SelectedHolder Instance { get; private set; }
    public GameObject selectedPrefab; // será asignado antes de cargar la escena

    void Awake()
    {
    if (Instance == null)
    {
        Instance = this;

        // Asegura que este GameObject sea root (no tenga padre)
        if (transform.parent != null)
            transform.SetParent(null);

        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }
    }

}
