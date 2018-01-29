
/// 2017.01.27 Clearsky Game Framework
/// 
/// parsing CSV 
/// Here we define the first three line of the CSV data sheet as the headers
///  1.names of the data field
///  2.value types of the data field
///  3.description

using UnityEngine;
using System.IO;
using System.Collections;


namespace Clearsky.framework.data
{
    public class CsvTable
    {

        #region useful shotcut

        public static CsvTable FromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                LogUtil.Log(LOG_TAG, filePath + "not found!");
                return null;
            }

            File.ReadAllLines(filePath);

            CsvTable table = new CsvTable();

            return table;
        }


        #endregion

        #region declarations

        private static string LOG_TAG = "CSV_TABLE";


        private static int HEADER_LINE_NAME = 0;//name of the data field
        private static int HEADER_LINE_TYPE = 1;//value type
        private static int HEADER_LINE_DESC = 2;//description

        private static int HEADER_LINE_COUNT = 3;

        private static char SEPERATE_CHAR = ',';


        public enum ValueType
        {
            TypeInteger,
            TypeFloat,
            TypeLong,
            TypeString,
            TypeUnkown
        }
        public struct Header
        {
            public string name;
            public ValueType type;
            public string desc;
        }


        private Header[] headers;
        private string[,] data;

        #endregion

        public CsvTable()
        {

        }

        public void LoadData(string[] rawDataLines)
        {
            //header 
            string[] names = rawDataLines[HEADER_LINE_NAME].Split(SEPERATE_CHAR);
            string[] types = rawDataLines[HEADER_LINE_TYPE].Split(SEPERATE_CHAR);
            string[] desc = rawDataLines[HEADER_LINE_DESC].Split(SEPERATE_CHAR);


            int headerCount = names.Length;//this length 

            headers = new Header[headerCount];

            for (int headerIdx = 0; headerIdx < headerCount; headerIdx++)
            {
                headers[headerIdx] = new Header();


                //type 
                string type = types[headerIdx].ToLower();

                if (type.Equals("int"))
                {
                    headers[headerIdx].type = ValueType.TypeInteger;
                }
                else if (type.Equals("string"))
                {
                    headers[headerIdx].type = ValueType.TypeString;
                }
                else if (type.Equals("float"))
                {
                    headers[headerIdx].type = ValueType.TypeFloat;
                }
                else if (type.Equals("long"))
                {
                    headers[headerIdx].type = ValueType.TypeLong;
                }
                else
                {
                    headers[headerIdx].type = ValueType.TypeUnkown;
                }


                headers[headerIdx].desc = desc[headerIdx];

            }

            //data
            int rowLength = rawDataLines.Length - HEADER_LINE_COUNT;
            int colLenght = headerCount;

            data = new string[rowLength, colLenght];

            for (int rowIdx = HEADER_LINE_COUNT; rowIdx < rawDataLines.Length; rowIdx++)
            {
                string[] rawData = rawDataLines[rowIdx].Split(SEPERATE_CHAR);
                for (int colIdx = 0; colIdx < rawData.Length; colIdx++)
                {
                    if (colIdx < rawData.Length)
                    {
                        data[rowIdx, colIdx] = rawData[colIdx];
                    }
                    else
                    {
                        data[rowIdx, colIdx] = "";
                    }
                }
            }
        }

        public string this[int row, int col]
        {
            get
            {
                return data[row, col];
            }
            set
            {
                data[row, col] = value;
            }
        }

        public int GetInt(int row, int col)
        {
            return int.Parse(data[row, col]);
        }

        public long GetLong(int row, int col)
        {
            return long.Parse(data[row, col]);
        }

        public float GetFloat(int row, int col)
        {
            return float.Parse(data[row, col]);
        }
    }

}
