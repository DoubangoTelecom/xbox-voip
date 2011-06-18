using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doubango.tinySAK
{
    public class TSK_RagelState
    {
        private int mCS;
        private int mP;
        private int mPE;
        private int mEOF;
        private int mTagStart;
        private int mTagEnd;
        private byte[] mData;

        public TSK_RagelState(byte[] data)
        {
            this.CS = 0;
            this.P = 0;
            this.PE = data.Length;
            this.EOF = 0;

            this.TagStart = 0;
            this.TagEnd = 0;

            mData = data;
        }

        public int CS
        {
            get { return mCS; }
            set { mCS = value; }
        }

        public int P
        {
            get { return mP; }
            set { mP = value; }
        }

        public int PE
        {
            get { return mPE; }
            set { mPE = value; }
        }

        public int EOF
        {
            get { return mEOF; }
            set { mEOF = value; }
        }

        public int TagStart
        {
            get { return mTagStart; }
            set { mTagStart = value; }
        }

        public int TagEnd
        {
            get { return mTagEnd; }
            set { mTagEnd = value; }
        }

        public byte[] Data
        {
            get { return mData; }
        }

        public static TSK_RagelState Init(byte[] data)
        {
            return new TSK_RagelState(data);
        }

        /// <summary>
        /// Ragel scanner utility functions
        /// </summary>
        public static class Scanner
        {
            public static String GetString(String data, int ts, int te)
            {
                int len = (te - ts);
                if (len > 0)
                {
                    try
                    {
                        return data.Substring(ts, len);
                    }
                    catch (Exception e)
                    {
                        TSK_Debug.Error(e.ToString());
                    }
                }
                return null;
            }

            public static Int32 GetInt32(String data, int ts, int te)
            {
                int len = (int)(te - ts);
                if (len >= 0)
                {
                    try
                    {
                        return Int32.Parse(data.Substring(ts, len));
                    }
                    catch (Exception e)
                    {
                        TSK_Debug.Error(e.ToString());
                    }
                }
                return -1;
            }

            public static Double GetFloat(String data, int ts, int te)
            {
                int len = (int)(te - ts);
                if (len >= 0)
                {
                    try
                    {
                        return Double.Parse(data.Substring(ts, len));
                    }
                    catch (Exception e)
                    {
                        TSK_Debug.Error(e.ToString());
                    }
                }
                return -1.0;
            }

            public static void AddParam(String data, int ts, int te, ref List<TSK_Param> @params)
            {
                int len = (te - ts);
                TSK_Param param = TSK_Param.Parse(data.Substring(ts, len));
                if (param != null)
                {
                    @params.Add(param);
                }
            }

            
        }

        /// <summary>
        /// Ragel parser utility functions
        /// </summary>
        public static class Parser
        {
            public static String GetString(String data, int p, int tag_start)
            {
                int len = (int)(p - tag_start);
                if (len > 0)
                {
                    try
                    {
                        return data.Substring(tag_start, len);
                    }
                    catch (Exception e)
                    {
                        TSK_Debug.Error(e.ToString());
                    }
                }
                return null;
            }

            public static List<String> AddString(String data, int p, int tag_start, List<String> strings)
            {
                String @string = Parser.GetString(data, p, tag_start);
                if (!String.IsNullOrEmpty(@string))
                {
                    strings.Add(@string);
                }
                return strings;
            }

            public static TSK_Param GetParam(String data, int p, int tag_start)
            {
                int len = (p - tag_start);
                return TSK_Param.Parse(data.Substring(tag_start, len));
            }

            public static List<TSK_Param> AddParam(String data, int p, int tag_start, List<TSK_Param> @params)
            {
                TSK_Param param = Parser.GetParam(data, p, tag_start);
                if (param != null)
                {
                    @params.Add(param);
                }
                return @params;
            }

            public static Int32 GetInt32(String data, int p, int tag_start)
            {
                return (Int32)Parser.GetInt64(data, p, tag_start);
            }

            public static Int16 GetInt16(String data, int p, int tag_start)
            {
                return (Int16)Parser.GetInt64(data, p, tag_start);
            }

            public static UInt32 GetUInt32(String data, int p, int tag_start)
            {
                return (UInt32)Parser.GetInt64(data, p, tag_start);
            }

            public static Int64 GetInt64(String data, int p, int tag_start)
            {
                int len = (p - tag_start);
                if (len >= 0)
                {
                    try
                    {
                        return Int64.Parse(data.Substring(tag_start, len));
                    }
                    catch (Exception e)
                    {
                        TSK_Debug.Error(e.ToString());
                    }
                }
                return -1;
            }

            public static void AddString(String data, int p, int tag_start, ref List<String> strings)
            {
                int len = (p - tag_start);
                String @string = data.Substring(tag_start, len);
                if (strings != null)
                {
                    strings.Add(@string);
                }
            }
        }
    }
}
