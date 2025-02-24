public enum PlayerAnimation
{
    IdleMelee,
    IdlePistol,
    IdleRifle,
    Walk,
    Run,
    Croudge,
    CroudgeWalk,
    AttackMelee,
    AttackMeleeCrit,
    AttackPistol,
    ReloadPistol,
    AttackRifle,
    ReloadRifle,
    Die
}

public static class PlayerAnimationExtensions
{
    private const string PREFIX = "AN_Character_";

    public static string ToAnimationName(this PlayerAnimation animation)
    {
        return PREFIX + animation.ToString();
    }
}

public enum ZombieAnimation
{
    Idle,
    Attack,
    GetHit,
    GetHitCrit,
    Walk,
    Run
}

public static class ZombieAnimationsExtension
{
    private const string PREFIX = "AN_Zombie_";

    public static string ToAnimationName(this ZombieAnimation animation)
    {
        return PREFIX + animation.ToString();
    }
}