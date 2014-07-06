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
	public class EveSetting : ViewModelBase {
		

		public EveSetting(string eveDirectory, string redGuardDirectory ,DateTime last24HourTS, bool eveServerStatusThread)
		{
			EveDirectory = eveDirectory;
			RedGuardDirectory = redGuardDirectory;
			Last24HourTS = last24HourTS;
			EveServerStatusThread = eveServerStatusThread;
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
	        private set
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

	    public bool EveServerStatusThread
	    {
	        get
	        {
	            return GetValue( () => EveServerStatusThread );
	        }
	        set
	        {
	            SetValue( () => EveServerStatusThread, value );
	        }
	    }
	}
}
