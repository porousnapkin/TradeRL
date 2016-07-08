using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class CombatTurnOrderView : DesertView
{
    public CombatSingleTurnOrder singleTurnOrderPrefab;
    public Transform singleTurnParent;
    List<CombatSingleTurnOrder> singleTurnsInOrder = new List<CombatSingleTurnOrder>();

    public void AddTurn(List<CombatController> characters)
    {
        var go = GameObject.Instantiate(singleTurnOrderPrefab.gameObject) as GameObject;
        go.transform.SetParent(singleTurnParent);

        var singleTurn = go.GetComponent<CombatSingleTurnOrder>();
        singleTurn.Setup(characters, singleTurnsInOrder.Count);
        singleTurnsInOrder.Add(singleTurn);

        VerifyOrder();
        SetupNames();
    }

    void VerifyOrder()
    {
        for(int i = 0; i < singleTurnsInOrder.Count; i++)
            singleTurnsInOrder[i].transform.SetSiblingIndex(i);
    }

    void SetupNames()
    {
        if (singleTurnsInOrder.Count == 0)
            return;

        singleTurnsInOrder[0].SetHeader("Now");
        for(int i = 1; i < singleTurnsInOrder.Count; i++)
            singleTurnsInOrder[i].SetHeader("Next");
    }

    public void RemoveFirstTurn()
    {
        GameObject.Destroy(singleTurnsInOrder[0].gameObject);
        singleTurnsInOrder.RemoveAt(0);

        VerifyOrder();
        SetupNames();
    }

    public void UpdateTurn(int turnStack, List<CombatController> newCharactersInOrder)
    {
        singleTurnsInOrder[turnStack].Setup(newCharactersInOrder, turnStack);

        VerifyOrder();
        SetupNames();
    }

    public void SetActiveCharacter(CombatController controller)
    {
        singleTurnsInOrder[0].SetCombatControllerActive(controller);
    }
}

public class CombatTurnOrderMediator : Mediator
{
    [Inject]public CombatTurnOrderView view { private get; set; }
    [Inject]public CombatTurnOrderMediated model { private get; set; }

	public override void OnRegister ()
    {
        model.addTurn += view.AddTurn;
        model.updateTurn += view.UpdateTurn;
        model.removeFirstTurn += view.RemoveFirstTurn;
        model.setActiveCharacter += view.SetActiveCharacter;
    }
}

public interface CombatTurnOrderMediated
{
    event System.Action<List<CombatController>> addTurn;
    event System.Action<int, List<CombatController>> updateTurn;
    event System.Action removeFirstTurn;
    event System.Action<CombatController> setActiveCharacter;
}

public interface CombatTurnOrderVisualizer
{
    void AddToTurnOrderDisplayStack(List<CombatController> charactersInOrder);
    void TurnOrderAltered(int stackDepth, List<CombatController> newCharactersInOrder);
    void ClearThisTurnsTurnOrder();
    void SetActiveCharacter(CombatController character);
}

public class CombatTurnOrderVisualizerImpl : CombatTurnOrderVisualizer, CombatTurnOrderMediated
{
    public event System.Action<List<CombatController>> addTurn = delegate { };
    public event System.Action<int, List<CombatController>> updateTurn = delegate { };
    public event System.Action removeFirstTurn = delegate { };
    public event System.Action<CombatController> setActiveCharacter = delegate {};

    public void AddToTurnOrderDisplayStack(List<CombatController> charactersInOrder)
    {
        addTurn(charactersInOrder);
    }

    public void TurnOrderAltered(int stackDepth, List<CombatController> newCharactersInOrder)
    {
        updateTurn(stackDepth, newCharactersInOrder);
    }

    public void ClearThisTurnsTurnOrder()
    {
        removeFirstTurn();
    }
    
    public void SetActiveCharacter(CombatController character)
    {
        setActiveCharacter(character);
    }
}