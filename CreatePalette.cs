using System;
using System.IO;

namespace FitsEdit
{
	/// <summary>
	/// Summary description for CreatePalette.
	/// </summary>
	public class CreatePalette
	{
		public CreatePalette()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void CreateGreyScale(string sFilename, int iMax)
		{
			int i;
			FileStream oStream = new FileStream(sFilename, FileMode.Create, FileAccess.Write, FileShare.Write);
			BinaryWriter oWriter = new BinaryWriter(oStream);

			for(i = 0; i < iMax; i++)
			{
				oWriter.Write((byte)(i / 256));
				oWriter.Write((byte)(i / 256));
				oWriter.Write((byte)(i / 256));
			}
			oWriter.Close();
			oStream.Close();
		}

		public static void CreateBlueScale(string sFilename, int iMax)
		{
			int i;
			FileStream oStream = new FileStream(sFilename, FileMode.Create, FileAccess.Write, FileShare.Write);
			BinaryWriter oWriter = new BinaryWriter(oStream);

			for(i = 0; i < iMax; i++)
			{
				oWriter.Write((byte)(0));
				oWriter.Write((byte)(0));
				oWriter.Write((byte)(i / 256));
			}
			oWriter.Close();
			oStream.Close();
		}

		public static void CreateGreenScale(string sFilename, int iMax)
		{
			int i;
			FileStream oStream = new FileStream(sFilename, FileMode.Create, FileAccess.Write, FileShare.Write);
			BinaryWriter oWriter = new BinaryWriter(oStream);

			for(i = 0; i < iMax; i++)
			{
				oWriter.Write((byte)(0));
				oWriter.Write((byte)(i / 256));
				oWriter.Write((byte)(0));
			}
			oWriter.Close();
			oStream.Close();
		}

		public static void CreateRedScale(string sFilename, int iMax)
		{
			int i;
			FileStream oStream = new FileStream(sFilename, FileMode.Create, FileAccess.Write, FileShare.Write);
			BinaryWriter oWriter = new BinaryWriter(oStream);

			for(i = 0; i < iMax; i++)
			{
				oWriter.Write((byte)(i / 256));
				oWriter.Write((byte)(0));
				oWriter.Write((byte)(0));
			}
			oWriter.Close();
			oStream.Close();
		}
	}
}
