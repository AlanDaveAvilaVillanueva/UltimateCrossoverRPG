using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData data;

    public int currentHP;

    public void Init(CharacterData d)
    {
        data = d;
        currentHP = data.maxHP;

        // Si quieres asignar animación e icono desde data:
        var animator = GetComponent<Animator>();
        if (animator && data.animator)
            animator.runtimeAnimatorController = data.animator;
    }
}
