/*
/*
 * ---------------------------------------
 * User: duketwo
 * Date: 24.12.2013
 * Time: 16:26
 * 
 * ---------------------------------------
 */
using System;
using Utility;

namespace Injector
{
	/// <summary>
	/// Description of EveAccountData.
	/// </summary>
	
	[Serializable]
	public class EveSetting : ViewModelBase 
    {
	    public EveSetting(string eveDirectory, string redGuardDirectory ,DateTime last24HourTS, bool eveServerStatusThread)
		{
	        try
	        {
                EveDirectory = eveDirectory;
                RedGuardDirectory = redGuardDirectory;
                Last24HourTS = last24HourTS;
                EveServerStatusThread = eveServerStatusThread;
	        }
	        catch (Exception ex)
	        {
	            Console.WriteLine("[EveSetting] Exception [" + ex + "]");
	        }
		}
		
		public EveSetting()
        { 
		}

	    private string _redGuardDirectory;
        
	    public string RedGuardDirectory
	    {
	        get
	        {
	            if (string.IsNullOrEmpty(_redGuardDirectory))
	            {
	                return _redGuardDirectory;
	            }

	            return string.Empty;
	        }
	        set
	        {
	            _redGuardDirectory = value;
	        }
	    }

	    private string _eveDirectory;
		public string EveDirectory 
        {
            get
            {
                if (string.IsNullOrEmpty(_eveDirectory))
                {
                    return _eveDirectory;
                }

                return string.Empty;
            }
		    set
            {
                _eveDirectory = value;
            }
        }

	    public DateTime Last24HourTS
	    {
	        get
	        {
	            return GetValue( () => Last24HourTS );
	        }
	        set
	        {
	            SetValue( () => Last24HourTS, value );
	        }
	    }


	    public bool? _eveServerStatusThread;
	    public bool? EveServerStatusThread
	    {
            get
            {
                if (_eveServerStatusThread != null)
                {
                    return _eveServerStatusThread;
                }

                return null;
            }
            set
            {
                _eveServerStatusThread = value;
            }
	    }
	}
}
