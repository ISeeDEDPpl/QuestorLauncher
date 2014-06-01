/*
* ---------------------------------------
* User: duketwo
* Date: 31.01.2014
* Time: 12:00
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
using System.Text.RegularExpressions;

namespace Injector
{
	/// <summary>
	/// Description of TorManager.
	/// </summary>
	public class EveManager : IDisposable
	{
		private static EveManager _instance  = new EveManager();
		private static Thread eveManagerThread = null;
		private static object thisLock = new object();
		private DateTime nextEveStart = DateTime.MinValue;
		private static Random rnd = new Random();
		private static Thread eveKillThread;
		
		
		
		public EveManager()
		{
		}
		
		public void StartEveMangerThread(){
			
			if(eveManagerThread == null || !eveManagerThread.IsAlive){
				Cache.Instance.Log("Starting EveManger Thread.");
				eveManagerThread = new Thread(EveManagerThread);
				eveManagerThread.Start();
			}
		}
		
		private bool DoesAEveThreadExist{ 
			get {
				foreach(EveAccount eA in Cache.Instance.EveAccountSerializeableSortableBindingList.List){
					if(eA.EveProcessExists()){
						return true;
					}
				}
				return false;
			}
		}
		
		public void KillAllEveInstances() {
			
			if(eveKillThread == null || !eveKillThread.IsAlive) {
				Cache.Instance.Log("Starting KillAllEveInstances Thread.");
				eveKillThread = new Thread(KillAllEveInstancesThread);
				eveKillThread.Start();
			} else {
				Cache.Instance.Log("Can't start a new killAllThread => eveKillThread.IsAlive");
			}
		}
		
		private void KillAllEveInstancesThread() {
			// gotta kill all instances here with a delay of ~
			while(DoesAEveThreadExist) {
				foreach(EveAccount eA in Cache.Instance.EveAccountSerializeableSortableBindingList.List){
					if(eA.EveProcessExists()){
						eA.KillEveProcess();
					}
				}
				Thread.Sleep(2333);
			}
		}
		
		private void EveManagerThread(){
			
			lock(thisLock){
				
				while(true){
					
					
					#region every 24 hours
					if(Cache.Instance.EveSettings.Last24HourTS.AddHours(24) < DateTime.UtcNow){
						Cache.Instance.EveSettings.Last24HourTS = DateTime.UtcNow;
						Cache.Instance.Log("[EveManagerThread] Last24HourTS.AddHours(24) < DateTime.UtcNow clearing vars // generating new start/end");
						
						foreach(EveAccount eA in Cache.Instance.EveAccountSerializeableSortableBindingList.List){
							eA.StartsPast24H = 0;
							eA.GenerateNewBeginEnd();
						}
					}
					#endregion
					
					for(int i = 0; i < Cache.Instance.EveAccountSerializeableSortableBindingList.List.Count; i++){
						
						EveAccount eA = Cache.Instance.EveAccountSerializeableSortableBindingList.List[i];
						
						if(EveServerStatus.Instance.IsEveServerOnline) {
							
							if(eA.EveProcessExists()){
								Process p = Process.GetProcessById(eA.Pid);
								if(((p.PrivateMemorySize64/1024)/1000) > 1500){
									Cache.Instance.Log("[EveManagerThread] Private working set is too big (> 1500), quitting this instance " + eA.AccountName +  " Memorysize " + ((p.PrivateMemorySize64/1024)/1000).ToString());
									eA.KillEveProcess();
								}
							}
							
							if(!eA.EveProcessExists() && eA.StartsPast24H <= 24 && eA.ShouldBeRunning && DateTime.UtcNow >= nextEveStart) {
								
								if(eA.IsActive) { 
									Cache.Instance.Log("[EveManagerThread] Starting new Eve Instance - AccountName: " + eA.AccountName);
									nextEveStart = DateTime.UtcNow.AddSeconds(rnd.Next(25,35));
									eA.StartEveInject();
								}
								
								
							}
							
							if(!eA.ShouldBeRunning && eA.EveProcessExists()){
								Cache.Instance.Log("[EveManagerThread] Stopping Eve Instance - AccountName: " + eA.AccountName);
								eA.KillEveProcess();
							}
							
						} else {

							eA.KillEveProcess();
						}
						
					}
					
					
					
					Thread.Sleep(10000);
				}
			}
		}
		
		
		public void Dispose(){
			Cache.Instance.Log("Stopping EveManger Thread.");
			
			//			foreach(EveAccount eA in Cache.Instance.EveAccountSerializeableSortableBindingList.List){
			//				if(eA.EveProcessExists())
			//					eA.KillEveProcess();
			//			}
			
			if(eveManagerThread != null){
				while(eveManagerThread.IsAlive){
					eveManagerThread.Abort();
					Thread.Sleep(1);
				}
			}
		}
		
		public bool IsEveManagerThreadRunning{
			get {
				return (eveManagerThread != null);
			}
		}
		
		public static EveManager Instance {
			get { return _instance; }
		}
		
		
		
		
	}
}
	