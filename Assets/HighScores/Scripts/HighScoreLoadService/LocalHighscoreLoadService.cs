using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalHighscoreLoadService : MonoBehaviour, IHighscoreLoadService {

    public HighScoreContainer Load() {
        HighScoreContainer highScoreContainer = new HighScoreContainer();

        string path = Application.persistentDataPath + PathData.FilePath;

        if (File.Exists(path)) {
            using (FileStream fs = File.OpenRead(path)) {
                using (BinaryReader reader = new BinaryReader(fs)) {
                    highScoreContainer.Read(reader);
                }
            }
        }

        return highScoreContainer;
    }

}