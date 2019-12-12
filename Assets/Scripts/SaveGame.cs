using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void save() {
    	int inputNum = 5;
    	File.WriteAllText(Application.dataPath + "save.txt", parseToString(inputNum));
    }

    void load() {
    	string saveString = File.ReadAllText(Application.dataPath + "save.txt");
    	int saveInt = parseFromString(saveString);
    }


    public string parseToString(int saveInt) {
    	string parsedValue = "" + saveInt;
    	return parsedValue;
    }

    public int parseFromString(string saveString) {
    	int parsedValue = int.Parse(saveString);

    	return parsedValue;
    }
}
