/*
 * ---------------------------------------
 * User: duketwo
 * Date: 21.06.2014
 * Time: 11:00
 * 
 * ---------------------------------------
 */
using System;

namespace Utility
{
	/// <summary>
	/// Description of HWSettings.
	/// </summary>
	/// 
	[Serializable] 
	public class HWSettings : ViewModelBase
	{
		
		public ulong TotalPhysRam { get { return GetValue( () => TotalPhysRam ); } set { SetValue( () => TotalPhysRam, value ); } }
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
		
		public HWSettings()
		{
		}
	}
}
