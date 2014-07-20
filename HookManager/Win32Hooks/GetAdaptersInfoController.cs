/*
 * ---------------------------------------
 * User: duketwo
 * Date: 21.06.2014
 * Time: 16:57
 * 
 * ---------------------------------------
 */

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using EasyHook;

namespace HookManager.Win32Hooks
{
	/// <summary>
	/// Description of GetAdaptersInfoController.
	/// </summary>
	public class GetAdaptersInfoController : IDisposable, IHook
	{
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
		private delegate int GetAdaptersInfoDelegate(IntPtr AdaptersInfo, IntPtr OutputBuffLen);

		private string _name;
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }
		

		[DllImport("Iphlpapi.dll", SetLastError = true)]
		public static extern int GetAdaptersInfo(IntPtr AdaptersInfo, IntPtr OutputBuffLen);

		private string _guid;
		private string _mac;
		private string _address;
		public GetAdaptersInfoController(IntPtr address, string guid, string mac, string ipaddress)
		{
			Name = typeof(GetAdaptersInfoController).Name;
			_guid = guid;
			_mac = mac;
			_address = ipaddress;
			
			try 
            {
				_name = string.Format("GetAdaptersInfoHook_{0:X}", address.ToInt32());
				_hook = LocalHook.Create(address, new GetAdaptersInfoDelegate(GetAdaptersInfoDetour), this);
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
			}
            catch (Exception)
            {	
				Error = true;
			}
		}

		private int GetAdaptersInfoDetour(IntPtr AdaptersInfo, IntPtr OutputBuffLen)
		{
			int result = GetAdaptersInfo(AdaptersInfo, OutputBuffLen);
			if (AdaptersInfo != IntPtr.Zero)
			{
				IP_ADAPTER_INFO structureBefore = (IP_ADAPTER_INFO)Marshal.PtrToStructure(AdaptersInfo, typeof(IP_ADAPTER_INFO));
				string macBefore = BitConverter.ToString(structureBefore.Address);
				
				global::HookManager.Win32Hooks.HookManager.Log("[GetAdaptersInfoDetour] Before: IP: " + structureBefore.IpAddressList.IpAddress.Address.ToString() + " GUID: " + structureBefore.AdapterName + " MAC: " + macBefore, Color.Orange);
				structureBefore.AdapterName = _guid.ToUpper();
				for (int i = 0; i < structureBefore.Address.Length - 1; i = i + 2)
				{
					string tekst = structureBefore.Address[i].ToString("X2", System.Globalization.CultureInfo.InvariantCulture);
					if (tekst == "00")
						tekst = "0";

					structureBefore.Address[i] = Convert.ToByte(_mac.Replace("-", "")[i].ToString() + _mac.Replace("-", "")[i + 1].ToString(), 16);
					structureBefore.Next = IntPtr.Zero;
				}
				
				structureBefore.IpAddressList.IpAddress.Address = _address;
				Marshal.StructureToPtr(structureBefore, AdaptersInfo, true);
				IP_ADAPTER_INFO structureAfter = (IP_ADAPTER_INFO)Marshal.PtrToStructure(AdaptersInfo, typeof(IP_ADAPTER_INFO));
				string macAfter = BitConverter.ToString(structureAfter.Address);
				
				global::HookManager.Win32Hooks.HookManager.Log("[GetAdaptersInfoDetour] After: IP: " + structureAfter.IpAddressList.IpAddress.Address.ToString() + " GUID: " + structureAfter.AdapterName + " MAC: " + macAfter, Color.Orange);
			}
			
			return result;
		}
		
		private string ByteArrayToString(byte[] arr)
		{
			System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
			return enc.GetString(arr);
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public struct IP_ADAPTER_INFO
		{
			public IntPtr Next;
			public Int32 ComboIndex;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256 + 4)]
			public string AdapterName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128 + 4)]
			public string AdapterDescription;
			public UInt32 AddressLength;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public byte[] Address;
			public Int32 Index;
			public UInt32 Type;
			public UInt32 DhcpEnabled;
			public IntPtr CurrentIpAddress;
			public IP_ADDR_STRING IpAddressList;
			public IP_ADDR_STRING GatewayList;
			public IP_ADDR_STRING DhcpServer;
			public bool HaveWins;
			public IP_ADDR_STRING PrimaryWinsServer;
			public IP_ADDR_STRING SecondaryWinsServer;
			public Int32 LeaseObtained;
			public Int32 LeaseExpires;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public struct IP_ADDR_STRING
		{
			public IntPtr Next;
			public IP_ADDRESS_STRING IpAddress;
			public IP_ADDRESS_STRING IpMask;
			public Int32 Context;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public struct IP_ADDRESS_STRING
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string Address;
		}

		public void Dispose()
		{
			if (_hook == null)
				return;

			_hook.Dispose();
			_hook = null;
		}
	}
}
