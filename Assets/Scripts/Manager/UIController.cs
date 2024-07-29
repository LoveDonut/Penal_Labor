using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIController : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] GameObject _itemBuyUI;
    [SerializeField] GameObject _roleUI;
    [SerializeField] CanvasGroup clearScreenCanvasGroup;


    PlayerController _playerController;
    float fadeDuration = 2f; // 페이드 인/아웃 지속 시간
    #endregion

    #region PrivateMethods

    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        if (clearScreenCanvasGroup != null)
        {
            clearScreenCanvasGroup.alpha = 0; // 처음에는 투명하게 설정
            clearScreenCanvasGroup.interactable = false;
            clearScreenCanvasGroup.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _roleUI.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            _roleUI.SetActive(false);
        }
    }

    #endregion

    #region PublicMethods

    public void SetItemBuyUI(bool b)
    {
        _itemBuyUI.SetActive(b);
        _playerController._isActive = true;

        // make player stop
        _playerController.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void ClearScreen()
    {
        if (clearScreenCanvasGroup != null)
        {
            clearScreenCanvasGroup.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            clearScreenCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        clearScreenCanvasGroup.interactable = true;
        clearScreenCanvasGroup.alpha = 1; // 페이드 인 완료 후 완전히 보이게 설정
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();        
#endif
    }

    #endregion
}
