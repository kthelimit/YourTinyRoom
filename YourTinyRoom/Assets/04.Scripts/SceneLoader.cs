﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public CanvasGroup fadeCG;
    [Range(0.5f, 2.0f)]
    public float fadeDuration = 1.0f;
    //                key     value  //쌍을 이루게된 것임
    public Dictionary<string, LoadSceneMode> loadScenes = new Dictionary<string, LoadSceneMode>();

    void InitSceneInfo() //호출할 씬 정보
    {
        loadScenes.Add("UI", LoadSceneMode.Additive);
        loadScenes.Add("Room", LoadSceneMode.Additive);
        //둘 다 loadScenes에 Add를 이용해서 추가함
    }
    IEnumerator Start()
    {
        InitSceneInfo();

        fadeCG.alpha = 1.0f; //처음 알파값 설정(불투명)
        //여러개의 씬을 코루틴으로 호출
        foreach (var _loadScene in loadScenes)
        {
            yield return StartCoroutine(LoadScene(_loadScene.Key, _loadScene.Value));
        }
        StartCoroutine(Fade(0.0f));

    }
    IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        //비동기 방식으로 씬을 로드하고 로드가 완료될 때까지 대기함
        yield return SceneManager.LoadSceneAsync(sceneName, mode);
        //호출된 씬을 활성화
        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
    }

    IEnumerator Fade(float finalAlpha)
    {
        //라이트 맵이 깨지는 것을 방지하기 위해 스테이지씬을 활성화
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UI"));
        fadeCG.blocksRaycasts = true;
        //절대값 함수로 백분율을 계산
        float fadeSpeed = Mathf.Abs(fadeCG.alpha - finalAlpha) / fadeDuration;
        //알파값을 조정
        while (!Mathf.Approximately(fadeCG.alpha, finalAlpha))
        {                        //MoveTowards는 보간함수 Lerp와 같은 함수.
            fadeCG.alpha = Mathf.MoveTowards(fadeCG.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;//한프레임씩 돈다.
        }
        fadeCG.blocksRaycasts = fadeCG;
        //페이드인이 완료된 후 SceneLoader씬은 삭제
        SceneManager.UnloadSceneAsync("SceneLoader");

    }

}
