using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Clearsky.framework;
using Clearsky.framework.data;

public class TestCase : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnGUI() 
    {
        if (GUILayout.Button("TestCSV")) 
        {
            LogUtil.Log(System.DateTime.Now.Millisecond);
            CsvTable table = CsvTable.FromFile(GlobalConfig.GAME_DATA_PATH + "/Instance.csv");
            LogUtil.Log(System.DateTime.Now.Millisecond);
            int index=table.GetColumnIndex("ID");
            LogUtil.LogWarning("index",index);

            CsvTable.SaveFile(table, GlobalConfig.GAME_DATA_PATH + "/Instances.csv");
            
        }
    }
}
