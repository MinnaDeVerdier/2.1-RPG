using System.Dynamic;
using System.Security.Principal;
using static System.Console;
namespace Assignment_2._1_RPG
{
    internal class Program
    {
        public static int clearedRooms = 0;
        public static int defeatedMonsters = 0;
        public static int foundMonsters = 0;
        public static int timeSpent = 0;
        public static int restTimes = 0;
        public static Random random= new Random();
        public static int DieSide(int sides)
        {
            int number = random.Next(1, sides + 1);
            return number;
        }
        public static int DiceSide(int number, int sides)
        {
            int sum = 0;
            for (int i = 0; i < number; i++)
                sum += DieSide(sides);
            return sum;
        }
        public class Character
        {
            public string Name { get; private set; }
            public int hitPoints;
            public Character(string name, int hp) { Name = name; hitPoints = hp; ArmourType = ""; WeaponType = ""; }
            public int ParryDice { get; private set; }
            public int ParrySides { get; private set; }
            public string WeaponType { get; private set; }
            public int WeaponDice { get; private set; }
            public int WeaponSides { get; private set; }
            public string ArmourType { get; private set; }
            public int Armour { get; private set; }
            public void SetParry(int amount, int sides) { ParryDice = amount; ParrySides = sides; }
            public void SetWeapon(string type, int amount, int sides) { WeaponType = type; WeaponDice = amount; WeaponSides = sides; }
            public void SetArmour(string type,int strength) { ArmourType = type; Armour = strength; }
        }
        public static void Fight(Character p1, Character p2)
        {
            while (p1.hitPoints > 0 && p2.hitPoints > 0)
            {
                p2.hitPoints -= (Attack(p1, p2));
                timeSpent++;
                if (p2.hitPoints < 1)
                {
                    WriteLine($"{p2.Name} har blivit besegrad. ");
                    defeatedMonsters++;
                    break;
                }
                p1.hitPoints -= (Attack(p2, p1));
                timeSpent++;
                if (p1.hitPoints < 1)
                {
                    WriteLine($"{p1.Name} har blivit besegrad. ");
                    break;
                }
            }
        } 
        public static int Attack(Character attacker, Character defender)
        {
            int HP = WeaponRoll(attacker);
            HP -= ParryRoll(defender);
            HP -= defender.Armour;
            if (HP > 0)
            {
                WriteLine($"{defender.Name} förlorade {HP}.");
                return HP;
            }
            else
            {
                WriteLine($"{defender.Name} försvarade sig, attacken misslyckades.");
                return 0;
            }
        }
        public static int WeaponRoll(Character p) { return DiceSide(p.WeaponDice, p.WeaponSides); }
        public static int ParryRoll(Character p) { return DiceSide(p.ParryDice, p.ParrySides); }   
        static void Main(string[] args)
        {
            WriteLine("      ~~~~~~Välkommen äventyrare!~~~~~~\nDitt namn: ");
            string name=ReadLine();
            Character Player = new(name, 25); Player.SetParry(1, 4); Player.SetWeapon("kortsvärd",1,10); Player.SetArmour("läder", 2);
            Character Monster = new("Monster",35); Monster.SetParry(0, 0); Monster.SetWeapon("klor",2, 6); Monster.SetArmour("tjock hud", 2 );
            WriteLine(Player.Name + " gör entré. Du har 60 min på dig.");            
            while (Player.hitPoints>0&&timeSpent<60)
            {
                int nextRoom = random.Next(0, 2);
                if (nextRoom == 0)
                { 
                    timeSpent += 1;
                    clearedRooms++;
                    WriteLine("\n... Rummet är tomt ... ");
                    goto ClearedRoomChoice;
                }
                else if ( nextRoom== 1)
                { 
                    foundMonsters++;
                    Monster = new("Monster", random.Next(20, 41)); Monster.SetParry(0, 0); Monster.SetWeapon("Klor", 2, random.Next(4, 7)); Monster.SetArmour("tjock hud", random.Next(0, 3));
                    WriteLine("\nDu möter ett monster!");
                    WriteLine($"{Monster.Name} har {Monster.hitPoints} hp. ATK {Monster.WeaponType}: {Monster.WeaponDice} x D{Monster.WeaponSides}. DEF parry: {Monster.ParryDice} x D{Monster.ParrySides}. DEF {Monster.ArmourType}: {Monster.Armour}. ");
                    Write("Fight? (Y/N) ");
                    string fightChoice = ReadLine();
                    if (fightChoice == "N" || fightChoice == "n")
                    {
                        WriteLine("\n... Du smög förbi monstret ...");
                        timeSpent += 5;
                        clearedRooms++;
                        goto ClearedRoomChoice;
                    }
                    else if (fightChoice == "Y" || fightChoice == "y")
                    {
                        Fight(Player, Monster);
                        if (Player.hitPoints < 1)
                            break;
                        clearedRooms++;
                        goto Rest;
                    }
                }
            ClearedRoomChoice:
                Write("Gå vidare? \nY/N: ");
                string clearedRoomChoice = ReadLine();
                if (clearedRoomChoice == "Y")
                    continue;
                else if (clearedRoomChoice == "N")
                    break;
                else
                    goto Rest;
            Rest:
                Write($"\nDu har {Player.hitPoints} hp. Gå vidare? \nSkriv [r] för att vila 15 min och fylla på 15 HP, [c] för att fortsätta till nästa rum, [q] för att avsluta: ");
                string restChoice = ReadLine();
                if (restChoice == "q" || restChoice == "Q")
                    break;
                else if (restChoice == "r" || restChoice == "R")
                { 
                    Player.hitPoints += 15;
                    timeSpent += 15;
                    restTimes++;
                    goto Rest; 
                }
                else if (restChoice == "c" || restChoice == "C")
                    continue;
                else
                    goto Rest;
            }
            if(Player.hitPoints < 1)
                WriteLine($"\n    xxx {Player.Name} dog xxx \nBättre lycka nästa gång!"); 
            else if (timeSpent >= 60)
                WriteLine("\nTiden är ute!");
            WriteLine($"Spelet är över. {Player.Name} tog sig igenom {clearedRooms} rum och dödade {defeatedMonsters} av {foundMonsters} monster.\n{Player.Name} spenderade {timeSpent} av 60 minuter i äventyret, och vilade {restTimes} gånger.");
        }
    }
}        