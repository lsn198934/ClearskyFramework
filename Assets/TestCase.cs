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
            Debug.Log(System.DateTime.Now.Millisecond);
            CsvTable table = CsvTable.FromFile(GlobalConfig.GAME_DATA_PATH + "/Instance.csv");
            Debug.Log(System.DateTime.Now.Millisecond);
            Debug.Log(table.GetLong(7, 1));
        }
    }
}
