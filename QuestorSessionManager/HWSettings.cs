/*
 * ---------------------------------------
 * User: duketwo
 * Date: 21.06.2014
 * Time: 11:00
 * 
 * ---------------------------------------
 */
using System;
using Utility;
using System.Linq;

namespace QuestorSessionManager
{
	/// <summary>
	/// Description of HWSettings.
	/// </summary>
	/// 
	[Serializable]
	public class HWSettings : ViewModelBase
	{
		
		public  ulong  TotalPhysRam { get { return GetValue( () => TotalPhysRam ); } set { SetValue( () => TotalPhysRam, value ); } }
		public string WindowsUserLogin { get { return GetValue( () => WindowsUserLogin ); } set { SetValue( () => WindowsUserLogin, value ); } }
		public string Computername { get { return GetValue( () => Computername ); } set { SetValue( () => Computername, value ); } }
		public string WindowsKey { get { return GetValue( () => WindowsKey ); } set { SetValue( () => WindowsKey, value ); } }
		public string NetworkAdapterGuid { get { return GetValue( () => NetworkAdapterGuid ); } set { SetValue( () => NetworkAdapterGuid, value ); } }
		public string NetworkAddress { get { return GetValue( () => NetworkAddress ); } set { SetValue( () => NetworkAddress, value ); } }
		public string MacAddress { get { return GetValue( () => MacAddress ); } set { SetValue( () => MacAddress, value ); } }
		public string ProcessorIdent { get { return GetValue( () => ProcessorIdent ); } set { SetValue( () => ProcessorIdent, value ); } }
		public string ProcessorRev { get { return GetValue( () => ProcessorRev ); } set { SetValue( () => ProcessorRev, value ); } }
		public string ProcessorCoreAmount { get { return GetValue( () => ProcessorCoreAmount ); } set { SetValue( () => ProcessorCoreAmount, value ); } }
		public string ProcessorLevel { get { return GetValue( () => ProcessorLevel ); } set { SetValue( () => ProcessorLevel, value ); } }
		public string GpuDescription { get { return GetValue( () => GpuDescription ); } set { SetValue( () => GpuDescription, value ); } }
		public uint GpuDeviceId { get { return GetValue( () => GpuDeviceId ); } set { SetValue( () => GpuDeviceId, value ); } }
		public uint GpuVendorId { get { return GetValue( () => GpuVendorId ); } set { SetValue( () => GpuVendorId, value ); } }
		public uint GpuRevision { get { return GetValue( () => GpuRevision ); } set { SetValue( () => GpuRevision, value ); } }
		public long GpuDriverversion { get { return GetValue( () => GpuDriverversion ); } set { SetValue( () => GpuDriverversion, value ); } }
		public string GpuIdentifier { get { return GetValue( () => GpuIdentifier ); } set { SetValue( () => GpuIdentifier, value ); } }
		public string ProxyIP { get { return GetValue( () => ProxyIP ); } set { SetValue( () => ProxyIP, value ); } }
		public string ProxyUsername { get { return GetValue( () => ProxyUsername ); } set { SetValue( () => ProxyUsername, value ); } }
		public string ProxyPassword { get { return GetValue( () => ProxyPassword ); } set { SetValue( () => ProxyPassword, value ); } }
		
		public HWSettings(ulong totalPhysRam, string windowsUserLogin, string computername, string windowsKey, string networkAdapterGuid, string networkAddress,
		                  string macAddress, string processorIdent, string processorRev, string processorCoreAmount,
		                  string processorLevel, string gpuDescription, uint gpuDeviceId, uint gpuVendorId, uint gpuRevision,
		                  long gpuDriverversion , string gpuIdentifier, string proxyIP, string proxyUsername,string proxyPassword ){
			
			
			TotalPhysRam = totalPhysRam;
			WindowsUserLogin = windowsUserLogin;
			Computername = computername;
			WindowsKey = windowsKey;
			NetworkAdapterGuid = networkAdapterGuid;
			MacAddress = macAddress;
			NetworkAddress = networkAddress;
			ProcessorIdent = processorIdent;
			ProcessorRev = processorRev;
			ProcessorCoreAmount = processorCoreAmount;
			ProcessorLevel = processorLevel;
			GpuDescription = gpuDescription;
			GpuDeviceId = gpuDeviceId;
			GpuVendorId = gpuVendorId;
			GpuRevision = gpuRevision;
			GpuDriverversion = gpuDriverversion;
			GpuIdentifier = gpuIdentifier;
			ProxyIP = proxyIP;
			ProxyUsername = proxyUsername;
			ProxyPassword = proxyPassword;
			
		}
		
		
		public string GenerateMacAddress(){
			Random random = new Random();
			byte[] array = new byte[6];
			random.NextBytes(array);
			string text = string.Concat((
				from byte_0 in array
				select string.Format("{0}-", byte_0.ToString("X2"))).ToArray<string>());
			return text.TrimEnd(new char[]
			                    {
			                    	'-'
			                    });
		}
		
		public string GenerateWindowsKey(){
			Random random = new Random();
			string text = string.Concat(new object[]
			                            {
			                            	"00",
			                            	random.Next(100, 1000),
			                            	"-OEM-",
			                            	random.Next(1000000, 9999999),
			                            	"-00",
			                            	random.Next(100, 999)
			                            });
			
			return text;
		}
		
		public string GenerateIpAddress(){
			Random random = new Random();
			string text = string.Concat(new object[]
			                            {
			                            	"192.168.",
			                            	random.Next(0, 255),
			                            	".",
			                            	random.Next(1, 255)
			                            });
			return text;
			
		}
		
		public string GenerateRamSize(){
			Random random = new Random();
			string[] sizes = new string[] { "2048", "4096", "8192", "16384" };
			return sizes[random.Next(sizes.Length)];
		}

		
		public HWSettings(){
			
		}
	}
}
