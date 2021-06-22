using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    private BattleManager mGameManager;
    // Start is called before the first frame update
    void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickTurnEnd()
    {
        mGameManager.TurnEnd();
    }
    public void OnSkipMove()
    {
        mGameManager.SkipMove();
    }

}
