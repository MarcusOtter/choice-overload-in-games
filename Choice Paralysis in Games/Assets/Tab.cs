using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    internal bool Visited;
    internal bool Active;

    [SerializeField] private Outline _selectedOutline;
    [SerializeField] private GameObject _selectedIndicator;
    [SerializeField] private GameObject _panel;

    internal void SetTabActive(bool active)
    {
        _selectedOutline.enabled = active;
        _selectedIndicator.SetActive(active);
        _panel.SetActive(active);

        if (active && !Visited)
        {
            Visited = true;
        }
    }
}
