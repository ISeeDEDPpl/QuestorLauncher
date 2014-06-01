///*
//* ---------------------------------------
//* User: duketwo
//* Date: 08.04.2014
//* Time: 11:35
//* 
//* ---------------------------------------
//*/
//using System;
//using System.ServiceModel;
//using Utility;
//using System.Linq;
//using System.Windows.Forms;
//using Library.Forms;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Runtime.Serialization;
//
//namespace Injector
//{
//	/// <summary>
//	/// Description of WCF.
//	/// </summary>
//	/// 
//	
//	[ServiceContract]
//	public interface IWCFMethods
//	{
//		[OperationContract]
//		void SetSkillTrain(string accountName, bool value);
//		[OperationContract]
//		bool GetSkillTrain(string accountName);
//		[OperationContract]
//		void SetSkillQueueEnd(string accountName, DateTime dt);
//		[OperationContract]
//		void SetLastHeartbeat(string accountName, DateTime dt);
//		[OperationContract]
//		List<Injector.EveAccount> GetEveAccountList();
//		[OperationContract]
//		void SetIsActive(string accountName, bool value);
//		[OperationContract]
//		void SetLastMessage(string accountName, string value);
//		[OperationContract]
//		bool GetMarket(string accountName);
//		[OperationContract]
//		bool GetScam(string accountName);
//		[OperationContract]
//		string GetScamMessage(string accountName);
//		[OperationContract]
//		void Ping();
//		
//	}
//	
//	
//	public class WCFMethods : IWCFMethods
//	{
//		public WCFMethods()
//		{
//			
//		}
//				
//		public bool GetSkillTrain(string accountName){
//			if(DoesAccountExists(accountName)){
//				return Cache.Instance.EveAccountSerializeableSortableBindingList.List.FirstOrDefault(s => s.AccountName.Equals(accountName)).SkillTrain;
//			}
//			return false;
//		}
//		
//		public bool GetMarket(string accountName){
//			if(DoesAccountExists(accountName)){
//				return Cache.Instance.EveAccountSerializeableSortableBindingList.List.FirstOrDefault(s => s.AccountName.Equals(accountName)).Market;
//			}
//			return false;
//		}
//		
//		
//		public void SetSkillTrain(string accountName, bool value){
//			if(DoesAccountExists(accountName)){
//				Cache.Instance.EveAccountSerializeableSortableBindingList.List.FirstOrDefault(s => s.AccountName.Equals(accountName)).SkillTrain = value;
//			}
//			
//		}
//		
//		
//		public void SetSkillQueueEnd(string accountName, DateTime dt) {
//			
//			if(DoesAccountExists(accountName)){
//				Cache.Instance.EveAccountSerializeableSortableBindingList.List.FirstOrDefault(s => s.AccountName.Equals(accountName)).SkillQueueEnd = dt;
//			}
//		}
//		
//		public void SetLastHeartbeat(string accountName, DateTime dt) {
//			
//			if(DoesAccountExists(accountName)){
//				Cache.Instance.EveAccountSerializeableSortableBindingList.List.FirstOrDefault(s => s.AccountName.Equals(accountName)).LastHeartbeat = dt;
//			}
//		}
//		
//		public void SetIsActive(string accountName, bool value){
//			if(DoesAccountExists(accountName)){
//				Cache.Instance.EveAccountSerializeableSortableBindingList.List.FirstOrDefault(s => s.AccountName.Equals(accountName)).IsActive = value;
//			}
//		}
//		
//		public void SetLastMessage(string accountName, string value){
//			if(DoesAccountExists(accountName)){
//				Cache.Instance.EveAccountSerializeableSortableBindingList.List.FirstOrDefault(s => s.AccountName.Equals(accountName)).LastMessage = value;
//			}
//		}
//		
//		public bool GetScam(string accountName){
//			if(DoesAccountExists(accountName)){
//				return Cache.Instance.EveAccountSerializeableSortableBindingList.List.FirstOrDefault(s => s.AccountName.Equals(accountName)).Scam;
//			}
//			return false;
//		}
//		
//		public string GetScamMessage(string accountName){
//			if(DoesAccountExists(accountName)){
//				return Cache.Instance.EveAccountSerializeableSortableBindingList.List.FirstOrDefault(s => s.AccountName.Equals(accountName)).ScamMessage;
//			}
//			return string.Empty;
//		}
//		
//		
//		
//		public List<Injector.EveAccount> GetEveAccountList() {
//			List<Injector.EveAccount> list = new List<Injector.EveAccount>();
//			
//			foreach(EveAccount eA in Cache.Instance.EveAccountSerializeableSortableBindingList.List) {
//				
//				list.Add(new EveAccount(eA.AccountName,eA.CharacterName,eA.Password,eA.ProxyIpPort,eA.ProxyUserPass,eA.StartTime,eA.EndTime,eA.StartsPast24H,eA.LastStartTime,eA.LastHeartbeat,eA.IsActive,eA.SkillTrain,eA.SkillQueueEnd,eA.Scam,eA.ScamMessage,eA.UseRedGuard,eA.AutoLogin,eA.AutoStart,eA.HideHookManager,eA.Pid));
//			}
//			
//			return list;
//		}
//		
//		public void Ping(){
//			
//		}
//		
//		private bool DoesAccountExists(String accountName){
//			return Cache.Instance.EveAccountSerializeableSortableBindingList.List.Any(s => s.AccountName.Equals(accountName));
//		}
//		
//		
//	}
//	
//	public class WCFServer
//	{
//		private static readonly WCFServer _instance = new WCFServer();
//		ServiceHost host;
//		
//		public WCFServer()
//		{
//			
//		}
//		
//		public void StartWCFServer(){
//			
//			host = new ServiceHost(typeof(WCFMethods),new Uri[]{new Uri("net.pipe://localhost")});
//			host.AddServiceEndpoint(typeof(IWCFMethods),new NetNamedPipeBinding(),"PipeReverse");
//			host.Open();
//			
//		}
//		
//		public void StopWCFServer(){
//			if(host!= null)
//				host.Close();
//		}
//		
//		
//		public static WCFServer Instance
//		{
//			get { return _instance; }
//		}
//	}
//}
