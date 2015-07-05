//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.VisualBasic.CompilerServices;

//namespace Microsoft.VisualBasic
//{
//    public class Strings
//    {
//        public static string StrConv(string str, VbStrConv Conversion, int LocaleID = 0)
//        {
//            string upper;
//            int num = 0;
//            CultureInfo cultureInfo;
//            try
//            {
//                if (LocaleID == 0 || LocaleID == 1)
//                {
//                    cultureInfo = Utils.GetCultureInfo();
//                    LocaleID = cultureInfo.LCID;
//                }
//                else
//                {
//                    try
//                    {
//                        cultureInfo = new CultureInfo(LocaleID & 65535);
//                    }
//                    catch (StackOverflowException stackOverflowException)
//                    {
//                        throw stackOverflowException;
//                    }
//                    catch (OutOfMemoryException outOfMemoryException)
//                    {
//                        throw outOfMemoryException;
//                    }
//                    catch (ThreadAbortException threadAbortException)
//                    {
//                        throw threadAbortException;
//                    }
//                    catch (Exception exception)
//                    {
//                        string[] strArrays = new string[] { Conversions.ToString(LocaleID) };
//                        throw new ArgumentException(Utils.GetResourceString("Argument_LCIDNotSupported1", strArrays));
//                    }
//                }
//                int num1 = Strings.PRIMARYLANGID(LocaleID);
//                if (((int)Conversion & -1856) != (int)VbStrConv.None)
//                {
//                    throw new ArgumentException(Utils.GetResourceString("Argument_InvalidVbStrConv"));
//                }
//                int conversion = (int)(Conversion & (VbStrConv.SimplifiedChinese | VbStrConv.TraditionalChinese));
//                if (conversion != 0)
//                {
//                    if (conversion == 768)
//                    {
//                        throw new ArgumentException(Utils.GetResourceString("Argument_StrConvSCandTC"));
//                    }
//                    if (conversion == 256)
//                    {
//                        if (!Strings.IsValidCodePage(936) || !Strings.IsValidCodePage(950))
//                        {
//                            throw new ArgumentException(Utils.GetResourceString("Argument_SCNotSupported"));
//                        }
//                        num = num | 33554432;
//                    }
//                    else if (conversion == 512)
//                    {
//                        if (!Strings.IsValidCodePage(936) || !Strings.IsValidCodePage(950))
//                        {
//                            throw new ArgumentException(Utils.GetResourceString("Argument_TCNotSupported"));
//                        }
//                        num = num | 67108864;
//                    }
//                }
//                switch (Conversion & VbStrConv.ProperCase)
//                {
//                    case VbStrConv.None:
//                        {
//                            if ((Conversion & VbStrConv.LinguisticCasing) == VbStrConv.None)
//                            {
//                                break;
//                            }
//                            throw new ArgumentException(Utils.GetResourceString("LinguisticRequirements"));
//                        }
//                    case VbStrConv.Uppercase:
//                        {
//                            if (Conversion != VbStrConv.Uppercase)
//                            {
//                                num = num | 512;
//                                break;
//                            }
//                            else
//                            {
//                                upper = cultureInfo.TextInfo.ToUpper(str);
//                                return upper;
//                            }
//                        }
//                    case VbStrConv.Lowercase:
//                        {
//                            if (Conversion != VbStrConv.Lowercase)
//                            {
//                                num = num | 256;
//                                break;
//                            }
//                            else
//                            {
//                                upper = cultureInfo.TextInfo.ToLower(str);
//                                return upper;
//                            }
//                        }
//                    case VbStrConv.ProperCase:
//                        {
//                            num = 0;
//                            break;
//                        }
//                }
//                if ((Conversion & (VbStrConv.Katakana | VbStrConv.Hiragana)) != VbStrConv.None && (num1 != 17 || !Strings.ValidLCID(LocaleID)))
//                {
//                    throw new ArgumentException(Utils.GetResourceString("Argument_JPNNotSupported"));
//                }
//                if ((Conversion & (VbStrConv.Wide | VbStrConv.Narrow)) != VbStrConv.None)
//                {
//                    if (num1 != 17 && num1 != 18 && num1 != 4)
//                    {
//                        throw new ArgumentException(Utils.GetResourceString("Argument_WideNarrowNotApplicable"));
//                    }
//                    if (!Strings.ValidLCID(LocaleID))
//                    {
//                        throw new ArgumentException(Utils.GetResourceString("Argument_LocalNotSupported"));
//                    }
//                }
//                switch (Conversion & (VbStrConv.Wide | VbStrConv.Narrow))
//                {
//                    case VbStrConv.Uppercase:
//                    case VbStrConv.Lowercase:
//                    case VbStrConv.ProperCase:
//                    case VbStrConv.Uppercase | VbStrConv.Wide:
//                    case VbStrConv.Lowercase | VbStrConv.Wide:
//                    case VbStrConv.Uppercase | VbStrConv.Lowercase | VbStrConv.ProperCase | VbStrConv.Wide:
//                    case VbStrConv.Uppercase | VbStrConv.Narrow:
//                    case VbStrConv.Lowercase | VbStrConv.Narrow:
//                    case VbStrConv.Uppercase | VbStrConv.Lowercase | VbStrConv.ProperCase | VbStrConv.Narrow:
//                        {
//                        Label1:
//                            VbStrConv vbStrConv = Conversion & (VbStrConv.Katakana | VbStrConv.Hiragana);
//                            if (vbStrConv != VbStrConv.None)
//                            {
//                                if (vbStrConv == (VbStrConv.Katakana | VbStrConv.Hiragana))
//                                {
//                                    throw new ArgumentException(Utils.GetResourceString("Argument_IllegalKataHira"));
//                                }
//                                if (vbStrConv == VbStrConv.Katakana)
//                                {
//                                    num = num | 2097152;
//                                }
//                                else if (vbStrConv == VbStrConv.Hiragana)
//                                {
//                                    num = num | 1048576;
//                                }
//                            }
//                            if ((Conversion & VbStrConv.ProperCase) == VbStrConv.ProperCase)
//                            {
//                                upper = Strings.ProperCaseString(cultureInfo, num, str);
//                                break;
//                            }
//                            else if (num == 0)
//                            {
//                                upper = str;
//                                break;
//                            }
//                            else
//                            {
//                                upper = Strings.vbLCMapString(cultureInfo, num, str);
//                                break;
//                            }
//                        }
//                    case VbStrConv.Wide:
//                        {
//                            num = num | 8388608;
//                            goto case VbStrConv.Uppercase | VbStrConv.Lowercase | VbStrConv.ProperCase | VbStrConv.Narrow;
//                        }
//                    case VbStrConv.Narrow:
//                        {
//                            num = num | 4194304;
//                            goto case VbStrConv.Uppercase | VbStrConv.Lowercase | VbStrConv.ProperCase | VbStrConv.Narrow;
//                        }
//                    case VbStrConv.Wide | VbStrConv.Narrow:
//                        {
//                            throw new ArgumentException(Utils.GetResourceString("Argument_IllegalWideNarrow"));
//                        }
//                    default:
//                        {
//                            goto case VbStrConv.Uppercase | VbStrConv.Lowercase | VbStrConv.ProperCase | VbStrConv.Narrow;
//                        }
//                }
//            }
//            catch (Exception exception1)
//            {
//                throw exception1;
//            }
//            return upper;
//        }

//    }

//    /// <summary>Indicates which type of conversion to perform when calling the StrConv function.</summary>
//    /// <filterpriority>1</filterpriority>
//    [Flags]
//    public enum VbStrConv
//    {
//        /// <summary>Performs no conversion.</summary>
//        None = 0,
//        /// <summary>Converts the string to uppercase characters. This member is equivalent to the Visual Basic constant vbUpperCase.</summary>
//        Uppercase = 1,
//        /// <summary>Converts the string to lowercase characters. This member is equivalent to the Visual Basic constant vbLowerCase.</summary>
//        Lowercase = 2,
//        /// <summary>Converts the first letter of every word in the string to uppercase. This member is equivalent to the Visual Basic constant vbProperCase.</summary>
//        ProperCase = 3,
//        /// <summary>Converts narrow (single-byte) characters in the string to wide (double-byte) characters. Applies to Asian locales. This member is equivalent to the Visual Basic constant vbWide.</summary>
//        Wide = 4,
//        /// <summary>Converts wide (double-byte) characters in the string to narrow (single-byte) characters. Applies to Asian locales. This member is equivalent to the Visual Basic constant vbNarrow.</summary>
//        Narrow = 8,
//        /// <summary>Converts Hiragana characters in the string to Katakana characters. Applies to Japanese locale only. This member is equivalent to the Visual Basic constant vbKatakana.</summary>
//        Katakana = 16,
//        /// <summary>Converts Katakana characters in the string to Hiragana characters. Applies to Japanese locale only. This member is equivalent to the Visual Basic constant vbHiragana.</summary>
//        Hiragana = 32,
//        /// <summary>Converts the string to Simplified Chinese characters. This member is equivalent to the Visual Basic constant vbSimplifiedChinese.</summary>
//        SimplifiedChinese = 256,
//        /// <summary>Converts the string to Traditional Chinese characters. This member is equivalent to the Visual Basic constant vbTraditionalChinese.</summary>
//        TraditionalChinese = 512,
//        /// <summary>Converts the string from file system rules for casing to linguistic rules. This member is equivalent to the Visual Basic constant vbLinguisticCasing.</summary>
//        LinguisticCasing = 1024
//    }
//}

//namespace Microsoft.VisualBasic.CompilerServices
//{
//    public class Utils
//    {
//        internal static CultureInfo GetCultureInfo()
//        {
//            return Thread.CurrentThread.CurrentCulture;
//        }
//    }
//}

