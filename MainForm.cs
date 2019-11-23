using System;
using System.Windows.Forms;

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
			listBox1.DataSource = BusinessLogic.GetDotNetVersions();
			textBox1.Text = listBox1.Items.Count > 0 ? listBox1.Items[listBox1.Items.Count - 1].ToString() : string.Empty;
		}
	}
}
