using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DotNetCheck
{
	public sealed partial class MainForm : Form
	{
		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="MainForm"/> class.
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		public MainForm()
		{
			InitializeComponent();
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Handles the Load event of the Form1 control.
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		private void Form1_Load(object sender, EventArgs e)
		{
			MinimumSize = Size;
            PerformCheck();
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Performs the check.
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		private void PerformCheck()
		{
			const string REGISTRY_KEY = @"SOFTWARE\Microsoft\.NETFramework\";
			// Get all version numbers of .NET installed on this computer.
			using (RegistryKey key = Registry.LocalMachine.OpenSubKey(REGISTRY_KEY))
			{
				if (key != null)
					foreach (string m in key.GetSubKeyNames().Where(m => m.StartsWith("v")).OrderBy(m => m))
						listBox1.Items.Add(m);
			}
			// Get the latest version number of .NET installed on this computer.
			using (RegistryKey key = Registry.LocalMachine.OpenSubKey(REGISTRY_KEY))
			{
				if (key != null)
					textBox1.Text = key.GetSubKeyNames().Where(m => m.StartsWith("v")).OrderByDescending(m => m).FirstOrDefault();
			}
		}
    }
}
