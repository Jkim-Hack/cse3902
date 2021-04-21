using cse3902.HUD;

namespace cse3902.Constants
{
    public class DamageConstants
    {
	    public const float DamageDisableDelay = 1.0f;
	    public const float DamageMaskDelay = 0.05f;

        public const int ArrowDamage = 2;
        public const int BombDamage = 4;
        public const int BoomerangDamage = 0;
        public const int MagicBeamDamage = WhiteSwordDamage;
        public const int MagicFireballDamage = MagicalSwordDamage;
        public const int WoodSwordDamage = 2;
        public const int WhiteSwordDamage = 2 * WoodSwordDamage;
        public const int MagicalSwordDamage = 2* WhiteSwordDamage;
        //public static int SwordDamage = 1;

        public static int SwordDamage
        {
            get
            {
                InventoryManager.ItemType type = InventoryManager.Instance.SwordSlot;
                int damage = 0;
                switch (type)
                {
                    case InventoryManager.ItemType.WoodSword:
                        damage = WoodSwordDamage;
                        break;
                    case InventoryManager.ItemType.WhiteSword:
                        damage = WhiteSwordDamage;
                        break;
                    case InventoryManager.ItemType.MagicalSword:
                        damage = MagicalSwordDamage;
                        break;
                }
                return damage;
            }
        }
    }
}
