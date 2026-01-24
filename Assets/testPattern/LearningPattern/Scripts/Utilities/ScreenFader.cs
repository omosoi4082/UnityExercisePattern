using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
//fade in,out 효과 
[RequireComponent(typeof(UIDocument))]//이 스크립트는 반드시 UIDocument 컴포넌트와 함께 있어야 한다
public class ScreenFader : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 0.4f;
    [SerializeField][Range(0, 1)] private float _grayValue = 0.4f;//[Range(0,1)]인스팩터 조절

    private VisualElement _faderElement;
    public float fadeDuration => _fadeDuration;

    protected void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        UIDocument document = GetComponent<UIDocument>();
        //선언, 스타일 만들기 
        _faderElement = new VisualElement
        {
            style =
            {
                position = Position.Absolute,
                    top = 0,
                    left = 0,
                    right = 0,
                    bottom = 0,
                    backgroundColor = new Color(0, 0, 0, 0),
                    opacity = 1
            },
            pickingMode = PickingMode.Ignore
        };
        document.rootVisualElement.Add(_faderElement);
    }
    public void FadeOut()
    {
        StartCoroutine(Fade(0, 1));
    }
    public void FadeIn()
    {
        StartCoroutine(Fade(1, 0));
    }

    private IEnumerator Fade(float startOpacity, float endOpacity)
    {
        float elapsedTime = 0;
        Color startColor = new Color(_grayValue, _grayValue, _grayValue, startOpacity);
        Color endColor = new Color(_grayValue, _grayValue, _grayValue, endOpacity);
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            t = Mathf.SmoothStep(0.0f, 1.0f, t);
            _faderElement.style.backgroundColor = new StyleColor(Color.Lerp(startColor, endColor, t));
            yield return null;
        }
        _faderElement.style.backgroundColor = new StyleColor(endColor);
    }
}
