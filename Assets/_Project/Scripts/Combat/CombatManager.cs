using UnityEngine;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    private enum CombatState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Busy,
        Won,
        Lost
    }

    [Header("Object References")]
    public static CombatManager Instance { get; private set; }
    [SerializeField] private CameraController _cameraController;

    [Header("Combatants")]
    public CombatUnit currentPlayer;
    public CombatUnit currentEnemy;

    [Header("Combat Settings")]
    public bool isCombatActive = false;
    private List<CombatUnit> _turnOrder = new List<CombatUnit>();
    private CombatState _currentState;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _currentState = CombatState.Start;
    }

    private void MakeTurnOrder()
    {
        _turnOrder.Clear();

        int playerInitiative = currentPlayer.RollInitiative();
        int enemyInitiative = currentEnemy.RollInitiative();

        if (playerInitiative >= enemyInitiative)
        {
            _turnOrder.Add(currentPlayer);
            _turnOrder.Add(currentEnemy);
        }
        else
        {
            _turnOrder.Add(currentEnemy);
            _turnOrder.Add(currentPlayer);
        }
 
        Debug.Log("Enemy Initiative: " + enemyInitiative);
        Debug.Log("Player Initiative: " + playerInitiative);
    }

    private void StartCombat()
    {
        if(_turnOrder[0] == currentPlayer)
        {
            _currentState = CombatState.PlayerTurn;
            PlayerTurn();
        }
        else
        {
            _currentState = CombatState.EnemyTurn;
            EnemyTurn();
        }
    }

    private void PlayerTurn()
    {
        StartCoroutine(currentPlayer.AttackSequence(currentEnemy, EndPlayerTurn));
    }

    private void EndPlayerTurn()
    {
        if(IsCombatOver()) return;
        _currentState = CombatState.EnemyTurn;
        EnemyTurn();
    }

    private void EnemyTurn()
    {
        StartCoroutine(currentEnemy.AttackSequence(currentPlayer, EndEnemyTurn));
    }

    private void EndEnemyTurn()
    {
        if(IsCombatOver()) return;
        _currentState = CombatState.PlayerTurn;
        PlayerTurn();
    }

    private bool IsCombatOver()
    {
        if (currentPlayer.currentHealth <= 0)
        {
            _currentState = CombatState.Lost;
            Destroy(currentPlayer.gameObject);
            ExitCombat();
            Debug.Log("Player has been defeated!");
            return true;
        }
        else if (currentEnemy.currentHealth <= 0)
        {
            _currentState = CombatState.Won;
            Destroy(currentEnemy.gameObject);
            ExitCombat();
            Debug.Log("Enemy has been defeated!");
            return true;
        }

        return false;
    }

    public void EnterCombat()
    {
        isCombatActive = true;
        currentPlayer.transform.position = currentPlayer.transform.position;
        _cameraController.ZoomOut();
        MakeTurnOrder();
        StartCombat();
    }

    public void ExitCombat()
    {
        isCombatActive = false;
        _cameraController.ZoomIn();
    }
}

