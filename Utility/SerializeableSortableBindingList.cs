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
using System.Reflection;
using System.IO;
using Library.Forms;

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
		private DateTime LastSerialize { get; set; }
		private int WriteDelayMs { get; set; }
        
		public SerializeableSortableBindingList(string filePathName, int writeDelayMs) 
        {
		    try
		    {
		        this.FilePathName = AssemblyPath + "\\" + filePathName;
		        this.LastSerialize = DateTime.MinValue;
		        
		        this._List = new SortableBindingList<T>();
		        this.WriteDelayMs = writeDelayMs;

		        if (!File.Exists(this.FilePathName))
		        {
		            _List = new SortableBindingList<T>();

		            _List.XmlSerialize(this.FilePathName);
		            _List.XmlDeserialize(this.FilePathName);
		        }
		        else
		        {
		            this._List = _List.XmlDeserialize(this.FilePathName);
		        }

		        if (_List != null)
		        {
                    this._List.ListChanged += OnListChangeHandler;    
		        }
		    }
		    catch (Exception ex)
		    {
		        Console.WriteLine("[SerializeableSortableBindingList] Exception [" + ex + "]");
		    }
        }
		
		private void OnListChangeHandler(object sender, ListChangedEventArgs e) {
			
			OnListChange();
		}
		
		private void OnListChange()
        {
			if(this.WriteDelayMs > 0) 
            {
                if (DateTime.UtcNow < LastSerialize.AddMilliseconds(this.WriteDelayMs)) return;
                
                this.LastSerialize = DateTime.UtcNow;
                _List.XmlSerialize(this.FilePathName);
            } 
            else 
            {
				_List.XmlSerialize(this.FilePathName);
				
			}
		}
		
		public SortableBindingList<T> List
        {
			get 
            {
				return _List;
			}
		}
		
		
		string _assemblyPath;
		public string AssemblyPath 
        {
			get 
            {
				if (_assemblyPath == null) 
                {	
					_assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				}

				return _assemblyPath;
			}
		}
	}
}
