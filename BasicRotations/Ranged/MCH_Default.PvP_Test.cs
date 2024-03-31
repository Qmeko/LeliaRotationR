namespace DefaultRotations.Ranged;

[Rotation("Lelia's PvPTest", CombatType.PvP, GameVersion = "6.58")]
[SourceCode(Path = "main/DefaultRotations/Ranged/MCH_Default.PvP_Test.cs")]
public sealed class MCH_LeliaDefaultPvP_Test : MachinistRotation
{

    public static IBaseAction MarksmansSpitePvP { get; } = new BaseAction((ActionID)29415,false);
    public static IBaseAction BishopAutoturretPvP2 { get; } = new BaseAction((ActionID)29412,false);


    protected override bool GeneralGCD(out IAction? act)
    {

        if (BishopAutoturretPvP.CanUse(out act, usedUp: true)) return true;

        //if (!Player.HasStatus(true, StatusID.Guard))
        //{
        //    act = BishopAutoturretPvP2;
        //    return true;
        //}

        return base.GeneralGCD(out act);
    }


    protected override bool AttackAbility(out IAction? act)
    {

        if (BishopAutoturretPvP2.CanUse(out act, usedUp: true)) return true;

        if (!Player.HasStatus(true,StatusID.Guard))
        {
            act = BishopAutoturretPvP2;
            return true;
        }

       
        return base.AttackAbility(out act);
    }
}
