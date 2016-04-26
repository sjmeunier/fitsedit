using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FitsEdit
{
	/// <summary>
	/// Summary description for frmImage.
	/// </summary>
	public class frmImage : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel pnlImage;
		private System.Windows.Forms.StatusBar stsImage;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmImage()
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
				if(components != null)
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
			this.pnlImage = new System.Windows.Forms.Panel();
			this.stsImage = new System.Windows.Forms.StatusBar();
			this.pnlImage.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlImage
			// 
			this.pnlImage.BackColor = System.Drawing.Color.Black;
			this.pnlImage.Controls.Add(this.stsImage);
			this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlImage.Location = new System.Drawing.Point(0, 0);
			this.pnlImage.Name = "pnlImage";
			this.pnlImage.Size = new System.Drawing.Size(448, 398);
			this.pnlImage.TabIndex = 0;
			this.pnlImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlImage_MouseUp);
			this.pnlImage.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlImage_Paint);
			// 
			// stsImage
			// 
			this.stsImage.Location = new System.Drawing.Point(0, 374);
			this.stsImage.Name = "stsImage";
			this.stsImage.Size = new System.Drawing.Size(448, 24);
			this.stsImage.TabIndex = 0;
			// 
			// frmImage
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 398);
			this.Controls.Add(this.pnlImage);
			this.Name = "frmImage";
			this.Text = "Image";
			this.Load += new System.EventHandler(this.frmImage_Load);
			this.pnlImage.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public string sFilename;
		public FitsFile oFile;
		private Palette oPal;
		private bool bLoaded = true;
		private bool bDraw = true;


		private void frmImage_Load(object sender, System.EventArgs e)
		{
			string sName;
			int iPos;

			iPos = sFilename.LastIndexOf("\\", 0, 1);
			if (iPos < 0)
			{
				sName = sFilename;
			}
			else
			{
				sName = sFilename.Substring(iPos);
			}
			this.Text = sName;
			oFile = new FitsFile(sFilename);
			bLoaded = true;
			//oPal = new Palette("c:\\greyscale.pal", oFile.iMax);
			oPal = new Palette(0, 65535, oFile.iMax);
			bDraw = true;
			this.Width = oFile.XSize + 10;
			this.Height = oFile.YSize + this.stsImage.Height + 30;

			pnlImage.Invalidate();

		}

		private void pnlImage_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			int i, j, iPixel;
			Color oColor;

			if (bDraw && bLoaded)
			{
				for (i = 0; i < oFile.XSize; i++)
				{
					for (j = 0; j < oFile.YSize; j++)
					{
						oColor = new Color();
						iPixel = ImageOps.ChangeContrast(oFile.Pixels[i,j], 500.0, 65535);

						
						oColor = Color.FromArgb(oPal.R[iPixel],oPal.G[iPixel],oPal.B[iPixel]);
						e.Graphics.FillRectangle(new SolidBrush(oColor),i,j,1,1);
					}
				}
				bDraw = false;
			}
		}

		private void pnlImage_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//MessageBox.Show(this, "X: " + e.X.ToString() + "  Y: " + e.Y.ToString() + "  Val: " + oFile.Pixels[e.X, e.Y].ToString());

		}



	}
}
