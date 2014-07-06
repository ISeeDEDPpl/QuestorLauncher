/*
 * ---------------------------------------
 * User: duketwo
 * Date: 03.02.2014
 * Time: 11:32
 * 
 * ---------------------------------------
 */
using System;
using System.IO;
using System.Threading;
using System.Xml;

namespace Injector
{
	/// <summary>
	/// Description of CurlManager.
	/// </summary>
	public class EveServerStatus : IDisposable
	{
		private static EveServerStatus _instance = new EveServerStatus();
		private static object thisLock = new object();
		private DateTime nextEveServerStatusCheck = DateTime.MinValue;
		private static Random rnd = new Random();
		private static int randomWaitTme = rnd.Next(7,12);
		private Thread eveServerStatusThread = null;
		
		
		private bool _isEveServerOnline = false;
		public bool IsEveServerOnline {
			get 
            {
				
				if(DateTime.UtcNow.Hour == 11 && DateTime.UtcNow.Minute <= randomWaitTme)
                {
					
					return false;
				}
				
				if(!Cache.Instance.EveSettings.EveServerStatusThread)
					return true;
				
				return _isEveServerOnline;
			}
		}
		
		public EveServerStatus()
		{
			
		}
		
		public static EveServerStatus Instance 
        {
			get { return _instance; }
		}
		
		
		public void StartEveServerStatusThread()
        {
			Cache.Instance.Log("Starting EveServerStatus Thread.");
			if(eveServerStatusThread == null || !eveServerStatusThread.IsAlive)
            {
				eveServerStatusThread = new Thread(GetEveServerStatusThread);
				eveServerStatusThread.Start();
			}
		}
		
		
		private void GetEveServerStatusThread() {
			
			while(true) 
            {
				
				if(DateTime.UtcNow >= nextEveServerStatusCheck)
                {
					
					nextEveServerStatusCheck = DateTime.UtcNow.AddSeconds(rnd.Next(180,360));
					
					string ServerStatusXml = CurlManager.Instance.GetPostPage("https://api.eveonline.com/Server/ServerStatus.xml.aspx","","","");
					//Cache.Instance.Log(ServerStatusXml);
					if(!String.IsNullOrEmpty(ServerStatusXml) && !ServerStatusXml.Contains("Error"))
                    {
//						Cache.Instance.Log("[GetEveServerStatusThread] !String.IsNullOrEmpty(ServerStatusXml)");
						using (XmlReader reader = XmlReader.Create(new StringReader(ServerStatusXml)))
						{
							while (reader.Read())
							{
								try 
                                {
									if(reader.Name.Equals("serverOpen"))
                                    {
										string innerValue = reader.ReadString();
										if(!string.IsNullOrEmpty(innerValue)) 
                                        {
											
											
											bool srvOpen = bool.Parse(innerValue);
											if(srvOpen)
                                            {
												_isEveServerOnline = srvOpen;
											}	
										}
									}
									
									if(reader.Name.Equals("cachedUntil"))
                                    {
										string innerValue = reader.ReadString();
										if(!string.IsNullOrEmpty(innerValue))
                                        {
											
											DateTime dt = DateTime.Parse(innerValue);
											if(dt > DateTime.UtcNow)
                                            {
												Cache.Instance.Log("[GetEveServerStatusThread] Cached Until // Next EveServer-Statuscheck:  " + innerValue);
												nextEveServerStatusCheck = dt.AddSeconds(10);
											}
										}
									
									}
									
								} 
                                catch (Exception e) 
                                {
									
									Cache.Instance.Log("[GetEveServerStatusThread]  Exception: " + e.ToString());
								}
								
							}
							
						}
						
					} 
                    else 
                    {
						Cache.Instance.Log("[GetEveServerStatusThread] String.IsNullOrEmpty(ServerStatusXml), couldnt retrieve XML");
						_isEveServerOnline = true;
					}
				}

				Thread.Sleep(10);
			}
			
		}
		
		
		public void Dispose(){
			
			if(eveServerStatusThread != null)
            {
				while(eveServerStatusThread.IsAlive)
                {
					
					eveServerStatusThread.Abort();
					Thread.Sleep(1);
				}

				Cache.Instance.Log("[GetEveServerStatusThread] Disposed.");
			}
		}
		
	}
}