using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cell : MonoBehaviour
{
    // Start is called before the first frame update

    public int Col;//x direction
    public int Row;//z direction
    public Material OriginalMaterial;
    public Material InMoveRangeMaterial;
    public Material InAttackRangeMaterial;
    public Material SelectedMaterial;
    public Material InPathRangeMaterial;
    public Stack<Material> MatialSaver;


    private BattleManager mManager;
    void Start()
    {
        MatialSaver = new Stack<Material>();
        mManager = GameObject.Find("GameManager").GetComponent<BattleManager>();//Find by Name

       
        Col = Convert.ToInt32(transform.position.x);
        Row = Convert.ToInt32(transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        SetCellSelectMaterial();
        mManager.ClickCellDown(this);
        
    }

    private void OnMouseUp()
    {
        SetCellOriginalMaterial();
        mManager.ClickCellUp(this);
        
    }

    public void SetCellSelectMaterial()
    {
        MatialSaver.Push(GetComponent<Renderer>().material);
        GetComponent<Renderer>().material = SelectedMaterial;
    }
    public void SetCellMoveRangeMaterial()
    {
        MatialSaver.Push(GetComponent<Renderer>().material);
        GetComponent<Renderer>().material = InMoveRangeMaterial;
    }

    public void SetCellAttackRangeMaterial()
    {
        MatialSaver.Push(GetComponent<Renderer>().material);
        GetComponent<Renderer>().material = InAttackRangeMaterial;
    }

    public void SetCellOriginalMaterial()
    {
        
        if(MatialSaver.Count == 0 || MatialSaver == null)
        {
            GetComponent<Renderer>().material = OriginalMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = MatialSaver.Pop();
        }
        
    }
    public void SetCellPathMaterial()
    {
        MatialSaver.Push(GetComponent<Renderer>().material);
        GetComponent<Renderer>().material = InPathRangeMaterial;
    }

}
