using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly
{
	public class Player
	{
		private int ID;
		private bool active;
		private string name;
		private string character;
		private PictureBox icon;
		private int money;
		private int position;
		private int dice1;
		private int dice2;
		private int totalDice;          //might not need this	- = dice1 + dice2
		private bool diceDouble;
		private int rollNumber;
		private bool rollAgain;
		private bool inJail;
		private int jailTurns;
		private int freeCards;
		private bool bankrupt;
		private int xcoord;             //for GUI
		private int ycoord;
		private Label moneyDisplay;     //for GUI
		private List<int> squares = new List<int>();		//squares owned and unmortgaged
		private List<int> mortgaged = new List<int>();		//squares owned and mortgaged
		private List<int> allOwnedSquares = new List<int>();		//all owned squares - mortgaged or unmortgaged
		private Label money_Change_Label;
		private PictureBox jailBars;

		public Player(int ID_in, bool active_in, string name_in, string character_in, PictureBox icon_in, int xcoord_in, int ycoord_in, Label moneyDisplay_in, Label money_change_in, PictureBox jailBars_in)
		{
			ID = ID_in;
			active = active_in;
			name = name_in;
			character = character_in;
			icon = icon_in;
			money = 1500;
			position = 0;
			dice1 = 0;
			dice2 = 0;
			totalDice = 0;
			diceDouble = false;
			rollNumber = 0;
			rollAgain = false;
			inJail = false;
			jailTurns = 0;
			freeCards = 0;
			bankrupt = false;
			xcoord = xcoord_in;
			ycoord = ycoord_in;
			moneyDisplay = moneyDisplay_in;
			money_Change_Label = money_change_in;
			jailBars = jailBars_in;
		} 
		


		//Create methods
		//Getters
		public int Get_ID()
		{
			return ID;
		}
		public bool Get_active()
		{
			return active;
		}
		public string Get_name()
		{
			return name;
		}
		public string Get_character()
		{
			return character;
		}
		public PictureBox Get_icon()
		{
			return icon;
		}
		public int Get_money()
		{
			return money;
		}
		public int Get_position()
		{
			return position;
		}
		public int Get_dice1()
		{
			return dice1;
		}
		public int Get_dice2()
		{
			return dice2;
		}
		/*public int Get_totalDice(int dice1, int dice2)
		{
			totalDice = dice1 + dice2;
			return totalDice;
		}*/
		public bool Get_diceDouble()
		{
			return diceDouble;
		}
		public int Get_rollNumber()
		{
			return rollNumber;
		}
		public bool Get_rollAgain(PictureBox diceImage1, PictureBox diceImage2)
		{
			if (rollNumber < 3 && diceDouble == true && inJail == false)
			{
				rollAgain = true;
				diceImage1.Visible = true;
				diceImage2.Visible = true;
				diceImage1.Enabled = true;
				diceImage2.Enabled = true;
			}
			else
			{
				rollAgain = false;
			}


			return rollAgain;
		}

		public bool Get_inJail()
		{
			return inJail;
		}
		public int Get_jailTurns()
		{
			return jailTurns;
		}
		public int Get_freeCards()
		{
			return freeCards;
		}
		public bool Get_bankrupt()
		{
			return bankrupt;
		}
		public List<int> Get_squares()
		{
			return squares;
		}
		public List<int> Get_mortgaged()
		{
			return mortgaged;
		}
		public Label Get_moneyDisplay()
		{
			return moneyDisplay;
		}



		//Setters

		public void Set_active(bool active_in)
		{
			active = active_in;
		}
		public async Task<int> Set_money(int new_money)
		{

			if (0 < new_money)
			{
				money_Change_Label.Visible = true;
                money_Change_Label.Text = "£ +" + (new_money);
				money_Change_Label.BackColor = System.Drawing.Color.LightGreen;
                await Task.Delay(2000);
				money_Change_Label.Visible = false;
            }
			else if (0 > new_money)
			{
                money_Change_Label.Visible = true;
                money_Change_Label.Text = "£ -" + (new_money);
				money_Change_Label.BackColor = System.Drawing.Color.IndianRed;
                await Task.Delay(2000);
                money_Change_Label.Visible = false;
            }

            money += new_money;
			
			if (name != "Bank")
			{
				moneyDisplay.Text = "£" + money.ToString();
			}

			return money;
		}

		public async void DiceMove(Square[] allSquares, int dice1, int dice2)          //move player based on dice roll
		{
			if (allSquares == null || allSquares.Length == 0)
				throw new ArgumentException("allSquares is null or empty");

			int oldposition = position;

			position = (position + dice1 + dice2) % allSquares.Length;              //allow wrap around
			int xcoord = allSquares[position].Get_xCoord();
			int ycoord = allSquares[position].Get_yCoord();

			icon.Location = new System.Drawing.Point(xcoord, ycoord);

			if (position < (oldposition))       //if player passed go
			{
				Set_money(200);
			}

			if (allSquares[position].Get_ID() == 30)        //if player lands on go to jail
			{
				await Task.Delay(1000);
				GoToJail(allSquares);
			}





		}

		public void CardMove(int new_position, int old_position_in, Square[] allSquares)       //move player based on chance card
		{
			int old_position = old_position_in;
			position = new_position;
			int xcoord = allSquares[position].Get_xCoord();
			int ycoord = allSquares[position].Get_yCoord();

			icon.Location = new System.Drawing.Point(xcoord, ycoord);

			if (old_position > position)        //if player passed go
			{
				Set_money(200);
            }


		}




		public int RollDice1(PictureBox diceImage1, Random Rnd)
		{
			dice1 = Rnd.Next(1, 7);

			switch (dice1)
			{
				case 1: diceImage1.Image = Properties.Resources.Dice1; break;
				case 2: diceImage1.Image = Properties.Resources.Dice2; break;
				case 3: diceImage1.Image = Properties.Resources.Dice3; break;
				case 4: diceImage1.Image = Properties.Resources.Dice4; break;
				case 5: diceImage1.Image = Properties.Resources.Dice5; break;
				case 6: diceImage1.Image = Properties.Resources.Dice6; break;
			}

			//rollNumber += 1;
			return dice1;
		}





		public int RollDice2(PictureBox diceImage2, Random Rnd)
		{
			dice2 = Rnd.Next(1, 7);

			switch (dice2)
			{
				case 1: diceImage2.Image = Properties.Resources.Dice1; break;
				case 2: diceImage2.Image = Properties.Resources.Dice2; break;
				case 3: diceImage2.Image = Properties.Resources.Dice3; break;
				case 4: diceImage2.Image = Properties.Resources.Dice4; break;
				case 5: diceImage2.Image = Properties.Resources.Dice5; break;
				case 6: diceImage2.Image = Properties.Resources.Dice6; break;
			}

			//rollNumber += 1;
			return dice2;
		}

		public void Reset_RollNumber()
		{
			rollNumber = 0;
		}

		public void Increment_RollNumber()
		{
			rollNumber += 1; 
		}




		public bool CheckDouble()
		{
            diceDouble = (dice1 == dice2);
            return diceDouble;
        }
		public void GoToJail(Square[] allSquares)
		{
			position = 10;
			inJail = true;
			jailTurns = 0;
			int xcoord = allSquares[10].Get_xCoord();
			int ycoord = allSquares[10].Get_yCoord();

			icon.Location = new System.Drawing.Point(xcoord, ycoord);

			jailBars.Visible = true;

		}
		public void GetOutOfJail()
		{
			inJail = false;
			jailTurns = 0;

			jailBars.Visible = false;
		}
		public void Increase_jailTurns()
		{
			jailTurns += 1;
		}
		public void set_freeCards(int change)
		{
			freeCards = freeCards + change;
		}
		public void set_Bankrupt()
		{
			bankrupt = true;
		}

		public void set_RollAgain(bool rollreset)               //rollagain - 1 if player gets to roll again, 0 if not
		{
			if ((CheckDouble() == true && rollNumber < 3) && !(rollreset == true))
			{
				rollAgain = true;
			}
			else
			{
				rollAgain = false;
			}
		}
		public void Add_Property(int squareID)
		{
			squares.Add(squareID);
		}
		
		
		
		public void Mortgage_Property(int squareID)
		{
			mortgaged.Add(squareID);
			squares.Remove(squareID);
		}
		public void Unmortgage_Property(int squareID)
		{
			squares.Add(squareID);
			mortgaged.Remove(squareID);
		
		}



		public void set_moneyDisplay(Label newMoneyDisplay)
		{
			moneyDisplay = newMoneyDisplay;
		}
		public void Dequeue_Player(Queue<Player> playerQueue)
		{
			Player currentPlayer = playerQueue.Dequeue();

		}

		public void Enqueue_Player(Queue<Player> playerQueue, Player currentPlayer)
		{
			if (currentPlayer.Get_bankrupt() == false)          //only re-enqueue if player is not bankrupt = might be unnecessary depending on logic
				playerQueue.Enqueue(currentPlayer);
		}

		public bool OwnsAllUtilities()
		{
			//method to check if player owns both utilities
			if (squares.Contains(12) && squares.Contains(28))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public int StationsOwned()
		{
			int houses = 0;
			if (squares.Contains(5))
			{
				houses = houses + 1;
			}
            if (squares.Contains(15))
            {
                houses = houses + 1;
            }
            if (squares.Contains(25))
            {
                houses = houses + 1;
            }
            if (squares.Contains(35))
            {
                houses = houses + 1;
            }

			return houses;
        }



        public bool CheckSet(string colourSet_in, Square[] allSquares)
        {
			string colourSet = colourSet_in;
            for (int i = 0; i < allSquares.Length; i++)							//for every square on the board
            {
                if (allSquares[i].Get_colourSet() == colourSet)					//check if it matches the intended colour
                {
                    if (!squares.Contains(allSquares[i].Get_ID()))				//if it does and it is not owned by the player
                    {
                        return false;                                           //return false - if >= 1 property in the set is not owned
                    }
                }																//therefore returns 
            }


            return true;														//else return true
        }




    }
}
