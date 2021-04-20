using cse3902.HUD;

namespace cse3902.Constants
{
    public class DamageConstants
    {
	    public const float DamageDisableDelay = 1.0f;
	    public const float DamageMaskDelay = 0.05f;

        public const int ArrowDamage = 2;
        public const int BombDamage = 4;
        public const int WoodSwordDamage = 2;
        public const int WhiteSwordDamage = 2 * WoodSwordDamage;
        public const int MagicalSwordDamage = 2* WhiteSwordDamage;
        //public static int SwordDamage = 1;

        public static int SwordDamage
        {
            get
            {
                InventoryManager.SwordType type = InventoryManager.Instance.SwordSlot;
                int damage = 0;
                switch (type)
                {
                    case InventoryManager.SwordType.Wood:
                        damage = WoodSwordDamage;
                        break;
                    case InventoryManager.SwordType.White:
                        damage = WhiteSwordDamage;
                        break;
                    case InventoryManager.SwordType.Magical:
                        damage = MagicalSwordDamage;
                        break;
                }
                return damage;
            }
        }
    }
}
