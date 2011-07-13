/* Copyright (C) 2010-2011 Mamadou Diop. 
* Copyright (C) 2011 Doubango Telecom <http://www.doubango.org>
*
* Contact: Mamadou Diop <diopmamadou(at)doubango(dot)org>
*	
* This file is part of Open Source Xbox-VoIP Project <http://code.google.com/p/xbox-voip/>
*
* Xbox-VoIP is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
*	
* XBox-Voip is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
*	
* You should have received a copy of the GNU General Public License
* along with XBox-Voip.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doubango.tinySAK
{
    public class TSK_Param
    {
        private String mName;
        private String mValue;
        private Boolean mTag;

        public TSK_Param(String name, String value)
        {
            mName = name;
            mValue = value;
        }

        public String Name
        {
            get { return mName; }
        }

        public String Value
        {
            get { return mValue; }
            set { mValue = value; }
        }

        public Boolean Tag
        {
            get { return mTag; }
            set { mTag = value; }
        }

        public override string  ToString()
        {
            return TSK_Param.ToString(this);
        }

        public static TSK_Param Create(String name, String value)
        {
            return new TSK_Param(name, value);
        }

        public static TSK_Param CreatNull()
        {
            return new TSK_Param(null, null);
        }

        public static TSK_Param Parse(String line)
        {
            if (!String.IsNullOrEmpty(line))
            {
                int start = 0;
                int end = line.Length;
                int equal = line.IndexOf("=");
                String name = null;
                String value = null;

                if (equal >= 0 && equal < end)
                {
                    name = line.Substring(start, (equal - start));
                    value = line.Substring(equal + 1, (end - equal - 1));
                }
                else
                {
                    name = line.Substring(start, end);
                }

                return TSK_Param.Create(name, value);
            }
            return null;
        }

        public static TSK_Param GetByName(List<TSK_Param> @params, String name)
        {
            if (@params != null && !String.IsNullOrEmpty(name))
            {
                return @params.FirstOrDefault(
                    (x) => { return x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase); }
                );
            }
            return null;
        }

        public static Boolean HasParam(List<TSK_Param> @params, String name)
        {
            return TSK_Param.GetByName(@params, name) != null;
        }

        public static List<TSK_Param> AddParam(List<TSK_Param> @params, String name, String value)
        {
            TSK_Param param = null;
            if (@params == null || String.IsNullOrEmpty(name))
            {
		        TSK_Debug.Error("Invalid parameter");
                return @params;
	        }

            if ((param = TSK_Param.GetByName(@params, name)) != null)
            {
                param.Value = value;/* Already exist ==> update the value. */
            }
            else
            {
                @params.Add(TSK_Param.Create(name, value));
            }

            return @params;
        }

        public static List<TSK_Param> RemoveParam(List<TSK_Param> @params, String name)
        {
            if (@params == null || String.IsNullOrEmpty(name))
            {
                TSK_Debug.Error("Invalid parameter");
                return @params;
            }

#if WINDOWS_PHONE
            again:
            foreach (TSK_Param param in @params)
            {
                if (String.Equals(param.Name, name, StringComparison.InvariantCultureIgnoreCase))
                {
                    @params.Remove(param);
                    goto again;
                }
            }
#else
            @params.RemoveAll(
                 (x) => { return x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase); }
            );
#endif

            return @params;
        }

        public static String ToString(TSK_Param param)
        {
            if (param != null)
            {
                return String.Format(!String.IsNullOrEmpty(param.Value) ? "{0}={1}" : "{0}", param.Name, param.Value);
	        }
	        return String.Empty;
        }
        
        public static String ToString(List<TSK_Param> @params, char separator)
        {
	        String ret = String.Empty;
	        if(@params != null){
                foreach(TSK_Param param in @params)
                {
                    if (String.IsNullOrEmpty(ret))
                    {
                        ret += String.Format(!String.IsNullOrEmpty(param.Value) ? "{0}={1}" : "{0}", param.Name, param.Value);
                    }
                    else
                    {
                        ret += String.Format(!String.IsNullOrEmpty(param.Value) ? "{0}{1}={2}" : "{0}{1}", separator, param.Name, param.Value);
                    }
                }
	        }
	        return ret;
        }
    }
}
