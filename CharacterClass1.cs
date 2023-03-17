using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._1
{
    internal class Character
    {
        public string Name { get; private set; }
        public int hitPoints;
        public Character(string name, int hp) { Name = name; hitPoints = hp; ArmourType = ""; WeaponType = ""; }
        public Dice weapon = new(0, 0);
        public Dice parry = new(0, 0);
        public string WeaponType { get; private set; }
        public string ArmourType { get; private set; }
        public int Armour { get; private set; }
        public void SetParry(int amount, int sides) { parry = new Dice(amount, sides); }
        public void SetWeapon(string type, int amount, int sides) { WeaponType = type; weapon = new Dice(amount, sides); }
        public void SetArmour(string type, int strength) { ArmourType = type; Armour = strength; }        
    }
}
