using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    [Header("Values")]
    public int currentCut;

    [Header("ActionChecks")]
    public CutSceneTalk startTalk;
    public CutSceneEndTalk endTalk;

    [Header("Components")]
    public PlayerController playerController;
    public GameObject cutSceneUI;
    public GameObject inGameUI;
    public Animator[] animator;

    private void Start()
    {
        for(int i = 0; i < animator.Length; i++)
        {
            animator[i].SetInteger("CurrentCut", currentCut);
        }
    }

    private void Update()
    {
        if (startTalk.isStartCheck)
        {
            playerController.canMove = false;
            cutSceneUI.SetActive(true);
            inGameUI.SetActive(false);
        }

        if (endTalk.isEndCheck)
        {
            playerController.canMove = true;
            cutSceneUI.SetActive(false);
        }
    }
}
