using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using TaleWorlds.Localization;

namespace ChildrenGrowFasterRedux
{
    public class SubModuleSettings : AttributeGlobalSettings<SubModuleSettings>
    {
        public override string Id => "ChildrenGrowFasterRedux";
        public override string DisplayName => new TextObject("{=CGFR_YYayWvdK}Children Grow Faster Redux").ToString();
        public override string FolderName => "ChildrenGrowFasterRedux";
        public override string FormatType => "json2";

        [SettingPropertyInteger("{=CGFR_xpz8K0H5}Growth Rate", 1, 100, Order = 0, RequireRestart = false, HintText = "{=CGFR_1ZWhgRlk}Children growth rate. 1 = normal rate; 2 = twice as fast; 3 = three times as fast growth rate and so on. [Default: 2]")]
        [SettingPropertyGroup("{=CGFR_5GjemDpY}Children Growth Rate Settings", GroupOrder = 0)]
        public int GrowthRate { get; set; } = 2;

        [SettingPropertyInteger("{=CGFR_86Zt3waL}When Hero Comes of Age?", 10, 18, Order = 1, RequireRestart = false, HintText = "{=CGFR_Aog3zurZ}Let this mod know when children will come of age. Why? Because if you use 'Bannerlord Expanded - Children Expanded' mod, there is a setting 'Hero Comes of Age', make this setting same as that one. [Default: 18]")]
        [SettingPropertyGroup("{=CGFR_5GjemDpY}Children Growth Rate Settings", GroupOrder = 0)]
        public int WhenHeroComesOfAge { get; set; } = 18;

        [SettingPropertyBool("{=CGFR_88NRzszQ}Affect Player Children Only", Order = 2, RequireRestart = false, HintText = "{=CGFR_s9e0XtHk}Mod only affects growth rate of player children. [Default: false]")]
        [SettingPropertyGroup("{=CGFR_5GjemDpY}Children Growth Rate Settings", GroupOrder = 0)]
        public bool AffectOnlyPlayerChildren { get; set; } = false;

        [SettingPropertyBool("{=CGFR_MYAPs8YR}Instant Children Growth", Order = 3, RequireRestart = false, HintText = "{=CGFR_vMl3gSRM}Children will grow to adulthood instantly. [Default: false]")]
        [SettingPropertyGroup("{=CGFR_5GjemDpY}Children Growth Rate Settings", GroupOrder = 0)]
        public bool DoChildGrowToAdultInstantly { get; set; } = false;

        [SettingPropertyBool("{=CGFR_fdWNNbDe}Affect Everyone", Order = 4, RequireRestart = false, HintText = "{=CGFR_XFONu6ZR}Growth rate affects all children and all adults. [Default: false]")]
        [SettingPropertyGroup("{=CGFR_5GjemDpY}Children Growth Rate Settings", GroupOrder = 0)]
        public bool AffectEveryone { get; set; } = false;


        [SettingPropertyBool("{=CGFR_LAO16M20}Random Traits For Player Children", Order = 0, RequireRestart = false, HintText = "{=CGFR_7aDvIEFA}Enables random traits for player children. [Default: false]")]
        [SettingPropertyGroup("{=CGFR_IXejEQz9}Random Traits", GroupOrder = 1)]
        public bool RandomTraitsForPlayerChildren { get; set; } = false;

        [SettingPropertyInteger("{=CGFR_P7mRMhWb}Trait Chance", 0, 100, Order = 1, RequireRestart = false, HintText = "{=CGFR_eF4AlwLL}Chance of children gaining a random trait (in percent). [Default: 5]")]
        [SettingPropertyGroup("{=CGFR_IXejEQz9}Random Traits", GroupOrder = 1)]
        public int RandomTraitChance { get; set; } = 5;

        [SettingPropertyInteger("{=CGFR_RoM13YnQ}Days Between Next Trait Can Be Added", 1, 100, Order = 2, RequireRestart = false, HintText = "{=CGFR_nOUVxHZI}How many days must pass since the last trait was added before attempting to add the next trait? Keep in mind that for the next trait to be added, the 'Trait Chance' value must be met. [Default: 10]")]
        [SettingPropertyGroup("{=CGFR_IXejEQz9}Random Traits", GroupOrder = 1)]
        public int DaysBetweenNextTraitCanBeAdded { get; set; } = 10;

        [SettingPropertyInteger("{=CGFR_DBnFt8A9}Child Trait Count Check", 1, 10, Order = 3, RequireRestart = false, HintText = "{=CGFR_kJimhLwp}How many traits does a child need to have for this mod to stop checking if a new trait can be added? [Default: 3]")]
        [SettingPropertyGroup("{=CGFR_IXejEQz9}Random Traits", GroupOrder = 1)]
        public int ChildTraitCountCheck { get; set; } = 3;


        [SettingPropertyBool("{=CGFR_daNt7nlf}Enable Adjustable Pregnancy Duration", Order = 0, RequireRestart = false, HintText = "{=CGFR_WcwRhJm0}Must be enabled for the below slider to work. This feature will enable Harmony Postfix patch so please check with your other mods that adjusts pregnancy duration before enabling it. Maybe some other mod already does the same thing. [Default: false]")]
        [SettingPropertyGroup("{=CGFR_vF2aVt8N}Pregnancy Duration (Harmony Patch)", GroupOrder = 2)]
        public bool enablePregnancyDuration { get; set; } = false;

        [SettingPropertyInteger("{=CGFR_yvgrrQcd}Adjust Pregnancy Duration", minValue: 1, maxValue: 100, Order = 1, RequireRestart = false, HintText = "{=CGFR_DlU01AjN}Adjust the number of days it takes for children to be born. [Default: 36]")]
        [SettingPropertyGroup("{=CGFR_vF2aVt8N}Pregnancy Duration (Harmony Patch)", GroupOrder = 2)]
        public int AdjsutPregnancyDuration { get; set; } = 36;


        [SettingPropertyBool("Logging for debugging", Order = 0, RequireRestart = false, HintText = "Logging for debugging. [Default: disabled]")]
        [SettingPropertyGroup("Technical settings", GroupOrder = 3)]
        public bool LoggingEnabled { get; set; } = false;
    }
}