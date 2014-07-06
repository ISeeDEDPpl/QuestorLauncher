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
using System.Drawing;
using System.Windows.Forms;
using EasyHook;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using Library.Forms;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utility;
using System.Runtime.Serialization;


namespace QuestorLauncherInterface {
	public class QuestorLauncherInterface: MarshalByRefObject
	{
		public void Ping()
		{
		}
	}
}

namespace Injector
{
	/// <summary>
	/// Description of EveAccountData.
	/// </summary>
	
	[Serializable]
	public class EveAccount : ViewModelBase  {
		
		public EveAccount(string accountName, string characterName, string password, string startTime, string endTime, bool isActive,bool useRedGuard, bool autoStart,bool hideHookManager)
		{
			AccountName = accountName;
			CharacterName = characterName;
			Password = password;
			Begin = startTime;
			End = endTime;
			StartsPast24H = 0;
			LastStartTime = DateTime.MinValue;
			IsActive = isActive;
			UseRedGuard = useRedGuard;
			HideHookManager = hideHookManager;
			Pid = 0;
			
		}
		
		public EveAccount() {
		}
		
		public string AccountName { get { return GetValue( () => AccountName ); } set { SetValue( () => AccountName, value ); } }
		public string CharacterName { get { return GetValue( () => CharacterName ); } set { SetValue( () => CharacterName, value ); } }
		public string Password { get { return GetValue( () => Password ); } set { SetValue( () => Password, value ); } }
//		public string ProxyIpPort { get { return GetValue( () => ProxyIpPort ); } set { SetValue( () => ProxyIpPort, value ); } }
//		public string ProxyUserPass { get { return GetValue( () => ProxyUserPass ); } set { SetValue( () => ProxyUserPass, value ); } }
		public string StartHour { get { return GetValue( () => StartHour ); } set { SetValue( () => StartHour, value ); GenerateNewBeginEnd(); } }
		public int HoursPerDay { get { return GetValue( () => HoursPerDay ); } set { SetValue( () => HoursPerDay, value ); GenerateNewBeginEnd(); } }
		[ReadOnly(true)]
		public string Begin { get { return GetValue( () => Begin ); } set { SetValue( () => Begin, value ); } }
		[ReadOnly(true)]
		public string End { get { return GetValue( () => End ); } set { SetValue( () => End, value ); } }
		public int StartsPast24H { get { return GetValue( () => StartsPast24H ); } set { SetValue( () => StartsPast24H, value ); } }
		public DateTime LastStartTime { get { return GetValue( () => LastStartTime ); } set { SetValue( () => LastStartTime, value ); } }
		
		public bool IsActive { get { return GetValue( () => IsActive ); } set { SetValue( () => IsActive, value ); } }
		
		public bool UseRedGuard { get { return GetValue( () => UseRedGuard ); } set {  SetValue( () => UseRedGuard, value );} }
		public bool UseAdaptEve { get { return GetValue( () => UseAdaptEve ); } set {  SetValue( () => UseAdaptEve, value );} }
		//public bool AutoLogin { get { return GetValue( () => AutoLogin ); } set { SetValue( () => AutoLogin, value ); } }
		public bool HideHookManager { get { return GetValue( () => HideHookManager ); } set { SetValue( () => HideHookManager, value ); } }
		[BrowsableAttribute(false)]
		public int Pid { get { return GetValue( () => Pid ); } set { SetValue( () => Pid, value ); } }
		[BrowsableAttribute(false)]
		public HWSettings HWSettings { get { return GetValue( () => HWSettings ); } set { SetValue( () => HWSettings, value ); } }
		
		
		private static Random rnd = new Random();
		private static DateTime lastEveInstanceKilled = DateTime.MinValue;
		private static int waitTimeBetweenEveInstancesKills = rnd.Next(15,25);
		
		
		public void GenerateNewBeginEnd() 
        {
			try 
            {
				int startHour = int.Parse(this.StartHour.Split(':')[0]);
				startHour = rnd.Next(startHour-1,startHour+1);
				int endHour = startHour+this.HoursPerDay;
				int r = rnd.Next(0,2);
				int startMinute,endMinute;
				
                if (r == 0)
                {
					startMinute = rnd.Next(0,31);
					endMinute = rnd.Next(0,31);
				} 
                else 
                {
					startMinute = rnd.Next(31,60);
					endMinute = rnd.Next(31,60);
				}
				
				this.Begin = startHour.ToString() + ":" + (startMinute < 10 ? "0" : "") + startMinute.ToString();
				this.End = endHour.ToString() + ":" + (endMinute < 10 ? "0" : "") + endMinute.ToString();
			
			} 
            catch (Exception)
            {
				throw;
			}
		}
		
		
		
		
		private DateTime DtStartTime{ get { return DateTime.ParseExact(this.Begin, "H:m", null); } }
		private DateTime DtEndTime{ get { return DateTime.ParseExact(this.End, "H:m", null); } }
		
		[BrowsableAttribute(false)]
		public bool ShouldBeRunning {
			get{
				try {
					return DateTime.UtcNow >= this.DtStartTime && DateTime.UtcNow <= this.DtEndTime;
				} catch (Exception) {
					return false;
				}
			}
		}
		
		
		public bool KillEveProcess(){
			
			if(lastEveInstanceKilled.AddSeconds(waitTimeBetweenEveInstancesKills) < DateTime.UtcNow) {
				lastEveInstanceKilled = DateTime.UtcNow;
				waitTimeBetweenEveInstancesKills = rnd.Next(17,35);
				if(EveProcessExists()){
					Process p = Process.GetProcessById(this.Pid);
					p.Kill();
					return true;
				}
			}
			return false;
			
		}
		
		
		public bool EveProcessExists() {
			return this.Pid != -1 && this.Pid != 0 && Process.GetProcesses().Any(x => x.Id == this.Pid) && Process.GetProcesses().FirstOrDefault(x => x.Id == this.Pid).ProcessName.ToLower().Contains("exefile");
		}
		
		
		public void StartEveInject(){
			
			string[] args = new string[] {this.AccountName,this.CharacterName,this.Password,this.UseRedGuard.ToString(),this.HideHookManager.ToString(), this.UseAdaptEve.ToString(), WCFServer.Instance.GetPipeName};
			this.Pid = 0;
			int processId = -1;
			
			string assemblyFolder = Cache.Instance.AssemblyPath + "\\EveSettings\\";
			string currentAppDataFolder = assemblyFolder + this.AccountName + "_AppData\\";
			string eveBasePath = Directory.GetParent(Directory.GetParent(Cache.Instance.EveLocation).ToString()).ToString();
			eveBasePath = eveBasePath.Replace(":\\","_");
			eveBasePath = eveBasePath.Replace("\\","_").ToLower() + "_tranquility";
			
			
			string appDataCCPEveFolder = currentAppDataFolder + "CCP\\EVE\\";
			string eveSettingFolder = currentAppDataFolder + "CCP\\EVE\\" + eveBasePath + "\\";
			string defaultSettingsFolder = assemblyFolder + "default\\";
			string eveCacheFolder = eveSettingFolder + "\\cache\\";
			
			try {
				
				foreach (string file in Directory.GetFiles(appDataCCPEveFolder, "*.crs").Where(item => item.EndsWith(".crs")))
				{
					File.Delete(file);
				}
				
				foreach (string file in Directory.GetFiles(appDataCCPEveFolder, "*.dmp").Where(item => item.EndsWith(".dmp")))
				{
					File.Delete(file);
				}
				
				if(Directory.Exists(eveCacheFolder)){
					Directory.Delete(eveCacheFolder,true);
				}
				
				if(Directory.Exists(assemblyFolder)){
					foreach(String d in Directory.GetDirectories(assemblyFolder)){
						string directoryName = new DirectoryInfo(d).Name;
						if(!Cache.Instance.EveAccountSerializeableSortableBindingList.List.Any(s => directoryName.Contains(s.AccountName)) && !directoryName.Equals("default")) {
							Cache.Instance.Log("[EveAccount] Deleting not used Folder: EveSettings\\" + directoryName);
							Directory.Delete(d,true);
						}
					}
				}
				
			} catch (Exception) {
				
				Cache.Instance.Log("[EveAccount] Couldn't clear the cache || Deleting *.crs // *. dmp files");
			}
			
			
			
			if(!Directory.Exists(eveSettingFolder)) {
				Utility.Utility.DirectoryCopy(defaultSettingsFolder,eveSettingFolder,true);
				Cache.Instance.Log("[EveAccount] EveSettingsFolder doesn't exists. copying default settings");
			} else {
				Cache.Instance.Log("[EveAccount] EveSettingsFolder already exists. not copying default settings");
			}
			
			string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string injectionFile = System.IO.Path.Combine(path, "AppDomainHandler.dll");
			String ChannelName = null;
			RemoteHooking.IpcCreateServer<QuestorLauncherInterface.QuestorLauncherInterface>(ref ChannelName, WellKnownObjectMode.SingleCall);
			
			EasyHook.RemoteHooking.CreateAndInject(Cache.Instance.EveLocation, "\"/triPlatform=dx9\"", (int)InjectionOptions.Default, injectionFile, injectionFile, out processId, ChannelName, args);
			
			bool redGuardDone = true;
			if(this.UseRedGuard){
				redGuardDone = false;
				redGuardDone = RedGuard.Instance.DoRedguard(this.AccountName);
			}
			
			this.StartsPast24H++;
			
			if(processId != -1 && processId != 0 && redGuardDone) {
				this.Pid = processId;
				this.LastStartTime = DateTime.UtcNow;
			} else {
				Cache.Instance.Log("[EveAccount] processId == -1 || processId == 0 || !redGuardDone!");
				//				this.IsActive = false;
			}
			
			
		}
	}
}
