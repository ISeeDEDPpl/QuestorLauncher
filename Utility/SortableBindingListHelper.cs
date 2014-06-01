/*
* ---------------------------------------
* User: duketwo
* Date: 20.03.2014
* Time: 23:08
* 
* ---------------------------------------
*/
using System;
using Library.Forms;
using System.Collections.Generic;

namespace Utility
{
	/// <summary>
	/// Description of SortableBindingListHelper.
	/// </summary>
	public static class SortableBindingListHelper<T> where T : class
	{
		public static void CopyTo (SortableBindingList<T> from, SortableBindingList<T> to){
			to.Clear();
			foreach(var t in from){
				to.Add(t);
			}
		}
		
		public static void CopyTo (IList<T> from, IList<T> to){
			to.Clear();
			foreach(var t in from){
				to.Add(t);
			}
		}
	}
}
