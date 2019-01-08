using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Menu_and_UI
{
    public class ObjectFader : MonoBehaviour
    {
        [SerializeField] private float _secondsToFadeIn = 1f;
        [SerializeField] private float _animationUpdateDelay = 0.02f;

        public void FadeInImage(Image imageToFade)
        {
            StartCoroutine(FadeImage(imageToFade));
        }

        public void FadeInTMPText(TextMeshProUGUI textToFade)
        {
            StartCoroutine(FadeTMPText(textToFade));
        }

        public void FadeInCanvasGroup(CanvasGroup canvasGroupToFade)
        {
            StartCoroutine(FadeCanvasGroup(canvasGroupToFade));
        }

        /// <summary>
        /// Fades in all the components that are on the same object as this ObjectFader
        /// </summary>
        public void FadeComponentsOnFader()
        {
            StartCoroutine(FadeImage(GetComponent<Image>()));
            StartCoroutine(FadeTMPText(GetComponent<TextMeshProUGUI>()));
            StartCoroutine(FadeCanvasGroup(GetComponent<CanvasGroup>()));
        }

        private IEnumerator FadeImage(Image imageToFade)
        {
            if (imageToFade == null) { yield break; }

            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.b, imageToFade.color.b, 0);
            var imageColor = imageToFade.color;

            while (imageToFade.color.a < 1)
            {
                imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, imageColor.a += _animationUpdateDelay / _secondsToFadeIn);
                imageToFade.color = imageColor;
                yield return new WaitForSeconds(_animationUpdateDelay);
            }
        }

        private IEnumerator FadeTMPText(TextMeshProUGUI textToFade)
        {
            if (textToFade == null) { yield break; }

            textToFade.alpha = 0;

            while (textToFade.alpha < 1)
            {
                textToFade.alpha += _animationUpdateDelay / _secondsToFadeIn;
                yield return new WaitForSeconds(_animationUpdateDelay);
            }
        }

        private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroupToFade)
        {
            if (canvasGroupToFade == null) { yield break; }
            
            canvasGroupToFade.alpha = 0;

            while (canvasGroupToFade.alpha < 1)
            {
                canvasGroupToFade.alpha += _animationUpdateDelay / _secondsToFadeIn;
                yield return new WaitForSeconds(_animationUpdateDelay);
            }
        }
    }
}
