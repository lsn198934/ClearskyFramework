
/// 2017.01.27 Clearsky Game Framework
/// 
/// parsing CSV 
/// Here we define the first three lines of the CSV data sheet as the headers
///  1.names of the data field
///  2.value types of the data field
///  3.description

using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text;


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

            List<string> list = new List<string>();
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string item;
                while ((item = streamReader.ReadLine()) != null)
                {
                    list.Add(item);
                }

                streamReader.Close();
            }

            CsvTable table = new CsvTable();
            table.LoadData(list.ToArray());
            return table;
        }

        public static void SaveFile(CsvTable table, string filePath) 
        {
            string[] lines=table.SaveToData();
            using (StreamWriter streamWriter = new StreamWriter(filePath)) 
            { 
                int idx=0;
                while(idx<lines.Length)
                {
                    streamWriter.WriteLine(lines[idx++]);
                }
                streamWriter.Flush();
                streamWriter.Close();
            }
            
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

                headers[headerIdx].name = names[headerIdx];

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

            for (int rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                string[] rawData = rawDataLines[HEADER_LINE_COUNT + rowIdx].Split(SEPERATE_CHAR);

                for (int colIdx = 0; colIdx < colLenght; colIdx++)
                {
                    if (colIdx < rawData.Length - 1)
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


        public string[] SaveToData()
        {
            List<string> lines = new List<string>();
            StringBuilder sb_name = new StringBuilder();
            StringBuilder sb_type = new StringBuilder();
            StringBuilder sb_desc = new StringBuilder();
            for (int i = 0; i < headers.Length; i++)
            {
                sb_name.Append(headers[i].name).Append(",");
                switch (headers[i].type)
                {
                    case ValueType.TypeFloat:
                        sb_type.Append("float").Append(",");
                        break;
                    case ValueType.TypeInteger:
                        sb_type.Append("int").Append(",");
                        break;
                    case ValueType.TypeLong:
                        sb_type.Append("long").Append(",");
                        break;
                    case ValueType.TypeUnkown:
                    case ValueType.TypeString:
                    default:
                        sb_type.Append("string").Append(",");
                        break;
                }
                sb_desc.Append(headers[i].desc).Append(",");
            }

            lines.Add(sb_name.ToString().TrimEnd(','));
            lines.Add(sb_type.ToString().TrimEnd(','));
            lines.Add(sb_desc.ToString().TrimEnd(','));


            long rowCount = data.GetLength(0);
            long colCount = data.GetLength(1);
            for (int i = 0; i < rowCount; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < colCount; j++)
                {
                    sb.Append(data[i, j]).Append(",");
                }
                lines.Add(sb.ToString().TrimEnd(','));
            }

            return lines.ToArray();
        }

        public Header[] Headers
        {
            get { return headers; }
        }

        public int RowCount
        {
            get { return data.GetLength(0); }
        }

        public int ColCount
        {
            get { return data.GetLength(1); }
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

        public int GetColumnIndex(string headerName)
        {
            for (int i = 0; i < headers.Length; i++)
            {
                if (headers[i].name.Equals(headerName)) return i;
            }

            LogUtil.Log(LOG_TAG, "No such headerName:" + headerName);
            return -1;
        }


        public int GetInt(int row, int col, int defaultValue = -1)
        {
            if (headers[col].type != ValueType.TypeInteger) return defaultValue;

            return int.Parse(data[row, col]);
        }

        public long GetLong(int row, int col, long defaultValue = -1L)
        {
            if (headers[col].type != ValueType.TypeLong) return defaultValue;

            return long.Parse(data[row, col]);
        }

        public float GetFloat(int row, int col, float defaultValue = -1f)
        {
            if (headers[col].type != ValueType.TypeFloat) return defaultValue;

            return float.Parse(data[row, col]);
        }
    }

}
