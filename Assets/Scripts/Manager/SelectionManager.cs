using UnityEngine;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }

    List<CharacterSelectableSmooth> characters = new List<CharacterSelectableSmooth>();
    CharacterSelectableSmooth currentSelected;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Register(CharacterSelectableSmooth c)
    {
        if (!characters.Contains(c)) characters.Add(c);
    }

    public void Unregister(CharacterSelectableSmooth c)
    {
        if (characters.Contains(c)) characters.Remove(c);
        if (currentSelected == c) currentSelected = null;
    }

    // Selecciona el pasado como argumento y deselecciona el resto
    public void Select(CharacterSelectableSmooth chosen)
    {
        if (chosen == null) return;

        // Si ya está seleccionado, no hacer nada (o podrías toggle si quieres)
        if (currentSelected == chosen) return;

        // Deseleccionar el actual
        if (currentSelected != null)
            currentSelected.SetSelected(false);

        // Seleccionar el nuevo
        chosen.SetSelected(true);
        currentSelected = chosen;
    }

    // Selección por índice (opcional)
    public void SelectIndex(int index)
    {
        if (index < 0 || index >= characters.Count) return;
        Select(characters[index]);
    }

    // Deseleccionar todo
    public void DeselectAll()
    {
        if (currentSelected != null)
        {
            currentSelected.SetSelected(false);
            currentSelected = null;
        }
    }
}
