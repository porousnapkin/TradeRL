using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using System;

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
        if (singleTurnsInOrder.Count == 0)
            return;
        GameObject.Destroy(singleTurnsInOrder[0].gameObject);
        singleTurnsInOrder.RemoveAt(0);

        VerifyOrder();
        SetupNames();
    }

    public void UpdateTurn(int turnStack, List<CombatController> newCharactersInOrder)
    {
        if (singleTurnsInOrder.Count == 0)
            return;
        singleTurnsInOrder[turnStack].Setup(newCharactersInOrder, turnStack);

        VerifyOrder();
        SetupNames();
    }

    public void SetActiveCharacter(CombatController controller)
    {
        singleTurnsInOrder[0].SetCombatControllerActive(controller);
    }

    public void Cleanup()
    {
        foreach (var turnOrderView in singleTurnsInOrder)
            GameObject.Destroy(turnOrderView.gameObject);

        singleTurnsInOrder.Clear();
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
        model.cleanup += view.Cleanup;
    }
}

public interface CombatTurnOrderMediated
{
    event Action<List<CombatController>> addTurn;
    event Action<int, List<CombatController>> updateTurn;
    event Action removeFirstTurn;
    event Action<CombatController> setActiveCharacter;
    event Action cleanup;
}

public interface CombatTurnOrderVisualizer
{
    void AddToTurnOrderDisplayStack(List<CombatController> charactersInOrder);
    void TurnOrderAltered(int stackDepth, List<CombatController> newCharactersInOrder);
    void ClearThisTurnsTurnOrder();
    void SetActiveCharacter(CombatController character);
    void Cleanup();
}

public class CombatTurnOrderVisualizerImpl : CombatTurnOrderVisualizer, CombatTurnOrderMediated
{
    public event Action<List<CombatController>> addTurn = delegate { };
    public event Action<int, List<CombatController>> updateTurn = delegate { };
    public event Action removeFirstTurn = delegate { };
    public event Action<CombatController> setActiveCharacter = delegate {};
    public event Action cleanup;

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

    public void Cleanup()
    {
        cleanup();
    }
}