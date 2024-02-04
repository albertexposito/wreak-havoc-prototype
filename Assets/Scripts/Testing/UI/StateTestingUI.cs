using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateTestingUI : MonoBehaviour
{

    private Dictionary<BasePlayerCharacter, TMP_Text> _characterListTextsDictionary;
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private Vector2 _offset = new Vector2(0, 80);

    [SerializeField] private TMP_Text _prefabTextField;

    private void Awake()
    {
        _characterListTextsDictionary = new Dictionary<BasePlayerCharacter, TMP_Text>();

        if(_mainCamera == null)
            _mainCamera = Camera.main;

    }

    public void AddCharacter(BasePlayerCharacter character)
    {
        Debug.Log($"[StateTestingUI] - Adding Character: {character.name}");

        TMP_Text text = Instantiate(_prefabTextField, transform);
        _characterListTextsDictionary.Add(character, text);
    }

    private void Update()
    {

        foreach (BasePlayerCharacter character in _characterListTextsDictionary.Keys)
            PrintStateInScreen(character);

    }

    private void PrintStateInScreen(BasePlayerCharacter character)
    {
        
        Vector3 screenPoint = _mainCamera.WorldToScreenPoint(character.transform.position);

        screenPoint.x += _offset.x;
        screenPoint.y += _offset.y;

        TMP_Text textField = _characterListTextsDictionary[character];

        textField.rectTransform.position = screenPoint;
        textField.text = character.CharacterStateName;
    }

}
