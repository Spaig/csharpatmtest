using System;

namespace csharpatmtest
{
	class Program {

		static int flag; //loop breaker
		static String commandline = null;// string for storing full command

		static void Main(String[] args)
		{

			Machine.restock();// call restock function for initial stock
			flag = 1;// initialize loop flag

			while (flag > 0)
			{// main program loop -
			 // supports upper and
			 // lowercase input for user
			 // friendliness
				commandline = Console.ReadLine();// stores input to string
				flag = Machine.menuLoop(commandline);//calls main program

			}
		} }

	class Machine {

		static int[] cashdrawer = new int[6];// declare array for storing amounts of each bill
		static int[] valuearray = new int[15];// array for storing inputs
		static String[] commandarray = new String[10];// array for storing commands
		
		static char command;// char for storing commands
		static int value;// int for second part of command

		public static int menuLoop(String commandline2)
		{
			
			if (commandline2.Length < 1 || commandline2 == null)
			{// safety
			 // checking
			 // for null
			 // input
				Console.WriteLine("Failure: Invalid Command");
			}

			commandarray = commandline2.Split(" ",9);// split string on space

			command = commandarray[0][0];
			// gets first letter from first
			// part of command

			if (command == 'r' || command == 'R')
			{// main command logic
				restock();// run restock method on R
				drawerPrint();// call main drawerPrint method
				return 1;
			}

			else if (command == 'w' || command == 'W')
			{
				value = valuesParser(1);
				drawerWithdrawal(value);// call withdrawal method with "value"
				return 1;
			}

			else if (command == 'i' || command == 'I')
			{

				valuesParser();

				foreach (int x in valuearray)
				{// for all values in valuearray, feed them
				 // to drawerprint

					if (x > 0)
					{// if there's an actual value of course

						drawerPrint(x);// call overloaded drawerPrint method
									   // with "value"
					}
				}
				return 1;
			}

			else if (command == 'q' || command == 'Q')
			{
				return 0;// exit message
			}

			else
			{// if no inputs match, invalid command state
				Console.WriteLine("Failure: Invalid Command");
				return 1;
			}

		}

		private static int valuesParser(int i)
		{// "overloaded" simplified method
		 // for single value

			String tempstr = "0";

			if (commandarray[1] == null) {
				return 0;
			//safety check
			}


			else {
				tempstr = commandarray[1].Replace("$", "");// replace "$" in
																  // second part of
																  // command with a
																  // noncharacter
			}


			try
			{
				int tempint = Int32.Parse(tempstr);// cast to an integer
				return tempint; // return de-dollar-signed entry into valuearray
			}
			catch (FormatException e)
			{
				return 0;
			}



		}

		private static void valuesParser()
		{

			int cursor = 0; // cursor for safe adding to valuearray

			for (int x = 1; x < commandarray.Length; x++)
			{// remove dollar sign
			 // from commandarray
			 // entries - starting
			 // with second entry

				if (commandarray[x] != null && commandarray[x].Length > 0)
				{

					String tempstr = commandarray[x].Replace("$", "");// replace "$"
																	  // with a
																	  // noncharacter
					int tempint = 0;

					try
					{
						tempint = Int32.Parse(tempstr);// cast to an integer
					}
					catch (FormatException e)
					{
						tempint = 0;
					}

					valuearray[cursor] = tempint; // store de-dollar-signed entry
												  // into valuearray

					cursor++; // increment cursor

					if (cursor > 8)
					{
						return;
					} // safety checking - don't want too many queries
				}

			}
			return;
		}

		private static void drawerWithdrawal(int w)
		{// takes withdrawal amount and
		 // runs processes
			if (w < 1) { return; }//escape hatch for nonvalue
			int original = w;// store original value

			int hundreds = w / 100;// how many hundreds? etc
			w = w % 100;

			if (cashdrawer[0] >= hundreds)
			{// if there's enough hundreds, dispense
			 // hundreds

				cashdrawer[0] = cashdrawer[0] - hundreds;
			}



			else
			{// else devolve to lower bills
				w = original;
			}

			int fifties = w / 50;
			w = w % 50;

			if (cashdrawer[1] >= fifties)
			{// if there's enough fifties, dispense
			 // fifties

				cashdrawer[1] = cashdrawer[1] - fifties;
			}

			else
			{// else devolve to lower bills

				w = original;
			}

			int twenties = w / 20;
			w = w % 20;

			if (cashdrawer[2] >= twenties)
			{// if there's enough twenties, dispense
			 // twenties

				cashdrawer[2] = cashdrawer[2] - twenties;
			}

			else
			{// else devolve to lower bills

				w = original;
			}

			int tens = w / 10;
			w = w % 50;

			if (cashdrawer[3] >= tens)
			{// if there's enough tens, dispense tens

				cashdrawer[3] = cashdrawer[3] - tens;
			}

			else
			{// else devolve to lower bills

				w = original;
			}

			int fives = w / 5;
			w = w % 5;
			if (cashdrawer[4] >= fives)
			{// if there's enough fives, dispense fives

				cashdrawer[4] = cashdrawer[4] - fives;
			}

			else
			{// else devolve to lower bills

				w = original;
			}

			int ones = w;

			if (cashdrawer[5] >= ones)
			{// if there's enough fives, dispense fives

				cashdrawer[5] = cashdrawer[5] - ones;
			}

			else
			{// else not enough cash

				Console.WriteLine("Failure: insufficient funds");// print error
				return;
			}

			Console.WriteLine("Success: Dispensed $" + original);//print success message
			drawerPrint();// prints drawer balances once done
			return;
		}

		public static void restock()
		{
			for (int x = 0; x < 6; x++)
			{// initialize each array
			 // entry to 10, for
			 // initial stock
				cashdrawer[x] = 10;
			}
			return;
		}

		public static void drawerPrint()
		{// prints all drawer balances
			Console.WriteLine("Machine balance:");
			Console.WriteLine("$100: - " + cashdrawer[0]);
			Console.WriteLine("$50: - " + cashdrawer[1]);
			Console.WriteLine("$20: - " + cashdrawer[2]);
			Console.WriteLine("$10: - " + cashdrawer[3]);
			Console.WriteLine("$5: - " + cashdrawer[4]);
			Console.WriteLine("$1: - " + cashdrawer[5]);
			return;

		}

		public static void drawerPrint(int x)
		{// overloaded method for specific
		 // drawer
			switch (x)
			{
				case 100:
					Console.WriteLine("$100: - " + cashdrawer[0]);
					return;
				case 50:
					Console.WriteLine("$50: - " + cashdrawer[1]);
					return;
				case 20:
					Console.WriteLine("$20: - " + cashdrawer[2]);
					return;
				case 10:
					Console.WriteLine("$10: - " + cashdrawer[3]);
					return;
				case 5:
					Console.WriteLine("$5: - " + cashdrawer[4]);
					return;
				case 1:
					Console.WriteLine("$1: - " + cashdrawer[5]);
					return;
				default:
					return;
			}
		} }
}
