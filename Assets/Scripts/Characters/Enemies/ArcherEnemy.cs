using UnityEngine;

namespace CardGame.Characters
{
    public class ArcherEnemy : Character, IEnemy
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _heal;

        public void SelectRandomMove()
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    Attack();
                    break;
                case 2:
                    Heal(_heal);
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
