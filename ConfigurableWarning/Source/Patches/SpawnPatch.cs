using ConfigurableWarning.API;
using ConfigurableWarning.Settings;
using CurvedUI;
using HarmonyLib;
using pworld.Scripts.Extensions;
using UnityEngine;
using Zorro.Core;

namespace ConfigurableWarning.Patches;

/// <summary>
///     Spawning mechanic patches
/// </summary>
[HarmonyPatch]
public class SpawnPatch {
    /// <summary>
    ///     Patches extra spawn budgets to be between the day one value
    ///     and day three value. This will interpolate the budget to be spread
    ///     across all days.
    /// </summary>
    /// <param name="day">The day to calculate for</param>
    /// <param name="__result">The output extra spawn budget</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(BigNumbers), nameof(BigNumbers.GetExtraBudgetForDay))]
    public static void GetExtraBudgetForDay(int day, ref int __result) {
        var bigNumbers = SingletonAsset<BigNumbers>.Instance;
        var max = bigNumbers.day3ExtraMoBudget;
        var min = bigNumbers.day1ExtraMoBudget;
        var diff = max - min;
        var days = OptionsState.Instance.Get<int>(BuiltInSettings.Keys.DaysPerQuota);
        var per = diff / days;

        __result = per * day + min;
    }

    /// <summary>
    ///     Patches the second wave monster budget to account for the potential
    ///     extra days.
    /// </summary>
    /// <param name="day">The day to calculate for</param>
    /// <param name="__result">The output budget</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(BigNumbers), nameof(BigNumbers.GetMonsterBudgetForDayFirstWave))]
    public static void GetMonsterBudgetForDayFirstWave(int day, ref int __result) {
        var bigNumbers = SingletonAsset<BigNumbers>.Instance;
        var days = OptionsState.Instance.Get<int>(BuiltInSettings.Keys.DaysPerQuota);
        var realDay = (day - 1) % days + 1;
        var res = 0;

        GetExtraBudgetForDay(realDay, ref res);

        var anim = Mathf.RoundToInt(bigNumbers.monsterBudgetPerDay.Evaluate(day) + res);
        var amount = SingletonAsset<BigNumbers>.Instance.firstSpawnAmount.PRndRange();

        SingletonAsset<BigNumbers>.Instance.firstWaveSizeWas = amount;
        anim = (bigNumbers.firstWaveSizeWas * anim).ToInt();

        Debug.LogWarning($"1st wave Day: {day} quotaDay: {realDay} budget {anim}");

        __result = anim;
    }

    /// <summary>
    ///     Patches the second wave monster budget to account for the potential
    ///     extra days.
    /// </summary>
    /// <param name="day">The day to calculate for</param>
    /// <param name="__result">The output budget</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(BigNumbers), nameof(BigNumbers.GetMonsterBudgetForSecondWave))]
    public static void GetMonsterBudgetForSecondWave(int day, ref int __result) {
        var bigNumbers = SingletonAsset<BigNumbers>.Instance;
        var days = OptionsState.Instance.Get<int>(BuiltInSettings.Keys.DaysPerQuota);
        var realDay = (day - 1) % days + 1;
        var res = 0;

        GetExtraBudgetForDay(realDay, ref res);

        var anim = Mathf.RoundToInt(bigNumbers.monsterBudgetPerDay.Evaluate(day) + res);
        anim = ((1f - bigNumbers.firstWaveSizeWas) * anim).ToInt();

        Debug.LogWarning($"2nd wave Day: {day} quotaDay: {realDay} budget {anim}");

        __result = anim;
    }
}