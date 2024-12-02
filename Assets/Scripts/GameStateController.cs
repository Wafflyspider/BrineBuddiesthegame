using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Bag }

public class GameStateController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] InventoryUI inventoryUI;

    private GameState state;

    private void Start()
    {
        
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;

        
        state = GameState.FreeRoam;

       
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
        inventoryUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.FreeRoam:
                playerController.HandleUpdate();

                
                if (Input.GetKeyDown(KeyCode.I))
                {
                    OpenInventory();
                }
                break;

            case GameState.Battle:
                battleSystem.HandleUpdate();
                break;

            case GameState.Bag:
                inventoryUI.HandleUpdate();

                
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.I))
                {
                    CloseInventory();
                }
                break;
        }
    }

    private void StartBattle()
    {
        state = GameState.Battle;

        
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        battleSystem.StartBattle();
    }

    private void EndBattle(bool won)
    {
        state = GameState.FreeRoam;

       
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void OpenInventory()
    {
        state = GameState.Bag;

       
        inventoryUI.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);
    }

    private void CloseInventory()
    {
        state = GameState.FreeRoam;

        
        inventoryUI.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }
}