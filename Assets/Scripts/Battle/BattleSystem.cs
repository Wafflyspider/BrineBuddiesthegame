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

  BattleState state;
  int currentAction;
  int currentMove;

  private void Start()
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

  private void Update()
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
      }
    }
  }

  void HandleMoveSelection()
  {
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
  }
}
