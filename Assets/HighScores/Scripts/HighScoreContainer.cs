using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class HighScoreContainer {

    //Maximum size of the List<HighScoreEntry> is decided by the HighScoreManager
    private Dictionary<GameModeSetting, List<HighScoreEntry>> entryLists;

    public HighScoreContainer() {
        entryLists = CreateHighScoreDictionary();
    }

    private Dictionary<GameModeSetting, List<HighScoreEntry>> CreateHighScoreDictionary() {
        return new Dictionary<GameModeSetting, List<HighScoreEntry>>(new GameModeSettingComparer());
    }

    public List<HighScoreEntry> TryGetEntries(GameModeSetting setting) {
        List<HighScoreEntry> entries;
        entryLists.TryGetValue(setting, out entries);
        return entries;
    }

    public void AddHighScore(GameModeSetting setting, HighScoreEntry newEntry, int maxEntries) {
        List<HighScoreEntry> entries;

        if (entryLists.ContainsKey(setting)) {
            entries = entryLists[setting];

            int entryCount = entries.Count;

            if (entries[entryCount - 1].Points >= newEntry.Points) {
                entries.Add(newEntry);
                entryCount++;
            }
            else {
                for (int i = 0; i < entryCount; i++) {
                    if (entries[i].Points >= newEntry.Points) {
                        continue;
                    }
                    entries.Insert(i, newEntry);
                    entryCount++;
                    break;
                }
            }

            while (entryCount > maxEntries) {
                entries.RemoveAt(maxEntries);
                entryCount--;
            }
        }
        else {
            //no entries yet
            entries = new List<HighScoreEntry>();
            entries.Add(newEntry);
            entryLists[setting] = entries;
        }
    }

    //Custom (de)serialisation methods for the dictionary
    public void Write(BinaryWriter writer) {
        BinaryFormatter formatter = new BinaryFormatter();

        //write size
        writer.Write(entryLists.Count);

        //write key value pairs
        foreach (KeyValuePair<GameModeSetting, List<HighScoreEntry>> pair in entryLists) {
            formatter.Serialize(writer.BaseStream, pair.Key);

            writer.Write(pair.Value.Count);
            foreach (HighScoreEntry entry in pair.Value) {
                formatter.Serialize(writer.BaseStream, entry);
            }
        }
    }

    public void Read(BinaryReader reader) {
        entryLists.Clear();

        BinaryFormatter formatter = new BinaryFormatter();

        //read size
        int dictionarySize = reader.ReadInt32();

        //read key value pairs
        for (int i = 0; i < dictionarySize; i++) {
            GameModeSetting key = (GameModeSetting)formatter.Deserialize(reader.BaseStream);

            int listSize = reader.ReadInt32();
            List<HighScoreEntry> value = new List<HighScoreEntry>();
            for (int j = 0; j < listSize; j++) {
                value.Add((HighScoreEntry)formatter.Deserialize(reader.BaseStream));
            }

            entryLists[key] = value;
        }
    }

}