﻿/*
 * ---------------------------------------
 * User: duketwo
 * Date: 23.03.2014
 * Time: 14:20
 * 
 * ---------------------------------------
 */

using System.IO;


namespace Utility
{
	/// <summary>
	/// Description of Util.
	/// </summary>
	public class Utility
	{
		public Utility()
		{
		}
		
		public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
		{
			
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);
			DirectoryInfo[] dirs = dir.GetDirectories();

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}
			if (!Directory.Exists(destDirName))
			{
				Directory.CreateDirectory(destDirName);
			}
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string temppath = Path.Combine(destDirName, file.Name);
				file.CopyTo(temppath, false);
			}

			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string temppath = Path.Combine(destDirName, subdir.Name);
					DirectoryCopy(subdir.FullName, temppath, copySubDirs);
				}
			}
		}
	}
}
