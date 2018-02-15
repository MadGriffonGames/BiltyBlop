using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Reflection;
using System.IO;
using UnityEngine;
#if UNITY_STANDALONE_OSX
using System.Net.NetworkInformation;
#endif
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DevToDev {
    public class MacOSHelperNative {
#if UNITY_STANDALONE_OSX
        [DllImport ("DevToDevOSX")]
        public static extern string dtd_a();
        [DllImport ("DevToDevOSX")]
        public static extern string dtd_g();
        [DllImport ("DevToDevOSX")]
        public static extern string dtd_p();
        [DllImport ("DevToDevOSX")]
        public static extern string dtd_o();
        [DllImport ("DevToDevOSX")]
        public static extern string dtd_d();
        [DllImport ("DevToDevOSX")]
        public static extern string dtd_e();
        [DllImport ("DevToDevOSX")] 
        public static extern string dtd_f();
        [DllImport("DevToDevOSX")]
        public static extern void dtd_z();
        [DllImport ("DevToDevOSX")]
        public static extern string dtd_i(string key);
        [DllImport ("DevToDevOSX")]
        public static extern void dtd_j(string key);
#endif
    }

    public class MacOSHelper {
        public static string dtd_g() {
#if UNITY_STANDALONE_OSX
			return MacOSHelperNative.dtd_g();
#endif
            return null;
        }

        public static string dtd_o() {
#if UNITY_STANDALONE_OSX
            return MacOSHelperNative.dtd_o();
#endif
            return null;
        }

        public static string dtd_p() {
#if UNITY_STANDALONE_OSX
            return MacOSHelperNative.dtd_p();
#endif
            return null;
        }

        public static string dtd_a() {
#if UNITY_STANDALONE_OSX
			return MacOSHelperNative.dtd_a();
#endif
            return null;
        }

        public static string dtd_e() {
#if UNITY_STANDALONE_OSX
			return MacOSHelperNative.dtd_e();
#endif
            return null;
        }

        public static string dtd_f() {
#if UNITY_STANDALONE_OSX
			return MacOSHelperNative.dtd_f();
#endif
            return null;
        }

        public static string dtd_d() {
#if UNITY_STANDALONE_OSX
			return MacOSHelperNative.dtd_d();
#endif
            return null;
        }

        public static void dtd_z() {
#if UNITY_STANDALONE_OSX
            MacOSHelperNative.dtd_z();
#endif
        }

        public static string dtd_i(string a) {
#if UNITY_STANDALONE_OSX
			return MacOSHelperNative.dtd_i(a);            
#endif
            return null;
        }

        public static void dtd_j(string a) {
#if UNITY_STANDALONE_OSX
			MacOSHelperNative.dtd_j(a);               
#endif
        }
    }
}
