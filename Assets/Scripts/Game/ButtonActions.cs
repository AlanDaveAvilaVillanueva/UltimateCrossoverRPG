// 28-11-2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    public GameObject fighterSprite; // Asigna el GameObject del sprite del personaje
    public Sprite attackSprite; // Asigna el sprite de ataque
    public Sprite comboSprite; // Asigna el sprite de combo
    public Sprite specialSprite; // Asigna el sprite especial
    public Sprite waitSprite; // Asigna el sprite de espera
    public Sprite dodgeSprite; // Asigna el sprite de esquivar
    public Sprite backpackSprite; // Asigna el sprite de mochila
    public Animator fighterAnimator; // Asigna el Animator que controla los sprites

    // Nombres de parámetros bool en el Animator
    public string paramAttack = "Attack-1";
    public string paramCombo = "Combo";
    public string paramSpecial = "Special";
    public string paramWait = "Wait";
    public string paramDodge = "Dodge";
    public string paramBackpack = "Backpack";

    private SpriteRenderer fighterSpriteRenderer;
    private string[] animatorBoolParams;
    private Coroutine animatorResetCoroutine;
    private string activeAnimatorParam;
    private bool initialized = false;

    [Header("Animator Auto-Reset")]
    [Tooltip("Si está activo, el bool del Animator se pondrá a false automáticamente después de 'animatorBoolResetDelay' segundos.")]
    public bool autoResetAnimatorBools = true;
    [Tooltip("Tiempo en segundos para resetear el bool activo del Animator. (0 o negativo = no reset automático)")]
    public float animatorBoolResetDelay = 0.25f;

    private void Start()
    {
        // Intentar inicializar en Start
        InitializeFighterSprite();
    }

    private void Update()
    {
        // Si no se ha inicializado, reintentar cada frame hasta que se encuentre el personaje
        if (!initialized)
        {
            InitializeFighterSprite();
        }
    }

    private void InitializeFighterSprite()
    {
        if (initialized) return; // Ya está inicializado

        // Si fighterSprite no está asignado en el inspector, busca el personaje instanciado
        if (fighterSprite == null)
        {
            // Estrategia 1: Buscar en SelectedHolder
            if (SelectedHolder.Instance != null && SelectedHolder.Instance.selectedCharacterInstance != null)
            {
                fighterSprite = SelectedHolder.Instance.selectedCharacterInstance;
                Debug.Log("ButtonActions: fighterSprite obtenido de SelectedHolder.");
            }
            else
            {
                // Estrategia 2: Buscar por componente BattleCharacter en la escena
                BattleCharacter battleChar = FindObjectOfType<BattleCharacter>();
                if (battleChar != null)
                {
                    fighterSprite = battleChar.gameObject;
                    Debug.Log("ButtonActions: fighterSprite encontrado por BattleCharacter.");
                }
                else
                {
                    // Aún no encontrado, esperar al siguiente frame
                    return;
                }
            }
        }

        // Obtén el componente SpriteRenderer del personaje
        fighterSpriteRenderer = fighterSprite.GetComponent<SpriteRenderer>();
        if (fighterSpriteRenderer == null)
        {
            Debug.LogWarning("ButtonActions: 'fighterSprite' no tiene componente SpriteRenderer.");
        }

        // Construye la lista de parámetros
        animatorBoolParams = new string[] {
            paramAttack,
            paramCombo,
            paramSpecial,
            paramWait,
            paramDodge,
            paramBackpack
        };

        if (fighterAnimator == null)
        {
            Debug.LogWarning("ButtonActions: 'fighterAnimator' no está asignado. Se usará SpriteRenderer como fallback.");
        }

        initialized = true;
        Debug.Log("ButtonActions: Inicialización completada correctamente.");
    }

    public void OnAttackButtonPressed()
    {
        ActivateSprite(attackSprite);
    }

    public void OnComboButtonPressed()
    {
        ActivateSprite(comboSprite);
    }

    public void OnSpecialButtonPressed()
    {
        ActivateSprite(specialSprite);
    }

    public void OnWaitButtonPressed()
    {
        ActivateSprite(waitSprite);
    }

    public void OnDodgeButtonPressed()
    {
        ActivateSprite(dodgeSprite);
    }

    public void OnBackpackButtonPressed()
    {
        ActivateSprite(backpackSprite);
    }

    private void ActivateSprite(Sprite newSprite)
    {
        // Si hay un Animator asignado, usamos parámetros bool para activar la animación correspondiente
        if (fighterAnimator != null)
        {
            // Determinar qué parámetro corresponde al sprite solicitado
            string targetParam = null;
            if (newSprite == attackSprite) targetParam = paramAttack;
            else if (newSprite == comboSprite) targetParam = paramCombo;
            else if (newSprite == specialSprite) targetParam = paramSpecial;
            else if (newSprite == waitSprite) targetParam = paramWait;
            else if (newSprite == dodgeSprite) targetParam = paramDodge;
            else if (newSprite == backpackSprite) targetParam = paramBackpack;

            if (!string.IsNullOrEmpty(targetParam))
            {
                ActivateAnimatorBool(targetParam);
            }
            else
            {
                Debug.LogWarning("Sprite solicitado no coincide con ninguno de los sprites configurados.");
            }
        }
        else if (fighterSpriteRenderer != null)
        {
            // Fallback: si no hay Animator, simplemente cambia el SpriteRenderer
            fighterSpriteRenderer.sprite = newSprite;
        }
        else
        {
            Debug.LogError("No se encontró el componente SpriteRenderer en el GameObject del personaje.");
        }
    }

    // Activa el parámetro bool indicado en el Animator y desactiva los demás
    private void ActivateAnimatorBool(string targetParam)
    {
        if (fighterAnimator == null) return;
        // Si hay una coroutine de reset en curso, detenerla (evita resets tardíos)
        if (animatorResetCoroutine != null)
        {
            StopCoroutine(animatorResetCoroutine);
            animatorResetCoroutine = null;
        }

        // Desactivar todos los bools primero
        if (animatorBoolParams != null)
        {
            for (int i = 0; i < animatorBoolParams.Length; i++)
            {
                var p = animatorBoolParams[i];
                if (!string.IsNullOrEmpty(p)) fighterAnimator.SetBool(p, false);
            }
        }

        // Activar el solicitado
        fighterAnimator.SetBool(targetParam, true);
        activeAnimatorParam = targetParam;

        // Iniciar reset automático si está habilitado y el delay es mayor que 0
        if (autoResetAnimatorBools && animatorBoolResetDelay > 0f)
        {
            animatorResetCoroutine = StartCoroutine(ResetAnimatorBoolCoroutine(targetParam, animatorBoolResetDelay));
        }
    }

    private System.Collections.IEnumerator ResetAnimatorBoolCoroutine(string param, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Solo resetear si el parámetro sigue siendo el activo (no fue cambiado por otro botón)
        if (fighterAnimator != null && activeAnimatorParam == param)
        {
            fighterAnimator.SetBool(param, false);
            activeAnimatorParam = null;
        }

        animatorResetCoroutine = null;
    }
}