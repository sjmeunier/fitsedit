using System;
using System.IO;

namespace FitsEdit
{
	/// <summary>
	/// Summary description for Palette.
	/// </summary>
	public class Palette
	{
		public byte [] R;
		public byte [] G;
		public byte [] B;
		public int Max;

		public Palette(string sFilename, int iMax)
		{
			int i;
			FileStream oStream = new FileStream(sFilename, FileMode.Open, FileAccess.Read, FileShare.Read);
			BinaryReader oReader = new BinaryReader(oStream);
			Max = iMax;

			R = new byte[Max];
			G = new byte[Max];
			B = new byte[Max];

			for (i = 0; i < Max; i++)
			{
				R[i] = oReader.ReadByte();
				G[i] = oReader.ReadByte();
				B[i] = oReader.ReadByte();
			}
			oReader.Close();
			oStream.Close();
		}

		public Palette(int iLow, int iHigh, int iMax)
		{
			int i, iDiff;
			byte bVal;

			Max = iMax;
			iDiff = iHigh - iLow;

			R = new byte[Max];
			G = new byte[Max];
			B = new byte[Max];
			for (i = 0; i < iLow; i++)
			{
				R[i] = 0;
				G[i] = 0;
				B[i] = 0;
			}

			for (i = iLow; i < iHigh; i++)
			{
				bVal = (byte)(((double)i / (double)iDiff) * 255.0);
				R[i] = bVal;
				G[i] = bVal;
				B[i] = bVal;
			}
			for (i = iHigh; i < Max; i++)
			{
				R[i] = 255;
				G[i] = 255;
				B[i] = 255;
			}
		}

	}
}
