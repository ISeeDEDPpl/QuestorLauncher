/*
 * ---------------------------------------
 * User: duketwo
 * Date: 19.03.2014
 * Time: 18:51
 * 
 * ---------------------------------------
 */

using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using Library.Forms;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace Utility
{
	/// <summary>
	/// Description of SerializeableSortableBindingList.
	/// </summary>
	[Serializable]
	public class SerializeableSortableBindingList<T> where T : class
	{
		protected SortableBindingList<T> _List;
		private string FilePathName { get; set; }
		private string FileName { get; set; }
		private DateTime LastSerialize { get; set; }
		private int WriteDelayMs { get; set; }

		
		public SerializeableSortableBindingList(string filePathName, int writeDelayMs) {
			
			this.FilePathName = AssemblyPath + "\\" + filePathName;
			this.LastSerialize = DateTime.MinValue;
			this.FileName = Path.GetFileName(this.FilePathName);

			this._List = new SortableBindingList<T>();
			this.WriteDelayMs = writeDelayMs;
			
			if(!File.Exists(this.FilePathName)) {
				_List = new SortableBindingList<T>();

				_List.XmlSerialize(this.FilePathName);
				_List.XmlDeserialize(this.FilePathName);
			} else {
				this._List = _List.XmlDeserialize(this.FilePathName);
			}
			this._List.ListChanged += OnListChangeHandler;
		}
		
		private void OnListChangeHandler(object sender, ListChangedEventArgs e) {
			
			OnListChange();
		}
		
		private void OnListChange(){
			
			if(this.WriteDelayMs > 0) {
				if(DateTime.UtcNow >= LastSerialize.AddMilliseconds(this.WriteDelayMs)) {
					this.LastSerialize = DateTime.UtcNow;
					_List.XmlSerialize(this.FilePathName);
					
				}
			} else {
				
				_List.XmlSerialize(this.FilePathName);
				
			}
		}
		
		public SortableBindingList<T> List{
			get {
				return _List;

			}
		}
		
		
		string _assemblyPath;
		public string AssemblyPath {
			get {
				if (_assemblyPath == null) {
					
					_assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				}
				return _assemblyPath;
			}
		}
	}
}
