using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalHighscoreSaveService : MonoBehaviour, IHighscoreSaveService {

    public void Save(HighScoreContainer saveData) {
        string path = Application.persistentDataPath + PathData.FilePath;

        using (FileStream fs = File.Create(path)) {
            using (BinaryWriter writer = new BinaryWriter(fs)) {
                saveData.Write(writer);
            }
        }
    }

}