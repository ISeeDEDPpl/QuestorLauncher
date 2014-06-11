/*
* ---------------------------------------
* User: duketwo
* Date: 07.03.2014
* Time: 15:50
* 
* ---------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using EasyHook;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using Library.Forms;
using System.Threading;
using Utility;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Injector
{
	/// <summary>
	/// Description of Cache.
	/// </summary>
	public class Cache
	{
		private static Cache _instance = new Cache();
		public static int CacheInstances = 0;
		public delegate void Message(string msg);
		public static event Message OnMessage;
		
		
		public Cache()
		{
			Interlocked.Increment(ref CacheInstances);
		}
		
		
		public SerializeableSortableBindingList<EveAccount> EveAccountSerializeableSortableBindingList = new SerializeableSortableBindingList<EveAccount>("AcccountData.xml", 0);
		
		private SerializeableSortableBindingList<EveSetting> eveSettingsSerializeableSortableBindingList = new SerializeableSortableBindingList<EveSetting>("EveSettings.xml", 0);
		public EveSetting EveSettings { 
			get {
				if(!eveSettingsSerializeableSortableBindingList.List.Any())
					eveSettingsSerializeableSortableBindingList.List.Add(new EveSetting("C:\\eveoffline\\bin\\exefile.exe", "C:\\redugard\\", DateTime.MinValue, true));
				return (EveSetting)eveSettingsSerializeableSortableBindingList.List[0];
			}
		}
		
		string _assemblyPath;
		public string AssemblyPath{
			get {
				if (_assemblyPath == null) {
					
					_assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				}
				return _assemblyPath;
			}
		}
		
		public string EveLocation {
			get {
				
				return Cache.Instance.EveSettings.EveDirectory;
			}
		}
		
		public void Log(string text)
		{
			try {
				if (OnMessage != null)
				{
					OnMessage(DateTime.UtcNow.ToString() + " " + text.ToString());
				}
			} catch (Exception) {
				
			}
			
		}
		
		
		public static Cache Instance
		{
			get { return _instance; }
		}
		
		~Cache()
		{
			Interlocked.Decrement(ref CacheInstances);
		}
		
	}
}
