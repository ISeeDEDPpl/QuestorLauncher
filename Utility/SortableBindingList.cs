using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Library.Forms
{
	/// <summary>
	/// Provides a generic collection that supports data binding and additionally supports sorting.
	/// See http://msdn.microsoft.com/en-us/library/ms993236.aspx
	/// If the elements are IComparable it uses that; otherwise compares the ToString()
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	/// 
	[Serializable]
	public class SortableBindingList<T> : BindingList<T> where T : class
	{
		private bool _isSorted;
		private ListSortDirection _sortDirection = ListSortDirection.Ascending;
		private PropertyDescriptor _sortProperty;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SortableBindingList{T}"/> class.
		/// </summary>
		public SortableBindingList()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SortableBindingList{T}"/> class.
		/// </summary>
		/// <param name="list">An <see cref="T:System.Collections.Generic.IList`1" /> of items to be contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</param>
		public SortableBindingList(IList<T> list)
			:base(list)
		{
		}
		
		/// <summary>
		/// Gets a value indicating whether the list supports sorting.
		/// </summary>
		protected override bool SupportsSortingCore
		{
			get { return true; }
		}
		
		/// <summary>
		/// Gets a value indicating whether the list is sorted.
		/// </summary>
		protected override bool IsSortedCore
		{
			get { return _isSorted; }
		}
		
		/// <summary>
		/// Gets the direction the list is sorted.
		/// </summary>
		protected override ListSortDirection SortDirectionCore
		{
			get { return _sortDirection; }
		}
		
		/// <summary>
		/// Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class; otherwise, returns null
		/// </summary>
		protected override PropertyDescriptor SortPropertyCore
		{
			get { return _sortProperty; }
		}
		
		/// <summary>
		/// Removes any sort applied with ApplySortCore if sorting is implemented
		/// </summary>
		protected override void RemoveSortCore()
		{
			_sortDirection = ListSortDirection.Ascending;
			_sortProperty = null;
		}
		
		/// <summary>
		/// Sorts the items if overridden in a derived class
		/// </summary>
		/// <param name="prop"></param>
		/// <param name="direction"></param>
		protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			_sortProperty = prop;
			_sortDirection = direction;
			
			List<T> list = Items as List<T>;
			if (list == null) return;
			
			list.Sort(Compare);
			
			_isSorted = true;
			//fire an event that the list has been changed.
			OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}
		
		//XmlDeserialize method
		public  SortableBindingList<T> XmlDeserialize(string fileName)
		{
			using(CrossProcessLockFactory.CreateCrossProcessLock(Path.GetFileName(fileName))){
				string data = System.IO.File.ReadAllText(fileName);
				System.Xml.Serialization.XmlSerializer xmlSer = new System.Xml.Serialization.XmlSerializer(typeof(SortableBindingList<T>));
				TextReader reader = new StringReader(data);
				object obj = xmlSer.Deserialize(reader);
				
				return (SortableBindingList<T>)obj;

			}
		}

		//XmlSerialize method
		public  void XmlSerialize(string fileName)
		{
			
			using(CrossProcessLockFactory.CreateCrossProcessLock(Path.GetFileName(fileName))){
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
				{
					
					System.Xml.Serialization.XmlSerializer xmlSer = new System.Xml.Serialization.XmlSerializer(this.GetType());
					StringWriter textWriter = new StringWriter();
					xmlSer.Serialize(textWriter, this);
					xmlSer = null;
					file.Write(textWriter.ToString());

				}
			}
		}

		
		private int Compare(T lhs, T rhs)
		{
			var result = OnComparison(lhs, rhs);
			//invert if descending
			if (_sortDirection == ListSortDirection.Descending)
				result = -result;
			return result;
		}
		
		private int OnComparison(T lhs, T rhs)
		{
			object lhsValue = lhs == null ? null : _sortProperty.GetValue(lhs);
			object rhsValue = rhs == null ? null : _sortProperty.GetValue(rhs);
			if (lhsValue == null)
			{
				return (rhsValue == null) ? 0 : -1; //nulls are equal
			}
			if (rhsValue == null)
			{
				return 1; //first has value, second doesn't
			}
			if (lhsValue is IComparable)
			{
				return ((IComparable)lhsValue).CompareTo(rhsValue);
			}
			if (lhsValue.Equals(rhsValue))
			{
				return 0; //both are the same
			}
			//not comparable, compare ToString
			return lhsValue.ToString().CompareTo(rhsValue.ToString());
		}
		
		
		
//		public bool IsFileLocked(string fileName){
//			return File.Exists(Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName) + ".lock");
//		}
//
//		public void LockFile(string fileName){
//
//			fileName = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName) + ".lock";
//			FileStream myFileStream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
//			myFileStream.Close();
//			myFileStream.Dispose();
//			File.SetLastWriteTimeUtc(fileName, DateTime.UtcNow);
//		}
//
//		public void UnlockFile(string fileName){
//			File.Delete(Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName) + ".lock");
//		}
	}
}