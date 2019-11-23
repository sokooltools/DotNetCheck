using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace DotNetCheck
{
	internal static class BusinessLogic
	{
		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Performs the check.
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		internal static List<string> GetDotNetVersions()
		{
			// Get all version numbers of .NET installed on this computer.
			const string REGISTRY_KEY = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\";
			var versions = new List<string>();
			// Look for all subkey versions prior to 4.
			using (RegistryKey key1 = Registry.LocalMachine.OpenSubKey(REGISTRY_KEY))
			{
				if (key1 != null)
				{
					IOrderedEnumerable<string> skNames = key1.GetSubKeyNames().Where(m => m.StartsWith("v")
														  && Convert.ToInt32(m.Substring(1, 1)) < 4).OrderBy(m => m);
					foreach (string name in skNames)
					{
						using (RegistryKey key2 = Registry.LocalMachine.OpenSubKey(REGISTRY_KEY + name))
						{
							object verValue = key2?.GetValue("Version");
							if (verValue != null)
								versions.Add(verValue.ToString());
						}
					}
				}
			}
			// Look for a non-null version value in one of two places.
			string[] names = { "Client", "Full" };
			foreach (string name in names)
			{
				using (RegistryKey key = Registry.LocalMachine.OpenSubKey(REGISTRY_KEY + @"v4\" + name))
				{
					object verValue = key?.GetValue("Version");
					if (verValue == null) continue;
					versions.Add(verValue.ToString());
					return versions;
				}
			}
			return versions;
		}
	}
}