using Monopoly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Monopoly
{
	public partial class background : Form
	{
		Random Rnd = new Random();

		public Queue<Player> playerQueue;
		private readonly Square[] allSquares;
		private Player player1;
		private Player player2;
		private Player player3;
		private Player player4;
		private Player bank;
		private Player[] allPlayers;
		private String[] titleDeeds;
		private String[] propertyPanel;
		private int[,] chanceCardsStart;
		private int[,] chanceCards;
		private int[,] communityChestsStart;
		private int[,] communityChests;

		int chanceCounter = 0;
		int communityChestCounter = 0;
		bool showDeed = false;
		bool inJailRoll = false;

        public background()
		{
			InitializeComponent();
			
			//instantiating players

			string player1Name = "Player 1";                              //user input		//placeholder unitl I sort out the textbox
			player1 = new Player(1, true, player1Name, "boat", Player1Icon, 1360, 844, Player1Money, Money_Change_Label1, Jail1);

			string player2Name = "Player 2";                              //user input		//placeholder unitl I sort out the textbox
			player2 = new Player(2, true, player2Name, "hat", Player2Icon, 1360, 902, Player2Money, Money_Change_Label2, Jail2);

			//acive != true			//depends on user input
			string player3Name = "Player 3";                               //user input		//placeholder unitl I sort out the textbox
			player3 = new Player(3, false, player3Name, "boot", Player3Icon, 1429, 844, Player3Money, Money_Change_Label3, Jail3);


			//acive != true			//depends on user input
			string player4Name = "Player 4";                              //user input		//placeholder unitl I sort out the textbox
			player4 = new Player(4, false, player4Name, "dog", Player4Icon, 1429, 902, Player4Money, Money_Change_Label4, Jail4);

			bank = new Player(0, false, "Bank", "", null, 0, 0, null, Bank_Change_Label, null);      //Bank player			//active = false????

			//Player Queue - allows the users to take turns - automatically adds players 1 & 2
			playerQueue = new Queue<Player>();
			playerQueue.Enqueue(player1);
			Player1Money.Text = ($"£ " + player1.Get_money().ToString());
			playerQueue.Enqueue(player2);
			Player2Money.Text = ($"£ " + player2.Get_money().ToString());
			if (player3.Get_active() == true)				// only adds player 3 & 4 if they are selected on the start page
			{
				playerQueue.Enqueue(player3);
				Player3Money.Text = ($"£ " + player3.Get_money().ToString());
			}
			else
			{
				Boot_Icon.Visible = false;
				Player3Money.Visible = false;
				PropertyPanel3.Visible = false;
				Player3Icon.Visible = false;
			}
			if (player4.Get_active() == true)               // only adds player 3 & 4 if they are selected on the start page
            {
				playerQueue.Enqueue(player4);
				Player4Money.Text = ($"£ " + player4.Get_money().ToString());
			}
			else
			{
                Dog_Icon.Visible = false;
                Player4Money.Visible = false;
                PropertyPanel4.Visible = false;
				Player4Icon.Visible = false;
            }

			//array of players - 
			allPlayers = new Player[]
			{
			bank, player1, player2, player3, player4
			};


			//Instantiating the properties
			//Browns - Drama Block:
			Square drama = new Square(1, "Drama Room", "Brown", 60, new int[] { 2, 10, 30, 90, 160, 250 }, 50, 966, 715, null, null, null, null, null);     //Drama
			Square rs = new Square(3, "RS Room", "Brown", 60, new int[] { 4, 20, 60, 180, 320, 450 }, 50, 846, 715, null, null, null, null, null);      //RS
			//Light Blues - Maths:
			Square maths = new Square(6, "Maths Room", "Light Blue", 100, new int[] { 6, 30, 90, 270, 400, 550 }, 50, 663, 715, MathsUnowned, MathsHNum, MathsHP, MathsHM, MathsMort);      //Maths
			Square economics = new Square(8, "Economics Room", "Light Blue", 100, new int[] { 6, 30, 90, 270, 400, 550 }, 50, 542, 715, EconomicsUnowned, EconomicsHNum, EconomicsHP, EconomicsHM, EconMort);      //Econ
			Square cs = new Square(9, "Computer Science Room", "Light Blue", 120, new int[] { 8, 40, 100, 300, 450, 600 }, 50, 484, 715, CSUnowned, CSHNum, CSHP, CSHM, CSMort);     //CS
			//Pinks - Languages:
			Square french = new Square(11, "French Room", "Pink", 140, new int[] { 10, 50, 150, 450, 625, 750 }, 100, 391, 628, null, null, null, null, null);      //French
			Square german = new Square(13, "German Room", "Pink", 140, new int[] { 10, 50, 150, 450, 625, 750 }, 100, 391, 500, null, null, null, null, null);      //German
			Square spanish = new Square(14, "Spanish Room", "Pink", 160, new int[] { 12, 60, 180, 500, 700, 900 }, 100, 391, 435, null, null, null, null, null);    //Spanish
			//Oranges - Humanities:
			Square english = new Square(16, "English Room", "Orange", 180, new int[] { 14, 70, 200, 550, 750, 950 }, 100, 391, 309, null, null, null, null, null);      //English
			Square history = new Square(18, "History Room", "Orange", 180, new int[] { 14, 70, 200, 550, 750, 950 }, 100, 391, 180, null, null, null, null, null);      //History
			Square geography = new Square(19, "Geography Room", "Orange", 200, new int[] { 16, 80, 220, 600, 800, 1000 }, 100, 391, 117, null, null, null, null, null);     //Geo
			//Reds - DT:
			Square food = new Square(21, "Food Tech Room", "Red", 220, new int[] { 18, 90, 250, 700, 875, 1050 }, 150, 485, 25, null, null, null, null, null);     //Food Tech
			Square textiles = new Square(23, "Textiles Room", "Red", 220, new int[] { 18, 90, 250, 700, 875, 1050 }, 150, 604, 25, null, null, null, null, null);      //Textiles
			Square pd = new Square(24, "Product Design Room", "Red", 240, new int[] { 20, 100, 300, 750, 925, 1100 }, 150, 664, 25, null, null, null, null, null);     //PD
			//Yellow - Sciences:
			Square biology = new Square(26, "Biology Lab", "Yellow", 260, new int[] { 22, 110, 330, 800, 975, 1150 }, 150, 785, 25, null, null, null, null, null);     //Bio
			Square chemistry = new Square(27, "Chemistry Lab", "Yellow", 260, new int[] { 22, 110, 330, 800, 975, 1150 }, 150, 845, 25, null, null, null, null, null);     //Chem
			Square physics = new Square(29, "Physics Lab", "Yellow", 280, new int[] { 24, 120, 360, 850, 1025, 1200 }, 150, 968, 25, null, null, null, null, null);        //Physics
			//Greens - Green Spaces:
			Square field = new Square(31, "Field", "Green", 300, new int[] { 26, 130, 390, 900, 1100, 1275 }, 200, 1050, 115, null, null, null, null, null);     //Field
			Square conservation = new Square(32, "Conservation Area", "Green", 300, new int[] { 26, 130, 390, 900, 1100, 1275 }, 200, 1050, 180, null, null, null, null, null);  //Conservation Area
			Square quad = new Square(34, "Quad", "Green", 320, new int[] { 28, 150, 450, 1000, 1200, 1400 }, 200, 1050, 307, null, null, null, null, null);      //Quad
			//Dark Blues - Common Rooms:
			Square lower6th = new Square(37, "Lower 6th Common Room", "Dark Blue", 350, new int[] { 35, 175, 500, 1100, 1300, 1500 }, 200, 1050, 500, null, null, null, null, null); //Lower Common Room
			Square upper6th = new Square(39, "Upper 6th Common Room", "Dark Blue", 400, new int[] { 50, 200, 600, 1400, 1700, 2000 }, 200, 1050, 626, null, null, null, null, null); // Upper Common Room
			//Stations - Sports Places:
			Square sports = new Square(5, "Sports Hall", "Station", 200, new int[] { 25, 50, 100, 200 }, 0, 722, 844, null, null, null, null, null);      //Sports Hall
			Square netball = new Square(15, "Netball Courts", "Station", 200, new int[] { 25, 50, 100, 200 }, 0, 391, 371, null, null, null, null, null);      //Netball Courts
			Square astros = new Square(25, "Astros", "Station", 200, new int[] { 25, 50, 100, 200 }, 0, 724, 55, null, null, null, null, null);      //Astros
			Square pool = new Square(35, "Swimming Pool", "Station", 200, new int[] { 25, 50, 100, 200 }, 0, 1050, 371, null, null, null, null, null);      //Swimming Pool
			//Utilities
			Square library = new Square(12, "Library", "Utility", 150, new int[] { 4, 10 }, 0, 391, 562, null, null, null, null, null);      //Library
			Square canteen = new Square(28, "Canteen", "Utility", 150, new int[] { 4, 10 }, 0, 908, 25, null, null, null, null, null);      //Canteen
			//Other Squares
			Square go = new Square(0, "GO", "GO", 0, new int[] { }, 0, 1046, 710, null, null, null, null, null);        //GO
			Square community1 = new Square(2, "Community Chest", "Community", 0, new int[] { }, 0, 906, 715, null, null, null, null, null);      //Community Chest 1
			Square community2 = new Square(17, "Community Chest", "Community", 0, new int[] { }, 0, 391, 243, null, null, null, null, null);      //Community Chest 2
			Square community3 = new Square(33, "Community Chest", "Community", 0, new int[] { }, 0, 1050, 242, null, null, null, null, null);      //Community Chest 3
			Square income = new Square(4, "Income Tax", "Tax", 0, new int[] { 200 }, 0, 785, 715, null, null, null, null, null);     //Income Tax
			Square chance1 = new Square(7, "Chance", "Chance", 0, new int[] { }, 0, 604, 715, null, null, null, null, null);      //Chance 1
			Square chance2 = new Square(22, "Chance", "Chance", 0, new int[] { }, 0, 543, 25, null, null, null, null, null);      //Chance 2
			Square chance3 = new Square(36, "Chance", "Chance", 0, new int[] { }, 0, 1050, 434, null, null, null, null, null);      //Chance 3
			Square jail = new Square(10, "Jail", "Jail", 0, new int[] { }, 0, 400, 712, null, null, null, null, null);      //Jail
			Square hall = new Square(20, "School Hall", "Free Parking", 0, new int[] { }, 0, 400, 28, null, null, null, null, null);      //Free Parking
			Square supertax = new Square(38, "Super Tax", "Tax", 0, new int[] { 100 }, 0, 1050, 562, null, null, null, null, null);       //Super Tax
			Square gotojail = new Square(30, "Go To Jail", "Go To Jail", 0, new int[] { }, 0, 1046, 29, null, null, null, null, null);      //Go To Jail



			allSquares = new Square[]
			{
			go, drama, community1, rs, income, sports, maths, chance1, economics, cs, jail,
			french, library, german, spanish, netball, english, community2, history, geography, hall,
			food, chance2, textiles, pd, astros, biology, chemistry, canteen, physics, gotojail,
			field, conservation, community3, quad, pool, chance3, lower6th, supertax, upper6th
			};

			titleDeeds = new string[]			//names of images for the title deeds
			{
			"-1", "Drama_Room", "-1", "RS_Room", "-1", "Sports_Hall", "Maths_Room", "-1", "Economics_Room", "CS_Room", "-1",
			"French_Room", "Library", "German_Room", "Spanish_Room", "Netball_Courts", "English_Room", "-1", "History_Room", "Geography_Room", "-1",
			"Food_Room", "-1", "Textiles_Room", "PD_Room", "Astros", "Biology_Lab", "Chemistry_Lab", "Canteen", "Physics_Lab", "-1",
			"Field", "Conservation_Area", "-1", "Quad", "Swimming_Pool", "-1", "Lower6_Room", "-1", "Upper6_Room"
			};

			
			
			propertyPanel = new string[]				//for GUI - Names of the picture boxes to show what properties are owned		- not actually used as of yet
			{
			"-1", "Brown1_1", "-1", "Brown1_2", "-1", "Station1_1", "LBlue1_1", "-1", "LBlue1_2", "LBlue1_3", "-1",					//player 1 = +0
			"Pink1_1", "Utility1_1", "Pink1_2", "Pink1_3", "Stations1_2", "Orange1_1", "-1", "Orange1_2", "Orange1_3", "-1",
			"Red1_1", "-1", "Red1_2", "Red1_3", "Station1_3", "Yellow1_1", "Yellow1_2", "Utility1_2", "Yellow1_3", "-1",
			"Green1_1", "Green1_2", "-1", "Green1_3", "Station1_4", "-1", "DBlue1_1", "-1", "DBlue1_2",


            "-1", "Brown2_1", "-1", "Brown2_2", "-1", "Station2_1", "LBlue2_1", "-1", "LBlue2_2", "LBlue2_3", "-1",					//player 2 = +40
			"Pink2_1", "Utility2_1", "Pink2_2", "Pink2_3", "Stations2_2", "Orange2_1", "-1", "Orange2_2", "Orange2_3", "-1",
            "Red2_1", "-1", "Red2_2", "Red2_3", "Station2_3", "Yellow2_1", "Yellow2_2", "Utility2_2", "Yellow2_3", "-1",
            "Green2_1", "Green2_2", "-1", "Green2_3", "Station2_4", "-1", "DBlue2_1", "-1", "DBlue2_2",

            "-1", "Brown3_1", "-1", "Brown3_2", "-1", "Station3_1", "LBlue3_1", "-1", "LBlue3_2", "LBlue3_3", "-1",					//player 3 = +80
			"Pink3_1", "Utility3_1", "Pink3_2", "Pink3_3", "Stations3_2", "Orange3_1", "-1", "Orange3_2", "Orange3_3", "-1",
            "Red3_1", "-1", "Red3_2", "Red3_3", "Station3_3", "Yellow3_1", "Yellow3_2", "Utility3_2", "Yellow3_3", "-1",
            "Green3_1", "Green3_2", "-1", "Green3_3", "Station3_4", "-1", "DBlue3_1", "-1", "DBlue3_2",

            "-1", "Brown4_1", "-1", "Brown4_2", "-1", "Station4_1", "LBlue4_1", "-1", "LBlue4_2", "LBlue4_3", "-1",					//player 4 = +120
			"Pink4_1", "Utility4_1", "Pink4_2", "Pink4_3", "Stations4_2", "Orange4_1", "-1", "Orange4_2", "Orange4_3", "-1",
            "Red4_1", "-1", "Red4_2", "Red4_3", "Station4_3", "Yellow4_1", "Yellow4_2", "Utility4_2", "Yellow4_3", "-1",
            "Green4_1", "Green4_2", "-1", "Green4_3", "Station4_4", "-1", "DBlue4_1", "-1", "DBlue4_2"
            };




			int[,] chanceCardsStart = new int[11, 3]
			{
			//CardID, Card type, Value					//0 = move, 1 = money, 2 = go to jail, 3 = get out of jail, 4 = general repairs, 5 = go back 3 spaces, 6 = pay each player
			{001, 0, 0},		//Advance to GO - position 0
			{002, 0, 11},		//Advance to french room - position 11
			{003, 0, 5},		//Take a trip to the sports hell - position 5
			{004, 0, 24},		//Advance to product design room - position 24
			//{005, 5, -3},		//Go back 3 spaces
			{006, 0, 39},		//Advance to upper 6th - position 39
			{007, 1, 50},		//Bake sale - collect  £50
			{008, 1, 150},		//Prefect - collect £150
			{009, 1, -15},		//Phone fine - pay £15
			{010, 4, 0},		//General repairs - pay £25 per house, £100 per hotel
			{011, 2, 10},		//Go to jail
			{012, 3, 0},        //Get out of jail free
			};

			int[,] communityChestsStart = new int[15, 3]
			{
			//CardID, Card type, Value					//0 = move, 1 = money, 2 = go to jail, 3 = get out of jail, 4 = general repairs, 5 = go back 3 spaces, 6 = pay each player
			{ 100, 0, 0},      //Advance to GO - position 0
			{ 101, 2, 10},     //Go to jail
			{ 102, 3, 0},      //Get out of jail free
			{ 103, 4, 0},      //General repairs - pay £40 per house, £115 per hotel
			{ 104, 1, -50},     //Overdue library book - pay £50
			{ 105, 1, -50},    //School fee - pay £50
			{ 106, 1, -100},   //School trip - pay £100
			{ 107, 1, 10},      //Second in class - collect £10
			{ 108, 1, 50},      //Helped a teacher - collect £50
			{ 109, 6, 10},     //It's your birthday - collect £10 from each player
			{ 110, 1, 100},    //Mentoring - collect £100
			{ 111, 1, 100},    //Won a talent show - collect £100
			{ 112, 1, 100},    //Sports day winner - collect £100
			{ 113, 1, 20},      //Income tax refund - collect £20
			{ 114, 1, 200},    //Golden ticket - collect £200
			};

			chanceCards = new int[11, 3];
			Random rand = new Random();
			int n = 0;

			//shuffling chance cards
			while (n <= 10)
			{
				int randomChanceCard;

				do
				{
					randomChanceCard = rand.Next(0, 11);        //choose one of the cards at random
				}
				while (chanceCardsStart[randomChanceCard, 0] == 0);     //checks if card has already been used - if it has, choose another

				chanceCards[n, 0] = chanceCardsStart[randomChanceCard, 0];          //copy card to shuffled array
				chanceCards[n, 1] = chanceCardsStart[randomChanceCard, 1];
				chanceCards[n, 2] = chanceCardsStart[randomChanceCard, 2];

				chanceCardsStart[randomChanceCard, 0] = 0;      //mark card as used - ID becomes 0

				n++;
			}


			for (int i = 0; i < chanceCards.GetLength(0); i++)
			{
				Console.WriteLine($"Chance Card {i}: {chanceCards[i, 0]}, {chanceCards[i, 1]}, {chanceCards[i, 2]}");
			}


			communityChests = new int[15, 3];
			int m = 0;

			//shuffling community chests
			while (m <= 14)
			{
				int randomCommCard;
				do
				{
					randomCommCard = rand.Next(0, 15);      //choose one of the cards at random
				}
				while (communityChestsStart[randomCommCard, 0] == 0);       //checks if card has already been used - if it has, choose another
				communityChests[m, 0] = communityChestsStart[randomCommCard, 0];            //copy card to shuffled array
				communityChests[m, 1] = communityChestsStart[randomCommCard, 1];
				communityChests[m, 2] = communityChestsStart[randomCommCard, 2];
				communityChestsStart[randomCommCard, 0] = 0;        //mark card as used - ID becomes 0
				m++;
			}

			for (int i = 0; i < communityChests.GetLength(0); i++)					// check the order of the cards in the console window - check randomness
			{
				Console.WriteLine(
					$"Community Chest {i}: {communityChests[i, 0]}, {communityChests[i, 1]}, {communityChests[i, 2]}"
				);
			}

		}

		
		
		
		
		
		//when the dice is clicked it //causes the player to move
									  //check the property
									  //do the appropriate actions
		
		private async void dice1_Click(object sender, EventArgs e)			//when dice is clicked
		{

			Player currentPlayer = playerQueue.Peek();						//player is the first in the queue

			if (currentPlayer.Get_inJail() == false)						//only roll the dice if the player is not in jail
			{
				currentPlayer.RollDice1(diceImage1, Rnd);					//roll both dice
				currentPlayer.RollDice2(diceImage2, Rnd);


                // Check for doubles
                if (currentPlayer.CheckDouble())
                {
					currentPlayer.Increment_RollNumber();

                    // three doubles rule
                    if (currentPlayer.Get_rollNumber() == 3)
                    {
                        currentPlayer.GoToJail(allSquares);
                        currentPlayer.Reset_RollNumber();
						End_Turn.Enabled = true;
						End_Turn.BackColor = System.Drawing.Color.MistyRose;
                        return; //  do not move if going to jail
                    }
                }
                else
                {
                    // No double → reset counter
                    currentPlayer.Reset_RollNumber();
					diceImage1.Enabled = false;							//don't allow the buttons to be clicked again if it is not a double
					diceImage2.Enabled = false;
                }


                currentPlayer.set_RollAgain(false);						// prevents rolling again
				currentPlayer.DiceMove(allSquares, currentPlayer.Get_dice1(), currentPlayer.Get_dice2());		//icon moves
				bool moved = true;			//flag to check property that has been landed after moving again (eg after getting a chance)

				do
				{
					moved = false;					// reset flag
					int currentSquareID = currentPlayer.Get_position();
					if (allSquares[currentSquareID].Get_colourSet() == "Chance" && !currentPlayer.Get_inJail())       // if player lands on chance and not in jail (extra validation)
					{
						string cardID = chanceCards[chanceCounter, 0].ToString();
						CardDisplay.Visible = true;			//show card
						diceImage1.Visible = false;			//hide dice
						diceImage2.Visible = false;
						Close_Card_Button.Visible = true;
						if (chanceCards[chanceCounter, 0] < 10)					//fixes issues with names of cards - in the future change the names to fix issues properly
						{
							var cardImage = (Image)Properties.Resources.ResourceManager.GetObject($"00" + cardID);
							CardDisplay.Image = cardImage;
						}
						else
						{
							var cardImage = (Image)Properties.Resources.ResourceManager.GetObject($"0" + cardID);
							CardDisplay.Image = cardImage;
						}


						if (chanceCards[chanceCounter, 1] == 0)             //0 = move
						{
							currentPlayer.CardMove(chanceCards[chanceCounter, 2], currentPlayer.Get_position(), allSquares);
							moved = true;			//raise flag
						}
						else if (chanceCards[chanceCounter, 1] == 1)            //1 = money
						{
							currentPlayer.Set_money(chanceCards[chanceCounter, 2]);
						}
						else if (chanceCards[chanceCounter, 1] == 2)            //2 = go to jail
						{
							currentPlayer.GoToJail(allSquares);
						}
						else if (chanceCards[chanceCounter, 1] == 3)            //3 = get out of jail free card
						{
							currentPlayer.set_freeCards(1);
						}
						else if (chanceCards[chanceCounter, 1] == 4)            //4 = general repairs			//not properly applicable as houses haven't been implemented - not tested
						{
							int totalrepairs = 0;
							foreach (int squareID in currentPlayer.Get_squares())
							{
								Square playerSquare = allSquares[squareID];
								if (playerSquare.Get_colourSet() != "Station" && playerSquare.Get_colourSet() != "Utility")
								{

									if (playerSquare.Get_hotel() == true)               //if there is a hotel, pay £100
									{
										totalrepairs += 100;
									}
									else                                                //else pay £25 per house		- can only be houses if there is no hotel
									{
										totalrepairs += (playerSquare.Get_houses() * 25);
									}
								}
								currentPlayer.Set_money(-totalrepairs);
							}
						}
						else if (chanceCards[chanceCounter, 1] == 5)            //5 = go back 3 spaces			- //got rid of this - could reimplement
						{
							currentPlayer.CardMove((currentPlayer.Get_position() - 3), currentPlayer.Get_position(), allSquares);
							moved = true;
						}
						else if (chanceCards[chanceCounter, 1] == 6)            //6 = pay each player - collect 10 from each player - not actually a chance card
						{
							int numPlayers = playerQueue.Count;
							int totalPay = (numPlayers - 1) * chanceCards[chanceCounter, 2];				//money the payee receives
							currentPlayer.Set_money(totalPay);
							foreach (Player p in playerQueue)
							{
								if (p.Get_ID() != currentPlayer.Get_ID())				//any player apart from the payee
								{
									p.Set_money(-chanceCards[chanceCounter, 2]);
								}
							}
						}

						chanceCounter++;					//move to next chance card
						if (chanceCounter >= 11)
						{
							chanceCounter = 0;				//go back to the start
						}

					}

					else if (allSquares[currentSquareID].Get_colourSet() == "Community" && !currentPlayer.Get_inJail())        //if player lands on community chest
					{
						moved = false;
						string cardID = communityChests[communityChestCounter, 0].ToString();
						CardDisplay.Visible = true;					//show card
						diceImage1.Visible = false;					//hide dice
						diceImage2.Visible = false;
						Close_Card_Button.Visible = true;
						var cardImage = (Image)Properties.Resources.ResourceManager.GetObject(cardID);
						CardDisplay.Image = cardImage;


						if (communityChests[communityChestCounter, 1] == 0)             //0 = move
						{
							currentPlayer.CardMove(communityChests[communityChestCounter, 2], currentPlayer.Get_position(), allSquares);
							moved = true;				//raise flag to recheck property
						}
						else if (communityChests[communityChestCounter, 1] == 1)            //1 = money
						{
							currentPlayer.Set_money(communityChests[communityChestCounter, 2]);
						}
						else if (communityChests[communityChestCounter, 1] == 2)            //2 = go to jail
						{
							currentPlayer.GoToJail(allSquares);
						}
						else if (communityChests[communityChestCounter, 1] == 3)            //3 = get out of jail free card
						{
							currentPlayer.set_freeCards(1);
						}
						else if (communityChests[communityChestCounter, 1] == 4)            //4 = general repairs - //not necesssary as houses hasn't been implemented
						{
							int totalrepairs = 0;
							foreach (int squareID in currentPlayer.Get_squares())
							{
								Square playerSquare = allSquares[squareID];
								if (playerSquare.Get_colourSet() != "Station" && playerSquare.Get_colourSet() != "Utility")				//doesn't apply to stations and utilities
								{

									if (playerSquare.Get_hotel() == true)               //if there is a hotel, pay £100
									{
										totalrepairs += 100;
									}
									else                                                //else pay £25 per house		- can only be houses if there is no hotel
									{
										totalrepairs += (playerSquare.Get_houses() * 25);
									}
								}
								currentPlayer.Set_money(-totalrepairs);
							}
						}
						else if (communityChests[communityChestCounter, 1] == 5)            //5 = go back 3 spaces		- got rid of this card
						{
							currentPlayer.CardMove((currentPlayer.Get_position() - 3), currentPlayer.Get_position(), allSquares);
							moved = true;
						}
						else if (communityChests[communityChestCounter, 1] == 6)            //6 = pay each player - collect 10 from each player
						{
							int numPlayers = playerQueue.Count;	
							int totalPay = (numPlayers - 1) * communityChests[communityChestCounter, 2];			//money the payee receives
							currentPlayer.Set_money(totalPay);
							foreach (Player p in playerQueue)
							{
								if (p.Get_ID() != currentPlayer.Get_ID())								//any player aparts from the playee
								{
									p.Set_money(-communityChests[communityChestCounter, 2]);
								}
							}
						}

						communityChestCounter++;
						if (communityChestCounter >= 12)			//go backs to the start at the end
						{
							communityChestCounter = 0;
						}

					}
					else if (allSquares[currentSquareID].Get_ownerID() == 0 && allSquares[currentSquareID].Get_colourSet() != "Tax")            // not owned - not community chest or chance - can buy or auction // exclude tax squares (handled by rent payment)
					{


						if (!currentPlayer.Get_inJail() && titleDeeds[currentSquareID] != "-1")				//show title deeds of actual properties
						{
							await Task.Delay(1000);				//added a delay for user experience

							var DeedImage = (Image)Properties.Resources.ResourceManager.GetObject(titleDeeds[currentSquareID]);

							Title_Deed.Image = DeedImage;

							if (CardDisplay.Visible == false)				//don't show the title deed if the chance card is shown
							{
                                if (currentPlayer.Get_money() >= allSquares[currentSquareID].Get_price())			//can't buy if it can't be afforded - greys out button
                                {
                                    Buy_Button.Visible = true;
                                    Buy_Button.Enabled = true;
                                    Buy_Button.BackColor = System.Drawing.Color.MistyRose;
                                }
                                else
                                {
                                    Buy_Button.Visible = true;
                                    Buy_Button.Enabled = false;
                                    Buy_Button.BackColor = System.Drawing.Color.Gainsboro;

                                }

                                Title_Deed.Visible = true;
                                DontBuyButton.Enabled = true;
                                DontBuyButton.Visible = true;
                                showDeed = false;

                            }
							else
							{
								showDeed = true;
							}
							diceImage1.Visible = false;
							diceImage2.Visible = false;

						}
					}
					else if (allSquares[currentSquareID].Get_ownerID() != currentPlayer.Get_ID())       //owned by another player - pay rent	//includes tax - pays the bank
					{
						Player ownerPlayer = allPlayers[allSquares[currentSquareID].Get_ownerID()];             //need to add utility and station rent calculation
						allSquares[currentSquareID].payRent(currentPlayer, ownerPlayer, currentPlayer);
					}

				} while (moved == true);



				if (currentPlayer.Get_rollAgain(diceImage1, diceImage2) == false)			// if roll again is false - end turn
				{
					End_Turn.Enabled = true;
					End_Turn.BackColor = System.Drawing.Color.MistyRose;
				}
			}
			else
			{
				End_Turn.Enabled = true;
			}
			
		}


		
		
		
		
		
		
		
		private async void dice2_Click(object sender, EventArgs e)				//same as dice 1
		{
            Player currentPlayer = playerQueue.Peek();

            if (currentPlayer.Get_inJail() == false)
            {
                currentPlayer.RollDice1(diceImage1, Rnd);
                currentPlayer.RollDice2(diceImage2, Rnd);


                // Check for doubles
                if (currentPlayer.CheckDouble())
                {
                    currentPlayer.Increment_RollNumber();

                    // three doubles rule
                    if (currentPlayer.Get_rollNumber() == 3)
                    {
                        currentPlayer.GoToJail(allSquares);
                        currentPlayer.Reset_RollNumber();
                        End_Turn.Enabled = true;
                        End_Turn.BackColor = System.Drawing.Color.MistyRose;
                        return; //  do not move if going to jail
                    }
                }
                else
                {
                    // No double → reset counter
                    currentPlayer.Reset_RollNumber();
                    diceImage1.Enabled = false;
                    diceImage2.Enabled = false;
                }


                currentPlayer.set_RollAgain(false);
                currentPlayer.DiceMove(allSquares, currentPlayer.Get_dice1(), currentPlayer.Get_dice2());
                bool moved = true;

                do
                {
                    moved = false;
                    int currentSquareID = currentPlayer.Get_position();
                    if (allSquares[currentSquareID].Get_colourSet() == "Chance" && !currentPlayer.Get_inJail())       // if player lands on chance
                    {
                        string cardID = chanceCards[chanceCounter, 0].ToString();
                        CardDisplay.Visible = true;
                        diceImage1.Visible = false;
                        diceImage2.Visible = false;
                        Close_Card_Button.Visible = true;
                        if (chanceCards[chanceCounter, 0] < 10)
                        {
                            var cardImage = (Image)Properties.Resources.ResourceManager.GetObject($"00" + cardID);
                            CardDisplay.Image = cardImage;
                        }
                        else
                        {
                            var cardImage = (Image)Properties.Resources.ResourceManager.GetObject($"0" + cardID);
                            CardDisplay.Image = cardImage;
                        }


                        if (chanceCards[chanceCounter, 1] == 0)             //0 = move
                        {
                            currentPlayer.CardMove(chanceCards[chanceCounter, 2], currentPlayer.Get_position(), allSquares);
                            moved = true;
                        }
                        else if (chanceCards[chanceCounter, 1] == 1)            //1 = money
                        {
                            currentPlayer.Set_money(chanceCards[chanceCounter, 2]);
                        }
                        else if (chanceCards[chanceCounter, 1] == 2)            //2 = go to jail
                        {
                            currentPlayer.GoToJail(allSquares);
                        }
                        else if (chanceCards[chanceCounter, 1] == 3)            //3 = get out of jail free card
                        {
                            currentPlayer.set_freeCards(1);
                        }
                        else if (chanceCards[chanceCounter, 1] == 4)            //4 = general repairs
                        {
                            int totalrepairs = 0;
                            foreach (int squareID in currentPlayer.Get_squares())
                            {
                                Square playerSquare = allSquares[squareID];
                                if (playerSquare.Get_colourSet() != "Station" && playerSquare.Get_colourSet() != "Utility")
                                {

                                    if (playerSquare.Get_hotel() == true)               //if there is a hotel, pay £100
                                    {
                                        totalrepairs += 100;
                                    }
                                    else                                                //else pay £25 per house		- can only be houses if there is no hotel
                                    {
                                        totalrepairs += (playerSquare.Get_houses() * 25);
                                    }
                                }
                                currentPlayer.Set_money(-totalrepairs);
                            }
                        }
                        else if (chanceCards[chanceCounter, 1] == 5)            //5 = go back 3 spaces
                        {
                            currentPlayer.CardMove((currentPlayer.Get_position() - 3), currentPlayer.Get_position(), allSquares);
                            moved = true;
                        }
                        else if (chanceCards[chanceCounter, 1] == 6)            //6 = pay each player - collect 10 from each player
                        {
                            int numPlayers = playerQueue.Count;
                            int totalPay = (numPlayers - 1) * chanceCards[chanceCounter, 2];
                            currentPlayer.Set_money(totalPay);
                            foreach (Player p in playerQueue)
                            {
                                if (p.Get_ID() != currentPlayer.Get_ID())
                                {
                                    p.Set_money(-chanceCards[chanceCounter, 2]);
                                }
                            }
                        }

                        chanceCounter++;
                        if (chanceCounter >= 11)
                        {
                            chanceCounter = 0;
                        }

                    }

                    else if (allSquares[currentSquareID].Get_colourSet() == "Community" && !currentPlayer.Get_inJail())        //if player lands on community chest
                    {
                        moved = false;
                        string cardID = communityChests[communityChestCounter, 0].ToString();
                        CardDisplay.Visible = true;
                        diceImage1.Visible = false;
                        diceImage2.Visible = false;
                        Close_Card_Button.Visible = true;
                        var cardImage = (Image)Properties.Resources.ResourceManager.GetObject(cardID);
                        CardDisplay.Image = cardImage;


                        if (communityChests[communityChestCounter, 1] == 0)             //0 = move
                        {
                            currentPlayer.CardMove(communityChests[communityChestCounter, 2], currentPlayer.Get_position(), allSquares);
                            moved = true;
                        }
                        else if (communityChests[communityChestCounter, 1] == 1)            //1 = money
                        {
                            currentPlayer.Set_money(communityChests[communityChestCounter, 2]);
                        }
                        else if (communityChests[communityChestCounter, 1] == 2)            //2 = go to jail
                        {
                            currentPlayer.GoToJail(allSquares);
                        }
                        else if (communityChests[communityChestCounter, 1] == 3)            //3 = get out of jail free card
                        {
                            currentPlayer.set_freeCards(1);
                        }
                        else if (communityChests[communityChestCounter, 1] == 4)            //4 = general repairs
                        {
                            int totalrepairs = 0;
                            foreach (int squareID in currentPlayer.Get_squares())
                            {
                                Square playerSquare = allSquares[squareID];
                                if (playerSquare.Get_colourSet() != "Station" && playerSquare.Get_colourSet() != "Utility")
                                {

                                    if (playerSquare.Get_hotel() == true)               //if there is a hotel, pay £100
                                    {
                                        totalrepairs += 100;
                                    }
                                    else                                                //else pay £25 per house		- can only be houses if there is no hotel
                                    {
                                        totalrepairs += (playerSquare.Get_houses() * 25);
                                    }
                                }
                                currentPlayer.Set_money(-totalrepairs);
                            }
                        }
                        else if (communityChests[communityChestCounter, 1] == 5)            //5 = go back 3 spaces
                        {
                            currentPlayer.CardMove((currentPlayer.Get_position() - 3), currentPlayer.Get_position(), allSquares);
                            moved = true;
                        }
                        else if (communityChests[communityChestCounter, 1] == 6)            //6 = pay each player - collect 10 from each player
                        {
                            int numPlayers = playerQueue.Count;
                            int totalPay = (numPlayers - 1) * communityChests[communityChestCounter, 2];
                            currentPlayer.Set_money(totalPay);
                            foreach (Player p in playerQueue)
                            {
                                if (p.Get_ID() != currentPlayer.Get_ID())
                                {
                                    p.Set_money(-communityChests[communityChestCounter, 2]);
                                }
                            }
                        }

                        communityChestCounter++;
                        if (communityChestCounter >= 12)
                        {
                            communityChestCounter = 0;
                        }

                    }
                    else if (allSquares[currentSquareID].Get_ownerID() == 0 && allSquares[currentSquareID].Get_colourSet() != "Tax")            // not owned - not community chest or chance - can buy or auction // exclude tax squares (handled by rent payment)
                    {


                        if (!currentPlayer.Get_inJail() && titleDeeds[currentSquareID] != "-1")
                        {
                            await Task.Delay(1000);

                            var DeedImage = (Image)Properties.Resources.ResourceManager.GetObject(titleDeeds[currentSquareID]);

                            Title_Deed.Image = DeedImage;

							if (CardDisplay.Visible == false)
							{
								if (currentPlayer.Get_money() >= allSquares[currentSquareID].Get_price())
								{ 
									Buy_Button.Visible = true;
									Buy_Button.Enabled = true;
                                    Buy_Button.BackColor = System.Drawing.Color.MistyRose;
                                }
								else
								{
									Buy_Button.Visible = true;
									Buy_Button.Enabled = false;
									Buy_Button.BackColor = System.Drawing.Color.Gainsboro;

								}

								Title_Deed.Visible = true;
                                DontBuyButton.Enabled = true;
                                DontBuyButton.Visible = true;
                                showDeed = false;
                            }
                            else
                            {
                                showDeed = true;
                            }
                            diceImage1.Visible = false;
                            diceImage2.Visible = false;

                        }
                    }
                    else if (allSquares[currentSquareID].Get_ownerID() != currentPlayer.Get_ID())       //owned by another player - pay rent	//includes tax - pays the bank
                    {
                        Player ownerPlayer = allPlayers[allSquares[currentSquareID].Get_ownerID()];             //need to add utility and station rent calculation
                        allSquares[currentSquareID].payRent(currentPlayer, ownerPlayer, currentPlayer);
                    }

                } while (moved == true);



                if (currentPlayer.Get_rollAgain(diceImage1, diceImage2) == false)
                {
                    End_Turn.Enabled = true;
                    End_Turn.BackColor = System.Drawing.Color.MistyRose;
                }

                if (currentPlayer.Get_rollNumber() > 3 /*&& currentPlayer.CheckDouble() == true*/)
                {
                    currentPlayer.GoToJail(allSquares);
                }
            }
            else
            {
                End_Turn.Enabled = true;
            }

        }

        private void Buy_Button_Click(object sender, EventArgs e)
		{
			Player currentPlayer = playerQueue.Peek();
			int currentSquareID = currentPlayer.Get_position();
			Square currentSquare = allSquares[currentSquareID];
			currentSquare.BuyProperty(currentPlayer);
			Buy_Button.Enabled = false;                     //players can't click the button twice

			Title_Deed.Visible = false;
			Buy_Button.Visible = false;
			DontBuyButton.Visible = false;

			if (currentPlayer.Get_diceDouble() == true)		//reshow dice if double was rolled
			{
                diceImage1.Visible = true;
				diceImage2.Visible = true;
                diceImage1.Enabled = true;
                diceImage2.Enabled = true;
            }
			else
			{
				diceImage1.Enabled = false;
				diceImage2.Enabled = false;
			}
		}

		private void Close_Card_Button_Click(object sender, EventArgs e)				//button to close chance cards
		{
            Player currentPlayer = playerQueue.Peek();
            CardDisplay.Visible = false;
			Close_Card_Button.Visible = false;

			if (showDeed == true)			//if flag was raised to show title deed when chance caard is shown - show title deed 
			{
				Title_Deed.Visible = true;
				Buy_Button.Visible = true;
				showDeed = false;
				diceImage1.Visible = false;
				diceImage2.Visible = false;
				DontBuyButton.Visible = true;
			}
			else
			{
				if (currentPlayer.Get_diceDouble() == true)				//reshow dice if double was rolled
				{
					diceImage1.Enabled = true;
					diceImage2.Enabled = true;
					diceImage1.Visible = true;
					diceImage2.Visible = true;
				}
				else
				{
					diceImage1.Enabled = false;
					diceImage2.Enabled = false;
				}
			}
		}

		private void End_Turn_Click(object sender, EventArgs e)
		{
			Player currentPlayer = playerQueue.Peek();
            currentPlayer.Reset_RollNumber();
            playerQueue.Dequeue();
			

			if (currentPlayer.Get_bankrupt() == false)
			{
				currentPlayer.set_RollAgain(false);
				currentPlayer.Reset_RollNumber();
				currentPlayer.Enqueue_Player(playerQueue, currentPlayer);
			}

			if (playerQueue.Count > 1)
			{
				currentPlayer = playerQueue.Peek();
				TurnIndicator.Text = ($"{currentPlayer.Get_name()}'s Turn");
				diceImage1.Visible = true;
				diceImage2.Visible = true;
				diceImage1.Enabled = true;
				diceImage2.Enabled = true;
				End_Turn.Enabled = false;
				End_Turn.BackColor = System.Drawing.Color.Gainsboro;

			}

			if (currentPlayer.Get_inJail() == true)                                                     //in jail	
            {
				if (currentPlayer.Get_jailTurns() >= 0 && currentPlayer.Get_jailTurns() < 3)			
                {
					
                    JailPay.Visible = true;
                    JailPay.Enabled = true;
                    JailRoll.Visible = true;
                    JailRoll.Enabled = true;
                    if (currentPlayer.Get_freeCards() > 0)
                    {
                        JailCard.Visible = true;
                        JailCard.Enabled = true;
                    }

                    End_Turn.Enabled = true;
                    End_Turn.BackColor = System.Drawing.Color.MistyRose;

                }
				else if (currentPlayer.Get_jailTurns() == 3)			//must pay on final turn
				{
					JailPay.Visible= true;
                    JailPay.Enabled = true;
                    if (currentPlayer.Get_freeCards() > 0)
                    {
                        JailCard.Visible = true;
                        JailCard.Enabled = true;
                    }
                }
				else
				{
						//Select end turn in jail
				}

				currentPlayer.Increase_jailTurns();

            }


		}

        private void JailPay_Click(object sender, EventArgs e)
        {
			Player currentPlayer = playerQueue.Peek();
			currentPlayer.Set_money(-50);
			currentPlayer.GetOutOfJail();
			JailPay.Visible = false;
			JailPay.Enabled = false;
			JailRoll.Visible = false;
			JailRoll.Enabled = false;
			JailCard.Visible = false;
			JailCard.Enabled = false;
        }

		//private void JailRoll_Click(object sender, EventArgs e)
		//{
		//	diceImage1.Enabled = true;
		//	diceImage2.Enabled = true;
		//	JailPay.Visible = false;
		//	JailPay.Enabled = false;
		//	JailRoll.Visible = false;
		//	JailRoll.Enabled = false;
		//	JailCard.Visible = false;
		//	JailCard.Enabled = false;

		//	inJailRoll = true;
		//}

		private void Rules_Button_Click(object sender, EventArgs e)
        {
            Form3 rules = new Form3();
            rules.WindowState = FormWindowState.Maximized;
            rules.Show();
        }

        private void background_Load(object sender, EventArgs e)
        {

        }

        private void DontBuyButton_Click(object sender, EventArgs e)
        {
			Title_Deed.Visible = false;
			Buy_Button.Visible = false;
			DontBuyButton.Visible = false;
			End_Turn.Enabled = true;
        }

        private void JailCard_Click(object sender, EventArgs e)
        {
            Player currentPlayer = playerQueue.Peek();
			currentPlayer.set_freeCards(-1);
            currentPlayer.GetOutOfJail();
            JailPay.Visible = false;
            JailPay.Enabled = false;
            JailRoll.Visible = false;
            JailRoll.Enabled = false;
            JailCard.Visible = false;
            JailCard.Enabled = false;
        }

        private void BuyHouses_Click(object sender, EventArgs e)
        {
			if (!Houses.Visible)
			{
                Houses.Visible = true;
            }
			else
			{
				Houses.Visible = false;
			}
			

        }

        private void Houses_SelectedIndexChanged(object sender, EventArgs e)
        {
			Player currentPlayer = playerQueue.Peek();
            TabControl tabControl = (TabControl)sender;

            if (tabControl.SelectedTab.Name == "LightBlues")
            {
				
				bool owned = currentPlayer.CheckSet("Light Blue", allSquares);
				if (owned)			//if set is owned
				{
					MathsHM.Enabled = true;
					MathsHP.Enabled = true;
					MathsUnowned.Visible = false;

					EconomicsHM.Enabled = true;
					EconomicsHP.Enabled = true;
					EconomicsUnowned.Visible = false;

					CSHM.Enabled = true;
					CSHP.Enabled = true;
					CSUnowned.Visible = false;
				}
				else				//if set is unowned
				{
					MathsHM.Enabled = false;
					MathsHP.Enabled = false;
					if (allSquares[6].Get_ownerID() == currentPlayer.Get_ID())
					{
						MathsUnowned.Visible = false;
					}
					else
					{
						MathsUnowned.Visible = true;
					}

					EconomicsHM.Enabled = false;
					EconomicsHP.Enabled = false;
                    if (allSquares[8].Get_ownerID() == currentPlayer.Get_ID())
                    {
                        EconomicsUnowned.Visible = false;
                    }
                    else
                    {
                        EconomicsUnowned.Visible = true;
                    }


                    CSHM.Enabled = false;
					CSHP.Enabled = false;
                    if (allSquares[9].Get_ownerID() == currentPlayer.Get_ID())
                    {
                        CSUnowned.Visible = false;
                    }
                    else
                    {
                        CSUnowned.Visible = true;
                    }

                }
            }

        }

        private void MathsHP_Click(object sender, EventArgs e)
        {
			Player currentPlayer = playerQueue.Peek();

			allSquares[6].Increment_Houses();
			currentPlayer.Set_money(-allSquares[6].Get_houseCost());
        }

        private void MathsHM_Click(object sender, EventArgs e)
        {
			Player currentPlayer = playerQueue.Peek();

			allSquares[6].Decrement_Houses();
			currentPlayer.Set_money(allSquares[6].Get_houseCost() / 2);				//sell houses for half price
        }

        private void EconomicsHP_Click(object sender, EventArgs e)
        {
            Player currentPlayer = playerQueue.Peek();

            allSquares[8].Increment_Houses();
            currentPlayer.Set_money(-allSquares[8].Get_houseCost());
        }

        private void EconomicsHM_Click(object sender, EventArgs e)
        {
            Player currentPlayer = playerQueue.Peek();

            allSquares[8].Decrement_Houses();
            currentPlayer.Set_money(allSquares[8].Get_houseCost() / 2);				//sell houses for half price
        }

        private void CSHP_Click(object sender, EventArgs e)
        {
            Player currentPlayer = playerQueue.Peek();

            allSquares[9].Increment_Houses();
            currentPlayer.Set_money(-allSquares[9].Get_houseCost());
        }

        private void CSHM_Click(object sender, EventArgs e)
        {
            Player currentPlayer = playerQueue.Peek();

            allSquares[9].Decrement_Houses();
            currentPlayer.Set_money(allSquares[9].Get_houseCost() / 2);				//sell houses for half price
        }







        private void Mortgage_Button_Click(object sender, EventArgs e)
        {
            if (!Mortgaging.Visible)
            {
                Mortgaging.Visible = true;
            }
            else
            {
                Mortgaging.Visible = false;
            }
        }



        private void Mortgaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player currentPlayer = playerQueue.Peek();
            TabControl tabControl = (TabControl)sender;

            if (tabControl.SelectedTab.Name == "LBluesM")
            {
				//Maths
				if (currentPlayer.Get_squares().Contains(allSquares[6].Get_ID()))				//if player owns maths room (6)
				{
					MathsMort.Enabled = true;				//ensure that the button can be clicked
					MathsMort.Text = "MORTGAGE";
				}
				else if (currentPlayer.Get_mortgaged().Contains(allSquares[6].Get_ID()) && (currentPlayer.Get_money() > allSquares[6].Calc_unmortgageValue()))		//if it is mortgaged and the player can afford to unmortgage it
				{
					MathsMort.Enabled = true;
					MathsMort.Text = "UNMORTGAGE";
				}
				else if (currentPlayer.Get_mortgaged().Contains(allSquares[6].Get_ID()) && (currentPlayer.Get_money() < allSquares[6].Calc_unmortgageValue()))		//if it is mortgaged and the player can NOT afford to unmortgage it
				{
					MathsMort.Enabled = false;
                    MathsMort.Text = "UNMORTGAGE";
                }
				else				// if it is not owned by the player
				{
					MathsMort.Enabled = false;
					MathsMort.Text = "UNOWNED";
				}


                //Economics
                if (currentPlayer.Get_squares().Contains(allSquares[8].Get_ID()))               //if player owns economics room (8)
                {
                    EconMort.Enabled = true;               //ensure that the button can be clicked
                    EconMort.Text = "MORTGAGE";
                }
                else if (currentPlayer.Get_mortgaged().Contains(allSquares[8].Get_ID()) && (currentPlayer.Get_money() > allSquares[8].Calc_unmortgageValue()))      //if it is mortgaged and the player can afford to unmortgage it
                {
                    EconMort.Enabled = true;
                    EconMort.Text = "UNMORTGAGE";
                }
                else if (currentPlayer.Get_mortgaged().Contains(allSquares[8].Get_ID()) && (currentPlayer.Get_money() < allSquares[8].Calc_unmortgageValue()))      //if it is mortgaged and the player can NOT afford to unmortgage it
                {
                    EconMort.Enabled = false;
                    EconMort.Text = "UNMORTGAGE";
                }
                else                // if it is not owned by the player
                {
                    EconMort.Enabled = false;
                    EconMort.Text = "UNOWNED";
                }


                //Computer Science
                if (currentPlayer.Get_squares().Contains(allSquares[9].Get_ID()))               //if player owns cs room (9)
                {
                    CSMort.Enabled = true;               //ensure that the button can be clicked
                    CSMort.Text = "MORTGAGE";
                }
                else if (currentPlayer.Get_mortgaged().Contains(allSquares[9].Get_ID()) && (currentPlayer.Get_money() > allSquares[9].Calc_unmortgageValue()))      //if it is mortgaged and the player can afford to unmortgage it
                {
                    CSMort.Enabled = true;
                    CSMort.Text = "UNMORTGAGE";
                }
                else if (currentPlayer.Get_mortgaged().Contains(allSquares[9].Get_ID()) && (currentPlayer.Get_money() < allSquares[9].Calc_unmortgageValue()))      //if it is mortgaged and the player can NOT afford to unmortgage it
                {
                    CSMort.Enabled = false;
                    CSMort.Text = "UNMORTGAGE";
                }
                else                // if it is not owned by the player
                {
                    CSMort.Enabled = false;
                    CSMort.Text = "UNOWNED";
                }


            }

        }

        private void MathsMort_Click(object sender, EventArgs e)
        {
			Player currentPlayer = playerQueue.Peek();
			if (MathsMort.Text == "MORTGAGE")
			{
				allSquares[6].Mortgage(currentPlayer);
				MathsMort.Text = "UNMORTGAGE";
			}
			else if (MathsMort.Text == "UNMORTGAGE")
			{
				allSquares[6].Unmortgage(currentPlayer);
                MathsMort.Text = "MORTGAGE";
            }
        }

        private void EconMort_Click(object sender, EventArgs e)
        {
            Player currentPlayer = playerQueue.Peek();
            if (EconMort.Text == "MORTGAGE")
            {
                allSquares[8].Mortgage(currentPlayer);
                EconMort.Text = "UNMORTGAGE";
            }
            else if (EconMort.Text == "UNMORTGAGE")
            {
                allSquares[8].Unmortgage(currentPlayer);
                EconMort.Text = "MORTGAGE";
            }
        }

        private void CSMort_Click(object sender, EventArgs e)
        {
            Player currentPlayer = playerQueue.Peek();
            if (CSMort.Text == "MORTGAGE")
            {
                allSquares[9].Mortgage(currentPlayer);
                CSMort.Text = "UNMORTGAGE";
            }
            else if (CSMort.Text == "UNMORTGAGE")
            {
                allSquares[9].Unmortgage(currentPlayer);
                CSMort.Text = "MORTGAGE";
            }
        }
    }
}

