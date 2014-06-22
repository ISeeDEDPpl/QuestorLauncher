/*
 * ---------------------------------------
 * User: duketwo
 * Date: 08.04.2014
 * Time: 11:50
 * 
 * ---------------------------------------
 */
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Utility;
using Injector;
using System.IO;

namespace HookManager

{
	/// <summary>
	/// Description of WCFClient.
	/// </summary>
	public class WCFClient
	{
		private static readonly WCFClient _instance = new WCFClient();
		private ChannelFactory<IWCFMethods> pipeFactory;
		private IWCFMethods pipeProxy;
		
		public WCFClient()
		{
			
			
		}
		
		public IWCFMethods GetPipeProxy{
			get {
				try {
					
					if(pipeFactory == null || pipeProxy == null) {
						CreateChannel();
					}
					
					pipeProxy.Ping();
					
				}
				catch(CommunicationObjectFaultedException)  {
					
					CreateChannel();
					
					
				}
				catch(CommunicationException) {
					
					CreateChannel();
				}
				
				catch(PipeException){
					CreateChannel();
				}
				
				catch (Exception e) {
					
					Win32Hooks.HookManager.Log("[WCFClient] " + "Exception " + e.ToString());
					//throw;
					
				}
				
				return pipeProxy;
				
			}
		}
		
		private void CreateChannel() {
			
			NetNamedPipeBinding binding = new NetNamedPipeBinding();
			binding.MaxReceivedMessageSize = 2147483647;
			binding.MaxBufferSize = 2147483647;
			pipeFactory = new ChannelFactory<IWCFMethods>( binding,new EndpointAddress("net.pipe://localhost/" + Win32Hooks.HookManager.Instance.PipeName));
			pipeProxy =	pipeFactory.CreateChannel();
		}
		
		public static WCFClient Instance
		{
			get { return _instance; }
		}
	}
}
