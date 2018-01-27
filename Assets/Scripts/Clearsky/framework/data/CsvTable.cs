
/// 2017.01.27 Clearsky Game Framework
///

using UnityEngine;
using System.IO;
using System.Collections;


namespace Clearsky.framework.data
{
    public class CsvTable
    {

        private static string LOG_TAG = "CSV_TABLE";
        public CsvTable(string filePath)
        {
            if (!File.Exists(filePath))
            {
                LogUtil.Log(LOG_TAG, filePath + "not found!");
                return;
            }
            string[] rawData = File.ReadAllLines(filePath);


        }
    }
}
