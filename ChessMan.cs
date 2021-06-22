using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct T_CharaCapability
{
    public string Name;
    public string Arms;
    public string Faction;
    public int StratLevel;

    public int MaxHP;
    public int MaxMP;
    public int ATK;
    public int DEF;
    public int AGI;
    public int CRI;
    public int PAR;
    public int CAT;
    public int LCK;

    public int ATKK;
    public int DEFK;
    public int MONK;
    public int PLAK;
    public int FORK;
}

public class ChessMan : MonoBehaviour
{
    // Start is called before the first frame update
    public bool SelectedFlag;
    public bool HasMoveFlag;
    public bool HasAttackedFlag;
    public T_CharaCapability CharaCapability;

    private BattleManager mManager;
    //private CfgManager mCfgManager;

    public Material SelectedMat;
    public Material UnSelectedMat;

    private float MoveSpeed = 10;
    private List<Vector2> MovePath;
    public int MoveAbility;

    public int Col;//x direction
    public int Row;//z direction

    public int CurrentHP;
    public int CurrentMP;


    void Start()
    {
        mManager = GameObject.Find("GameManager").GetComponent<BattleManager>();
        
        InitCharaCapability();

        InitCharaStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        mManager.ClickChessMan(this);
    }

    public void AttackEnemy(ChessMan EnemyChess)
    {
        EnemyChess.CurrentHP -= CharaCapability.ATK;
        Debug.Log("Attack " + EnemyChess.name + "Damege " + CharaCapability.ATK);
        HasAttackedFlag = true;
        if(EnemyChess.CurrentHP <= 0)
        {
            EnemyChess.Death();
            List<Vector2> Path = new List<Vector2>();
            Path.Add(new Vector2(Col, Row));
            Path.Add(new Vector2(EnemyChess.Col, EnemyChess.Row));
            
            MoveByPath(Path);
        }
        mManager.CloseAttackRange();

    }
    
    public void CancelSelect()
    {
        if (true == SelectedFlag)
        {
            this.GetComponent<Renderer>().material = UnSelectedMat;
            SelectedFlag = false;
        }
    }

   

    public void Reset()
    {
        SelectedFlag = false;
        HasMoveFlag = false;
        HasAttackedFlag = false;
        
        this.GetComponent<Renderer>().material = UnSelectedMat;
    }

    private void InitCharaCapability()
    {
        if (name.Contains("Enemy"))
        {
            CharaCapability.Faction = "Enemy";
            CharaCapability.ATK = 20;
            
        }
        else
        {
            CharaCapability.Faction = "Player";
            CharaCapability.ATK = 60;
        }

        CharaCapability.MaxHP = 100;
        
        CurrentHP = CharaCapability.MaxHP;
        CurrentMP = CharaCapability.MaxMP;
    }

    private void InitCharaStatus()
    {
        SelectedFlag = false;
        HasMoveFlag = false;
        HasAttackedFlag = false;

        MoveAbility = 5;
        Col = Convert.ToInt32(transform.position.x);
        Row = Convert.ToInt32(transform.position.z);
        MovePath = new List<Vector2>();
    }

    private void Death()
    {
        Debug.Log(name + " Death!");
        mManager.AllChessMan.Remove(this);
        mManager.UpdateEmptyMapPos();
        Destroy(gameObject);        
    }

    public void MoveByPath(List<Vector2> Path)
    {
        if (Path.Count > 1)
        {
            MovePath = new List<Vector2>(Path.ToArray());
            transform.position = new Vector3(MovePath[0].x, transform.position.y, MovePath[0].y);
            Col = Convert.ToInt32(MovePath[MovePath.Count - 1].x);
            Row = Convert.ToInt32(MovePath[MovePath.Count - 1].y);
            mManager.UpdateEmptyMapPos();
            StartCoroutine(MoveOnPath());
            
        }
    }

    IEnumerator MoveOnPath()
    {
        
        foreach (var point in MovePath)
            yield return StartCoroutine(MoveToPosition(point));
        mManager.ClosePath();
        if (HasAttackedFlag == false && "Player" == CharaCapability.Faction)
        {
            mManager.CloseMoveRange();
            mManager.ShowAttackRange(Col, Row);            
        }
        mManager.SelectChessMan.HasMoveFlag = true;

        yield return null;
    }

    IEnumerator MoveToPosition(Vector2 target)
    {
        Vector3 Target3D = new Vector3(target.x, transform.position.y, target.y);
        while (transform.position != Target3D)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target3D, MoveSpeed * Time.deltaTime);
            if((Target3D - transform.position).magnitude <= MoveSpeed * Time.deltaTime)
            {
                transform.position = Target3D;
            }
            yield return 0;
        }
    }
}
