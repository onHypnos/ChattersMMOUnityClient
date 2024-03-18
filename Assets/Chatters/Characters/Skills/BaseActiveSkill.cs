namespace Chatters.Characters.Skills
{
    public enum Range
    {
        Melee,
        Range
    }

    public enum AttackType
    {
        phys,
        magic
    }

    [System.Flags]
    public enum WeaponType
    {
        bow,
        staff,
        melee
    }
    
    public abstract class BaseActiveSkill
    {
        public void Invoke()
        {
            
        }
    }

    public class BaseMeleeAttack : BaseActiveSkill
    {
        
    }

    public class BaseRangeAttack : BaseActiveSkill
    {
        
    }
}