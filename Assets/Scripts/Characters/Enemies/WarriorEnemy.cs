using UnityEngine;

namespace CardGame.Characters
{
    public class WarriorEnemy : Character, IEnemy
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _defense;

        public void SelectRandomMove()
        {
            switch(Random.Range(1, 3))
            {
                case 1:
                    Attack();
                    break;
                case 2:
                    AddDefense(_defense);
                    break;
            }
        }

        public override void Attack()
        {
            base.Attack();
            _characterHolder.Player.GetDamage(_damage);
        }
    }
}
