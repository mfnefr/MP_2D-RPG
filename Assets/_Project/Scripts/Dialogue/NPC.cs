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
            isPlayerInRange = true;
            _interactIcon.Show(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && DialogueManager.Instance.isDialoguePlaying)
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
            DialogueManager.Instance.StartDialogue(_inkKnotName, _npcName);
        }
    }
}
