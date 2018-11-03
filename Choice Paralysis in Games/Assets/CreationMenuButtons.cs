using System.Collections.Generic;
using UnityEngine;

public class CreationMenuButtons : MonoBehaviour
{
    [SerializeField] private List<Tab> _tabs;

    private Tab _activeTab;

    private void Start()
    {
        _activeTab = _tabs[0];
        _activeTab.SetTabActive(true);
    }

    public void ActivateNewTab(int tabNumber)
    {
        _activeTab.SetTabActive(false);
        _tabs[tabNumber].SetTabActive(true);
        _activeTab = _tabs[tabNumber];
    }
}
