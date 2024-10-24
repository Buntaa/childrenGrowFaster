using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace childrenGrowFaster
{
    public class SubModuleDebugging
    {
        [CommandLineFunctionality.CommandLineArgumentFunction("create_and_marry_hero", "debug")]
        public static string CreateAndMarryHero(List<string> strings)
        {
            if (Hero.MainHero.Spouse == null)
            {
                CreateAndMarryNewHero();
            }
            return "Hero created and married to main hero.";
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("set_child_age", "debug")]
        public static string SetChildAge(List<string> strings)
        {
            try
            {
                if (strings.Count < 2)
                {
                    return "Usage: set_child_age [child_name] [new_age]";
                }
                string childName = strings[0];
                float newAge = float.Parse(strings[1]);

                if (newAge < 0 || newAge > 18)
                {
                    return "Error: Age must be between 0 and 18.";
                }
                Hero targetChild = null;
                foreach (Hero h in Hero.AllAliveHeroes)
                {
                    if ((h.Father == Hero.MainHero || h.Mother == Hero.MainHero) && h.Name.ToString().ToLower().Contains(childName.ToLower()))
                    {
                        targetChild = h;
                        break;
                    }
                }

                if (targetChild == null)
                {
                    return $"Error: Could not find child with name '{childName}'";
                }

                // calculate new birthday
                CampaignTime currentTime = CampaignTime.Now;
                CampaignTime newBday = currentTime - CampaignTime.Years(newAge);
                targetChild.SetBirthDay(newBday);

                return $"Set {targetChild.Name} to age {newAge:F1}.";


            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
        }

        private static void CreateAndMarryNewHero()
        {
            // creating hero stuff (wish it could be more compact ;c )
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