/*
* Copyright (c) 2021 PlayEveryWare
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

#if !EOS_DISABLE
using Epic.OnlineServices.Platform;
using Epic.OnlineServices.Auth;
#endif
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace PlayEveryWare.EpicOnlineServices
{
    using Epic.OnlineServices.IntegratedPlatform;
    using Extensions;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the default deployment ID to use when a given sandbox ID is active.
    /// </summary>
    [Serializable]
    public class SandboxDeploymentOverride
    {
        public string sandboxID;
        public string deploymentID;
    }

    /// <summary>
    /// Represents the EOS Configuration used for initializing EOS SDK.
    /// </summary>
    [Serializable]
    public class EOSConfig : Config
    {
        static EOSConfig()
        {
            InvalidEncryptionKeyRegex = new Regex("[^0-9a-fA-F]");
            RegisterFactory(() => new EOSConfig());
        }

        protected EOSConfig() : base("EpicOnlineServicesConfig.json") { }

        /// <value><c>Product Name</c> defined in the [Development Portal](https://dev.epicgames.com/portal/)</value>
        public string productName;

        /// <value>Version of Product</value>
        public string productVersion;

        /// <value><c>Product Id</c> defined in the [Development Portal](https://dev.epicgames.com/portal/)</value>
        public string productID;

        /// <value><c>Sandbox Id</c> defined in the [Development Portal](https://dev.epicgames.com/portal/)</value>
        public string sandboxID;

        /// <value><c>Deployment Id</c> defined in the [Development Portal](https://dev.epicgames.com/portal/)</value>
        public string deploymentID;

        /// <value><c>SandboxDeploymentOverride</c> pairs used to override Deployment ID when a given Sandbox ID is used</value>
        public List<SandboxDeploymentOverride> sandboxDeploymentOverrides;

        /// <value><c>Client Secret</c> defined in the [Development Portal](https://dev.epicgames.com/portal/)</value>
        public string clientSecret;

        /// <value><c>Client Id</c> defined in the [Development Portal](https://dev.epicgames.com/portal/)</value>
        public string clientID;

        /// <value><c>Encryption Key</c> used by default to decode files previously encoded and stored in EOS</value>
        public string encryptionKey;

        /// <value><c>Flags</c> used to initilize the EOS platform.</value>
        public List<string> platformOptionsFlags;

        /// <value><c>Flags</c> used to set user auth when logging in.</value>
        public List<string> authScopeOptionsFlags;

        /// <value><c>Tick Budget</c> used to define the maximum amount of execution time the EOS SDK can use each frame.</value>
        public uint tickBudgetInMilliseconds;

        /// <value><c>Network Work Affinity</c> specifies thread affinity for network management that is not IO.</value>
        public string ThreadAffinity_networkWork;
        /// <value><c>Storage IO Affinity</c> specifies affinity for threads that will interact with a storage device.</value>
        public string ThreadAffinity_storageIO;
        /// <value><c>Web Socket IO Affinity</c> specifies affinity for threads that generate web socket IO.</value>
        public string ThreadAffinity_webSocketIO;
        /// <value><c>P2P IO Affinity</c> specifies affinity for any thread that will generate IO related to P2P traffic and management.</value>
        public string ThreadAffinity_P2PIO;
        /// <value><c>HTTP Request IO Affinity</c> specifies affinity for any thread that will generate http request IO.</value>
        public string ThreadAffinity_HTTPRequestIO;
        /// <value><c>RTC IO Affinity</c> specifies affinity for any thread that will generate IO related to RTC traffic and management.</value>
        public string ThreadAffinity_RTCIO;


        /// <value><c>Always Send Input to Overlay </c>If true, the plugin will always send input to the overlay from the C# side to native, and handle showing the overlay. This doesn't always mean input makes it to the EOS SDK</value>
        public bool alwaysSendInputToOverlay;

        /// <value><c>Initial Button Delay</c> Stored as a string so it can be 'empty'</value>
        public string initialButtonDelayForOverlay;

        /// <value><c>Repeat button delay for overlay</c> Stored as a string so it can be 'empty' </value>
        public string repeatButtonDelayForOverlay;

        /// <value><c>HACK: send force send input without delay</c>If true, the native plugin will always send input received directly to the SDK. If set to false, the plugin will attempt to delay the input to mitigate CPU spikes caused by spamming the SDK </value>
        public bool hackForceSendInputDirectlyToSDK;

        /// <value><c> set to 'true' if the application is a dedicated game server</c>>
        public bool isServer;

        public static Regex InvalidEncryptionKeyRegex;
        
        private static bool IsEncryptionKeyValid(string key)
        {
            return
                //key not null
                key != null &&
                //key is 64 characters
                key.Length == 64 &&
                //key is all hex characters
                !InvalidEncryptionKeyRegex.Match(key).Success;
        }

#if !EOS_DISABLE

        public static IntegratedPlatformManagementFlags GetIntegratedPlatformManagementFlags(List<string> stringFlags)
        {
            return StringsToEnum<IntegratedPlatformManagementFlags>(stringFlags,
                IntegratedPlatformManagementFlagsExtensions.TryParse);
        }

        /// <summary>
        /// Returns a single PlatformFlags enum value that results from a
        /// bitwise OR operation of all the platformOptionsFlags flags on this
        /// config.
        /// </summary>
        /// <returns>A PlatformFlags enum value.</returns>
        public PlatformFlags GetPlatformFlags()
        {
            return StringsToEnum<PlatformFlags>(platformOptionsFlags, PlatformFlagsExtensions.TryParse);
        }

        /// <summary>
        /// Returns a single AuthScopeFlags enum value that results from a
        /// bitwise OR operation of all the authScopeOptionsFlags flags on this
        /// config.
        /// </summary>
        /// <returns>An AuthScopeFlags enum value.</returns>
        public AuthScopeFlags GetAuthScopeFlags()
        {
            return StringsToEnum<AuthScopeFlags>(authScopeOptionsFlags, AuthScopeFlagsExtensions.TryParse);
        }
#endif

        //-------------------------------------------------------------------------
        public float GetInitialButtonDelayForOverlayAsFloat()
        {
            return float.Parse(initialButtonDelayForOverlay);
        }

        //-------------------------------------------------------------------------
        public void SetInitialButtonDelayForOverlayFromFloat(float f)
        {
            initialButtonDelayForOverlay = f.ToString();
        }

        //-------------------------------------------------------------------------
        public float GetRepeatButtonDelayForOverlayAsFloat()
        {
           return float.Parse(repeatButtonDelayForOverlay);
        }

        //-------------------------------------------------------------------------
        public void SetRepeatButtonDelayForOverlayFromFloat(float f)
        {
            repeatButtonDelayForOverlay = f.ToString();
        }

        //-------------------------------------------------------------------------
        public ulong GetThreadAffinityNetworkWork(ulong defaultValue = 0)
        {
            return GetULongFromString(ThreadAffinity_networkWork, defaultValue);
        }

        //-------------------------------------------------------------------------
        public ulong GetThreadAffinityStorageIO(ulong defaultValue = 0)
        {
            return GetULongFromString(ThreadAffinity_storageIO, defaultValue);
        }
 
        //-------------------------------------------------------------------------
        public ulong GetThreadAffinityWebSocketIO(ulong defaultValue = 0)
        {
            return GetULongFromString(ThreadAffinity_webSocketIO, defaultValue);
        }

        //-------------------------------------------------------------------------
        public ulong GetThreadAffinityP2PIO(ulong defaultValue = 0)
        {
            return GetULongFromString(ThreadAffinity_P2PIO, defaultValue);
        }

        //-------------------------------------------------------------------------
        public ulong GetThreadAffinityHTTPRequestIO(ulong defaultValue = 0)
        {
            return GetULongFromString(ThreadAffinity_HTTPRequestIO, defaultValue);
        }

        //-------------------------------------------------------------------------
        public ulong GetThreadAffinityRTCIO(ulong defaultValue = 0)
        {
            return GetULongFromString(ThreadAffinity_RTCIO, defaultValue);
        }

        /// <summary>
        /// Wrapper function for ulong.Parse. Returns the result of passing that
        /// function to the ulong.Parse method, setting the value to the given
        /// defaultValue if the parsing fails.
        /// </summary>
        /// <param name="str">The string to parse into a ulong.</param>
        /// <param name="defaultValue">
        /// The value to return in the event parsing fails.
        /// </param>
        /// <returns>
        /// The result of parsing the string to a ulong, or defaultValue if
        /// parsing fails.
        /// </returns>
        private static ulong GetULongFromString(string str, ulong defaultValue)
        {
            if (!ulong.TryParse(str, out ulong value))
            {
                value = defaultValue;
            }

            return value;
        }

        //-------------------------------------------------------------------------
        public bool IsEncryptionKeyValid()
        {
            return IsEncryptionKeyValid(encryptionKey);
        }
    }
}
