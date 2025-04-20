using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using HarmonyLib;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using System.Reflection;
using System;

namespace ChildrenGrowFasterRedux
{
    public class SubModule : MBSubModuleBase
    {
        private Harmony? _harmony;

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            _harmony = new Harmony("ChildrenGrowFasterRedux");
            _harmony.PatchAll();
            InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=CGRR_ufs59VVy}Children Grow Faster Redux loaded succesfully.").ToString(), Colors.Green));
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            if (Campaign.Current is Campaign campaign && campaign.GameMode == CampaignGameMode.Campaign)
            {
                CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarterObject;
                campaignGameStarter.AddBehavior(new ChildrenGrowFasterCampaignBehavior());
            }
        }

        public override void OnGameEnd(Game game)
        {
            var eventField = typeof(CampaignEvents).GetField("DailyTickEvent", BindingFlags.Static | BindingFlags.NonPublic);
            var eventDelegate = (MulticastDelegate)eventField?.GetValue(null);
            if (eventDelegate != null && eventDelegate.GetInvocationList().Length > 0)
            {
                CampaignEvents.DailyTickEvent.ClearListeners(this);
            }
            base.OnGameEnd(game);
        }
    }
}