using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    float flashDuration = 1f; // 깜빡이는 지속 시간

    Image _redOverlay;
    ManageStage _stageManager;

    void Awake()
    {
        _redOverlay = GetComponent<Image>();
        _stageManager = transform.parent.GetComponentInParent<ManageStage>();
    }

    void Start()
    {
        if (_redOverlay != null)
        {
            _redOverlay.color = new Color(1, 0, 0, 0); // 처음에는 투명하게 설정
        }
    }

    public void FlashScreen()
    {
        if (_redOverlay != null)
        {
            StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        while(_stageManager._isGatherEverything)
        {
            // 페이드인
            yield return StartCoroutine(Fade(0f, 0.5f, flashDuration / 2));

            // 페이드아웃
            yield return StartCoroutine(Fade(0.5f, 0f, flashDuration / 2));
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = _redOverlay.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            _redOverlay.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Ensure the final alpha is set
        _redOverlay.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
