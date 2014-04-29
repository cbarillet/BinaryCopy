using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BinaryCopy2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void bOpenDestinationFileDialog_Click(object sender, RoutedEventArgs e)
		{
			// Create an instance of the open file dialog box.
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

			folderBrowserDialog.Description = "Select the directory where you want to copy the selected file.";

			System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
			// Get the selected file name and display in a TextBox
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				// Open document
				string path = folderBrowserDialog.SelectedPath;
				tbDestinationPath.Text = path;
			}
		}

		private void bOpenSourceFileDialog_Click(object sender, RoutedEventArgs e)
		{
			// Create an instance of the open file dialog box.
			Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();

			// Set filter options and filter index.
			openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
			openFileDialog1.FilterIndex = 1;

			// Display OpenFileDialog by calling ShowDialog method
			Nullable<bool> result = openFileDialog1.ShowDialog();

			// Get the selected file name and display in a TextBox
			if (result == true)
			{
				// Open document
				string filename = openFileDialog1.FileName;
				tbSourcePath.Text = filename;
			}
		}

		private void Copy_Click(object sender, RoutedEventArgs e)
		{

			string filename = System.IO.Path.GetFileName(tbSourcePath.Text);
			using(Stream source = File.OpenRead(tbSourcePath.Text))
			{
				using(Stream dest = File.Create(tbDestinationPath.Text + "\\" + filename))
				{
					byte[] buffer = new byte[2048];
					int bytesRead;
					while((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
					{
						dest.Write(buffer, 0, bytesRead);
					}
				}
			}
		}
	}
}
