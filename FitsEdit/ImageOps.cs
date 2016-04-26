using System;

namespace FitsEdit
{
	/// <summary>
	/// Summary description for ImageOps.
	/// </summary>
	public class ImageOps
	{
		public ImageOps()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static int ChangeContrast(int iVal, double iPercentage, int iMax)
		{
			int iTmp;

			iTmp = (int)(iPercentage * (double)iVal);
			if (iTmp >= iMax)
			{
				iTmp = iMax - 1;
			}
			return iTmp;
		}
	}
}
