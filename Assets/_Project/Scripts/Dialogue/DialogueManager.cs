using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("Object References")]
    public static DialogueManager Instance {get; private set;}

    [Header("Ink")]
    [SerializeField] private TextAsset _mainInkJson;

    [Header("UI")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _speakerNameText;
    [SerializeField] private Transform _choicesContainer;
    [SerializeField] private Button _choiceButtonPrefab;

    [Header("Properties")]
    public bool isDialoguePlaying {get; private set;}
    private Story _currentStory;
    private readonly List<Button> _activeChoiceButtons = new List<Button>();

    private void Awake()
    {
        Instance = this;
        _currentStory = new Story(_mainInkJson.text);
        BindExternalFunctions();
        _dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!isDialoguePlaying) return;

        if (_currentStory.currentChoices.Count == 0 && Mouse.current.rightButton.wasPressedThisFrame)
        {
            ContinueStory();
        }
    }

    private void ContinueStory()
    {
        while (_currentStory.canContinue)
        {
            string nextLine = _currentStory.Continue();

            if (!string.IsNullOrWhiteSpace(nextLine))
            {
                _dialogueText.text = nextLine;
                DisplayChoices();
                return;
            }
        }

        if (_currentStory.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else
        {
            EndDialogue();
        }
    }

    private void DisplayChoices()
    {
        ClearChoices();
        List<Choice> currentChoices = _currentStory.currentChoices;

        for(int i = 0; i < currentChoices.Count; i++)
        {
            int choiceIndex = i;
            Button button = Instantiate(_choiceButtonPrefab, _choicesContainer);
            button.GetComponentInChildren<TextMeshProUGUI>().text = currentChoices[i].text;
            button.onClick.AddListener(() => OnChoiceSelected(choiceIndex));

            _activeChoiceButtons.Add(button);
        }
    }

    private void ClearChoices()
    {
        foreach(Button button in _activeChoiceButtons)
        {
            Destroy(button.gameObject);
        }

        _activeChoiceButtons.Clear();
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        ClearChoices();
        ContinueStory();
    }

    public void StartDialogue(string inkKnotName, string npcName)
    {
        isDialoguePlaying = true;
        _dialoguePanel.SetActive(true);
        _speakerNameText.text = npcName;
        _currentStory.ChoosePathString(inkKnotName);
        ContinueStory();
    }

    public void EndDialogue()
    {
        isDialoguePlaying = false;
        _dialoguePanel.SetActive(false);
    }

    public object GetStoryVariable(string variableName)
    {
        return _currentStory.variablesState[variableName];
    }

    private void BindExternalFunctions()
    {
        _currentStory.BindExternalFunction("GiveItem", (string itemName, int count) =>
        {
            InventoryManager.Instance.AddItemByName(itemName, count);
        });

        _currentStory.BindExternalFunction("RemoveItem", (string itemName, int count) =>
        {
            return InventoryManager.Instance.RemoveItem(itemName, count);
        });

        _currentStory.BindExternalFunction("HasItem", (string itemName) =>
        {
            return InventoryManager.Instance.HasItem(itemName);
        });
    }
}
