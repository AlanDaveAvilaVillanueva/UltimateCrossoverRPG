using UnityEngine;

public class SelectedHolder : MonoBehaviour
{
    public static SelectedHolder Instance { get; private set; }
    public GameObject selectedPrefab; // será asignado antes de cargar la escena
    public GameObject selectedCharacterInstance; // Guarda la instancia del personaje cuando se instantia en la escena Game

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
