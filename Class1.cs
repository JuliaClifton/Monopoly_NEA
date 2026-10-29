using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly
{
	public class Square
	{
		private int ID;         //ID = position on the board
		private int ownerID;
		private string name;
		private string colourSet;
		private int price;
		private int[] rents;
		private int houses;
		private bool hotel;
		private int houseCost;
		private bool mortgaged;
		private int xCoord;     //for GUI
		private int yCoord;

		//for buying houses
		private Label UnownedLabel;
		private Label HousesNumLabel;
		private Button HousesPlus;
		private Button HousesMinus;

		private Button MortgageButton;


		public Square(int ID_in, string name_in, string colourSet_in, int price_in, int[] rent_in, int housesCost_in, int xCoord_in, int yCoord_in, Label UnownedLabel_in, Label HousesNumLabel_in, Button HousesPlus_in, Button HousesMinus_in, Button Mortgage_in)
		{
			ID = ID_in;
			ownerID = 0;       //starts off with no owner / owned by bank
			name = name_in;
			colourSet = colourSet_in;
			price = price_in;
			rents = rent_in;
			houses = 0;
			hotel = false;
			houseCost = housesCost_in;
			mortgaged = false;
			xCoord = xCoord_in;
			yCoord = yCoord_in;

			UnownedLabel = UnownedLabel_in;
			HousesNumLabel = HousesNumLabel_in;
			HousesPlus = HousesPlus_in;
			HousesMinus = HousesMinus_in;

			MortgageButton = Mortgage_in;
		}
		
		//getters
		public int Get_ID()
		{
			return ID;
		}
		public int Get_ownerID()
		{
			return ownerID;
		}
		public string Get_name()
		{
			return name;
		}
		public string Get_colourSet()
		{
			return colourSet;
		}
		public int Get_price()
		{
			return price;
		}
		public int Get_rent(Player currentPlayer, Player payee)           //depenent on number of houses + 2x if  whole set owned + stations + utilities
		{
			int rent = 0;
			if (!mortgaged)
			{
				if (colourSet == "Utility")
				{
					if (payee.OwnsAllUtilities())   //method to check if player owns both utilities
					{
						rent = 10 * (currentPlayer.Get_dice1() + currentPlayer.Get_dice2());      //diceRoll variable to be created in Player class
					}
					else
					{
						rent = 4 * (currentPlayer.Get_dice1() + currentPlayer.Get_dice2());
					}
				}
				else if (colourSet == "Station")
				{
					//rent is 25 if one station owned, 50 if two, 100 if three, 200 if four
					rent = rents[payee.StationsOwned() - 1];
				}
				else
				{
					rent = rents[houses];       //depending on houses

				}
			}
			return rent;                    // have to add on the two times 
		}

		public int Get_houses()
		{
			return houses;
		}
		public int Get_houseCost()
		{
			return houseCost;
		}
		public bool Get_hotel()
		{
			return hotel;
		}

		public bool Get_mortgaged()
		{
			return mortgaged;
		}

		public int Get_xCoord()
		{
			return xCoord;
		}

		public int Get_yCoord()
		{
			return yCoord;
		}

		public void Set_ownerID(int newOwner)
		{
			ownerID = newOwner;
		}

		public void Set_houses(int newHouses)
		{
			houses = newHouses;
		}

		public void Set_hotel(bool newHotel)
		{
			hotel = newHotel;
		}

		public void Set_xCoord(int newX)
		{
			xCoord = newX;
		}

		public void Set_yCoord(int newY)
		{
			yCoord = newY;
		}





		public void Mortgage(Player current)
		{
			Player currentPlayer = current;
			mortgaged = true;
			currentPlayer.Mortgage_Property(ID);
			currentPlayer.Set_money(Calc_mortgageValue());
		}

		public void Unmortgage(Player current)
		{
            Player currentPlayer = current;
            mortgaged = false;
			currentPlayer.Unmortgage_Property(ID);
            currentPlayer.Set_money(-Calc_unmortgageValue());
        }




		//other methods
		public int Calc_mortgageValue()
		{
			int mortgageValue = price / 2;
			return mortgageValue;
		}
		public int Calc_unmortgageValue()
		{
			int unmortgageValue = (int)Math.Round((price / 2) * 1.1);
			return unmortgageValue;
		}
		public void payRent(Player payer, Player payee, Player currentPlayer)
		{
			int rent = Get_rent(currentPlayer, payee);
			payer.Set_money(-rent);
			payee.Set_money(rent);


		}

		public void BuyProperty(Player buyer)
		{
			buyer.Set_money(-price);
			Set_ownerID(buyer.Get_ID());
			buyer.Add_Property(ID);
			

		}

		public void Increment_Houses()
		{
			if (houses < 5)
			{
				houses++;
                HousesNumLabel.Text = houses.ToString();
            }
			
        }

		public void Decrement_Houses()
		{
            if (houses > 0)
            {
                houses--;
                HousesNumLabel.Text = houses.ToString();
            }
        }

	}
}
