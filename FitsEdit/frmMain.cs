using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace FitsEdit
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mnuExit;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem mnuLoadFile;
		private System.Windows.Forms.MenuItem mnuSaveAs;
		private System.Windows.Forms.MenuItem mnuSave;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem mnuCreateGS;
		private System.Windows.Forms.MenuItem mnuCreateBS;
		private System.Windows.Forms.MenuItem mnuCreateRS;
		private System.Windows.Forms.MenuItem mnuCreateGreenS;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuLoadFile = new System.Windows.Forms.MenuItem();
			this.mnuSaveAs = new System.Windows.Forms.MenuItem();
			this.mnuSave = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mnuExit = new System.Windows.Forms.MenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.mnuCreateGS = new System.Windows.Forms.MenuItem();
			this.mnuCreateBS = new System.Windows.Forms.MenuItem();
			this.mnuCreateRS = new System.Windows.Forms.MenuItem();
			this.mnuCreateGreenS = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuLoadFile,
																					  this.mnuSaveAs,
																					  this.mnuSave,
																					  this.menuItem2,
																					  this.mnuExit});
			this.menuItem1.Text = "&File";
			// 
			// mnuLoadFile
			// 
			this.mnuLoadFile.Index = 0;
			this.mnuLoadFile.Text = "&Load...";
			this.mnuLoadFile.Click += new System.EventHandler(this.mnuLoadFile_Click);
			// 
			// mnuSaveAs
			// 
			this.mnuSaveAs.Index = 1;
			this.mnuSaveAs.Text = "&Save As...";
			// 
			// mnuSave
			// 
			this.mnuSave.Index = 2;
			this.mnuSave.Text = "&Save";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 3;
			this.menuItem2.Text = "-";
			// 
			// mnuExit
			// 
			this.mnuExit.Index = 4;
			this.mnuExit.Text = "E&xit";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuCreateGS,
																					  this.mnuCreateRS,
																					  this.mnuCreateGreenS,
																					  this.mnuCreateBS});
			this.menuItem3.Text = "&Setup";
			// 
			// mnuCreateGS
			// 
			this.mnuCreateGS.Index = 0;
			this.mnuCreateGS.Text = "&Create Greyscale";
			this.mnuCreateGS.Click += new System.EventHandler(this.mnuCreateGS_Click);
			// 
			// mnuCreateBS
			// 
			this.mnuCreateBS.Index = 3;
			this.mnuCreateBS.Text = "Create &Bluescale";
			this.mnuCreateBS.Click += new System.EventHandler(this.mnuCreateBS_Click);
			// 
			// mnuCreateRS
			// 
			this.mnuCreateRS.Index = 1;
			this.mnuCreateRS.Text = "Create &Redscale";
			this.mnuCreateRS.Click += new System.EventHandler(this.mnuCreateRS_Click);
			// 
			// mnuCreateGreenS
			// 
			this.mnuCreateGreenS.Index = 2;
			this.mnuCreateGreenS.Text = "Create &Greenscale";
			this.mnuCreateGreenS.Click += new System.EventHandler(this.mnuCreateGreenS_Click);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(608, 454);
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "frmMain";
			this.Text = "Fits Edit";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		private void mnuExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
		
		}

		private void mnuLoadFile_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.ShowDialog(this);
			frmImage oImage = new frmImage();
			oImage.MdiParent = this;
			oImage.sFilename = openFileDialog1.FileName;
			oImage.Show();
		}

		private void mnuCreateGS_Click(object sender, System.EventArgs e)
		{
			CreatePalette.CreateGreyScale("greyscale.pal", 65535);
		}

		private void mnuCreateRS_Click(object sender, System.EventArgs e)
		{
			CreatePalette.CreateRedScale("red.pal", 65535);
		}

		private void mnuCreateGreenS_Click(object sender, System.EventArgs e)
		{
			CreatePalette.CreateGreenScale("green.pal", 65535);
		}

		private void mnuCreateBS_Click(object sender, System.EventArgs e)
		{
			CreatePalette.CreateBlueScale("blue.pal", 65535);
		}
	
	}
}
