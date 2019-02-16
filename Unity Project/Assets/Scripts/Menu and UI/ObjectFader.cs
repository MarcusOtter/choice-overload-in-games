using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Menu_and_UI
{
    public class ObjectFader : MonoBehaviour
    {
        [SerializeField] private float _animationDuration = 1f;
        [SerializeField] private float _animationUpdateDelay = 0.02f;

        public void FadeInImage(Image imageToFade)
        {
            StartCoroutine(FadeImage(imageToFade, true));
        }

        public void FadeInTMPText(TextMeshProUGUI textToFade)
        {
            StartCoroutine(FadeTMPText(textToFade, true));
        }

        public void FadeInCanvasGroup(CanvasGroup canvasGroupToFade)
        {
            StartCoroutine(FadeCanvasGroup(canvasGroupToFade, true));
        }

        /// <summary>
        /// Fades in all the components that are on the same object as this ObjectFader
        /// </summary>
        public void FadeInComponentsOnFader()
        {
            StartCoroutine(FadeImage(GetComponent<Image>(), true));
            StartCoroutine(FadeTMPText(GetComponent<TextMeshProUGUI>(), true));
            StartCoroutine(FadeCanvasGroup(GetComponent<CanvasGroup>(), true));
        }

        /// <summary>
        /// Fades out all the components that are on the same object as this ObjectFader
        /// </summary>
        public void FadeOutComponentsOnFader()
        {
            StartCoroutine(FadeImage(GetComponent<Image>(), false));
            StartCoroutine(FadeTMPText(GetComponent<TextMeshProUGUI>(), false));
            StartCoroutine(FadeCanvasGroup(GetComponent<CanvasGroup>(), false));
        }

        private IEnumerator FadeImage(Image imageToFade, bool fadeIn)
        {
            if (imageToFade == null) { yield break; }

            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.b, imageToFade.color.b, fadeIn ? 0 : 1);
            var imageColor = imageToFade.color;

            if (fadeIn)
            {
                while (imageToFade.color.a < 1)
                {
                    imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, imageColor.a += _animationUpdateDelay / _animationDuration);
                    imageToFade.color = imageColor;
                    yield return new WaitForSeconds(_animationUpdateDelay);
                }
            }
            else
            {
                while (imageToFade.color.a > 0)
                {
                    imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, imageColor.a -= _animationUpdateDelay / _animationDuration);
                    imageToFade.color = imageColor;
                    yield return new WaitForSeconds(_animationUpdateDelay);
                }
            }

        }

        private IEnumerator FadeTMPText(TextMeshProUGUI textToFade, bool fadeIn)
        {
            if (textToFade == null) { yield break; }

            textToFade.alpha = fadeIn ? 0 : 1;

            if (fadeIn)
            {
                while (textToFade.alpha < 1)
                {
                    textToFade.alpha += _animationUpdateDelay / _animationDuration;
                    yield return new WaitForSeconds(_animationUpdateDelay);
                }
            }
            else
            {
                while (textToFade.alpha > 0)
                {
                    textToFade.alpha -= _animationUpdateDelay / _animationDuration;
                    yield return new WaitForSeconds(_animationUpdateDelay);
                }
            }
        }

        private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroupToFade, bool fadeIn)
        {
            if (canvasGroupToFade == null) { yield break; }
            
            canvasGroupToFade.alpha = fadeIn ? 0 : 1;

            if (fadeIn)
            {
                while (canvasGroupToFade.alpha < 1)
                {
                    canvasGroupToFade.alpha += _animationUpdateDelay / _animationDuration;
                    yield return new WaitForSeconds(_animationUpdateDelay);
                }
            }
            else
            {
                while (canvasGroupToFade.alpha > 0)
                {
                    canvasGroupToFade.alpha -= _animationUpdateDelay / _animationDuration;
                    yield return new WaitForSeconds(_animationUpdateDelay);
                }
            }

        }
    }
}
