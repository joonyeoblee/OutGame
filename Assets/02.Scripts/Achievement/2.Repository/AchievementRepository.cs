using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementRepository : MonoBehaviour
{
    private static readonly string SAVE_KEY = "ACHIEVEMENT_DTO_";

    public void Save(List<AchievementDTO> dataList)
    {
        AchievementSaveDataList saveData = new AchievementSaveDataList();
        saveData.DataList = dataList.ConvertAll(data => new AchievementSaveData
        {
            ID = data.ID,
            CurrentValue = data.CurrentValue,
            RewardClaimed = data.RewardClaimed
        });
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public List<AchievementDTO> Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return null;
        string json = PlayerPrefs.GetString(SAVE_KEY);

        AchievementSaveDataList saveDataList =
            JsonUtility.FromJson<AchievementSaveDataList>(json);

        return saveDataList.DataList.ConvertAll(data => new AchievementDTO(
            data.ID, data.CurrentValue, data.RewardClaimed));
    }
}



[Serializable]
public class AchievementSaveDataList
{
    public List<AchievementSaveData> DataList;
}
