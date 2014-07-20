/*
 * ---------------------------------------
 * User: duketwo
 * Date: 21.06.2014
 * Time: 14:18
 * 
 * ---------------------------------------
 */
using System;
using System.Linq;

namespace QuestorSessionManager
{
	/// <summary>
	/// Description of Driver.
	/// </summary>
	public class Driver
	{
		public ulong Product
		{
			get;
			set;
		}
		public ulong Version
		{
			get;
			set;
		}
		public ulong SubVersion
		{
			get;
			set;
		}
		public ulong Build
		{
			get;
			set;
		}
		public Driver(string string_0)
		{
			string[] array = string_0.Split(new char[]
			{
				'.'
			});
			if (array.Count<string>() != 4)
			{
				throw new ArgumentException("Wrong format driver");
			}
			this.Product = Convert.ToUInt64(array[0]);
			this.Version = Convert.ToUInt64(array[1]);
			this.SubVersion = Convert.ToUInt64(array[2]);
			this.Build = Convert.ToUInt64(array[3]);
		}
	}
}
