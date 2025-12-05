// 04-12-2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    private Transform spawnPoint; // Referencia al objeto SpawnPoint

    void Start()
    {
        // Buscar el objeto SpawnPoint en la escena
        spawnPoint = GameObject.Find("SpawnPoint").transform;

        if (spawnPoint == null)
        {
            Debug.LogError("No se encontró el objeto SpawnPoint en la escena.");
            return;
        }

        if (SelectedHolder.Instance != null && SelectedHolder.Instance.selectedPrefab != null)
        {
            // Instanciar el personaje seleccionado en el punto de spawn
            GameObject selectedCharacter = Instantiate(SelectedHolder.Instance.selectedPrefab, spawnPoint.position, Quaternion.identity);

            // Configurar el personaje instanciado
            BattleCharacter battleCharacter = selectedCharacter.GetComponent<BattleCharacter>();
            if (battleCharacter != null)
            {
                battleCharacter.hp = battleCharacter.maxHP; // Inicializar HP
                Debug.Log("Personaje seleccionado instanciado: " + selectedCharacter.name);
            }
        }
        else
        {
            Debug.LogError("No se encontró un personaje seleccionado en SelectedHolder.");
        }
    }
}