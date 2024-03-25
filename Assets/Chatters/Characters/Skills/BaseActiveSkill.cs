namespace Chatters.Characters.Skills
{
    public enum Range
    {
        Melee,
        Range
    }

    public enum AttackType
    {
        Phys,
        Magic
    }

    [System.Flags]
    public enum WeaponType
    {
        Range,
        Staff,
        Melee
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