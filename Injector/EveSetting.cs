/*
/*
 * ---------------------------------------
 * User: duketwo
 * Date: 24.12.2013
 * Time: 16:26
 * 
 * ---------------------------------------
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Injector
{
	/// <summary>
	/// Description of EveAccountData.
	/// </summary>
	
	[Serializable]
	public class EveSetting : ViewModelBase {
		

		public EveSetting(string eveDirectory, string redGuardDirectory ,DateTime last24HourTS, bool eveServerStatusThread)
		{
			EveDirectory = eveDirectory;
			RedGuardDirectory = redGuardDirectory;
			Last24HourTS = last24HourTS;
			EveServerStatusThread = eveServerStatusThread;
			
		}
		
		public EveSetting(){ 
			
		}
		
		public string RedGuardDirectory { get { return GetValue( () => RedGuardDirectory ); } set { SetValue( () => RedGuardDirectory, value ); } }
		public string EveDirectory { get { return GetValue( () => EveDirectory ); } set { SetValue( () => EveDirectory, value ); } }
		public DateTime Last24HourTS { get { return GetValue( () => Last24HourTS ); } set { SetValue( () => Last24HourTS, value ); } }
		public bool EveServerStatusThread { get { return GetValue( () => EveServerStatusThread ); } set { SetValue( () => EveServerStatusThread, value ); } }
	}
}
