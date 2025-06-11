using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class CSVLoader : MonoBehaviour
{

    private List<AttendanceRewardDTO> _dataList;
    private bool _isLoading = false;
    private bool _isLoaded = false;

    public void Load(string addressableKey, Action onComplete = null)
    {
        if (_isLoaded || _isLoading)
        {
            onComplete?.Invoke();
            return;
        }

        _isLoading = true;
        _ = LoadInternalAsync(addressableKey,onComplete);
    }

    public List<AttendanceRewardDTO> GetAll()
    {
        if (!_isLoaded)
        {
            Debug.LogError("[AttendanceRewardRepository] Data not loaded yet.");
            return new List<AttendanceRewardDTO>();
        }

        return _dataList;
    }

    private async Task LoadInternalAsync(string addressableKey, Action onComplete)
    {
        List<AttendanceRewardDTO> result = new List<AttendanceRewardDTO>();

        var handle = Addressables.LoadAssetAsync<TextAsset>(addressableKey);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("[AttendanceRewardRepository] Failed to load CSV.");
            _dataList = result;
            _isLoaded = true;
            _isLoading = false;
            onComplete?.Invoke();
            return;
        }

        var csvText = handle.Result.text;
        using (var reader = new StringReader(csvText))
        {
            bool isFirstLine = true;
            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                string[] values = line.Split(',');
                if (values.Length < 4) continue;

                var data = new AttendanceRewardDTO
                {
                    ID = values[0],
                    GoalAttendanceDay = int.Parse(values[1]),
                    RewardCurrencyType = EnumTryParse<ECurrencyType>(values[2], ECurrencyType.Gold),
                    RewardAmount = int.Parse(values[3])
                };
                result.Add(data);
            }
        }

        Addressables.Release(handle);

        _dataList = result;
        _isLoaded = true;
        _isLoading = false;
        Debug.Log($"[AttendanceRewardRepository] Loaded {_dataList.Count} entries.");
        onComplete?.Invoke();
    }

    private T EnumTryParse<T>(string value, T defaultValue) where T : struct
    {
        return Enum.TryParse(value, out T result) ? result : defaultValue;
    }
}