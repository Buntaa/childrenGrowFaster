using System;
using TaleWorlds.CampaignSystem.GameComponents;
using HarmonyLib;
using JetBrains.Annotations;
using TaleWorlds.MountAndBlade;
using MCM.Abstractions.Base.Global;
using TaleWorlds.Library;

namespace ChildrenGrowFasterRedux
{
    [HarmonyPatch(typeof(DefaultPregnancyModel), nameof(DefaultPregnancyModel.PregnancyDurationInDays), MethodType.Getter)]
    public static class AdjustPregnancyDuration
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        public static void Postfix(ref float __result)
        {
            try
            {
                if (GlobalSettings<SubModuleSettings>.Instance.AdjsutPregnancyDuration > 0 && GlobalSettings<SubModuleSettings>.Instance.enablePregnancyDuration) 
                {
                    __result = GlobalSettings<SubModuleSettings>.Instance.AdjsutPregnancyDuration;
                }
            }
            catch (Exception e)
            {
                InformationManager.DisplayMessage(new InformationMessage("Children Grow Faster Redux: " + e.Message));
            }
        }
    }
}