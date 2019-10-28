using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour {

    [SerializeField]
    private int entriesSavedPerSetting = 10;

    private HighScoreContainer highScoreContainer;

    private IHighscoreSaveService saveService;
    private IHighscoreLoadService loadService;

    public int EntriesSavedPerSetting {
        get { return entriesSavedPerSetting; }
    }

    private void Awake() {
        saveService = GetComponent<IHighscoreSaveService>();
        loadService = GetComponent<IHighscoreLoadService>();
        highScoreContainer = loadService.Load();
    }

    public List<HighScoreEntry> TryGetEntries(GameModeSetting setting) {
        return highScoreContainer.TryGetEntries(setting);
    }

    public void AddEntry(GameModeSetting setting, HighScoreEntry newEntry) {
        highScoreContainer.AddHighScore(setting, newEntry, entriesSavedPerSetting);
        saveService.Save(highScoreContainer);
    }

    private void OnDestroy() {
        saveService.Save(highScoreContainer);
    }

}
