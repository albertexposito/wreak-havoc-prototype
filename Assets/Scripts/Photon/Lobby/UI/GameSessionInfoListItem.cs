using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSessionInfoListItem : MonoBehaviour
{

    public TMP_Text sessionNameText;
    public TMP_Text playerCountText;
    public Button joinButton;

    private SessionInfo _sessionInfo;

    public void Initialize(Action<SessionInfo> joinButtonClickCallback)
    {
        joinButton.onClick.AddListener(() =>
        {
            if(_sessionInfo != null)
                joinButtonClickCallback.Invoke(_sessionInfo);
        });

        gameObject.SetActive(false);
    }

    public void SetInformation(SessionInfo sessionInfo)
    {

        if(sessionInfo == null)
        {
            ClearInformation();
            return;
        }

        _sessionInfo = sessionInfo;

        sessionNameText.text = sessionInfo.Name;
        playerCountText.text = $"{sessionInfo.PlayerCount}/{sessionInfo.MaxPlayers}";

        bool isJoinButtonActive = true;

        if (sessionInfo.PlayerCount >= sessionInfo.MaxPlayers)
            isJoinButtonActive = false;

        joinButton.interactable = isJoinButtonActive;

        gameObject.SetActive(true);

    }

    public void ClearInformation()
    {
        _sessionInfo = null;

        sessionNameText.text = string.Empty;
        playerCountText.text = string.Empty;

        joinButton.interactable = false;

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        joinButton.onClick.RemoveAllListeners();
    }
}
