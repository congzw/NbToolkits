using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace NbIsDebug
{
	public partial class MainForm : Form
	{
		private void lv_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.All;
			}
		}

		private void lv_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				LoadFiles((string[])e.Data.GetData(DataFormats.FileDrop));
			}
		}

		private void LoadFiles(params string[] filenames)
		{
			foreach (string filename in filenames)
			{
				if (Directory.Exists(filename))
				{
					LoadFiles(Directory.GetFiles(filename));
				}
				else
				{
					LoadFile(filename);
				}
			}
		}

		private void LoadFile(string filename)
		{
			string buildType, version = "";
			StringBuilder sb = new StringBuilder(256);
			string name = Path.GetFileName(filename);

			try
			{
				Cursor = Cursors.WaitCursor;
				Assembly assm = Assembly.LoadFrom(filename);
				var assemblyDebugBuild = isAssemblyDebugBuild(assm);
				buildType = assemblyDebugBuild ? "Debug" : "Release";
				version = assm.GetName().Version.ToString();
			}
			catch(Exception ex)
			{
				buildType = "<error>: " + ex.Message;
			}
			finally
			{
				Cursor = Cursors.Default;
			}

			lv.Items.Add(new ListViewItem(new[] { name, version, buildType }));				
		}
		
		private void MenuHandler(object sender, EventArgs e)
		{
			switch (((MenuItem)sender).Text)
			{
				case "&Clear":
					lv.Items.Clear();
					break;

				case "E&xit":
					Close();
					break;
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			//var test = @"D:\WS_Local\NbTookits\src\ExePackage\bin\Release\ExePackage.exe";
			//MessageBox.Show(GetVersionInfo(test));
		}

		private bool isAssemblyDebugBuild(Assembly assemb)
		{
			//bool retVal = false;
			//foreach (object att in assemb.GetCustomAttributes(false))
			//{
			//    if (att.GetType() == Type.GetType("System.Diagnostics.DebuggableAttribute"))
			//    {
			//        retVal = ((DebuggableAttribute)att).IsJITTrackingEnabled;
			//    }
			//}
			//return retVal;

			object[] attribs = assemb.GetCustomAttributes(typeof(DebuggableAttribute), false);
			if (attribs.Length == 0)
			{
				return false;
			}

			var debuggableAttribute = (DebuggableAttribute)attribs[0];
			return debuggableAttribute.IsJITTrackingEnabled;
		}


	}
}
