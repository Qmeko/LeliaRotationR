namespace DefaultRotations.Melee;

[Rotation("Lelia's PvP", CombatType.PvP, GameVersion = "6.58",
    Description = "")]
[SourceCode(Path = "main/DefaultRotations/Ranged/MNK_Default.PvP.cs")]

public sealed class MNK_LeliaDefaultPvP : MonkRotation
{ 
    public static IBaseAction MeteodrivePvP { get; } = new BaseAction((ActionID)29485);


    [RotationConfig(CombatType.PvP, Name = "LBを使用します。")]
    private bool LBInPvP { get; set; } = false;

    [Range(1, 100000, ConfigUnitType.None, 1)]

    [RotationConfig(CombatType.PvP, Name = "LB:メテオドライブを行うために必要な敵のHPは？")]
    public int MDValue { get; set; } = 50000;

    [RotationConfig(CombatType.PvP, Name = "万象闘気圏を行うために必要な敵のHPは？")]
    public int ENValue { get; set; } = 50000;

    [RotationConfig(CombatType.PvP, Name = "抜重歩法を個別に使用します。")]
    private bool TCInPvP { get; set; } = false;

    [RotationConfig(CombatType.PvP, Name = "自分が防御中は攻撃を中止します。")]
    private bool GuardCancel { get; set; } = false;

    protected override bool GeneralGCD(out IAction? act)
    {
        act = null;

        if (GuardCancel && Player.HasStatus(true, StatusID.Guard)) return false;
        if (SixsidedStarPvP.CanUse(out act, usedUp: true)) return true;

        if (LBInPvP && HostileTarget?.CurrentHp < MDValue && PhantomRushPvP.IsInCooldown && LimitBreakLevel>=1 && InCombat)
        {
            if (RisingPhoenixPvE.CanUse(out act, skipAoeCheck: true)) return true;
            if (EnlightenmentPvP.CanUse(out act, usedUp: true, skipAoeCheck: true)) return true;
            if (SixsidedStarPvP.CanUse(out act, usedUp: true)) return true;
            if (MeteodrivePvP.CanUse(out act, usedUp: true) && InCombat) return true;
            if (RisingPhoenixPvP.CanUse(out act, skipAoeCheck: true)) return true;
            if (PhantomRushPvP.CanUse(out act, usedUp: true)) return true;
        }

        if ((!HostileTarget?.HasStatus(true, StatusID.Guard) ?? false) && !Player.HasStatus(true, StatusID.Sprint) && !EnlightenmentPvP.Cooldown.IsCoolingDown && HostileTarget?.CurrentHp < ENValue && InCombat)
        {
            if (EnlightenmentPvP.CanUse(out act, skipAoeCheck: true)) return true;
        }

        if (PhantomRushPvP.CanUse(out act, usedUp: true)) return true;
        if (DemolishPvP.CanUse(out act, usedUp: true)) return true;
        if (TwinSnakesPvP.CanUse(out act, usedUp: true)) return true;
        if (DragonKickPvP.CanUse(out act, usedUp: true)) return true;
        if (SnapPunchPvP.CanUse(out act, usedUp: true)) return true;
        if (TrueStrikePvP.CanUse(out act, usedUp: true)) return true;
        if (BootshinePvP.CanUse(out act, usedUp: true)) return true;

        if (GuardCancel && Player.HasStatus(true, StatusID.Guard)) return false;
        if (!Player.HasStatus(true, StatusID.Guard) &&  !Player.HasStatus(true, StatusID.Sprint) &&
            SprintPvP.CanUse(out act, usedUp: true)) return true;

        return base.GeneralGCD(out act);
    }

    protected override bool AttackAbility(out IAction? act)
    {
        if ((LBInPvP && HostileTarget?.CurrentHp < MDValue &&  LimitBreakLevel>=1 && InCombat))
        {
            if (MeteodrivePvP.CanUse(out act, usedUp: true)) return true;
            if (RisingPhoenixPvP.CanUse(out act, skipAoeCheck: true) && InCombat) return true;
            if (SixsidedStarPvP.CanUse(out act, usedUp: true) && InCombat) return true;
            if (RisingPhoenixPvP.CanUse(out act, skipAoeCheck: true) && InCombat) return true;
        }

        if ((!HostileTarget?.HasStatus(true, StatusID.Guard) ?? false) && TCInPvP && HostileTarget && InCombat)
        {
            if (ThunderclapPvP.CanUse(out act, usedUp: true) && InCombat) return true;
        }
        else
        {
            if (ThunderclapPvP.CanUse(out act, usedUp: true) && InCombat) return true;
        }

        if ((!HostileTarget?.HasStatus(true, StatusID.Guard) ?? false) && 
            SixsidedStarPvP.CanUse(out act, usedUp: true) && InCombat) return true;
        if ((!HostileTarget?.HasStatus(true, StatusID.Guard) ?? false) && 
            RisingPhoenixPvP.CanUse(out act, skipAoeCheck: true) && InCombat) return true;
        if ((!HostileTarget?.HasStatus(true, StatusID.Guard) ?? false) && 
            RiddleOfEarthPvP.CanUse(out act, usedUp: true) && InCombat) return true;
        if ((!HostileTarget?.HasStatus(true, StatusID.Guard) ?? false) && 
            Player.HasStatus(true, StatusID.EarthResonance) && InCombat)
        {
            if (EarthsReplyPvP.CanUse(out act, usedUp: true)) return true;
        }

        return base.AttackAbility(out act);
    }
}

