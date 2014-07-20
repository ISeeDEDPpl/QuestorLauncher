/*
 * ---------------------------------------
 * User: duketwo
 * Date: 23.03.2014
 * Time: 13:14
 * 
 * ---------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace QuestorSessionManager
{
	/// <summary>
	/// Description of UserPassGen.
	/// </summary>
	public class UserPassGen
	{

		
		private static UserPassGen _instance = new UserPassGen();
		public static int UserPassGenInstances = 0;
		
		
		Random rnd  = new Random();
		
		public UserPassGen()
		{
			Interlocked.Increment(ref UserPassGenInstances);
		}
		~UserPassGen()
		{
			Interlocked.Decrement(ref UserPassGenInstances);
		}
		
		public static UserPassGen Instance
		{
			get { return _instance; }
		}
		
		
		public string GenerateFirstname()
		{
			
			List<string> lst = new List<string>();
			string str = string.Empty;
			lst.Add("Aiden");
			lst.Add("Jackson");
			lst.Add("Mason");
			lst.Add("Liam");
			lst.Add("Jacob");
			lst.Add("Jayden");
			lst.Add("Ethan");
			lst.Add("Noah");
			lst.Add("Lucas");
			lst.Add("Logan");
			lst.Add("Caleb");
			lst.Add("Caden");
			lst.Add("Jack");
			lst.Add("Ryan");
			lst.Add("Connor");
			lst.Add("Michael");
			lst.Add("Elijah");
			lst.Add("Brayden");
			lst.Add("Benjamin");
			lst.Add("Nicholas");
			lst.Add("Alexander");
			lst.Add("William");
			lst.Add("Matthew");
			lst.Add("James");
			lst.Add("Landon");
			lst.Add("Nathan");
			lst.Add("Dylan");
			lst.Add("Evan");
			lst.Add("Luke");
			lst.Add("Andrew");
			lst.Add("Gabriel");
			lst.Add("Gavin");
			lst.Add("Joshua");
			lst.Add("Owen");
			lst.Add("Daniel");
			lst.Add("Carter");
			lst.Add("Tyler");
			lst.Add("Cameron");
			lst.Add("Christian");
			lst.Add("Wyatt");
			lst.Add("Henry");
			lst.Add("Eli");
			lst.Add("Joseph");
			lst.Add("Max");
			lst.Add("Isaac");
			lst.Add("Samuel");
			lst.Add("Anthony");
			lst.Add("Grayson");
			lst.Add("Zachary");
			lst.Add("David");
			lst.Add("Christopher");
			lst.Add("John");
			lst.Add("Isaiah");
			lst.Add("Levi");
			lst.Add("Jonathan");
			lst.Add("Oliver");
			lst.Add("Chase");
			lst.Add("Cooper");
			lst.Add("Tristan");
			lst.Add("Colton");
			lst.Add("Austin");
			lst.Add("Colin");
			lst.Add("Charlie");
			lst.Add("Dominic");
			lst.Add("Parker");
			lst.Add("Hunter");
			lst.Add("Thomas");
			lst.Add("Alex");
			lst.Add("Ian");
			lst.Add("Jordan");
			lst.Add("Cole");
			lst.Add("Julian");
			lst.Add("Aaron");
			lst.Add("Carson");
			lst.Add("Miles");
			lst.Add("Blake");
			lst.Add("Brody");
			lst.Add("Adam");
			lst.Add("Sebastian");
			lst.Add("Adrian");
			lst.Add("Nolan");
			lst.Add("Sean");
			lst.Add("Riley");
			lst.Add("Bentley");
			lst.Add("Xavier");
			lst.Add("Hayden");
			lst.Add("Jeremiah");
			lst.Add("Jason");
			lst.Add("Jake");
			lst.Add("Asher");
			lst.Add("Micah");
			lst.Add("Jace");
			lst.Add("Brandon");
			lst.Add("Josiah");
			lst.Add("Hudson");
			lst.Add("Nathaniel");
			lst.Add("Bryson");
			lst.Add("Ryder");
			lst.Add("Justin");
			lst.Add("Bryce");

			//—————female

			lst.Add("Sophia");
			lst.Add("Emma");
			lst.Add("Isabella");
			lst.Add("Olivia");
			lst.Add("Ava");
			lst.Add("Lily");
			lst.Add("Chloe");
			lst.Add("Madison");
			lst.Add("Emily");
			lst.Add("Abigail");
			lst.Add("Addison");
			lst.Add("Mia");
			lst.Add("Madelyn");
			lst.Add("Ella");
			lst.Add("Hailey");
			lst.Add("Kaylee");
			lst.Add("Avery");
			lst.Add("Kaitlyn");
			lst.Add("Riley");
			lst.Add("Aubrey");
			lst.Add("Brooklyn");
			lst.Add("Peyton");
			lst.Add("Layla");
			lst.Add("Hannah");
			lst.Add("Charlotte");
			lst.Add("Bella");
			lst.Add("Natalie");
			lst.Add("Sarah");
			lst.Add("Grace");
			lst.Add("Amelia");
			lst.Add("Kylie");
			lst.Add("Arianna");
			lst.Add("Anna");
			lst.Add("Elizabeth");
			lst.Add("Sophie");
			lst.Add("Claire");
			lst.Add("Lila");
			lst.Add("Aaliyah");
			lst.Add("Gabriella");
			lst.Add("Elise");
			lst.Add("Lillian");
			lst.Add("Samantha");
			lst.Add("Makayla");
			lst.Add("Audrey");
			lst.Add("Alyssa");
			lst.Add("Ellie");
			lst.Add("Alexis");
			lst.Add("Isabelle");
			lst.Add("Savannah");
			lst.Add("Evelyn");
			lst.Add("Leah");
			lst.Add("Keira");
			lst.Add("Allison");
			lst.Add("Maya");
			lst.Add("Lucy");
			lst.Add("Sydney");
			lst.Add("Taylor");
			lst.Add("Molly");
			lst.Add("Lauren");
			lst.Add("Harper");
			lst.Add("Scarlett");
			lst.Add("Brianna");
			lst.Add("Victoria");
			lst.Add("Liliana");
			lst.Add("Aria");
			lst.Add("Kayla");
			lst.Add("Annabelle");
			lst.Add("Gianna");
			lst.Add("Kennedy");
			lst.Add("Stella");
			lst.Add("Reagan");
			lst.Add("Julia");
			lst.Add("Bailey");
			lst.Add("Alexandra");
			lst.Add("Jordyn");
			lst.Add("Nora");
			lst.Add("Carolin");
			lst.Add("Mackenzie");
			lst.Add("Jasmine");
			lst.Add("Jocelyn");
			lst.Add("Kendall");
			lst.Add("Morgan");
			lst.Add("Nevaeh");
			lst.Add("Maria");
			lst.Add("Eva");
			lst.Add("Juliana");
			lst.Add("Abby");
			lst.Add("Alexa");
			lst.Add("Summer");
			lst.Add("Brooke");
			lst.Add("Penelope");
			lst.Add("Violet");
			lst.Add("Kate");
			lst.Add("Hadley");
			lst.Add("Ashlyn");
			lst.Add("Sadie");
			lst.Add("Paige");
			lst.Add("Katherine");
			lst.Add("Sienna");
			lst.Add("Piper");
			int rndint = rnd.Next(1,lst.Count-1);
			str = lst[rndint];
			return str;
		}
		
		public string GenerateUsername(){
			int rndint = rnd.Next(1,5);
			string retUsername = string.Empty;
			switch(rndint){
				case 1:
					retUsername = GenerateFirstname() + rnd.Next(1115,9123456).ToString();
					break;
				case 2:
					retUsername = "1337" + GenerateFirstname() + rnd.Next(1115,9123456).ToString();
					break;
				case 3:
					retUsername = GenerateFirstname() +  GenerateFirstname() + rnd.Next(200,999).ToString();
					break;
				case 4:
					retUsername = GenerateFirstname() + rnd.Next(200,999).ToString() + GenerateFirstname();
					break;
				case 5:
					retUsername = GenerateFirstname() + GenerateFirstname() + GenerateFirstname() + rnd.Next(1,10).ToString();
					break;
			}
			
			return retUsername;
		}
		
		public string GeneratePassword()
		{
			int length = rnd.Next(8,15);
			string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
			string res = String.Empty;
			while (0 < length--)
				res += valid[rnd.Next(valid.Length)];
			return res + rnd.Next(11,99) + valid[rnd.Next(valid.Length)].ToString().ToUpper();
		}
	}
}
