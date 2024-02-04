using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModelHandler : MonoBehaviour
{

    private Animator _anim;

    [SerializeField] private GameObject _characterModel;
    [SerializeField] private MeshRenderer _bodyMeshRenderer;

    public void SetMeshColor(int index)
    {
        Debug.Log($"[CharacterModelHandler] - Setting color for index: {index}");

        if (index == -1)
            return;

        if(LocalGameManager.Instance != null)
        {
            Color meshColor = LocalGameManager.Instance.GetColorByIndex(index);
            _bodyMeshRenderer.material.color = meshColor;
        }
    }

    public void DisableCharacterVisuals()
    {
        _characterModel.SetActive(false);
    }

    public void EnableCharacterVisuals()
    {
        _characterModel.SetActive(true);
    }

}
