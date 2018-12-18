using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// TODO
public class LoadPlayerGraphics : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Image _headImage;
    [SerializeField] private Image _bodyImage;

    [Header("Sprite Renderers")]
    [SerializeField] private SpriteRenderer _headRenderer;
    [SerializeField] private SpriteRenderer _bodyRenderer;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }
}
