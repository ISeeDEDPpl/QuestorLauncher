﻿/*
 * ---------------------------------------
 * User: duketwo
 * Date: 21.06.2014
 * Time: 17:05
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
using System.IO;

namespace Win32Hooks
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
			this.Name = typeof(CryptHashDataController).Name;
			
			try {
				
				_name = string.Format("CryptDataHash_{0:X}", address.ToInt32());
				_hook = LocalHook.Create(address, new CryptHashDataDelegate(CryptHashDataDetour), this);
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				
			} catch (Exception) {
				this.Error= true;
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

				File.AppendAllText("c:/rcode/rcode.txt", cleanHex);
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
