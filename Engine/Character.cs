using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Engine
{
    internal class CharacterDeathEvent : Engine.Event
    {
        public Character Subject { get; private set; }

        public CharacterDeathEvent(Character character) : base()
        {
            Subject = character;
        }
    }

    internal class CharacterDamageEvent : Engine.Event
    {
        public Character Subject { get; private set; }
        public Character Instigator { get; private set; }
        public int Amount { get; private set; }

        public CharacterDamageEvent(Character instigator, Character subject, int amount) : base()
        {
            Subject = subject;
            Instigator = instigator;
            Amount = amount;
        }
    }

    internal class Character : Actor
    {

        public string Name { get; protected set; }
        public int BaseHealth { get; protected set; }
        public int Power { get; protected set; }
        public int AbilityCooldown { get; protected set; }

        protected int health;
        protected int cooldown;

        public Character() : base()
        {
            health = BaseHealth;
        }
        public virtual void Ability(Character target = null) { }

        public virtual void Attack(Character target)
        {
            if (target != null)
                target.Damage(Power, this);
        }

        public void Damage(int amount, Character instigator)
        {
            PASEventHandler.GetInstance().TriggerEvent(new CharacterDamageEvent(instigator, this, amount));

            health -= amount;

            OnRecieveDamage(amount, instigator);

            if (health <= 0)
            {
                PASEventHandler.GetInstance().TriggerEvent(new CharacterDeathEvent(instigator));
            }
        }

        public virtual void OnRecieveDamage(int amount, Character instigator)
        { }
    }
}
