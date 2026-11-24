using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("UI")]
    [SerializeField] private InteractIcon _interactIcon;

    [Header("Ink")]
    [SerializeField] private string _npcName;
    [SerializeField] private string _inkKnotName;

    [Header("Properties")]
    public bool isPlayerInRange;

    protected void Update()
    {
        if (isPlayerInRange && Keyboard.current.eKey.wasPressedThisFrame && !DialogueManager.Instance.isDialoguePlaying)
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Player entered range of NPC: {_npcName}");
            isPlayerInRange = true;
            _interactIcon.Show(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            DialogueManager.Instance.EndDialogue();
            _interactIcon.Hide();
        }
    }

    public void Interact()
    {
        if (!DialogueManager.Instance.isDialoguePlaying)
        {
            DialogueManager.Instance.StartDialogue(_inkKnotName);
        }
    }
}
