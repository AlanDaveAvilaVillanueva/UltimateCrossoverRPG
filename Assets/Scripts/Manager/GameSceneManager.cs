// 04-12-2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public Transform spawnPoint; // Punto donde se instanciará el personaje seleccionado

    void Start()
    {
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