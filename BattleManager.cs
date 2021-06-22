using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
//using ExcelDataReader;
using UnityEngine.UI;
public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
   
    public List<Cell> AllMapCells;
    public List<Vector2> AllMapPos;
    public List<Vector2> EmptyMapPos;
    public List<Cell> MoveRangeCells;
    public List<Cell> AttackRangeCells;

    public List<ChessMan> AllChessMan;

    public List<Cell> PathCells;
    public List<Vector2> Path;
    public ChessMan SelectChessMan;

    public int player1;
    public int player2;
    public int Rounds;
    public Text RongdsShow;
    void Start()
    {
        
        SelectChessMan = null;
        Rounds = 0;
        RongdsShow.text = "回合数:" + Rounds;

        foreach (var Cell in GameObject.FindGameObjectsWithTag("Cell"))
        {
            AllMapCells.Add(Cell.GetComponent<Cell>());
            AllMapPos.Add(new Vector2(Cell.GetComponent<Cell>().Col, Cell.GetComponent<Cell>().Row));
            EmptyMapPos.Add(new Vector2(Cell.GetComponent<Cell>().Col, Cell.GetComponent<Cell>().Row));
        }

        foreach (var ChessMan in GameObject.FindGameObjectsWithTag("ChessMan"))
        {
            AllChessMan.Add(ChessMan.GetComponent<ChessMan>());
            EmptyMapPos.Remove(new Vector2(ChessMan.GetComponent<ChessMan>().Col, ChessMan.GetComponent<ChessMan>().Row));
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CloseMoveRange();
            foreach(var ChessMan in AllChessMan)
            {
                ChessMan.CancelSelect();
            }
            SelectChessMan = null;
            CloseAttackRange();
        }
    }

    public void ShowMoveRange(int Col, int Row, int Range)
    {
        for (int i = 0; i < AllMapCells.Count; i++)
        {
            if(FindInAllChessManPos(AllMapCells[i].Col, AllMapCells[i].Row) && AllMapCells[i].Col != Col && AllMapCells[i].Row != Row)
            {
                continue;
            }
            
            if (Range >= (Math.Abs(Col - AllMapCells[i].Col) + Math.Abs(Row - AllMapCells[i].Row)))
            {
                Path = AStarPathFinder.FindPath(new Vector2(Col, Row), new Vector2(AllMapCells[i].Col, AllMapCells[i].Row), EmptyMapPos);

                if ((Path.Count - 1 <= Range )&& (Path.Count > 0))
                {
                    MoveRangeCells.Add(AllMapCells[i]);
                }
                Path.Clear();
            }
        }
        for (int i = 0; i < MoveRangeCells.Count; i++)
        {
            MoveRangeCells[i].SetCellMoveRangeMaterial();
        }
    }
    public void CloseMoveRange()
    {
        for (int i = 0; i < MoveRangeCells.Count; i++)
        {
            MoveRangeCells[i].SetCellOriginalMaterial();
        }
        MoveRangeCells.Clear();
 
    }

    public void ShowAttackRange(int Col, int Row)
    {
        List<Vector2> NeighborCell = new List<Vector2>();
        NeighborCell.Add(new Vector2(Col, Row) + new Vector2(-1, 0));
        NeighborCell.Add(new Vector2(Col, Row) + new Vector2(1, 0));
        NeighborCell.Add(new Vector2(Col, Row) + new Vector2(0, 1));
        NeighborCell.Add(new Vector2(Col, Row) + new Vector2(0, -1));

        foreach (Vector2 EachCell in NeighborCell)
        {            
            Cell TmpCell = AllMapCells.Find(
                delegate (Cell tCell)
                {
                    return tCell.Col == EachCell.x && tCell.Row == EachCell.y;
                });
            if(AllMapCells.Contains(TmpCell))
            {
                TmpCell.SetCellAttackRangeMaterial();
                AttackRangeCells.Add(TmpCell);
            } 
        }
    }
    public void CloseAttackRange()
    {
        foreach(var TmpCell in AttackRangeCells)
        {
            TmpCell.SetCellOriginalMaterial();
        }
        AttackRangeCells.Clear();
    }
    private bool FindInAllChessManPos(int Col, int Row)
    {
        foreach (var ChessMan in AllChessMan)
        {
            if ((ChessMan.Col == Col) && (ChessMan.Row == Row))
            {
                return true;
            }
        }
        return false;
    }

    public bool CellInAttackRange(Cell InCell)
    {
        return AttackRangeCells.Contains(InCell);
    }
    public bool InMoveRange(Cell InCell)
    {
        foreach (var TmpCell in MoveRangeCells)
        {
            if (InCell.transform.position.x == TmpCell.Col && InCell.transform.position.z == TmpCell.Row)
            {
                return true;
            }
        }
        return false;
    }
   
    public void ShowPath()
    {
        foreach (var V2dCell in Path)
        {
            for (int i = 0; i < AllMapCells.Count; i++)
            {
                if ((V2dCell.x == AllMapCells[i].Col) && (V2dCell.y == AllMapCells[i].Row))
                {
                    PathCells.Add(AllMapCells[i]);
                    AllMapCells[i].SetCellPathMaterial();
                }
            }
        }
    }
    public void ClosePath()
    {
        for (int i = 0; i < PathCells.Count; i++)
        {
            PathCells[i].SetCellOriginalMaterial();
        }
        PathCells.Clear();
        Path.Clear();

    }
    public void SetSelctedChessMan(ChessMan Chess)
    {
        SelectChessMan = Chess;
    }
    public void UpdateEmptyMapPos()
    {
        EmptyMapPos.Clear();
        foreach (var Cell in GameObject.FindGameObjectsWithTag("Cell"))
        {
            EmptyMapPos.Add(new Vector2(Cell.GetComponent<Cell>().Col, Cell.GetComponent<Cell>().Row));
        }

        foreach (var ChessMan in GameObject.FindGameObjectsWithTag("ChessMan"))
        {
            EmptyMapPos.Remove(new Vector2(ChessMan.GetComponent<ChessMan>().Col, ChessMan.GetComponent<ChessMan>().Row));
        }

    }

    public ChessMan GetChessManByCell(Cell InCell)
    {
        for(int i = 0; i< AllChessMan.Count; i++)
        {
            if (InCell.Col == AllChessMan[i].Col && InCell.Row == AllChessMan[i].Row)
            {
                return AllChessMan[i];
            }
        }
        return null;
    }

    public void TurnEnd()
    {
        Debug.Log("Turn:" + Rounds);
        Rounds++;
        RongdsShow.text = "回合数:" + Rounds;
        foreach (var Chess in AllChessMan)
        {
            Chess.Reset();
        }
        ClosePath();
        CloseAttackRange();
        CloseMoveRange();

        FriendsTurn();
        EnemysTurn();
    }
    public void FriendsTurn()
    {
        Debug.Log("FriendsTurn" + Rounds);
    }

    public void EnemysTurn()
    {
        Debug.Log("EnemysTurn" + Rounds);
        StartCoroutine(EnemyAction());
        //EnemyAction();
    }
    public bool InAttacking()
    {
        if(null == SelectChessMan)
        {
            return false;
        }
        return SelectChessMan.HasAttackedFlag == false && SelectChessMan.HasMoveFlag == true;
    }
    public bool ChessInAttackRange(int Col, int Row)
    {
        if(null == AttackRangeCells.Find(
            delegate (Cell tCell)
            {
                return tCell.Col == Col && tCell.Row == Row;
            }))
        {
            return false;
        }
        return true;
    }

    public void SkipMove()
    {
        if(SelectChessMan == null)
        {
            return;
        }
        if(SelectChessMan.CharaCapability.Faction == "Enemy")
        {
            return;
        }
        if(SelectChessMan.HasMoveFlag == false)
        {
            SelectChessMan.HasMoveFlag = true;
        }
        CloseMoveRange();
        ShowAttackRange(SelectChessMan.Col, SelectChessMan.Row);
    }

    IEnumerator EnemyAction()
    {
        for (int i = 0; i< AllChessMan.Count; i++)
        {
            if(AllChessMan[i].CharaCapability.Faction == "Enemy")
            {
               
                SelectChessMan = AllChessMan[i];
                
                ChessMan AimChessman = GetAimChessMan();
                if(AimChessman == null)
                {
                    continue;
                }
                if(Path.Count > 1)
                {
                    ShowPath();
                    SelectChessMan.MoveByPath(Path);
                    Path.Clear();
                }
                SelectChessMan.HasMoveFlag = true;
                ShowAttackRange(SelectChessMan.Col, SelectChessMan.Row);
                if (ChessInAttackRange(AimChessman.Col, AimChessman.Row))
                {
                    SelectChessMan.AttackEnemy(AimChessman);                    
                }
                SelectChessMan.HasAttackedFlag = true;
                CloseAttackRange();
                SelectChessMan.Reset();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    
    public ChessMan GetAimChessMan()
    {
        int MinPathNum = 100;
        List<Vector2> MinPath = new List<Vector2>();
        ChessMan MinPathAim = null;
        foreach (var ChessMan in AllChessMan)
        {
            if (ChessMan.CharaCapability.Faction == "Player")
            {
                EmptyMapPos.Add(new Vector2(ChessMan.Col, ChessMan.Row));
                Path = AStarPathFinder.FindPath(new Vector2(SelectChessMan.Col, SelectChessMan.Row), new Vector2(ChessMan.Col, ChessMan.Row), EmptyMapPos);
                UpdateEmptyMapPos();
                if (Path.Count < MinPathNum && Path.Count != 0)
                {
                    MinPathNum = Path.Count;
                    MinPathAim = ChessMan;
                    MinPath = new List<Vector2>(Path.ToArray());
                }
            }
        }

        if(MinPathNum == 100)
        {
            return null;
        }

        if(SelectChessMan.MoveAbility < MinPath.Count -1)
        {
            int pathNum = SelectChessMan.MoveAbility + 1;
            MinPath.RemoveRange(pathNum, MinPath.Count - pathNum);                
            
        }
        else if(MinPath.Count > 0)
        {
            MinPath.RemoveAt(MinPath.Count - 1);
        }

        Path = new List<Vector2>(MinPath.ToArray());
        return MinPathAim;
    }

    //player
    public void ClickChessMan(ChessMan chess)
    {
        if(chess.CharaCapability.Faction == "Player")
        {
            CloseMoveRange();
            CloseAttackRange();
            SetSelctedChessMan(chess);
            if (false == chess.HasMoveFlag)
            {
                ShowMoveRange(chess.Col, chess.Row, chess.MoveAbility);
            }
            else if (false == chess.HasAttackedFlag)
            {
                ShowAttackRange(chess.Col, chess.Row);
            }
        }
        else if(SelectChessMan != null)
        {
            if(chess.CharaCapability.Faction == "Enemy" && ChessInAttackRange(chess.Col, chess.Row)
                    && SelectChessMan.HasAttackedFlag == false )
            {
                SelectChessMan.AttackEnemy(chess);
                CloseAttackRange();
            }   
        }
                
    }
    public void ClickCellDown(Cell cell)
    {
        if (SelectChessMan == null)
        {
            return;
        }
        if (InMoveRange(cell) && SelectChessMan.HasMoveFlag == false)
        {

            Path = AStarPathFinder.FindPath(new Vector2(SelectChessMan.Col, SelectChessMan.Row), new Vector2(cell.Col, cell.Row), EmptyMapPos);
            ShowPath();
        }
    }
    public void ClickCellUp(Cell cell)
    {
        if(SelectChessMan == null)
        { 
            return; 
        }
        if(InMoveRange(cell) && SelectChessMan.HasMoveFlag == false)
        {
            
            SelectChessMan.MoveByPath(Path);
            Path.Clear();
        }
        
    }

}