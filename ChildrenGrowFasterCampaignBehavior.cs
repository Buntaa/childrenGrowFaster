using System;
using System.Collections.Generic;
using System.Linq;
using MCM.Abstractions.Base.Global;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace ChildrenGrowFasterRedux
{
    internal class ChildrenGrowFasterCampaignBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents() => CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, OnDailyTickEvent);

        public override void SyncData(IDataStore dataStore) { }

        private readonly SubModuleSettings settings = AttributeGlobalSettings<SubModuleSettings>.Instance ?? new SubModuleSettings();

        private int daysSinceLastPlayerChildTraitAdded = 0;

        private bool playerChildTraitAdded = false;

        private void OnDailyTickEvent()
        {
            LogMessage("OnDailyTickEvent() Called.");

            if (settings.AffectOnlyPlayerChildren)
                ApplyGrowthRateToPlayerChildren();
            else if (settings.AffectOnlyPlayerClanChildren)
                ApplyGrowthRateToPlayerClanChildren();
            else
                ApplyGrowthRateToAllChildren();

            if (settings.AffectEveryone)
                ApplyGrowthRateToEveryoneElse();

            if (settings.RandomTraitsForPlayerChildren)
            {
                if (playerChildTraitAdded && daysSinceLastPlayerChildTraitAdded < settings.DaysBetweenNextTraitCanBeAdded)
                {
                    daysSinceLastPlayerChildTraitAdded++;
                }
                else
                {
                    daysSinceLastPlayerChildTraitAdded = 0;
                    playerChildTraitAdded = false;
                }

                if (!playerChildTraitAdded && MBRandom.RandomInt(0, 100) < settings.RandomTraitChance)
                {
                    GiveRandomTraitToChild();
                    playerChildTraitAdded = true;
                }
            }
        }

        private void ApplyGrowthRateToPlayerChildren()
        {
            LogMessage("ApplyGrowthRateToPlayerChildren() Called.");
            ApplyGrowthRate(hero => hero.IsChild && hero.Age < Campaign.Current.Models.AgeModel.HeroComesOfAge && (hero.Father == Hero.MainHero || hero.Mother == Hero.MainHero));
        }

        private void ApplyGrowthRateToPlayerClanChildren()
        {
            LogMessage("ApplyGrowthRateToPlayerClan Children() Called.");
            ApplyGrowthRate(hero => hero.IsChild && hero.Age < Campaign.Current.Models.AgeModel.HeroComesOfAge && (hero.Clan == Hero.MainHero.Clan));
        }

        private void ApplyGrowthRateToAllChildren()
        {
            LogMessage("ApplyGrowthRateToAllChildren() Called.");
            ApplyGrowthRate(hero => hero.IsChild && hero.Age < Campaign.Current.Models.AgeModel.HeroComesOfAge);
        }

        

        private void ApplyGrowthRateToEveryoneElse()
        {
            ApplyGrowthRate(hero => hero.Age >= Campaign.Current.Models.AgeModel.HeroComesOfAge);
        }

        private void ApplyGrowthRate(Func<Hero, bool> heroFilter)
        {
            LogMessage("ApplyGrowthRate() Called.");

            if (settings.DoChildGrowToAdultInstantly)
            {
                ApplyInstantGrowthToChildren(Hero.AllAliveHeroes.Where(heroFilter));
                return;
            }

            float additionalDaysPerDay = settings.GrowthRate;
            foreach (Hero hero in Hero.AllAliveHeroes.Where(heroFilter))
            {
                hero.SetBirthDay(hero.BirthDay - CampaignTime.Days(additionalDaysPerDay - 1));
            }
        }

        private void ApplyInstantGrowthToChildren(IEnumerable<Hero> heroes)
        {
            LogMessage("ApplyInstantGrowthToChildren() Called.");

            foreach (Hero hero in heroes)
            {
                if (hero.IsChild && hero.Age < Campaign.Current.Models.AgeModel.HeroComesOfAge)
                {
                    CampaignTime currentTime = CampaignTime.Now;
                    CampaignTime newBday = currentTime - CampaignTime.Years(Campaign.Current.Models.AgeModel.HeroComesOfAge);
                    hero.SetBirthDay(newBday);
                    LogMessage($"{hero.Name} has instantly grown into an adult (Age: {hero.Age}).");
                }
            }
        }

        private void GiveRandomTraitToChild()
        {
            LogMessage("GiveRandomTraitToChild() Called.");

            if (Hero.MainHero.Children == null || Hero.MainHero.Children.Count == 0)
                return;

            Hero selectedChild = Hero.MainHero.Children[MBRandom.RandomInt(Hero.MainHero.Children.Count)];

            int traitCount = 0;

            foreach (TraitObject trait in CampaignUIHelper.GetHeroTraits())
            {
                if (selectedChild.GetTraitLevel(trait) != 0)
                    traitCount++;
            }

            if (traitCount > this.settings.ChildTraitCountCheck)
            {
                return;
            }

            TraitObject[] array = new TraitObject[]
            {
                DefaultTraits.Mercy,
                DefaultTraits.Generosity,
                DefaultTraits.Honor,
                DefaultTraits.Valor,
                DefaultTraits.Calculating
            };

            TraitObject traitToAdd = array[MBRandom.RandomInt(array.Length)];
            int traitLevel = MBRandom.RandomInt(-1, 3);
            selectedChild.SetTraitLevel(traitToAdd, traitLevel);
            LogMessage(string.Format("{0} has gained the trait {1} with level {2}!", selectedChild.Name, traitToAdd.Name, traitLevel));
        }

        private void LogMessage(string message)
        {
            if (settings.LoggingEnabled)
            {
                InformationManager.DisplayMessage(new InformationMessage(message, Colors.Yellow));
            }
        }
    }
}