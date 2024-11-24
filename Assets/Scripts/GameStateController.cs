using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle,}

public class GameStateController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;

    private GameState state;

    private void Start()
    {
        // Ensure that the event handlers are properly assigned
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;

        // Initialize the state
        state = GameState.FreeRoam;

        // Ensure initial setup of components
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.FreeRoam:
                playerController.HandleUpdate();
                break;

            case GameState.Battle:
                battleSystem.HandleUpdate();
                break;
        }
    }

    private void StartBattle()
    {
        state = GameState.Battle;

        // Manage UI and game object visibility
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        battleSystem.StartBattle();
    }

    private void EndBattle(bool won)
    {
        state = GameState.FreeRoam;

        // Manage UI and game object visibility
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void ReturnToFreeRoam()
    {
        state = GameState.FreeRoam;

        // Manage UI and game object visibility
        worldCamera.gameObject.SetActive(true);
    }
}