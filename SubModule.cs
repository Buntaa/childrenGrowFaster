using MCM.Abstractions.Base.Global;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;


namespace childrenGrowFaster
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);
            if (game.GameType is Campaign)
            {
                CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, OnDailyTick);
            }
        }

        private void OnDailyTick()
        {
           if (GlobalSettings<SubModuleSettings>.Instance.affectOnlyPlayerChildren)
           {
                applyGrowthRateToPlayerChildren();
           }
           else 
           {
                applyGrowthRateToAllChildren();
           }
           if (GlobalSettings<SubModuleSettings>.Instance.instantGrowth)
           {
                float growthRate = GlobalSettings<SubModuleSettings>.Instance.newGrowthRate;
                foreach (Hero h in Hero.AllAliveHeroes)
                {
                    if (h.IsChild && h.Age < 18 && (h.Father == Hero.MainHero || h.Mother == Hero.MainHero))
                    {
                        h.SetBirthDay(h.BirthDay - CampaignTime.Days(growthRate * 100));
                    }
                }
           }
        }

        private void applyGrowthRateToPlayerChildren()
        {
            float growthRate = GlobalSettings<SubModuleSettings>.Instance.newGrowthRate;
            foreach (Hero h in Hero.AllAliveHeroes)
            {
                if (h.IsChild && h.Age < 18 && (h.Father == Hero.MainHero || h.Mother == Hero.MainHero)) // checks if child is players child. also checks for players gender.
                {
                    h.SetBirthDay(h.BirthDay - CampaignTime.Days(growthRate + 1f)); // +1f triples growth rate essentially
                }
            }
        }

        private void applyGrowthRateToAllChildren()
        {
            float growthRate = GlobalSettings<SubModuleSettings>.Instance.newGrowthRate;
            foreach (Hero h in Hero.AllAliveHeroes)
            {
                if (h.IsChild && h.Age < 18)
                {
                    h.SetBirthDay(h.BirthDay - CampaignTime.Days(growthRate + 1f)); 
                }
            }
        }
    }
}