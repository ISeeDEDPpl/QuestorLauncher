/*
 * ---------------------------------------
 * User: duketwo
 * Date: 19.03.2014
 * Time: 20:51
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
using System.Security.Principal;
using System.Security.AccessControl;


public class CrossProcessLockFactory
{
	
	public static IDisposable CreateCrossProcessLock()
	{
		return new ProcessLock(-1);
	}

	public static IDisposable CreateCrossProcessLock(int delayMs)
	{
		return new ProcessLock(delayMs);
	}
	
	public static IDisposable CreateCrossProcessLock(string uniqueString)
	{
		return new ProcessLock(-1,uniqueString);
	}
	
	
	public static IDisposable CreateCrossProcessLock(int delayMs, string uniqueString)
	{
		return new ProcessLock(delayMs,uniqueString);
	}
	
}

public class ProcessLock : IDisposable
{
	// the name of the global mutex;
	//private const string MutexName = "FAA9569-7DFE-4D6D-874D-19123FB16CBC-8739827-[SystemSpecicString]";

	private Mutex _globalMutex;

	private bool _owned = false;
	

	public ProcessLock(int delayMs, string uniqueString = "default")
	{
		try
		{
			uniqueString = "FAA9569-7DFE-4D6D-874D-19123FB16CBC-8739827-[" + uniqueString + "]";
			_globalMutex = new Mutex(true, uniqueString, out _owned);
			if (_owned == false)
			{
				// did not get the mutex, wait for it.
				_owned = _globalMutex.WaitOne(delayMs);
			}
		}
		catch (Exception ex)
		{
			if(ex is AbandonedMutexException){
				// is thrown if another process is terminated, the process who receives this now owns the mutex.
			} else {
				Trace.TraceError(ex.Message);
				throw;
			}
		}
	}

	public void Dispose()
	{
		if (_owned)
		{
			_globalMutex.ReleaseMutex();
		}
		_globalMutex = null;
	}
}