using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FitsEdit
{
	/// <summary>
	/// Summary description for FitsFile.
	/// </summary>
	public class FitsFile
	{

		public string Simple;
		public int BitPix;
		public int Naxis;
		public int XSize;
		public int YSize;
		public string Object;
		public string Date;
		public string DateObs;
		public string Origin;
		public string Instrument;
		public string Telescope;
		public string Observer;
		public string Comment;
		public double BZero;
		public double BScale;
		public int [,] Pixels;
		public int iMax;
		public uint iNumPixels;
		public byte bT1, bT2;

		public FitsFile(string sFilename)
		{
			int i, j;
			string sTmp;
			string [] aTmp;
			FileStream oStream = new FileStream(sFilename, FileMode.Open, FileAccess.Read, FileShare.Read);
			BinaryReader oReader = new BinaryReader(oStream);

			Comment = "";
			for(;;)
			{
				sTmp = new string(oReader.ReadChars(80));
				if (sTmp.Substring(0,7).Trim().ToUpper() == "COMMENT")
				{
					Comment += sTmp;
				}
				else if(sTmp.Substring(0,3).Trim().ToUpper() == "END")
				{
					sTmp = new string(oReader.ReadChars(80));
					break;
				}
				else
				{
					aTmp = sTmp.Split('=');
					if (aTmp.Length < 2)
						continue;

					aTmp[0] = aTmp[0].Trim().ToUpper();
					aTmp[1] = aTmp[1].Trim();

					if (aTmp[0] == "SIMPLE")
					{
						Simple = aTmp[1];
					}
					else if (aTmp[0] == "BITPIX")
					{
                        aTmp[1] = Regex.Match(aTmp[1], "[^\\D]+").ToString();
                        BitPix = Convert.ToInt32(aTmp[1]);
					}
					else if (aTmp[0] == "NAXIS")
					{
                        aTmp[1] = Regex.Match(aTmp[1], "[^\\D]+").ToString();
                        Naxis = Convert.ToInt32(aTmp[1]);
					}
					else if (aTmp[0] == "NAXIS1")
					{
                        aTmp[1] = Regex.Match(aTmp[1], "[^\\D]+").ToString();
                        XSize = Convert.ToInt32(aTmp[1]);
					}
					else if (aTmp[0] == "NAXIS2")
					{
                        aTmp[1] = Regex.Match(aTmp[1], "[^\\D]+").ToString();
                        YSize = Convert.ToInt32(aTmp[1]);
					}
					else if (aTmp[0] == "OBJECT")
					{
						Object = aTmp[1];
					}
					else if (aTmp[0] == "DATE")
					{
						Date = aTmp[1];
					}				
					else if (aTmp[0] == "DATE-OBS")
					{
						DateObs = aTmp[1];
					}				
					else if (aTmp[0] == "ORIGIN")
					{
						Origin = aTmp[1];
					}				
					else if (aTmp[0] == "INSTRUME")
					{
						Instrument = aTmp[1];
					}				
					else if (aTmp[0] == "TELESCOP")
					{
						Telescope = aTmp[1];
					}				
					else if (aTmp[0] == "OBSERVER")
					{
						Observer = aTmp[1];
					}				
					else if (aTmp[0] == "BZERO")
					{
                        aTmp[1] = Regex.Match(aTmp[1], "[^\\D]+").ToString();
                        BZero = Convert.ToDouble(aTmp[1]);
					}				
					else if (aTmp[0] == "BSCALE")
					{
                        aTmp[1] = Regex.Match(aTmp[1], "[^\\D]+").ToString();
                        BScale = Convert.ToDouble(aTmp[1]);
					}				
				}

			}
			iNumPixels = (uint)(XSize * YSize);
			iMax = 65535;

			Pixels = new int[XSize, YSize];
			for(j = YSize - 1; j  >= 0; j--)
			{
				for(i = 0; i < XSize; i++)
				{

					bT2 = oReader.ReadByte();
					bT1 = oReader.ReadByte();
					Pixels[i, j] = ((int)bT2 * 255) + (int)bT1;

					if (Pixels[i, j] >= (int)BZero)
					{
						Pixels[i, j] -= (int)BZero;
					}
					else
					{
						Pixels[i, j] = (int)BZero - Pixels[i, j];
					}
				}
			}
			oReader.Close();
			oStream.Close();
		}

		public FitsFile()
		{
		}

		//-----------------------------------------------------------
		//Add
		//Adds specified number to all pixels in specified area
		//-----------------------------------------------------------
		public void Add(int iNum, int iStartX, int iStartY, int iEndX, int iEndY)
		{
			int i, j;

			for (i = iStartX; i <= iEndX; i++)
			{
				for (j = iStartY; j <= iEndY; j++)
				{
					if ((Pixels[i,j] + iNum) < Pixels[i,j])
					{
						Pixels[i,j] = iMax;
					}
					else
					{
						Pixels[i,j] += iNum;
					}
				}
			}
		}

		//-----------------------------------------------------------
		//Add
		//Adds specified number to all pixels in image
		//-----------------------------------------------------------
		public void Add(int iNum)
		{
			Add(iNum, 0, 0, XSize - 1, YSize - 1);
		}

		//-----------------------------------------------------------
		//AddCircle
		//Adds specified number to all pixels in specified circle
		//-----------------------------------------------------------
		public void AddCircle(int iNum, int iCentreX, int iCentreY, int iRadius)
		{
			int i, j;

			for (i = (iCentreX - iRadius); i <= (iCentreX + iRadius); i++)
			{
				for (j = (iCentreY - iRadius); j <= (iCentreY + iRadius); j++)
				{
					if (((i - iCentreX) * (i - iCentreX)) + ((j - iCentreY) * (j - iCentreY)) <= (iRadius * iRadius))
					{
						if ((Pixels[i,j] + iNum) < Pixels[i,j])
						{
							Pixels[i,j] = iMax;
						}
						else
						{
							Pixels[i,j] += iNum;
						}
					}
				}
			}
		}

		//-----------------------------------------------------------
		//Subtract
		//Subtracts specified number from all pixels in specified area
		//-----------------------------------------------------------
		public void Subtract(int iNum, int iStartX, int iStartY, int iEndX, int iEndY)
		{
			int i, j;

			for (i = iStartX; i <= iEndX; i++)
			{
				for (j = iStartY; j <= iEndY; j++)
				{
					if ((Pixels[i,j] - iNum) > Pixels[i,j])
					{
						Pixels[i,j] = 0;
					}
					else
					{
						Pixels[i,j] -= iNum;
					}
				}
			}
		}


		//-----------------------------------------------------------
		//Subtract
		//Subtracts specified number from all pixels in image
		//-----------------------------------------------------------
		public void Subtract(int iNum)
		{
			Subtract(iNum, 0, 0, XSize - 1, YSize - 1);
		}

		//-----------------------------------------------------------
		//SubtractCircle
		//Subtracts specified number to all pixels in specified circle
		//-----------------------------------------------------------
		public void SubtractCircle(int iNum, int iCentreX, int iCentreY, int iRadius)
		{
			int i, j;

			for (i = (iCentreX - iRadius); i <= (iCentreX + iRadius); i++)
			{
				for (j = (iCentreY - iRadius); j <= (iCentreY + iRadius); j++)
				{
					if (((i - iCentreX) * (i - iCentreX)) + ((j - iCentreY) * (j - iCentreY)) <= (iRadius * iRadius))
					{
						if ((Pixels[i,j] - iNum) > Pixels[i,j])
						{
							Pixels[i,j] = 0;
						}
						else
						{
							Pixels[i,j] -= iNum;
						}
					}
				}
			}
		}

		//-----------------------------------------------------------
		//Divides
		//Divides all pixels in specified area by specified number
		//-----------------------------------------------------------
		public void Divide(int iNum, int iStartX, int iStartY, int iEndX, int iEndY)
		{
			int i, j;

			if (iNum != 0)
			{
				for (i = iStartX; i <= iEndX; i++)
				{
					for (j = iStartX; j <= iEndY; j++)
					{
						Pixels[i,j] /= iNum;
					}
				}
			}
		}

		//-----------------------------------------------------------
		//Divides
		//Divides all pixels in image by specified number
		//-----------------------------------------------------------
		public void Divide(int iNum)
		{
			Divide(iNum, 0, 0, XSize - 1, YSize - 1);
		}

		//-----------------------------------------------------------
		//DivideCircle
		//Divide all pixels by specified number in specified circle
		//-----------------------------------------------------------
		public void DivideCircle(int iNum, int iCentreX, int iCentreY, int iRadius)
		{
			int i, j;

			for (i = (iCentreX - iRadius); i <= (iCentreX + iRadius); i++)
			{
				for (j = (iCentreY - iRadius); j <= (iCentreY + iRadius); j++)
				{
					if (((i - iCentreX) * (i - iCentreX)) + ((j - iCentreY) * (j - iCentreY)) <= (iRadius * iRadius))
					{
						Pixels[i,j] /= iNum;
					}
				}
			}
		}

		//-----------------------------------------------------------
		//Multiply
		//Multiply all pixels in specified area by specified number
		//-----------------------------------------------------------
		public void Multiply(int iNum, int iStartX, int iStartY, int iEndX, int iEndY)
		{
			int i, j;


			for (i = iStartX; i <= iEndX; i++)
			{
				for (j = iStartY; j <= iEndY; j++)
				{
					if ((Pixels[i,j] * iNum) < Pixels[i,j])
					{
						Pixels[i,j] = iMax;
					}
					else
					{
						Pixels[i,j] *= iNum;
					}				
				}
			}
		}

		//-----------------------------------------------------------
		//Multiply
		//Multiply all pixels in image by specified number
		//-----------------------------------------------------------
		public void Multiply(int iNum)
		{
			Multiply(iNum, 0, 0, XSize - 1, YSize - 1);
		}

		//-----------------------------------------------------------
		//MultiplyCircle
		//Multiply all pixels by specified number in specified circle
		//-----------------------------------------------------------
		public void MultiplyCircle(int iNum, int iCentreX, int iCentreY, int iRadius)
		{
			int i, j;

			for (i = (iCentreX - iRadius); i <= (iCentreX + iRadius); i++)
			{
				for (j = (iCentreY - iRadius); j <= (iCentreY + iRadius); j++)
				{
					if (((i - iCentreX) * (i - iCentreX)) + ((j - iCentreY) * (j - iCentreY)) <= (iRadius * iRadius))
					{
						if ((Pixels[i,j] * iNum) < Pixels[i,j])
						{
							Pixels[i,j] = iMax;
						}
						else
						{
							Pixels[i,j] *= iNum;
						}						
					}
				}
			}
		}

		//-----------------------------------------------------------
		//GetSum
		//Getsthe total of all pixels in specified area
		//-----------------------------------------------------------
		public double GetSum(int iStartX, int iStartY, int iEndX, int iEndY)
		{
			double fSum = 0.0;
			int i, j;

			for (i = iStartX; i <= iEndX; i++)
			{
				for (j = iStartY; j <= iEndY; j++)
				{
					fSum += (double)Pixels[i,j];			
				}
			}
            return fSum;
		}

		//-----------------------------------------------------------
		//GetSumCircle
		//Gets the total of all pixels in specified circle
		//-----------------------------------------------------------
		public double GetSumCircle(int iCentreX, int iCentreY, int iRadius)
		{
			double fSum = 0.0;
			int i, j;

			for (i = (iCentreX - iRadius); i <= (iCentreX + iRadius); i++)
			{
				for (j = (iCentreY - iRadius); j <= (iCentreY + iRadius); j++)
				{
					if (((i - iCentreX) * (i - iCentreX)) + ((j - iCentreY) * (j - iCentreY)) <= (iRadius * iRadius))
					{
						fSum += (double)Pixels[i,j];	
					}
				}
			}
			return fSum;
		}

		//-----------------------------------------------------------
		//GetSum
		//Gets the total of all pixels in entire image
		//-----------------------------------------------------------
		public double GetSum()
		{
			return GetSum(0, 0, XSize - 1, YSize - 1);
		}

		//-----------------------------------------------------------
		//GetMean
		//Gets the mean of all pixels in specified area
		//-----------------------------------------------------------
		public double GetMean(int iStartX, int iStartY, int iEndX, int iEndY)
		{
			return GetSum(iStartX, iStartY, iEndX, iEndY) / (double)iNumPixels;
		}

		//-----------------------------------------------------------
		//GetMean
		//Gets the mean of all pixels in entire image
		//-----------------------------------------------------------
		public double GetMean()
		{
			return GetSum() / (double)iNumPixels;
		}

		//-----------------------------------------------------------
		//GetMeanCircle
		//Gets the mean of all pixels in specified circle
		//-----------------------------------------------------------
		public double GetMeanCircle(int iCentreX, int iCentreY, int iRadius)
		{
			double fSum = 0.0;
			int i, j,iCount;

			iCount = 0;
			for (i = (iCentreX - iRadius); i <= (iCentreX + iRadius); i++)
			{
				for (j = (iCentreY - iRadius); j <= (iCentreY + iRadius); j++)
				{
					if (((i - iCentreX) * (i - iCentreX)) + ((j - iCentreY) * (j - iCentreY)) <= (iRadius * iRadius))
					{
						fSum += (double)Pixels[i,j];	
						iCount++;
					}
				}
			}
			return fSum / (double)iCount;
		}

		//-----------------------------------------------------------
		//GetPeakLoc
		//Gets the peak value and location in specified area
		//-----------------------------------------------------------
		public void GetPeakLoc(ref int iPeak, ref int iX, ref int iY, int iStartX, int iStartY, int iEndX, int iEndY)
		{
			int i, j;

			iPeak = 0;
			for (i = iStartX; i <= iEndX; i++)
			{
				for (j = iStartY; j <= iEndY; j++)
				{
					if (Pixels[i,j] >= iPeak)
					{
						iPeak = Pixels[i,j];
						iX = i;
						iY = j;
					}
				}
			}
		}

		//-----------------------------------------------------------
		//GetPeakLoc
		//Gets the peak value and location in entire image
		//-----------------------------------------------------------
		public void GetPeakLoc(ref int iPeak, ref int iX, ref int iY)
		{
			GetPeakLoc(ref iPeak, ref iX, ref iY, 0, 0, XSize - 1, YSize - 1);
		}

		//-----------------------------------------------------------
		//GetPeak
		//Gets the peak value in specified area
		//-----------------------------------------------------------
		public int GetPeak(int iStartX, int iStartY, int iEndX, int iEndY)
		{
			int i, j;
			int iPeak;

			iPeak = 0;
			for (i = iStartX; i <= iEndX; i++)
			{
				for (j = iStartY; j <= iEndY; j++)
				{
					if (Pixels[i,j] >= iPeak)
					{
						iPeak = Pixels[i,j];
					}
				}
			}
			return iPeak;
		}

		//-----------------------------------------------------------
		//GetPeak
		//Gets the peak value in entire image
		//-----------------------------------------------------------
		public int GetPeak()
		{
			return GetPeak(0, 0, XSize - 1, YSize - 1);
		}

		//-----------------------------------------------------------
		//GetPeakCircle
		//Gets the peak value in specified circle
		//-----------------------------------------------------------
		public int GetPeakCircle(int iCentreX, int iCentreY, int iRadius)
		{
			int iPeak;
			int i, j;

			iPeak = 0;
			for (i = (iCentreX - iRadius); i <= (iCentreX + iRadius); i++)
			{
				for (j = (iCentreY - iRadius); j <= (iCentreY + iRadius); j++)
				{
					if (((i - iCentreX) * (i - iCentreX)) + ((j - iCentreY) * (j - iCentreY)) <= (iRadius * iRadius))
					{
						if (Pixels[i,j] >= iPeak)
						{
							iPeak = Pixels[i,j];
						}
					}
				}
			}
			return iPeak;
		}

		//-----------------------------------------------------------
		//GetMinLoc
		//Gets the min value and location in specified area
		//-----------------------------------------------------------
		public void GetMinLoc(ref int iMin, ref int iX, ref int iY, int iStartX, int iStartY, int iEndX, int iEndY)
		{
			int i, j;

			iMin = iMax;
			for (i = iStartX; i <= iEndX; i++)
			{
				for (j = iStartY; j <= iEndY; j++)
				{
					if (Pixels[i,j] <= iMin)
					{
						iMin = Pixels[i,j];
						iX = i;
						iY = j;
					}
				}
			}
		}

		//-----------------------------------------------------------
		//GetMinLoc
		//Gets the min value and location in entire image
		//-----------------------------------------------------------
		public void GetMinLoc(ref int iMin, ref int iX, ref int iY)
		{
			GetMinLoc(ref iMin, ref iX, ref iY, 0, 0, XSize - 1, YSize - 1);
		}

		//-----------------------------------------------------------
		//GetMin
		//Gets the min value in specified area
		//-----------------------------------------------------------
		public int GetMin(int iStartX, int iStartY, int iEndX, int iEndY)
		{
			int i, j;
			int iMin;

			iMin = iMax;
			for (i = iStartX; i <= iEndY; i++)
			{
				for (j = iStartY; j <= iEndY; j++)
				{
					if (Pixels[i,j] <= iMin)
					{
						iMin = Pixels[i,j];
					}
				}
			}
			return iMin;
		}

		//-----------------------------------------------------------
		//GetMin
		//Gets the min value in entire image
		//-----------------------------------------------------------
		public int GetMin()
		{
			return GetMin(0, 0, XSize - 1, YSize - 1);
		}

		//-----------------------------------------------------------
		//GetMinCircle
		//Gets the min value in specified circle
		//-----------------------------------------------------------
		public int GetMinCircle(int iCentreX, int iCentreY, int iRadius)
		{
			int iMin;
			int i, j;

			iMin = iMax;
			for (i = (iCentreX - iRadius); i <= (iCentreX + iRadius); i++)
			{
				for (j = (iCentreY - iRadius); j <= (iCentreY + iRadius); j++)
				{
					if (((i - iCentreX) * (i - iCentreX)) + ((j - iCentreY) * (j - iCentreY)) <= (iRadius * iRadius))
					{
						if (Pixels[i,j] <= iMin)
						{
							iMin = Pixels[i,j];
						}
					}
				}
			}
			return iMin;
		}
	}
}
