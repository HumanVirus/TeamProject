using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{



        public string _exp;
        void Start()
        {
            List<Dictionary<string, object>> data = CSVReader.Read("testuser");

            for (var i = 0; i < data.Count; i++)
            {
                Debug.Log("index " + (i).ToString() + " : " + data[i]["id"] + " " + data[i]["name"] + " " + data[i]["default_damage"]);
            }

            _exp = data[1]["name"].ToString();
            Debug.Log(_exp);
            Debug.Log(data);
        }

    
}


