/*
 * ---------------------------------------
 * User: duketwo
 * Date: 21.06.2014
 * Time: 17:02
 * 
 * ---------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using EasyHook;
using System.Windows.Forms;
using System.Drawing;

namespace Win32Hooks
{
	/// <summary>
	/// Description of DX9Controller.
	/// </summary>
	public class DX9Controller : IDisposable, IHook
	{
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate UInt32 GetAdapterIdentifierDelegate(UInt32 Adapter, UInt64 Flags, [In][Out] IntPtr pIdentifier);
		private GetAdapterIdentifierDelegate GetAdapterIdentifierOriginal;

		[DllImport("d3d9.dll")]
		private static extern IntPtr Direct3DCreate9(uint sdkVersion);

		private string _name;
		private LocalHook _hook;
		private Injector.HWSettings _settings;
		public bool Error { get; set; }
		public string Name  { get; set; }

		public DX9Controller(Injector.HWSettings settings)
		{
			this._settings = settings;
			this.Name = typeof(DX9Controller).Name;
			IntPtr direct3D = Direct3DCreate9(32);
			
			
			try {
				if (direct3D == IntPtr.Zero){
					MessageBox.Show("error");
					throw new Exception("Failed to create D3D.");
				}

				IntPtr adapterIdentPtr = Marshal.ReadIntPtr(Marshal.ReadIntPtr(direct3D), 20);

				GetAdapterIdentifierOriginal = (GetAdapterIdentifierDelegate)Marshal.GetDelegateForFunctionPointer(adapterIdentPtr, typeof(GetAdapterIdentifierDelegate));

				_name = string.Format("GetAdapterIdentHook_{0:X}", adapterIdentPtr.ToInt32());
				_hook = LocalHook.Create(adapterIdentPtr, new GetAdapterIdentifierDelegate(GetAdapterIdentifierDetour), this);
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				
			} catch (Exception ex) {
				
				HookManager.Log("[DX9Controller] Exception: " + ex.ToString());
				this.Error = true;
			}
		}

		private UInt32 GetAdapterIdentifierDetour(UInt32 Adapter, UInt64 Flags, [In][Out] IntPtr pIdentifier)
		{
			
			var result = GetAdapterIdentifierOriginal(Adapter, Flags, pIdentifier);
			
			
			D3DADAPTER_IDENTIFIER9 newStructBefore = (D3DADAPTER_IDENTIFIER9)Marshal.PtrToStructure(pIdentifier, typeof(D3DADAPTER_IDENTIFIER9));
			string before = string.Format("[DX9Controller] Before: Description: {0} GpuDeviceId: {1} GpuIdentifier: {2} GpuDriverversion: {3} GpuRevision: {4} GpuVendorId: {5}",
			                              newStructBefore.Description,newStructBefore.DeviceId.ToString(), newStructBefore.DeviceIdentifier.ToString(), ((long)newStructBefore.DriverVersion.QuadPart).ToString(), newStructBefore.Revision.ToString(), newStructBefore.VendorId.ToString(), Color.Orange);
			HookManager.Log(before, Color.Orange);
		
			newStructBefore.Description = _settings.GpuDescription;
			newStructBefore.DeviceId = _settings.GpuDeviceId;
			
			newStructBefore.DeviceIdentifier = Guid.Parse(_settings.GpuIdentifier);
			
			newStructBefore.DriverVersion.QuadPart = _settings.GpuDriverversion;
			newStructBefore.Revision = _settings.GpuRevision;
			newStructBefore.VendorId = _settings.GpuVendorId;
			Marshal.StructureToPtr(newStructBefore, pIdentifier, true);
			
			D3DADAPTER_IDENTIFIER9 newStructAfter = (D3DADAPTER_IDENTIFIER9)Marshal.PtrToStructure(pIdentifier, typeof(D3DADAPTER_IDENTIFIER9));
			
			string after = string.Format("[DX9Controller] After: Description: {0} GpuDeviceId: {1} GpuIdentifier: {2} GpuDriverversion: {3} GpuRevision: {4} GpuVendorId: {5}",
			                             newStructAfter.Description,newStructAfter.DeviceId.ToString(), newStructAfter.DeviceIdentifier.ToString(), ((long)newStructAfter.DriverVersion.QuadPart).ToString(), newStructAfter.Revision.ToString(), newStructAfter.VendorId.ToString(), Color.Orange);
			HookManager.Log(after, Color.Orange);
			return 0; // D3D_OK
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct _GUID
		{
			public Int32 Data1;
			public Int16 Data2;
			public Int16 Data3;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public byte[] Data4;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct D3DADAPTER_IDENTIFIER9
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			public string Driver;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			public string Description;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string DeviceName;
			[MarshalAs(UnmanagedType.Struct)]
			public LARGE_INTEGER DriverVersion;
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 VendorId;
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 DeviceId;
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 SubSysId;
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 Revision;
			[MarshalAs(UnmanagedType.Struct)]
			public Guid DeviceIdentifier;
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 WHQLLevel;
		}

		[StructLayout(LayoutKind.Explicit, Size = 8)]
		public struct LARGE_INTEGER
		{
			[FieldOffset(0)]
			public Int64 QuadPart;
			[FieldOffset(0)]
			public UInt32 LowPart;
			[FieldOffset(4)]
			public UInt32 HighPart;
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
