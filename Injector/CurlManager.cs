/*
 * ---------------------------------------
 * User: duketwo
 * Date: 03.02.2014
 * Time: 11:32
 * 
 * ---------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EasyHook;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using Library.Forms;
using System.Threading;
using SeasideResearch.LibCurlNet;

namespace Injector
{
	/// <summary>
	/// Description of CurlManager.
	/// </summary>
	public class CurlManager
	{
		private static CurlManager _instance = new CurlManager();
		private static object thisLock = new object();
		
		public CurlManager()
		{
		}
		
		public static CurlManager Instance{
			get { return _instance; }
		}
		
		private string CurrentPage { get; set; }
		private string UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0";
		private Int32 WriteData(Byte[] buf, Int32 size, Int32 nmemb, Object extraData)
		{
			lock(thisLock){
				foreach (byte b in buf)
					this.CurrentPage += ((char)b);
				return buf.Length;
			}
		}
		
		public string GetPostPage(string url, string postData, string proxyPort, string userPassword, bool deleteCookies = true)
		{
		    try
		    {
                lock (thisLock)
                {

                    if (deleteCookies)
                        DeleteCookies();

                    Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);
                    Easy easy = new Easy();
                    try
                    {
                        this.CurrentPage = string.Empty;
                        Easy.WriteFunction wf = this.WriteData;
                        easy.SetOpt(CURLoption.CURLOPT_URL, url);
                        easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);
                        easy.SetOpt(CURLoption.CURLOPT_SSL_VERIFYHOST, 0);
                        easy.SetOpt(CURLoption.CURLOPT_SSL_VERIFYPEER, 0);
                        if (!string.IsNullOrEmpty(proxyPort)) easy.SetOpt(CURLoption.CURLOPT_PROXY, proxyPort);
                        if (!string.IsNullOrEmpty(userPassword)) easy.SetOpt(CURLoption.CURLOPT_PROXYUSERPWD, userPassword);
                        if (!string.IsNullOrEmpty(proxyPort)) easy.SetOpt(CURLoption.CURLOPT_PROXYTYPE, CURLproxyType.CURLPROXY_SOCKS5);
                        easy.SetOpt(CURLoption.CURLOPT_USERAGENT, UserAgent);
                        easy.SetOpt(CURLoption.CURLOPT_COOKIEFILE, "injector.cookie");
                        easy.SetOpt(CURLoption.CURLOPT_COOKIEJAR, "injector.cookie");
                        easy.SetOpt(CURLoption.CURLOPT_FOLLOWLOCATION, 1);
                        easy.SetOpt(CURLoption.CURLOPT_AUTOREFERER, 1);
                        easy.SetOpt(CURLoption.CURLOPT_CONNECTTIMEOUT, 15);
                        if (!string.IsNullOrEmpty(postData)) easy.SetOpt(CURLoption.CURLOPT_POSTFIELDS, postData);
                        easy.Perform();
                        return this.CurrentPage;


                    }
                    catch (Exception exp)
                    {
                        if (exp is ThreadAbortException)
                        {
                            this.CurrentPage = string.Empty;
                        }
                        
                        Cache.Instance.Log("[GetPostPage] Exception " + exp.ToString());
                    }
                    finally
                    {
                        if (easy != null)
                            easy.Cleanup();
                        Curl.GlobalCleanup();
                    }

                    return this.CurrentPage = string.Empty;
                }
			
		    }
            catch (Exception exp)
            {
                if (exp is ThreadAbortException)
                {
                    this.CurrentPage = string.Empty;
                }

                Cache.Instance.Log("[GetPostPage] Exception " + exp.ToString());
            }
            finally
            {
                //Curl.GlobalCleanup();
            }

            return this.CurrentPage = string.Empty;
		}
		
		public void DeleteCookies(){
			lock(thisLock){
				string[] filePaths = Directory.GetFiles(AssemblyPath);
				foreach (string filePath in filePaths)
				{
					if (filePath.Substring(filePath.Length-6,6) == "cookie")
					{
						File.Delete(filePath);
					}
					
				}
			}
		}
		
		string _assemblyPath;
		string AssemblyPath{
			get {
				if (_assemblyPath == null) {
					
					_assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				}
				return _assemblyPath;
			}
		}
		
	}
}
