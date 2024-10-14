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

        public CharacterDeathEvent(Character character) : base() {
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
        public int BaseHealth { get; protected set; }
        public int Power { get; protected set; }

        int health;

        public Character() : base() 
        {
            health = BaseHealth;
        }
        public virtual void Ability() { }

        public void Damage(int amount, Character instigator)
        {
            PASEventHandler.GetInstance().TriggerEvent(new CharacterDamageEvent(instigator, this, amount));
            
            health -= amount;

            OnRecieveDamage(amount, instigator);

            if(health <= 0)
            {
                PASEventHandler.GetInstance().TriggerEvent(new CharacterDeathEvent(instigator));
            }
        }

        public virtual void OnRecieveDamage(int amount, Character instigator)
        { }
    }
}
