using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
//using ExcelDataReader;
/*
public class CfgManager : MonoBehaviour
{
    // Start is called before the first frame update

    //public Text m_Message;

    private string CfgExcelFilePath;
    private DataSet CharacterCfgDataSet;

    private Dictionary<int, List<string>> excelDataDic;
    void Start()
    {
        CfgExcelFilePath = Application.dataPath + "/Resources/ExcelDataReader/UserLevel.xls";
        LoadCfgData();
    }

    // Update is called once per frame
    void Update()
    {

    }
  
    private void LoadCfgData()
    {            
        FileStream fileStream = File.Open(CfgExcelFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader dataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
        CharacterCfgDataSet = dataReader.AsDataSet();
        dataReader.Close();
    }

    public void GetCharaCapability(string ID, T_CharaCapability CharaCap)
    {
        for(int Rows = 0;  Rows < CharacterCfgDataSet.Tables["StartStatus"].Rows.Count; Rows++)
        {
            if (CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][0].ToString() == ID)
            {
                CharaCap.Name = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][1].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][2].ToString();
                CharaCap.Faction = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][3].ToString();

                CharacterCfgDataSet.Tables["StartStatus"].Columns.DataType = Type.GetType("System.Int32");
                CharaCap.StratLevel = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][4].v
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][5].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][6].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][7].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][8].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][9].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][10].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][11].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][12].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][13].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][14].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][15].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][16].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][17].ToString();
                CharaCap.Arms = CharacterCfgDataSet.Tables["StartStatus"].Rows[Rows][18].ToString();
                return;

            }
        }
        foreach(DataRow TmpRow in CharacterCfgDataSet.Tables["StartStatus"].Rows)
        {
            foreach (DataColumn dr in CharacterCfgDataSet.Tables["StartStatus"].Columns)
            {

            }
        }


            
        Debug.Log("Cannot Find ID" + ID);
        return;
    }

    public void GetArmsGorwth(string ArmsID, T_CharaCapability CharaCap)
    {
        for (int Rows = 0; Rows < CharacterCfgDataSet.Tables["ArmGrow"].Rows.Count; Rows++)
        {
            if (CharacterCfgDataSet.Tables["ArmGrow"].Rows[Rows][0].ToString() == ArmsID)
            {
                CharaCap = CharacterCfgDataSet.Tables["ArmGrow"].Rows[Rows].Table;
                return;
            }
        }
        Debug.Log("Cannot Find ArmsID" + ArmsID);
        return;
    }

    public void GetHeroGorwth(string HeroName, DataTable CharaCap)
    {
        for (int Rows = 0; Rows < CharacterCfgDataSet.Tables["HeroGrow"].Rows.Count; Rows++)
        {
            if (CharacterCfgDataSet.Tables["HeroGrow"].Rows[Rows][0].ToString() == HeroName)
            {
                CharaCap = CharacterCfgDataSet.Tables["HeroGrow"].Rows[Rows].Table;
                return;
            }
        }
        Debug.Log("Cannot Find " + HeroName);
        return;
    }
}

    
*/