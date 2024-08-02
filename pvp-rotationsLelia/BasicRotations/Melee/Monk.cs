﻿namespace PvPRotations.Melee;
[Rotation("Mnk-PvP:Lelia", CombatType.PvP, GameVersion = "7", Description = "PvP")]
[Api(3)]

public sealed class MNKPvPLelia : MonkRotation
{
    #region Settings
    [RotationConfig(CombatType.PvP, Name = "Use Sprint out of combat?")]
    public bool UseSprint { get; set; } = true;
    #endregion

    protected override bool EmergencyAbility(IAction nextGCD, out IAction? act)
    {
        act = null;
        if (Player.HasStatus(true, StatusID.Guard)) return false;
        if (Player.GetHealthRatio() < 0.7 && RecuperatePvP.CanUse(out act)) return true;

        if (nextGCD.IsTheSameTo(true, PhantomRushPvP) && !Player.HasStatus(true, StatusID.FireResonance) && RisingPhoenixPvP.CanUse(out act, skipAoeCheck: true, usedUp: true)) return true;

        if ((Player.CurrentHp < Player.MaxHp) && RiddleOfEarthPvP.CanUse(out act, skipAoeCheck: true) && HasHostilesInRange) return true;

        return base.EmergencyAbility(nextGCD, out act);
    }

    protected override bool AttackAbility(IAction nextGCD, out IAction? act)
    {
        act = null;
        if (Player.HasStatus(true, StatusID.Guard)) return false;

        if (Player.HasStatus(true, StatusID.EarthResonance))
        {
            if (Player.WillStatusEnd(3, true, StatusID.EarthResonance) && EarthsReplyPvP.CanUse(out act, skipAoeCheck: true)) return true;
            if ((Player.CurrentHp < (Player.MaxHp - 40000)) && EarthsReplyPvP.CanUse(out act, skipAoeCheck: true)) return true;
        }

        return base.AttackAbility(nextGCD, out act);
    }

    protected override bool GeneralAbility(IAction nextGCD, out IAction? act)
    {
        act = null;
        if (Player.HasStatus(true, StatusID.Guard)) return false;
        if (UseSprint) { if (!InCombat && SprintPvP.CanUse(out act)) return true; }

        if (!Player.HasStatus(true, StatusID.WindResonance) && HostileTarget.DistanceToPlayer() < 3 && ThunderclapPvP.CanUse(out act)) return true;

        return base.GeneralAbility(nextGCD, out act);
    }

    protected override bool GeneralGCD(out IAction? act)
    {
        act = null;
        if (Player.HasStatus(true, StatusID.Guard)) return false;

        if (IsLastAbility((ActionID)SixsidedStarPvP.ID) && EnlightenmentPvP.CanUse(out act, skipAoeCheck: true)) return true;

        if (PhantomRushPvP.CanUse(out act, skipAoeCheck: true)) return true;
        if (DemolishPvP.CanUse(out act)) return true;
        if (TwinSnakesPvP.CanUse(out act)) return true;
        if (DragonKickPvP.CanUse(out act)) return true;
        if (SnapPunchPvP.CanUse(out act)) return true;
        if (TrueStrikePvP.CanUse(out act)) return true;
        if (BootshinePvP.CanUse(out act)) return true;

        return base.GeneralGCD(out act);
    }
}