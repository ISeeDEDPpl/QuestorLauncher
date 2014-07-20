/*
 * ---------------------------------------
 * User: duketwo
 * Date: 08.04.2014
 * Time: 11:35
 * 
 * ---------------------------------------
 */
using System;
using System.ServiceModel;
using Utility;
using System.Linq;
using System.Windows.Forms;
using Library.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace QuestorSessionManager
{
	/// <summary>
	/// Description of WCF.
	/// </summary>
	/// 
	
	[ServiceKnownType(typeof(QuestorSessionManager.EveAccount))]
	[ServiceKnownType(typeof(QuestorSessionManager.HWSettings))]
	[ServiceContract]
	public interface IWCFMethods
	{
//		[OperationContract]
//		void SetSkillTrain(string accountName, bool value);
		[OperationContract]
		void Ping();
		[OperationContract]
		HWSettings GetHWSettings(string accountName);
		[OperationContract]
		List<EveAccount> GetEveAccountList();
		
	}
	[KnownType(typeof(QuestorSessionManager.EveAccount))]
	[KnownType(typeof(QuestorSessionManager.HWSettings))]
	public class WCFMethods : IWCFMethods
	{
		public WCFMethods()
		{
			
		}
		
		public List<EveAccount> GetEveAccountList(){
			return Cache.Instance.EveAccountSerializeableSortableBindingList.List.ToList();
		}
		
		public HWSettings GetHWSettings(string accountName){
			
			EveAccount eA = Cache.Instance.EveAccountSerializeableSortableBindingList.List.FirstOrDefault( s => s.AccountName.Equals(accountName));
			if(eA.HWSettings != null) 
				return eA.HWSettings;
			return null;
		}
		
		public void Ping(){
			
		}

	}
	
	public class WCFServer
	{
		private static readonly WCFServer _instance = new WCFServer();
		ServiceHost host;
		
		public WCFServer()
		{
			
		}
		private static Guid pipeName;
		public void StartWCFServer(){
			if(pipeName == Guid.Empty)
				pipeName = Guid.NewGuid();
			host = new ServiceHost(typeof(WCFMethods),new Uri[]{new Uri("net.pipe://localhost/" + pipeName.ToString())});
			host.AddServiceEndpoint(typeof(IWCFMethods),new NetNamedPipeBinding(),"");
			host.Open();
			
		}
		
		public string GetPipeName{
			get {
				return pipeName.ToString();
			}
		}
		
		public void StopWCFServer(){
			if(host!= null)
				host.Close();
		}
		
		
		public static WCFServer Instance
		{
			get { return _instance; }
		}
	}
}
