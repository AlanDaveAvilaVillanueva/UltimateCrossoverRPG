using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Identity")]
    public int id;
    public string characterName;

    [Header("Stats base")]
    public int maxHP;
    public int attack;
    public int defense;
    public int speed;

    [Header("Visuals opcionales")]
    public Sprite icon;
    public RuntimeAnimatorController animator;
}
