using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingLab3V2
{
	public class Versions
	{
		public int Unit_1_Part_1 { get; set; }
		public int Unit_1_Part_2 { get; set; }
		public int Unit_1_Part_3 { get; set; }
		public int? Unit_2_Part_1 { get; set; }
		public int? Unit_2_Part_2 { get; set; }
		public int? Unit_2_Part_3 { get; set; }

		public Versions(string Versions)
		{
			if (IsCorrect(Versions))
            {
				string[] splitedUnits = Versions.Split('-');
				string[] splitedPartsOfUnit1 = splitedUnits[0].Split('.');
				Unit_1_Part_1 = Convert.ToInt32(splitedPartsOfUnit1[0]);
				Unit_1_Part_2 = Convert.ToInt32(splitedPartsOfUnit1[1]);
				Unit_1_Part_3 = Convert.ToInt32(splitedPartsOfUnit1[2]);

				if (splitedUnits.Length > 1)
				{
					string[] splitedPartsOfUnit2 = splitedUnits[1].Split('.');
					switch (splitedPartsOfUnit2.Length)
					{
						case 1:
							Unit_2_Part_1 = ConvertPartToDigit(splitedPartsOfUnit2[0]);
							Unit_2_Part_2 = null;
							Unit_2_Part_3 = null;
							break;
						case 2:
							Unit_2_Part_1 = ConvertPartToDigit(splitedPartsOfUnit2[0]);
							Unit_2_Part_2 = ConvertPartToDigit(splitedPartsOfUnit2[1]);
							Unit_2_Part_3 = null;
							break;
						case 3:
							Unit_2_Part_1 = ConvertPartToDigit(splitedPartsOfUnit2[0]);
							Unit_2_Part_2 = ConvertPartToDigit(splitedPartsOfUnit2[1]);
							Unit_2_Part_3 = ConvertPartToDigit(splitedPartsOfUnit2[2]);
							break;
					}
				}
            }
			else
            {
				throw new ArgumentException("Недопустимый формат версии");
			}
		}

		private static int ConvertPartToDigit(string part)
		{
			if (part == "alpha")
			{
				return -2;
			}
			if (part == "beta")
			{
				return -1;
			}
			return Convert.ToInt32(part);
		}

		private static string ConvertDigitToPart(int? digit)
		{
			if (digit == -2)
			{
				return "alpha";
			}
			if (digit == -1)
			{
				return "beta";
			}
			return digit.ToString();
		}


		private static bool IsCorrect(string Versions)
		{
			string[] splitedByDashVersion = Versions.Split('-');
			if (splitedByDashVersion.Length == 1)
			{
				int n = 0;
				string[] splitedByDotVersion = Versions.Split('.');
				if (splitedByDotVersion.Length == 3)
				{
					if (Int32.TryParse(splitedByDotVersion[0], out n) == true)
					{
						if (Int32.TryParse(splitedByDotVersion[1], out n) == true)
						{
							if (Int32.TryParse(splitedByDotVersion[2], out n) == true)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			if (splitedByDashVersion.Length == 2)
			{
				bool flagOfPt1 = false;
				bool flagOfPt2 = false;
				int n = 0;
				string[] splitedByDotVersionPt1 = splitedByDashVersion[0].Split('.');
				if (splitedByDotVersionPt1.Length == 3)
				{
					if (Int32.TryParse(splitedByDotVersionPt1[0], out n) == true)
					{
						if (Int32.TryParse(splitedByDotVersionPt1[1], out n) == true)
						{
							if (Int32.TryParse(splitedByDotVersionPt1[2], out n) == true)
							{
								flagOfPt1 = true;
							}
						}
					}
				}
				string[] splitedByDotVersionPt2 = splitedByDashVersion[1].Split('.');
				if (splitedByDotVersionPt2.Length <= 3)
				{
					int tryParseResult = 0;
					switch (splitedByDotVersionPt2.Length)
					{
						case 1:
							if (splitedByDotVersionPt2[0].Length == 1 || splitedByDotVersionPt2[0] == "alpha" || splitedByDotVersionPt2[0] == "beta" || Int32.TryParse(splitedByDotVersionPt2[0], out tryParseResult))
							{
								flagOfPt2 = true;
							}
							break;
						case 2:
							if (splitedByDotVersionPt2[0].Length == 1 || splitedByDotVersionPt2[0] == "alpha" || splitedByDotVersionPt2[0] == "beta" || Int32.TryParse(splitedByDotVersionPt2[0], out tryParseResult))
							{
								if (splitedByDotVersionPt2[1].Length == 1 || splitedByDotVersionPt2[1] == "alpha" || splitedByDotVersionPt2[1] == "beta" || Int32.TryParse(splitedByDotVersionPt2[1], out tryParseResult))
								{
									flagOfPt2 = true;
								}
							}
							break;
						case 3:
							if (splitedByDotVersionPt2[0].Length == 1 || splitedByDotVersionPt2[0] == "alpha" || splitedByDotVersionPt2[0] == "beta" || Int32.TryParse(splitedByDotVersionPt2[0], out tryParseResult))
							{
								if (splitedByDotVersionPt2[1].Length == 1 || splitedByDotVersionPt2[1] == "alpha" || splitedByDotVersionPt2[1] == "beta" || Int32.TryParse(splitedByDotVersionPt2[1], out tryParseResult))
								{
									if (splitedByDotVersionPt2[2].Length == 1 || splitedByDotVersionPt2[2] == "alpha" || splitedByDotVersionPt2[2] == "beta" || Int32.TryParse(splitedByDotVersionPt2[2], out tryParseResult))
									{
										flagOfPt2 = true;
									}
								}
							}
							break;
					}
				}
				if (flagOfPt1 == true && flagOfPt2 == true)
				{
					return true;
				}
			}
			return false;
		}

		public static bool operator >(Versions version1, Versions version2)
		{
			return IsMore(version1, version2);
		}
		public static bool operator <(Versions version1, Versions version2)
		{
			return !IsMore(version1, version2);
		}

		private static bool IsMore(Versions v1, Versions v2)
		{
			if (v1.Unit_1_Part_1 > v2.Unit_1_Part_1)
			{
				return true;
			}
			else if (v1.Unit_1_Part_1 == v2.Unit_1_Part_1)
			{
				if (v1.Unit_1_Part_2 > v2.Unit_1_Part_2)
				{
					return true;
				}
				else if (v1.Unit_1_Part_2 == v2.Unit_1_Part_2)
				{
					if (v1.Unit_1_Part_3 > v2.Unit_1_Part_3)
					{
						return true;
					}
					else if (v1.Unit_1_Part_3 == v2.Unit_1_Part_3)
					{
						if (v1.Unit_2_Part_1 > v2.Unit_2_Part_1)
						{
							return true;
						}
						if (v1.Unit_2_Part_1 == null && v2.Unit_2_Part_1 != null)
						{
							return true;
						}
						if (v1.Unit_2_Part_1 == v2.Unit_2_Part_1)
						{
							if (v1.Unit_2_Part_2 > v2.Unit_2_Part_2)
							{
								return true;
							}
							if (v1.Unit_2_Part_2 == null && v2.Unit_2_Part_2 != null)
							{
								return true;
							}
							if (v1.Unit_2_Part_2 == v2.Unit_2_Part_2)
							{
								if (v1.Unit_2_Part_3 > v2.Unit_2_Part_3)
								{
									return true;
								}
								if (v1.Unit_2_Part_3 == null && v2.Unit_2_Part_3 != null)
								{
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		public static bool operator >=(Versions version1, Versions version2)
		{
			return IsMoreOrEqual(version1, version2);
		}

		private static bool IsMoreOrEqual(Versions v1, Versions v2)
		{
			if (v1.Unit_1_Part_1 > v2.Unit_1_Part_1)
			{
				return true;
			}
			else if (v1.Unit_1_Part_1 == v2.Unit_1_Part_1)
			{
				if (v1.Unit_1_Part_2 > v2.Unit_1_Part_2)
				{
					return true;
				}
				else if (v1.Unit_1_Part_2 == v2.Unit_1_Part_2)
				{
					if (v1.Unit_1_Part_3 > v2.Unit_1_Part_3)
					{
						return true;
					}
					else if (v1.Unit_1_Part_3 == v2.Unit_1_Part_3)
					{
						if (v1.Unit_2_Part_1 > v2.Unit_2_Part_1)
						{
							return true;
						}
						if (v1.Unit_2_Part_1 == null && v2.Unit_2_Part_1 != null)
						{
							return true;
						}
						if (v1.Unit_2_Part_1 == v2.Unit_2_Part_1)
						{
							if (v1.Unit_2_Part_2 > v2.Unit_2_Part_2)
							{
								return true;
							}
							if (v1.Unit_2_Part_2 == null && v2.Unit_2_Part_2 != null)
							{
								return true;
							}
							if (v1.Unit_2_Part_2 == v2.Unit_2_Part_2)
							{
								if (v1.Unit_2_Part_3 > v2.Unit_2_Part_3)
								{
									return true;
								}
								if (v1.Unit_2_Part_3 == null && v2.Unit_2_Part_3 != null)
								{
									return true;
								}
								if (v1.Unit_2_Part_3 == v2.Unit_2_Part_3)
								{
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		public static bool operator <=(Versions version1, Versions version2)
		{
			return IsLessOrEqual(version1, version2);
		}

		private static bool IsLessOrEqual(Versions v1, Versions v2)
		{
			if (v1.Unit_1_Part_1 > v2.Unit_1_Part_1)
			{
				return false;
			}
			else if (v1.Unit_1_Part_1 == v2.Unit_1_Part_1)
			{
				if (v1.Unit_1_Part_2 > v2.Unit_1_Part_2)
				{
					return false;
				}
				else if (v1.Unit_1_Part_2 == v2.Unit_1_Part_2)
				{
					if (v1.Unit_1_Part_3 > v2.Unit_1_Part_3)
					{
						return false;
					}
					else if (v1.Unit_1_Part_3 == v2.Unit_1_Part_3)
					{
						if (v1.Unit_2_Part_1 > v2.Unit_2_Part_1)
						{
							return false;
						}
						if (v1.Unit_2_Part_1 == null && v2.Unit_2_Part_1 != null)
						{
							return false;
						}
						if (v1.Unit_2_Part_1 == v2.Unit_2_Part_1)
						{
							if (v1.Unit_2_Part_2 > v2.Unit_2_Part_2)
							{
								return false;
							}
							if (v1.Unit_2_Part_2 == null && v2.Unit_2_Part_2 != null)
							{
								return false;
							}
							if (v1.Unit_2_Part_2 == v2.Unit_2_Part_2)
							{
								if (v1.Unit_2_Part_3 > v2.Unit_2_Part_3)
								{
									return false;
								}
								if (v1.Unit_2_Part_3 == null && v2.Unit_2_Part_3 != null)
								{
									return false;
								}
								if (v1.Unit_2_Part_3 == v2.Unit_2_Part_3)
								{
									return true;
								}
							}
						}
					}
				}
			}
			return true;
		}

		public static bool operator ==(Versions version1, Versions version2)
		{
			return IsEqual(version1, version2);
		}

		public static bool operator !=(Versions version1, Versions version2)
		{
			return !IsEqual(version1, version2);
		}

		private static bool IsEqual(Versions v1, Versions v2)
		{
			if (v1.ToString() == v2.ToString())
			{
				return true;
			}
			return false;
		}

		public override string ToString()
		{
			if (Unit_2_Part_1 != null && Unit_2_Part_2 != null && Unit_2_Part_3 != null)
			{
				return $"{Unit_1_Part_1}.{Unit_1_Part_2}.{Unit_1_Part_3}-{ConvertDigitToPart(Unit_2_Part_1)}.{ConvertDigitToPart(Unit_2_Part_2)}.{ConvertDigitToPart(Unit_2_Part_3)}";
			}
			if (Unit_2_Part_1 != null && Unit_2_Part_2 != null && Unit_2_Part_3 == null)
			{
				return $"{Unit_1_Part_1}.{Unit_1_Part_2}.{Unit_1_Part_3}-{ConvertDigitToPart(Unit_2_Part_1)}.{ConvertDigitToPart(Unit_2_Part_2)}";
			}
			if (Unit_2_Part_1 != null && Unit_2_Part_2 == null && Unit_2_Part_3 == null)
			{
				return $"{Unit_1_Part_1}.{Unit_1_Part_2}.{Unit_1_Part_3}-{ConvertDigitToPart(Unit_2_Part_1)}";
			}
			if (Unit_2_Part_1 == null && Unit_2_Part_2 == null && Unit_2_Part_3 == null)
			{
				return $"{Unit_1_Part_1}.{Unit_1_Part_2}.{Unit_1_Part_3}";
			}
			return "";
		}
	}
}

