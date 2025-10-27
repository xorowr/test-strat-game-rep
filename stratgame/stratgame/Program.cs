



using System.Diagnostics;
using System.Security.Authentication;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    static void choices()
    {
        Console.WriteLine("Time to make your move! Enter the number that corresponds to the move you would like to do.");
        Console.WriteLine("[1] Attack - Standard strike on the enemy. - 1-10 damage and 80% chance to hit - Costs 5 energy");
        Console.WriteLine("[2] Special Attack - A special attack - 5-20 damage and 50% chance to hit - Costs 10 energy.");
        Console.WriteLine("[3] Recharge - Recharges energy at 4 times the normal rate. Increase the chance you'll be hit by 20%. No energy is used.");
        Console.WriteLine("[4] Dodge - Dodges the enemy's attack. Decrease enemy's chance of hitting by 30%. Recharges only 2 energy.");
        Console.WriteLine("[5] Heal - Can convert up to half of the stored energy into health. This can be used alongsie another action. Costs 10 energy.");
    }

    static void ChoicesNoHeal()
    {
        Console.WriteLine("Time to make your move! Enter the number that corresponds to the move you would like to do.");
        Console.WriteLine("[1] Attack - Standard strike on the enemy. - 1-10 damage and 80% chance to hit - Costs 5 energy");
        Console.WriteLine("[2] Special Attack - A special attack - 5-20 damage and 50% chance to hit - Costs 10 energy.");
        Console.WriteLine("[3] Recharge - Recharges energy at 4 times the normal rate. Increase the chance you'll be hit by 20%. No energy is used.");
        Console.WriteLine("[4] Dodge - Dodges the enemy's attack. Decrease enemy's chance of hitting by 30%. Recharges only 2 energy.");
    }

    static void HealthEnergyStats(int playerhealth, int playerenergy, int enemyhealth, int enemyenergy)
    {
        Console.WriteLine("Your health: " + playerhealth + "/100");
        Console.WriteLine("Your energy: " + playerenergy + "/50");
        Console.WriteLine("Enemy's health: " + enemyhealth + "/100");
        Console.WriteLine("Enemy's energy " + enemyenergy + "/50");
    }

    private static void PlayerAttack(ref Random roll, int MinAttackDamage, int MaxAttackDamage, ref int enemyhealth, ref int playerenergy, int TurnRecharge, ref bool pturnsuccess)
    {
        int damage = roll.Next(MinAttackDamage, MaxAttackDamage);
        Console.WriteLine("Your attack was successful. You dealt " + damage + " damage");
        enemyhealth -= damage;
        playerenergy += TurnRecharge;
        pturnsuccess = true;
    }

    private static void PlayerSpecialAttack(ref Random roll, int MinSpecialAttackDamage, int MaxSpecialAttackDamage, ref int enemyhealth, ref int playerenergy, int TurnRecharge, ref bool pturnsuccess)
    {
        int damage = roll.Next(MinSpecialAttackDamage, MaxSpecialAttackDamage);
        Console.WriteLine("Your attack was successful. You dealt " + damage + " damage");
        enemyhealth -= damage;
        playerenergy += TurnRecharge;
        pturnsuccess = true;
    }

    private static void EnemyAttack(ref Random roll, int MinAttackDamage, int MaxAttackDamage, ref int playerhealth, ref int enemyenergy, int TurnRecharge, ref bool eturnsuccess)
    {
        int damage = roll.Next(MinAttackDamage, MaxAttackDamage);
        Console.WriteLine("The enemy's attack was successful. They dealt " + damage + " damage");
        playerhealth -= damage;
        enemyenergy += TurnRecharge;
        eturnsuccess = true;
    }

    private static void EnemySpecialAttack(ref Random roll, int MinSpecialAttackDamage, int MaxSpecialAttackDamage, ref int playerhealth, ref int enemyenergy, int TurnRecharge, ref bool eturnsuccess)
    {
        int damage = roll.Next(MinSpecialAttackDamage, MaxSpecialAttackDamage);
        Console.WriteLine("The enemy's attack was successful. They dealt " + damage + " damage");
        playerhealth -= damage;
        enemyenergy += TurnRecharge;
        eturnsuccess = true;
    }

    private static void Main(string[] args)
    {
        // constants for various areas of the code
        const string attack = "1";
        const string specialattack = "2";
        const string recharge = "3";
        const string dodge = "4";
        const string heal = "5";

        // minimum amount of energy and health needed for recharge and heals
        const int HealthThreshold = 99;
        const int EnergyThreshold = 49;

        // success rates of each move
        const int DodgeAttackSuccess = 5;
        const int AttackSuccess = 8;
        const int RechargeAttackSuccess = 10;
        const int SpecialAttackSuccess = 5;
        const int DodgeSpecialAttackSuccess = 3;
        const int RechargeSpecialAttackSuccess = 7;


        const int TurnRecharge = 4;
        const int EnergyCheck = 10;
        const int MaxHealth = 100;
        const int MaxEnergy = 50;
        const int MaxRecharge = 16;
        const int AttackEnergy = 5;
        const int SpecialAttackAndHealEnergy = 10;
        const int MinEnergyNeeded = 5;
        const int MaxEnergyRecharge = 35;
        const int HalfRecharge = 2;
        const int MinAttackDamage = 1;
        const int MaxAttackDamage = 10;
        const int MinSpecialAttackDamage = 5;
        const int MaxSpecialAttackDamage = 20;



        // variables
        int playerhealth = MaxHealth;
        int playerenergy = MaxEnergy;
        int enemyhealth = MaxHealth;
        int enemyenergy = MaxEnergy;
        bool pturnsuccess = false;
        bool eturnsuccess = false;


        // introduction to the game
        Console.WriteLine("Welcome to the battle of your lifetime!");
        Console.WriteLine("To win, you must defeat your opponent. Pretty easy, right?");
        Console.WriteLine("You have five actions you can pick from each turn. Some of these consume energy. You recharge 4 points of energy each turn. I'll explain what they are properly when we get there.");
        Console.WriteLine("Good luck out there. Or bad luck... Depends which side I'm on today.");
        Console.WriteLine();


        Console.WriteLine("Before I forget, here are the current stats of you and the enemy.");
        HealthEnergyStats(playerhealth, playerenergy, enemyhealth, enemyenergy);

        Console.WriteLine("Right! I think that's everything. Are you ready? Not that I care or anything...");
        Console.ReadLine();
        Console.WriteLine("I don't actually care. I'm asking to be nice. Anyway, let's get this show on the road!");
        Console.WriteLine();

        Random roll = new Random();


        while (playerhealth > 0 && enemyhealth! > 0 || playerhealth! > 0 && enemyhealth > 0)
        {
            choices();
            string playerchoice = Console.ReadLine();

            int enemyroll = roll.Next(1, 5);
            string enemychoice = Convert.ToString(enemyroll);

            // continues choice selection until a turn has been successfully made
            while (pturnsuccess == false)
            {
                // choice 1 (attack) - player 
                if (playerchoice == attack)
                {
                    if (playerenergy < MinEnergyNeeded)
                    {
                        Console.WriteLine("You do not have enough energy for this move. Pick a new move.");
                        choices();
                        playerchoice = Console.ReadLine();
                        pturnsuccess = false;

                    }
                    playerenergy -= AttackEnergy;
                    int chance = roll.Next(1, 10);
                    if (enemychoice == dodge && chance <= DodgeAttackSuccess)
                    {
                        PlayerAttack(ref roll, MinAttackDamage, MaxAttackDamage, ref enemyhealth, ref playerenergy, TurnRecharge, ref pturnsuccess);
                        break;
                    }
                    else if (enemychoice == dodge && chance > DodgeAttackSuccess)
                    {
                        Console.WriteLine("The enemy's dodge was successful!");
                    }
                    if (enemychoice == recharge && chance <= RechargeAttackSuccess)
                    {
                        PlayerAttack(ref roll, MinAttackDamage, MaxAttackDamage, ref enemyhealth, ref playerenergy, TurnRecharge, ref pturnsuccess);
                        break;
                    }
                    if (enemychoice != dodge && enemychoice != recharge && chance <= AttackSuccess)
                    {
                        PlayerAttack(ref roll, MinAttackDamage, MaxAttackDamage, ref enemyhealth, ref playerenergy, TurnRecharge, ref pturnsuccess);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Your attack has failed.");
                        playerenergy += TurnRecharge;
                        pturnsuccess = true;
                        break;
                    }
                }



                // choice 2 (special attack) - player
                if (playerchoice == specialattack)
                {
                    if (playerenergy < EnergyCheck)
                    {
                        Console.WriteLine("You do not have enough energy for this move. Pick a new move.");
                        choices();
                        playerchoice = Console.ReadLine();
                        pturnsuccess = false;


                    }
                    playerenergy -= SpecialAttackAndHealEnergy;
                    int chance = roll.Next(1, 10);
                    if (enemychoice == dodge && chance <= DodgeSpecialAttackSuccess)
                    {
                        PlayerSpecialAttack(ref roll, MinSpecialAttackDamage, MaxSpecialAttackDamage, ref enemyhealth, ref playerenergy, TurnRecharge, ref pturnsuccess);
                        break;
                    }
                    else if (enemychoice == dodge && chance > DodgeSpecialAttackSuccess)
                    {
                        Console.WriteLine("The enemy's dodge was successful!");
                    }
                    if (enemychoice == recharge && chance <= RechargeSpecialAttackSuccess)
                    {
                        PlayerSpecialAttack(ref roll, MinSpecialAttackDamage, MaxSpecialAttackDamage, ref enemyhealth, ref playerenergy, TurnRecharge, ref pturnsuccess);
                        break;
                    }
                    if (enemychoice != dodge && enemychoice != recharge && chance <= SpecialAttackSuccess)
                    {
                        PlayerSpecialAttack(ref roll, MinSpecialAttackDamage, MaxSpecialAttackDamage, ref enemyhealth, ref playerenergy, TurnRecharge, ref pturnsuccess);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Your attack has failed.");
                        playerenergy += TurnRecharge;
                        pturnsuccess = true;
                        break;
                    }


                }


                // choice 3 (recharge) - player
                if (playerchoice == recharge)
                {
                    if (playerenergy >= MaxEnergy)
                    {
                        Console.WriteLine("You do not need to recharge. Please pick a new option.");
                        choices();
                        playerchoice = Console.ReadLine();
                        pturnsuccess = false;
                    }
                    int newenergy = MaxEnergy - playerenergy;
                    if (playerenergy < MaxEnergyRecharge)
                    {
                        playerenergy += MaxRecharge;
                        Console.WriteLine("You recharged 16 energy, which is the max energy you can recharge.");
                        pturnsuccess = true;
                    }
                    else
                    {
                        playerenergy += newenergy;
                        Console.WriteLine("You have recharged " + newenergy + " energy");
                        pturnsuccess = true;
                    }
                }



                // choice 4 (dodge) - player
                if (playerchoice == dodge)
                {
                    Console.WriteLine("You are attempting to dodge. You will find out if its successful on the enemy's turn.");
                    if (playerenergy < EnergyThreshold)
                    {
                        playerenergy += HalfRecharge;
                        Console.WriteLine("You have recharged 2 energy");
                        pturnsuccess = true;

                    }
                    else
                    {
                        Console.WriteLine("You do not need to recharge");
                        pturnsuccess = true;

                    }
                }



                // choice 5 (heal) - player
                if (playerchoice == heal)
                {
                    if (playerhealth < HealthThreshold && playerenergy > EnergyCheck)
                    {
                        playerenergy -= SpecialAttackAndHealEnergy;
                        int HalfEnergy = playerenergy / 2;
                        int MissingHealth = MaxHealth - playerhealth;
                        int HealAmount = (HalfEnergy < MissingHealth) ? HalfEnergy : MissingHealth;
                        if ((HealAmount + playerhealth) > MaxHealth)
                        {
                            int HealDifference = (HealAmount + playerhealth) - MaxHealth;
                            HealAmount -= HealDifference;
                        }
                        Console.WriteLine("You have healed " + HealAmount + " health");
                        Console.WriteLine("You can now pick to do another move. It cannot be another heal");
                        ChoicesNoHeal();
                        playerchoice = Console.ReadLine();
                        while (playerchoice == heal)
                        {
                            Console.WriteLine("You cannot pick heal again. Please select a new action");
                            ChoicesNoHeal();
                            playerchoice = Console.ReadLine();
                        }
                        pturnsuccess = false;

                    }
                    else
                    {
                        Console.WriteLine("You either cannot or do not need to heal. You can pick to do a new action.");
                        choices();
                        playerchoice = Console.ReadLine();
                        pturnsuccess = false;

                    }

                }

                // errors out with any invalid input
                if (playerchoice != attack && playerchoice != specialattack && playerchoice != recharge && playerchoice != dodge && playerchoice != heal)
                {
                    Console.WriteLine("Your input was invalid. Please pick a new input.");
                    choices();
                    playerchoice = Console.ReadLine();
                    pturnsuccess = false;
                }


            }

            // turn summary
            Console.WriteLine("After your turn, here are the stats of you and the enemy:");
            HealthEnergyStats(playerhealth, playerenergy, enemyhealth, enemyenergy);
            Console.WriteLine();
            eturnsuccess = false;

            if (playerhealth <= 0)
            {
                Console.WriteLine("Game over!");
                Console.WriteLine("The enemy has won. You lose.");
            }
            else if (enemyhealth <= 0)
            {
                Console.WriteLine("Game over!");
                Console.WriteLine("You have won! Congrats");
            }

            // rerolls if it lands on a recharge when a recharge is not needed
            while (enemychoice == recharge && enemyenergy > EnergyThreshold)
            {
                enemyroll = roll.Next(1, 5);
                enemychoice = Convert.ToString(enemyroll);
            }


            while (eturnsuccess == false)
            {

                // choice 1 - enemy
                if (enemychoice == attack)
                {
                    if (enemyenergy < MinEnergyNeeded)
                    {
                        Console.WriteLine("The enemy does not have enough energy for this move. They are picking a new move.");
                        enemyroll = roll.Next(1, 5);
                        enemychoice = Convert.ToString(enemyroll);
                        eturnsuccess = false;

                    }
                    enemyenergy -= AttackEnergy;
                    int chance = roll.Next(1, 10);
                    if (playerchoice == dodge && chance <= DodgeAttackSuccess)
                    {
                        EnemyAttack(ref roll, MinAttackDamage, MaxAttackDamage, ref playerhealth, ref enemyenergy, TurnRecharge, ref eturnsuccess);
                        break;
                    }
                    else if (playerchoice == dodge && chance > DodgeAttackSuccess)
                    {
                        Console.WriteLine("Your dodge was successful!");
                    }
                    if (playerchoice == recharge && chance <= RechargeAttackSuccess)
                    {
                        EnemyAttack(ref roll, MinAttackDamage, MaxAttackDamage, ref playerhealth, ref enemyenergy, TurnRecharge, ref eturnsuccess);
                        break;
                    }
                    if (playerchoice != dodge && playerchoice != recharge && chance <= AttackSuccess)
                    {
                        EnemyAttack(ref roll, MinAttackDamage, MaxAttackDamage, ref playerhealth, ref enemyenergy, TurnRecharge, ref eturnsuccess);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("The enemy's attack has failed.");
                        enemyenergy += TurnRecharge;
                        eturnsuccess = true;
                        break;
                    }
                }

                // choice 2 - enemy
                if (enemychoice == specialattack)
                {
                    if (enemyenergy < EnergyCheck)
                    {
                        Console.WriteLine("The enemy does not have enough energy for this move. Pick a new move.");
                        choices();
                        playerchoice = Console.ReadLine();
                        pturnsuccess = false;
                    }
                    enemyenergy -= SpecialAttackAndHealEnergy;
                    int chance = roll.Next(1, 10);
                    if (playerchoice == dodge && chance <= DodgeSpecialAttackSuccess)
                    {
                        EnemySpecialAttack(ref roll, MinSpecialAttackDamage, MaxSpecialAttackDamage, ref playerhealth, ref enemyenergy, TurnRecharge, ref eturnsuccess);
                        break;
                    }
                    else if (playerchoice == dodge && chance > DodgeSpecialAttackSuccess)
                    {
                        Console.WriteLine("Your dodge was successful!");
                    }
                    if (playerchoice == recharge && chance <= RechargeSpecialAttackSuccess)
                    {
                        EnemySpecialAttack(ref roll, MinSpecialAttackDamage, MaxSpecialAttackDamage, ref playerhealth, ref enemyenergy, TurnRecharge, ref eturnsuccess);
                        break;
                    }
                    if (playerchoice != dodge && playerchoice != recharge && chance <= SpecialAttackSuccess)
                    {
                        EnemySpecialAttack(ref roll, MinSpecialAttackDamage, MaxSpecialAttackDamage, ref playerhealth, ref enemyenergy, TurnRecharge, ref eturnsuccess);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("The enemy's attack has failed.");
                        enemyenergy += TurnRecharge;
                        eturnsuccess = true;
                        break;
                    }


                }



                // choice 3 - enemy
                if (enemychoice == recharge)
                {
                    if (enemyenergy >= MaxEnergy)
                    {
                        Console.WriteLine("The enemy does not need to recharge. They are picking a new option.");
                        enemyroll = roll.Next(1, 5);
                        enemychoice = Convert.ToString(enemyroll);
                        eturnsuccess = false;
                    }
                    int newenergy = MaxEnergy - enemyenergy;
                    if (enemyenergy < MaxEnergyRecharge)
                    {
                        enemyenergy += MaxRecharge;
                        Console.WriteLine("The enemy has recharged 16 energy, which is the max energy they can recharge.");
                        eturnsuccess = true;
                    }
                    else
                    {
                        enemyenergy += newenergy;
                        Console.WriteLine("The enemy has recharged " + newenergy + " energy");
                        eturnsuccess = true;
                    }
                }



                // choice 4 = enemy
                if (enemychoice == dodge)
                {
                    bool hitdecrease = true;
                    Console.WriteLine("The enemy is attempting to dodge. They will find out if its successful on the player's turn.");
                    if (enemyenergy < EnergyThreshold)
                    {
                        enemyenergy += HalfRecharge;
                        Console.WriteLine("The enemy has recharged 2 energy");
                        eturnsuccess = true;
                    }
                    else
                    {
                        Console.WriteLine("The enemy does not need to recharge");
                        eturnsuccess = true;
                    }
                }


                // choice 5 - enemy
                if (enemychoice == heal)
                {
                    if (enemyhealth < HealthThreshold && enemyenergy >= EnergyCheck)
                    {
                        enemyenergy -= SpecialAttackAndHealEnergy;
                        int HalfEnergy = enemyenergy / 2;
                        int MissingHealth = MaxHealth - enemyhealth;
                        int HealAmount = (HalfEnergy > MissingHealth) ? HalfEnergy : MissingHealth;
                        if ((HealAmount + enemyhealth) > MaxHealth)
                        {
                            int HealDifference = (HealAmount + enemyhealth) - MaxHealth;
                            HealAmount -= HealDifference;
                        }
                        Console.WriteLine("The enemy has healed " + HealAmount + " health. They will now do a second move.");
                        enemyroll = roll.Next(1, 5);
                        enemychoice = Convert.ToString(enemyroll);
                        while (enemychoice == heal)
                        {
                            enemyroll = roll.Next(1, 5);
                            enemychoice = Convert.ToString(enemyroll);
                        }
                        eturnsuccess = false;

                    }
                    else
                    {
                        Console.WriteLine("The enemy cannot or does not need to heal. They can pick to do a new action.");
                        enemyroll = roll.Next(1, 5);
                        enemychoice = Convert.ToString(enemyroll);
                        eturnsuccess = false;

                    }

                }

            }


            // turn summary
            Console.WriteLine("After the enemy's turn, here are the stats of you and the enemy:");
            HealthEnergyStats(playerhealth, playerenergy, enemyhealth, enemyenergy);
            Console.WriteLine();
            pturnsuccess = false;

        }
        Console.WriteLine("Game over!");
        if (playerhealth <= 0)
        {
            Console.WriteLine("The enemy has won. You lose.");
        }
        else
        {
            Console.WriteLine("You have won! Congrats");
        }
    }
}

