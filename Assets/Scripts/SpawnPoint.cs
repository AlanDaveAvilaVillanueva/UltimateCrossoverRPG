// 04-12-2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    void Start()
    {
        // Log para depuración
        Debug.Log("SpawnPoint: SelectedHolder.Instance = " + (SelectedHolder.Instance != null ? "encontrado" : "NULL"));
        if (SelectedHolder.Instance != null)
            Debug.Log("SpawnPoint: selectedPrefab = " + (SelectedHolder.Instance.selectedPrefab != null ? SelectedHolder.Instance.selectedPrefab.name : "NULL"));

        // Si hay un prefab en SelectedHolder, instanciarlo
        if (SelectedHolder.Instance != null && SelectedHolder.Instance.selectedPrefab != null)
        {
            // Instanciar el personaje seleccionado con posición específica x=-3, y=0
            Vector3 characterPosition = new Vector3(-3f, 0f, 0f);
            GameObject selectedCharacter = Instantiate(SelectedHolder.Instance.selectedPrefab, characterPosition, Quaternion.identity);

            // Guardar la referencia en SelectedHolder para que otros scripts (como ButtonActions) puedan acceder a ella
            SelectedHolder.Instance.selectedCharacterInstance = selectedCharacter;

            // Configurar el personaje instanciado
            BattleCharacter battleCharacter = selectedCharacter.GetComponent<BattleCharacter>();
            if (battleCharacter != null)
            {
                battleCharacter.hp = battleCharacter.maxHP; // Inicializar HP
                Debug.Log("Personaje instanciado en posición (0.3, 0): " + selectedCharacter.name);
            }
        }
        else
        {
            // Fallback: buscar si el personaje ya existe en la escena (tal vez instanciado en el menú)
            BattleCharacter existingCharacter = FindObjectOfType<BattleCharacter>();
            if (existingCharacter != null)
            {
                // Reposicionar al personaje existente a (0.3, 0)
                existingCharacter.transform.position = new Vector3(-3f, 0f, 0f);
                SelectedHolder.Instance.selectedCharacterInstance = existingCharacter.gameObject;
                Debug.Log("SpawnPoint: Personaje existente reposicionado a (-3, 0)");
            }
            else
            {
                Debug.LogError("SpawnPoint: No se encontró prefab en SelectedHolder ni personaje instanciado en la escena.");
            }
        }
    }
}