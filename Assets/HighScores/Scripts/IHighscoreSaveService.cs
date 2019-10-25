using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHighscoreSaveService {

    void Save(List<HighScoreContainer> saveData);

}
