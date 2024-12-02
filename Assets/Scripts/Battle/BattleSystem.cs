using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy}
public class BattleSystem : MonoBehaviour
{
  [SerializeField] BattleUnit playerUnit;
  [SerializeField] BattleHud playerHud;
  [SerializeField] BattleUnit enemyUnit;
  [SerializeField] BattleHud enemyHud;
  [SerializeField] Dialogbox dialogBox;

  public event Action<bool> OnBattleOver;

  BattleState state;
  int currentAction;
  int currentMove;

  public void StartBattle()
  {
    StartCoroutine(SetUpBattle());
  }

  public IEnumerator SetUpBattle()
  {
    playerUnit.Setup();
    enemyUnit.Setup();
    playerHud.SetData(playerUnit.Creatures);
    enemyHud.SetData(enemyUnit.Creatures);

    dialogBox.SetMoveNames(playerUnit.Creatures.Moves);

    yield return dialogBox.TypeDialog($"A Wild {enemyUnit.Creatures.Base.name} appeared");
    yield return new WaitForSeconds(1f);

    PlayerAction();
  }

  void PlayerAction()
  {
    state = BattleState.PlayerAction;
    StartCoroutine(dialogBox.TypeDialog("Choose action"));
    dialogBox.EnableactionSelector(true);
  }

  void PlayerMove()
  {
    state = BattleState.PlayerMove;
    dialogBox.EnableactionSelector(false);
    dialogBox.EnableDialogText(false);
    dialogBox.EnablemoveSelector(true);
  }

  //Player Creatures turn in battle
  IEnumerator PerformPlayerMove()
  {
    state = BattleState.Busy;

    // Ensure the currentMove is within bounds
    if (currentMove < 0 || currentMove >= playerUnit.Creatures.Moves.Count)
    {
        Debug.LogWarning("Invalid move selected.");
        yield break;
    }

    var move = playerUnit.Creatures.Moves[currentMove];
    yield return dialogBox.TypeDialog($"{playerUnit.Creatures.Base.name} used {move.Base.name}");

    yield return new WaitForSeconds(1f);

    // Enemy takes damage from the player's move
    bool isFainted = enemyUnit.Creatures.TakeDamage(move, playerUnit.Creatures);

    // Update enemy HUD to show new HP
    yield return enemyHud.UpdateHP();

    // Check if the enemy has fainted
    if (isFainted)
    {
      //Notify Creature has Fainted
        yield return dialogBox.TypeDialog($"{enemyUnit.Creatures.Base.name} fainted!");
        
      yield return new WaitForSeconds(2f);
      OnBattleOver(true);
    }
    else
    {
        // Proceed with enemy's turn
        StartCoroutine(EnemyMove());
    }
  }

  //Enemy Creatures turn in battle
  IEnumerator EnemyMove()
{
    state = BattleState.EnemyMove;

    // Get a random move from the enemy's move list
    var move = enemyUnit.Creatures.GetRandomMove();
    if (move == null)
    {
        Debug.LogWarning("Enemy has no moves available.");
        yield break;
    }

    // Show dialog for the enemy's move
    yield return dialogBox.TypeDialog($"{enemyUnit.Creatures.Base.name} used {move.Base.name}");

    yield return new WaitForSeconds(1f);

    // Player takes damage from the enemy's move
    bool isFainted = playerUnit.Creatures.TakeDamage(move, enemyUnit.Creatures);

    // Update player HUD to show new HP
    yield return playerHud.UpdateHP();

    // Check if player has fainted
    if (isFainted)
    {
      //Notify Creature has Fainted
        yield return dialogBox.TypeDialog($"{playerUnit.Creatures.Base.name} fainted!");
        
        yield return new WaitForSeconds(2f);
        OnBattleOver(false);
    }
    else
    {
        // Return control to player
        PlayerAction();
    }
}

  public void HandleUpdate()
  {
    if (state == BattleState.PlayerAction)
    {
      HandleActionSelection();
    }
    else if (state == BattleState.PlayerMove)
    {
      HandleMoveSelection();
    }
  }
  
  void HandleActionSelection()
  {
    //Arrow Keys select event
    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
      if (currentAction < 1)
        ++currentAction;
    }
    else if (Input.GetKeyDown(KeyCode.UpArrow))
    {
      if (currentAction > 0)
          --currentAction;
    }

    dialogBox.UpdateActionSelection(currentAction);

    if (Input.GetKeyDown(KeyCode.Z))
    {
      if (currentAction == 0)
      {
        //fight selected
        PlayerMove();
      }
      else  if (currentAction == 1)
      {
        //run selected
        StartCoroutine(TryToRun());
      }
    }
  }
  IEnumerator TryToRun()
{
    state = BattleState.Busy;

    yield return dialogBox.TypeDialog("Attempting to run...");
    yield return new WaitForSeconds(2f);

    // 50% chance to escape
    bool escapeSuccessful = UnityEngine.Random.value < 0.5f;

    if (escapeSuccessful)
    {
        // Notify the player that they successfully escaped
        yield return dialogBox.TypeDialog("You managed to escape!");
        yield return new WaitForSeconds(2f);
        
        // End the battle, using the OnBattleOver event
        OnBattleOver?.Invoke(false); // false indicates battle ended without a win
    }
    else
    {
        // Notify the player that escape failed
        yield return dialogBox.TypeDialog("You couldn't escape!");
        yield return new WaitForSeconds(2f);

        // Enemy takes its turn after failed escape
        StartCoroutine(EnemyMove());
    }
}

  void HandleMoveSelection()
  {
    //Arrow Keys moves to select move
    if (Input.GetKeyDown(KeyCode.RightArrow))
    {
      if (currentMove < playerUnit.Creatures.Moves.Count - 1)
        ++currentMove;
    }
    else if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
      if (currentMove > 0)
          --currentMove;
    }
    else if (Input.GetKeyDown(KeyCode.DownArrow))
    {
      if (currentMove < playerUnit.Creatures.Moves.Count - 2)
        currentMove += 2;
    }
    else if (Input.GetKeyDown(KeyCode.UpArrow))
    {
      if (currentMove > 1)
          currentMove -= 2;
    }

     dialogBox.UpdateMoveSelection(currentMove, playerUnit.Creatures.Moves[currentMove]);

    if (Input.GetKeyDown(KeyCode.Z))
    {
      dialogBox.EnablemoveSelector(false);
      dialogBox.EnableDialogText(true);
      StartCoroutine(PerformPlayerMove());
    }

  }
}
