using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class Auflösung : MonoBehaviour
{
    public UIDocument uIDocument;
    private Toggle suspectToggle1;
    private Toggle suspectToggle2;
    private Toggle suspectToggle3;
    private Toggle suspectToggle4;
    private Toggle answerToggle1;
    private Toggle answerToggle2;
    private Toggle answerToggle3;
    private Toggle answerToggle4;
    private Button solveButton;

    public Material idleMaterial;
    public Material activeMaterial;
    public Material interactionMaterial;

    public Renderer indicatorRenderer;


    // Start is called before the first frame update
    void Awake()
    {

        var uIDocument = GetComponent<UIDocument>();

        suspectToggle1 = uIDocument.rootVisualElement.Q("sustoggle1") as Toggle;
        suspectToggle2 = uIDocument.rootVisualElement.Q("sustoggle2") as Toggle;
        suspectToggle3 = uIDocument.rootVisualElement.Q("sustoggle3") as Toggle;
        suspectToggle4 = uIDocument.rootVisualElement.Q("sustoggle4") as Toggle;
        answerToggle1 = uIDocument.rootVisualElement.Q("statementtoggle2") as Toggle;
        answerToggle2 = uIDocument.rootVisualElement.Q("statementtoggle3") as Toggle;
        answerToggle3 = uIDocument.rootVisualElement.Q("statementtoggle4") as Toggle;
        answerToggle4 = uIDocument.rootVisualElement.Q("statementtoggle5") as Toggle;

        solveButton = uIDocument.rootVisualElement.Q("resultbutton") as Button;

        solveButton.RegisterCallback<ClickEvent>(SolveAnswer);

    }

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        // Suche die Sphere nach dem Namen oder Tag
            GameObject sphere = GameObject.Find("SphereName");  // Ersetze "SphereName" mit dem tatsächlichen Namen der Sphere
            if (sphere != null)
            {
                indicatorRenderer = sphere.GetComponent<Renderer>();
                indicatorRenderer.material = activeMaterial;
            }
            else
            {
                Debug.LogError("Sphere nicht gefunden!");
            }

    }

    public void SolveAnswer(ClickEvent evt)
    {
        bool res1 = suspectToggle1.value;
        bool res2 = suspectToggle2.value;
        bool res3 = suspectToggle3.value;
        bool res4 = suspectToggle4.value;
        bool res5 = answerToggle1.value;
        bool res6 = answerToggle2.value;
        bool res7 = answerToggle3.value;
        bool res8 = answerToggle4.value;

        if (!res1 && res2 && !res3 && !res4 && !res5 && res6 && res7 && !res8)
        {
            RigthAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    public void RigthAnswer() {
        Debug.Log("Richtige Antwort");
        indicatorRenderer.material = interactionMaterial;
    }

    public void WrongAnswer() {
        Debug.Log("Falsche Antwort");
        indicatorRenderer.material = idleMaterial;
    }

}
