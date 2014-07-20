/*
 * ---------------------------------------
 * User: duketwo
 * Date: 21.06.2014
 * Time: 17:05
 * 
 * ---------------------------------------
 */

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using EasyHook;

namespace HookManager.Win32Hooks
{
	/// <summary>
	/// Description of CryptHashDataController.
	/// </summary>
	public class CryptHashDataController : IDisposable, IHook
	{
		[UnmanagedFunctionPointer(CallingConvention.Winapi)]
		private delegate bool CryptHashDataDelegate(IntPtr hHash, IntPtr pbData, Int32 dwDataLen, uint dwFlags);

		private string _name;
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }

		[DllImport("advapi32.dll")]
		public static extern bool CryptHashData(IntPtr hHash, IntPtr pbData, Int32 dwDataLen, uint dwFlags);

		public CryptHashDataController(IntPtr address)
		{
			Name = typeof(CryptHashDataController).Name;
			
			try 
            {	
				_name = string.Format("CryptDataHash_{0:X}", address.ToInt32());
				_hook = LocalHook.Create(address, new CryptHashDataDelegate(CryptHashDataDetour), this);
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				
			} 
            catch (Exception) 
            {
				Error= true;
			}
		}

		DateTime boot = DateTime.Now;
		private bool CryptHashDataDetour(IntPtr hHash, IntPtr pbData, Int32 dwDataLen, uint dwFlags)
		{
			var result = CryptHashData(hHash, pbData, dwDataLen, dwFlags);

			if (DateTime.Now.Subtract(boot).TotalSeconds >= 6)
			{
				byte[] bytes = new byte[dwDataLen];
				Marshal.Copy(pbData, bytes, 0, dwDataLen);
				var hexText = Encoding.ASCII.GetString(bytes);

				var cleanHex = "";
				for (int i = 0; i < hexText.Length - 1; i++)
				{
					var charretje = hexText[i];
					if (charretje != 3)
						cleanHex += hexText[i].ToString();

				}

				cleanHex += 3;
				File.AppendAllText("c:/rcode2.txt", cleanHex);
			}

			return result;
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
