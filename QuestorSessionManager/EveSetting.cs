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

namespace QuestorSessionManager
{
	/// <summary>
	/// Description of EveAccountData.
	/// </summary>
	
	[Serializable]
	public class EveSetting : ViewModelBase 
    {
	    public EveSetting(string exeFileLocation, string redGuardDirectory ,DateTime last24HourTS, bool eveServerStatusThread)
		{
	        try
	        {
                EXEFileLocation = exeFileLocation;
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

        private string _exeFileLocation;
		public string EXEFileLocation 
        {
            get
            {
                if (string.IsNullOrEmpty(_exeFileLocation))
                {
                    return _exeFileLocation;
                }

                return string.Empty;
            }
		    set
            {
                _exeFileLocation = value;
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
