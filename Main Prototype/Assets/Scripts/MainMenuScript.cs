using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MainMenuScript : MonoBehaviour
{
    private UIDocument _document;

    private Button _button;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _button = _document.rootVisualElement.Q("level1") as Button;
        _button.RegisterCallback<ClickEvent>(OpenLevel);
    }



    public void OpenLevel(ClickEvent evt)
    {
        SceneManager.LoadScene("SampleScene");
    }
}
