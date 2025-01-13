namespace CodeClash.Core.BusinessLogicHelpers;

public static class CompetitionRuleHelper
{
    public static float GetCompetitionOverhead(float sentTimeTotalSeconds, float meanWorkingProgramTime) =>
        0.3f * sentTimeTotalSeconds + 0.7f * meanWorkingProgramTime;
}