using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogbox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color hightlightedColor;
    [SerializeField] Text dialogText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;
    [SerializeField] List<Text> actionText;
    [SerializeField] List<Text> moveText;
    [SerializeField] Text PPText;
    [SerializeField] Text typeText;

    public void  SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
    }


    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    public void EnableactionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }
    public void EnablemoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i=0; i<actionText.Count; ++i)
        {
            if (i ==selectedAction)
                actionText[i].color = hightlightedColor;
            else
                actionText[i].color = Color.black;
        }
    }

    public void UpdateMoveSelection(int selectedMove, Move move)
    {
        for (int i=0; i<moveText.Count; ++i)
        {
            if(i == selectedMove)
                moveText[i].color = hightlightedColor;
                else
                moveText[i].color = Color.black;
        }
    }

    public void SetMoveNames(List<Move> moves)
    {
        for (int i = 0; i < moveText.Count; ++i)
        {
            if (i < moves.Count)
                moveText[i].text = moves[i].Base.name;
            else
                moveText[i].text = "-";
        }

        // Display PP and type for the first move if there are any moves
        if (moves.Count > 0)
        {
            PPText.text = $"PP {moves[0].PP}/{moves[0].Base.PP}";
            typeText.text = moves[0].Base.Type.ToString();
        }
        else
        {
            PPText.text = "PP -";
            typeText.text = "-";
        }
    }
}