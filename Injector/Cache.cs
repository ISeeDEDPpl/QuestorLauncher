/*
* ---------------------------------------
* User: duketwo
* Date: 07.03.2014
* Time: 15:50
* 
* ---------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Threading;
using Utility;

namespace Injector
{
	/// <summary>
	/// Description of Cache.
	/// </summary>
	public class Cache
	{
		private static Cache _instance = new Cache();
		public static int CacheInstances = 0;
		public delegate void Message(string msg);
		public static event Message OnMessage;
		
		public Cache()
		{
			Interlocked.Increment(ref CacheInstances);
		}

        private SerializeableSortableBindingList<EveAccount> _eveAccountSerializeableSortableBindingList;
	    public SerializeableSortableBindingList<EveAccount> EveAccountSerializeableSortableBindingList
	    {
	        get
	        {
                _eveAccountSerializeableSortableBindingList = new SerializeableSortableBindingList<EveAccount>("AcccountData.xml", 0);

	            if (_eveAccountSerializeableSortableBindingList != null)
	            {
	                return _eveAccountSerializeableSortableBindingList;
	            }
	            else
	            {
                    return null;
	            }
	        }
	    } 
		
		private SerializeableSortableBindingList<EveSetting> eveSettingsSerializeableSortableBindingList = new SerializeableSortableBindingList<EveSetting>("EveSettings.xml", 0);

	    private EveSetting _defaultEveSettings;
        public EveSetting EveSettings 
        {
			get 
            {
                try
                {
                    _defaultEveSettings = new EveSetting("C:\\eveoffline\\bin\\exefile.exe", "C:\\redguard\\", DateTime.MinValue, true);

                    if (eveSettingsSerializeableSortableBindingList == null)
                    {
                        return _defaultEveSettings;
                    }

                    if (eveSettingsSerializeableSortableBindingList.List == null)
                    {
                        return _defaultEveSettings;
                    }

                    if (eveSettingsSerializeableSortableBindingList != null && eveSettingsSerializeableSortableBindingList.List != null)
                    {
                        if (!eveSettingsSerializeableSortableBindingList.List.Any())
                        {
                            return _defaultEveSettings;
                        }

                        return (EveSetting)eveSettingsSerializeableSortableBindingList.List[0];
                    }

                    Log("[EveSettings] EveSettings is null");
                    return null;
                }
                catch (Exception ex)
                {
                    Log("[EveSettings] Exception [" + ex + "]");
                    return null;
                }
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
		
		public string EveLocation 
        {
			get 
            {
                try
                {
                    if (Cache.Instance.EveSettings != null)
                    {
                        return Cache.Instance.EveSettings.EveDirectory;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    Log("[EveLocation] Exception [" + ex + "]");
                    return null;
                }
            }
		}
		
		public void Log(string text)
		{
			try 
            {
				if (OnMessage != null)
				{
					OnMessage(DateTime.UtcNow.ToString() + " " + text.ToString());
				}
			} 
            catch (Exception) 
            {
				
			}
		}
		
		
		public static Cache Instance
		{
			get { return _instance; }
		}
		
		~Cache()
		{
			Interlocked.Decrement(ref CacheInstances);
		}
		
	}
}
