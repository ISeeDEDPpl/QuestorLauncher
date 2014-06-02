/*
 * created by duketwo
 * Date: 31.05.2014
 * Time: 13:21
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO.Compression;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Injector
{
	/// <summary>
	/// Description of Questor.
	/// </summary>
	public class Questor
	{
		private static Questor _instance = new Questor();
		public static int QuestorInstances = 0;
		
		public Questor()
		{
			Interlocked.Increment(ref QuestorInstances);
		}
		
		
		public void DownloadQuestor() {
			string qLink = "https://github.com/ISeeDEDPpl/Questor/archive/BleedingEdge.zip";
			
			Cache.Instance.Log("Downloading Questor from: " + qLink);
			
			WebClient webClient = new WebClient();
			string questorSourcePath = Cache.Instance.AssemblyPath + "\\QuestorSource\\";
			if(!Directory.Exists(questorSourcePath))
				Directory.CreateDirectory(questorSourcePath);
			webClient.DownloadFile(qLink, questorSourcePath + "BleedingEdge.zip");
			
			Cache.Instance.Log("Finished Downloading Questor from: " + qLink);
		}
		
		public void ExtractQuestor(){
			
			string questorSourcePath = Cache.Instance.AssemblyPath + "\\QuestorSource\\";
			string qZipFile = questorSourcePath + "BleedingEdge.zip";
			
			Cache.Instance.Log("Extracting Questor from: " + qZipFile);
			
			
			if(Directory.Exists(questorSourcePath + "Questor-BleedingEdge"))
				Directory.Delete(questorSourcePath + "Questor-BleedingEdge",true);
			ZipFile.ExtractToDirectory(qZipFile,  questorSourcePath);
			
			if(File.Exists(Cache.Instance.AssemblyPath + "\\DirectEve\\DirectEve.dll")) {
				Cache.Instance.Log("DirectEve.dll found, copying");
				File.Copy(Cache.Instance.AssemblyPath + "\\DirectEve\\DirectEve.dll",questorSourcePath + "Questor-BleedingEdge\\DirectEve\\DirectEve.dll",true);
			}
			
			Cache.Instance.Log("Done Extracting Questor from: " + qZipFile);
		}
		
		public void CompileQuestor(){
			
			Cache.Instance.Log("Compiling Questor.");
			string questorSourcePathBE = Cache.Instance.AssemblyPath + "\\QuestorSource\\Questor-BleedingEdge\\";
			
			Process compileProcess = new Process();
			compileProcess.StartInfo.UseShellExecute = false;
			compileProcess.StartInfo.FileName = questorSourcePathBE + "#compile#.bat";
			compileProcess.StartInfo.Arguments = "/nopause";
			compileProcess.StartInfo.WorkingDirectory = questorSourcePathBE;
			compileProcess.StartInfo.CreateNoWindow = true;
			compileProcess.Start();
			
			while(!compileProcess.HasExited){
				Thread.Sleep(10);
			}
			Cache.Instance.Log("Done Compiling Questor.");
		}
		
		public void CopyQuestorBinary(){
			
			Cache.Instance.Log("Copying Questor.");
			string questorSourcePath = Cache.Instance.AssemblyPath + "\\QuestorSource\\Questor-BleedingEdge\\output";
			string questorDestinationPath = Cache.Instance.AssemblyPath + "\\Questor\\";
			if(!Directory.Exists(questorSourcePath))
				return;
			
			if(!Directory.Exists(questorDestinationPath))
				Directory.CreateDirectory(questorDestinationPath);
			
			//Now Create all of the directories
			foreach (string dirPath in Directory.GetDirectories(questorSourcePath, "*", SearchOption.AllDirectories)) {
				if(!Directory.Exists(dirPath.Replace(questorSourcePath, questorDestinationPath)))
					Directory.CreateDirectory(dirPath.Replace(questorSourcePath, questorDestinationPath));
			}
			//Copy all the files & Replaces any files with the same name
			foreach (string newPath in Directory.GetFiles(questorSourcePath, "*.*",SearchOption.AllDirectories)) {
				if(newPath.EndsWith(".exe") || newPath.EndsWith(".dll"))
					File.Copy(newPath, newPath.Replace(questorSourcePath, questorDestinationPath), true);
				else {
					
					if(!File.Exists(newPath.Replace(questorSourcePath, questorDestinationPath)))
						File.Copy(newPath, newPath.Replace(questorSourcePath, questorDestinationPath), false);
					
				}
				
			}
			
			foreach(String f in Directory.GetFiles(Cache.Instance.AssemblyPath + "\\QuestorSource\\Questor-BleedingEdge\\DirectEve")) {
				File.Copy(f,f.Replace(Cache.Instance.AssemblyPath + "\\QuestorSource\\Questor-BleedingEdge\\DirectEve", questorDestinationPath),true);
			}
			
			if(File.Exists(Cache.Instance.AssemblyPath + "\\DirectEve\\DirectEve.lic")) {
				Cache.Instance.Log("DirectEve.lic found, copying");
				File.Copy(Cache.Instance.AssemblyPath + "\\DirectEve\\DirectEve.lic", questorDestinationPath + "DirectEve.lic",true);
			}
			
			Cache.Instance.Log("Done Copying Questor.");
			Cache.Instance.Log("Make sure you have a valid DirectEve.dic file in the .\\Questor Directory");
			Cache.Instance.Log("Make sure you are using the most recent DirectEve version ( .\\QuestorSource\\Questor-BleedingEdge\\DirectEve\\DirectEve.dll )");
			Cache.Instance.Log("If not replace ( .\\QuestorSource\\Questor-BleedingEdge\\DirectEve\\DirectEve.dll ) with the newest version and compile again.");
		}
		
		public static Questor Instance
		{
			get { return _instance; }
		}
		
		~Questor(){
			Interlocked.Decrement(ref QuestorInstances);
		}
	}
}
