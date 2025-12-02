using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterSelectableSmooth : MonoBehaviour
{
    SpriteRenderer sr;
    [Header("Transición")]
    [SerializeField] float transitionTime = 0.25f;
    [SerializeField] bool startSelected = false;

    Coroutine running;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        // Preservar alfa actual
        float a = sr.color.a;
        sr.color = (startSelected ? Color.white : Color.black);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
    }

    void OnEnable()
    {
        // Registrar en el manager si existe
        if (SelectionManager.Instance != null)
            SelectionManager.Instance.Register(this);

        // Si comienza seleccionado y hay manager, asegurarse que sea el único seleccionado
        if (startSelected && SelectionManager.Instance != null)
            SelectionManager.Instance.Select(this);
    }

    void OnDisable()
    {
        SelectionManager.Instance?.Unregister(this);
    }

    // Llamar desde el manager o desde UI
    public void SetSelected(bool selected)
    {
        Color target = selected ? Color.white : Color.black;
        // preservar alfa
        target.a = sr.color.a;
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(TransitionColor(target));
    }

    IEnumerator TransitionColor(Color target)
    {
        Color start = sr.color;
        float t = 0f;
        float duration = Mathf.Max(0.0001f, transitionTime);

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            sr.color = Color.Lerp(start, target, t);
            yield return null;
        }

        sr.color = target;
        running = null;
    }

    // Ejemplo simple de selección por click (puedes quitarlo si usas UI o input)
    void OnMouseDown()
    {
        if (SelectionManager.Instance != null)
            SelectionManager.Instance.Select(this);
        else
            SetSelected(true);
    }
}
