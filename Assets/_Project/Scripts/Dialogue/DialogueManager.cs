using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Object References")]
    public static DialogueManager Instance {get; private set;}
    
    [Header("UI")]
    [SerializeField] private GameObject _dialoguePanel;

    [Header("Properties")]
    public bool isDialoguePlaying {get; private set;}

    private void Awake()
    {
        Instance = this;
        _dialoguePanel.SetActive(false);
    }

    public void StartDialogue(string inkKnotName)
    {
        isDialoguePlaying = true;
        _dialoguePanel.SetActive(true);
    }

    public void EndDialogue()
    {
        isDialoguePlaying = false;
        _dialoguePanel.SetActive(false);
    }
}
