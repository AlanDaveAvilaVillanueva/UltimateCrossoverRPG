using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectButton : MonoBehaviour
{
    public CharacterData data;  // arrastras en el Inspector
    public string battleSceneName = "BattleScene";

    public void OnClickSelect()
    {
        PlayerPrefs.SetInt("ChosenCharacterID", data.id);
    }
    public void BattleScene()
    {
        SceneManager.LoadScene(battleSceneName);
    }
    
}
