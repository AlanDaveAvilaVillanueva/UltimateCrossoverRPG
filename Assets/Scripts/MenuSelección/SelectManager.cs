using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characterPrefabs; // arrastra aquí tus prefabs (orden coincide con los retratos)
    public int selectedIndex = 0;         // índice elegido (por defecto 0)
    public string gameSceneName = "Game"; // cambia por el nombre real

    // Llamar desde cada retrato al hacer click (preview + guardar índice)
    public void PickCharacter(int index)
    {
        if (index < 0 || index >= characterPrefabs.Length)
        {
            Debug.LogWarning("PickCharacter: index fuera de rango: " + index);
            return;
        }

        selectedIndex = index;
        Debug.Log("Preview character index: " + index);

        // Aquí puedes actualizar UI de preview (sprite, nombre, highlight). Ej: llamar a un método UpdatePreview()
    }

    // Llamar desde el botón "Comenzar"
    public void StartGame()
    {
        if (SelectedHolder.Instance == null)
        {
            // Si por alguna razon no existe el holder lo creamos
            GameObject holderGO = new GameObject("SelectedHolder");
            holderGO.AddComponent<SelectedHolder>();
        }
        
        // Asignar el prefab seleccionado
        SelectedHolder.Instance.selectedPrefab = characterPrefabs[selectedIndex];
        Debug.Log("SelectManager: Prefab asignado = " + (SelectedHolder.Instance.selectedPrefab != null ? SelectedHolder.Instance.selectedPrefab.name : "NULL"));
        
        // Cargar la escena
        Debug.Log("SelectManager: Cargando escena " + gameSceneName);
        SceneManager.LoadScene(gameSceneName);
    }
}
