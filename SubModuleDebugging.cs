using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace ChildrenGrowFaster
{
    public class SubModuleDebugging
    {
        [CommandLineFunctionality.CommandLineArgumentFunction("create_and_marry_hero", "debug")]
        public static string CreateAndMarryHero(List<string> strings)
        {
            CreateAndMarryNewHero();
            return "Hero created and married to main hero.";
        }

        private static void CreateAndMarryNewHero()
        {
            TextObject heroName = new TextObject("Debug Wife");
            Clan heroClan = Clan.PlayerClan;
            CultureObject heroCulture = Hero.MainHero.Clan.Culture;
            CharacterObject templateCharacter = CharacterObject.FindFirst(character => character.Culture == heroCulture && character.Occupation == Occupation.Lord);
            Hero newHero = HeroCreator.CreateSpecialHero(templateCharacter, Hero.MainHero.HomeSettlement, Hero.MainHero.Clan, null);
            newHero.SetName(heroName, heroName);
            newHero.CharacterObject.IsFemale = true;
            newHero.Clan = heroClan;
            heroClan.Heroes.Add(newHero);
            newHero.SetNewOccupation(Occupation.Lord);
            newHero.ChangeState(Hero.CharacterStates.Active);
            CampaignEventDispatcher.Instance.OnHeroCreated(newHero, false);

            if (newHero != null)
            {
                Hero.MainHero.Spouse = newHero;
                newHero.Spouse = Hero.MainHero;
            }
        }
    }
}