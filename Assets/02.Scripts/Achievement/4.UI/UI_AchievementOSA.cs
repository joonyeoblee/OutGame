using System;
using System.Collections.Generic;
using Com.ForbiddenByte.OSA.Core;
using Com.ForbiddenByte.OSA.CustomParams;
using Com.ForbiddenByte.OSA.DataHelpers;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///     업적 리스트 OSA 어댑터
/// </summary>
public class UI_AchievementOSA : OSA<UI_AchievementOSA.MyAchievementParams, UI_AchievementOSA.MyAchievementViewsHolder>
{
    public SimpleDataHelper<AchievementDTO> Data { get; private set; }

    [SerializeField]
    private GameObject _achievePopup;
    private UI_Notification _notificationUI;

    private readonly HashSet<string> _alreadyClaimableIds = new HashSet<string>();

    protected override void Start()
    {
        Data = new SimpleDataHelper<AchievementDTO>(this);
        base.Start();

        AchievementManager.Instance.OnDataChanged += Refresh;
        Refresh(); // 최초 호출
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Get();

        Data.ResetItems(achievements);
        
    }
    protected override MyAchievementViewsHolder CreateViewsHolder(int itemIndex)
    {
        MyAchievementViewsHolder holder = new MyAchievementViewsHolder();
        holder.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
        return holder;
    }

    protected override void UpdateViewsHolder(MyAchievementViewsHolder holder)
    {
        AchievementDTO dto = Data[holder.ItemIndex];
        holder.Refresh(dto);

        // if (dto.HasPendingVisualSizeChange)
        // {
        //     holder.MarkForRebuild();
        //     ScheduleComputeVisibilityTwinPass();
        //     dto.HasPendingVisualSizeChange = false;
        // }
    }

    [Serializable]
    public class MyAchievementParams : BaseParamsWithPrefab
    {
        // 필요한 경우 인스펙터에 노출할 설정 가능
    }

    public class MyAchievementViewsHolder : BaseItemViewsHolder
    {
        private UI_AchievementSlot _slot;

        public override void CollectViews()
        {
            base.CollectViews();
            _slot = root.GetComponent<UI_AchievementSlot>();
        }

        public void Refresh(AchievementDTO dto)
        {
            _slot.Refresh(dto);
        }

        public override void MarkForRebuild()
        {
            base.MarkForRebuild();

            ContentSizeFitter fitter = root.GetComponent<ContentSizeFitter>();
            if (fitter != null)
                fitter.enabled = true;
        }

        public override void UnmarkForRebuild()
        {
            ContentSizeFitter fitter = root.GetComponent<ContentSizeFitter>();
            if (fitter != null)
                fitter.enabled = false;

            base.UnmarkForRebuild();
        }
    }
}
