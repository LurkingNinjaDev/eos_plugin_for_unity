// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.AntiCheatClient
{
	public sealed partial class AntiCheatClientInterface : Handle
	{
		public AntiCheatClientInterface()
		{
		}

		public AntiCheatClientInterface(System.IntPtr innerHandle) : base(innerHandle)
		{
		}

		public const int AddexternalintegritycatalogApiLatest = 1;

		public const int AddnotifyclientintegrityviolatedApiLatest = 1;

		public const int AddnotifymessagetopeerApiLatest = 1;

		public const int AddnotifymessagetoserverApiLatest = 1;

		public const int AddnotifypeeractionrequiredApiLatest = 1;

		public const int AddnotifypeerauthstatuschangedApiLatest = 1;

		public const int BeginsessionApiLatest = 3;

		public const int EndsessionApiLatest = 1;

		public const int GetprotectmessageoutputlengthApiLatest = 1;

		/// <summary>
		/// Maximum size of an individual message provided through <see cref="OnMessageToPeerCallback" />.
		/// </summary>
		public const int OnmessagetopeercallbackMaxMessageSize = 512;

		/// <summary>
		/// Maximum size of an individual message provided through <see cref="OnMessageToServerCallback" />.
		/// </summary>
		public const int OnmessagetoservercallbackMaxMessageSize = 512;

		/// <summary>
		/// A special peer handle that represents the client itself.
		/// It does not need to be registered or unregistered and is
		/// used in OnPeerActionRequiredCallback to quickly signal to the user
		/// that they will not be able to join online play.
		/// </summary>
		public System.IntPtr PeerSelf = (System.IntPtr)(-1);

		/// <summary>
		/// This file contains the deprecated types for EOS AntiCheat Client. In a future version, these types will be removed.
		/// </summary>
		public const int PollstatusApiLatest = 1;

		public const int ProtectmessageApiLatest = 1;

		public const int ReceivemessagefrompeerApiLatest = 1;

		public const int ReceivemessagefromserverApiLatest = 1;

		public const int RegisterpeerApiLatest = 3;

		public const int RegisterpeerMaxAuthenticationtimeout = 120;

		/// <summary>
		/// Limits on RegisterTimeoutSeconds parameter
		/// </summary>
		public const int RegisterpeerMinAuthenticationtimeout = 40;

		public const int UnprotectmessageApiLatest = 1;

		public const int UnregisterpeerApiLatest = 1;

		/// <summary>
		/// Optional. Adds an integrity catalog and certificate pair from outside the game directory,
		/// for example to support mods that load from elsewhere.
		/// Mode: All
		/// </summary>
		/// <param name="options">Structure containing input data.</param>
		/// <returns>
		/// <see cref="Result.Success" /> - If the integrity catalog was added successfully
		/// <see cref="Result.InvalidParameters" /> - If input data was invalid
		/// </returns>
		public Result AddExternalIntegrityCatalog(ref AddExternalIntegrityCatalogOptions options)
		{
			AddExternalIntegrityCatalogOptionsInternal optionsInternal = new AddExternalIntegrityCatalogOptionsInternal();
			optionsInternal.Set(ref options);

			var funcResult = Bindings.EOS_AntiCheatClient_AddExternalIntegrityCatalog(InnerHandle, ref optionsInternal);

			Helper.Dispose(ref optionsInternal);

			return funcResult;
		}

		/// <summary>
		/// Add a callback when a message must be displayed to the local client informing them on a local integrity violation,
		/// which will prevent further online play.
		/// Mode: Any.
		/// </summary>
		/// <param name="options">Structure containing input data</param>
		/// <param name="clientData">This value is returned to the caller when NotificationFn is invoked</param>
		/// <param name="notificationFn">The callback to be fired</param>
		/// <returns>
		/// A valid notification ID if successfully bound, or <see cref="Common.InvalidNotificationid" /> otherwise
		/// </returns>
		public ulong AddNotifyClientIntegrityViolated(ref AddNotifyClientIntegrityViolatedOptions options, object clientData, OnClientIntegrityViolatedCallback notificationFn)
		{
			AddNotifyClientIntegrityViolatedOptionsInternal optionsInternal = new AddNotifyClientIntegrityViolatedOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var notificationFnInternal = new OnClientIntegrityViolatedCallbackInternal(OnClientIntegrityViolatedCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, notificationFn, notificationFnInternal);

			var funcResult = Bindings.EOS_AntiCheatClient_AddNotifyClientIntegrityViolated(InnerHandle, ref optionsInternal, clientDataAddress, notificationFnInternal);

			Helper.Dispose(ref optionsInternal);

			Helper.AssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// Add a callback issued when a new message must be dispatched to a connected peer. The bound function will only be called
		/// between a successful call to <see cref="BeginSession" /> and the matching <see cref="EndSession" /> call in mode <see cref="AntiCheatClientMode.PeerToPeer" />.
		/// Mode: <see cref="AntiCheatClientMode.PeerToPeer" />.
		/// </summary>
		/// <param name="options">Structure containing input data</param>
		/// <param name="clientData">This value is returned to the caller when NotificationFn is invoked</param>
		/// <param name="notificationFn">The callback to be fired</param>
		/// <returns>
		/// A valid notification ID if successfully bound, or <see cref="Common.InvalidNotificationid" /> otherwise
		/// </returns>
		public ulong AddNotifyMessageToPeer(ref AddNotifyMessageToPeerOptions options, object clientData, OnMessageToPeerCallback notificationFn)
		{
			AddNotifyMessageToPeerOptionsInternal optionsInternal = new AddNotifyMessageToPeerOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var notificationFnInternal = new OnMessageToPeerCallbackInternal(OnMessageToPeerCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, notificationFn, notificationFnInternal);

			var funcResult = Bindings.EOS_AntiCheatClient_AddNotifyMessageToPeer(InnerHandle, ref optionsInternal, clientDataAddress, notificationFnInternal);

			Helper.Dispose(ref optionsInternal);

			Helper.AssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// Add a callback issued when a new message must be dispatched to the game server. The bound function will only be called
		/// between a successful call to <see cref="BeginSession" /> and the matching <see cref="EndSession" /> call in mode <see cref="AntiCheatClientMode.ClientServer" />.
		/// Mode: <see cref="AntiCheatClientMode.ClientServer" />.
		/// </summary>
		/// <param name="options">Structure containing input data</param>
		/// <param name="clientData">This value is returned to the caller when NotificationFn is invoked</param>
		/// <param name="notificationFn">The callback to be fired</param>
		/// <returns>
		/// A valid notification ID if successfully bound, or <see cref="Common.InvalidNotificationid" /> otherwise
		/// </returns>
		public ulong AddNotifyMessageToServer(ref AddNotifyMessageToServerOptions options, object clientData, OnMessageToServerCallback notificationFn)
		{
			AddNotifyMessageToServerOptionsInternal optionsInternal = new AddNotifyMessageToServerOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var notificationFnInternal = new OnMessageToServerCallbackInternal(OnMessageToServerCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, notificationFn, notificationFnInternal);

			var funcResult = Bindings.EOS_AntiCheatClient_AddNotifyMessageToServer(InnerHandle, ref optionsInternal, clientDataAddress, notificationFnInternal);

			Helper.Dispose(ref optionsInternal);

			Helper.AssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// Add a callback issued when an action must be applied to a connected client. The bound function will only be called
		/// between a successful call to <see cref="BeginSession" /> and the matching <see cref="EndSession" /> call in mode <see cref="AntiCheatClientMode.PeerToPeer" />.
		/// Mode: <see cref="AntiCheatClientMode.PeerToPeer" />.
		/// </summary>
		/// <param name="options">Structure containing input data</param>
		/// <param name="clientData">This value is returned to the caller when NotificationFn is invoked</param>
		/// <param name="notificationFn">The callback to be fired</param>
		/// <returns>
		/// A valid notification ID if successfully bound, or <see cref="Common.InvalidNotificationid" /> otherwise
		/// </returns>
		public ulong AddNotifyPeerActionRequired(ref AddNotifyPeerActionRequiredOptions options, object clientData, OnPeerActionRequiredCallback notificationFn)
		{
			AddNotifyPeerActionRequiredOptionsInternal optionsInternal = new AddNotifyPeerActionRequiredOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var notificationFnInternal = new OnPeerActionRequiredCallbackInternal(OnPeerActionRequiredCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, notificationFn, notificationFnInternal);

			var funcResult = Bindings.EOS_AntiCheatClient_AddNotifyPeerActionRequired(InnerHandle, ref optionsInternal, clientDataAddress, notificationFnInternal);

			Helper.Dispose(ref optionsInternal);

			Helper.AssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// Add an optional callback issued when a connected peer's authentication status changes. The bound function will only be called
		/// between a successful call to <see cref="BeginSession" /> and the matching <see cref="EndSession" /> call in mode <see cref="AntiCheatClientMode.PeerToPeer" />.
		/// Mode: <see cref="AntiCheatClientMode.PeerToPeer" />.
		/// </summary>
		/// <param name="options">Structure containing input data</param>
		/// <param name="clientData">This value is returned to the caller when NotificationFn is invoked</param>
		/// <param name="notificationFn">The callback to be fired</param>
		/// <returns>
		/// A valid notification ID if successfully bound, or <see cref="Common.InvalidNotificationid" /> otherwise
		/// </returns>
		public ulong AddNotifyPeerAuthStatusChanged(ref AddNotifyPeerAuthStatusChangedOptions options, object clientData, OnPeerAuthStatusChangedCallback notificationFn)
		{
			AddNotifyPeerAuthStatusChangedOptionsInternal optionsInternal = new AddNotifyPeerAuthStatusChangedOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var notificationFnInternal = new OnPeerAuthStatusChangedCallbackInternal(OnPeerAuthStatusChangedCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, notificationFn, notificationFnInternal);

			var funcResult = Bindings.EOS_AntiCheatClient_AddNotifyPeerAuthStatusChanged(InnerHandle, ref optionsInternal, clientDataAddress, notificationFnInternal);

			Helper.Dispose(ref optionsInternal);

			Helper.AssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// Begins a multiplayer game session. After this call returns successfully, the client is ready to exchange
		/// anti-cheat messages with a game server or peer(s). When leaving one game session and connecting to a
		/// different one, a new anti-cheat session must be created by calling <see cref="EndSession" /> and <see cref="BeginSession" /> again.
		/// Mode: All
		/// </summary>
		/// <param name="options">Structure containing input data.</param>
		/// <returns>
		/// <see cref="Result.Success" /> - If the session was started successfully
		/// <see cref="Result.InvalidParameters" /> - If input data was invalid
		/// <see cref="Result.AntiCheatInvalidMode" /> - If the current mode does not support this function
		/// </returns>
		public Result BeginSession(ref BeginSessionOptions options)
		{
			BeginSessionOptionsInternal optionsInternal = new BeginSessionOptionsInternal();
			optionsInternal.Set(ref options);

			var funcResult = Bindings.EOS_AntiCheatClient_BeginSession(InnerHandle, ref optionsInternal);

			Helper.Dispose(ref optionsInternal);

			return funcResult;
		}

		/// <summary>
		/// Ends a multiplayer game session, either by leaving an ongoing session or shutting it down entirely.
		/// Mode: All
		/// 
		/// Must be called when the multiplayer session ends, or when the local user leaves a session in progress.
		/// </summary>
		/// <param name="options">Structure containing input data.</param>
		/// <returns>
		/// <see cref="Result.Success" /> - If the session was ended normally
		/// <see cref="Result.InvalidParameters" /> - If input data was invalid
		/// <see cref="Result.AntiCheatInvalidMode" /> - If the current mode does not support this function
		/// </returns>
		public Result EndSession(ref EndSessionOptions options)
		{
			EndSessionOptionsInternal optionsInternal = new EndSessionOptionsInternal();
			optionsInternal.Set(ref options);

			var funcResult = Bindings.EOS_AntiCheatClient_EndSession(InnerHandle, ref optionsInternal);

			Helper.Dispose(ref optionsInternal);

			return funcResult;
		}

		/// <summary>
		/// Optional NetProtect feature for game message encryption.
		/// Calculates the required decrypted buffer size for a given input data length.
		/// This will not change for a given SDK version, and allows one time allocation of reusable buffers.
		/// Mode: <see cref="AntiCheatClientMode.ClientServer" />.
		/// </summary>
		/// <param name="options">Structure containing input data.</param>
		/// <param name="outBufferLengthBytes">On success, the OutBuffer length in bytes that is required to call ProtectMessage on the given input size.</param>
		/// <returns>
		/// <see cref="Result.Success" /> - If the output length was calculated successfully
		/// <see cref="Result.InvalidParameters" /> - If input data was invalid
		/// <see cref="Result.AntiCheatInvalidMode" /> - If the current mode does not support this function
		/// </returns>
		public Result GetProtectMessageOutputLength(ref GetProtectMessageOutputLengthOptions options, out uint outBufferSizeBytes)
		{
			GetProtectMessageOutputLengthOptionsInternal optionsInternal = new GetProtectMessageOutputLengthOptionsInternal();
			optionsInternal.Set(ref options);

			outBufferSizeBytes = Helper.GetDefault<uint>();

			var funcResult = Bindings.EOS_AntiCheatClient_GetProtectMessageOutputLength(InnerHandle, ref optionsInternal, ref outBufferSizeBytes);

			Helper.Dispose(ref optionsInternal);

			return funcResult;
		}

		/// <summary>
		/// Polls for changes in client integrity status.
		/// Mode: All
		/// 
		/// The purpose of this function is to allow the game to display information
		/// about anti-cheat integrity problems to the user. These are often the result of a
		/// corrupt game installation rather than cheating attempts. This function does not
		/// check for violations, it only provides information about violations which have
		/// automatically been discovered by the anti-cheat client. Such a violation may occur
		/// at any time and afterwards the user will be unable to join any protected multiplayer
		/// session until after restarting the game. Note that this function returns <see cref="Result.NotFound" />
		/// when everything is normal and there is no violation to display.
		/// 
		/// NOTE: This API is deprecated. In order to get client status updates,
		/// use AddNotifyClientIntegrityViolated to register a callback that will
		/// be called when violations are triggered.
		/// </summary>
		/// <param name="options">Structure containing input data.</param>
		/// <param name="outViolationType">On success, receives a code describing the violation that occurred.</param>
		/// <param name="outMessage">On success, receives a string describing the violation which should be displayed to the user.</param>
		/// <returns>
		/// <see cref="Result.Success" /> - If violation information was returned successfully
		/// <see cref="Result.LimitExceeded" /> - If OutMessage is too small to receive the message string. Call again with a larger OutMessage.
		/// <see cref="Result.NotFound" /> - If no violation has occurred since the last call
		/// </returns>
		public Result PollStatus(ref PollStatusOptions options, out AntiCheatClientViolationType outViolationType, out Utf8String outMessage)
		{
			PollStatusOptionsInternal optionsInternal = new PollStatusOptionsInternal();
			optionsInternal.Set(ref options);

			outViolationType = Helper.GetDefault<AntiCheatClientViolationType>();

			uint OutMessageLength = options.OutMessageLength;
			System.IntPtr outMessageAddress = Helper.AddAllocation(OutMessageLength);

			var funcResult = Bindings.EOS_AntiCheatClient_PollStatus(InnerHandle, ref optionsInternal, ref outViolationType, outMessageAddress);

			Helper.Dispose(ref optionsInternal);

			Helper.Get(outMessageAddress, out outMessage);
			Helper.Dispose(ref outMessageAddress);

			return funcResult;
		}

		/// <summary>
		/// Optional NetProtect feature for game message encryption.
		/// Encrypts an arbitrary message that will be sent to the game server and decrypted on the other side.
		/// Mode: <see cref="AntiCheatClientMode.ClientServer" />.
		/// 
		/// Options.Data and OutBuffer may refer to the same buffer to encrypt in place.
		/// </summary>
		/// <param name="options">Structure containing input data.</param>
		/// <param name="outBuffer">On success, buffer where encrypted message data will be written.</param>
		/// <param name="outBytesWritten">On success, the number of bytes that were written to OutBuffer.</param>
		/// <returns>
		/// <see cref="Result.Success" /> - If the message was protected successfully
		/// <see cref="Result.InvalidParameters" /> - If input data was invalid
		/// <see cref="Result.AntiCheatInvalidMode" /> - If the current mode does not support this function
		/// </returns>
		public Result ProtectMessage(ref ProtectMessageOptions options, System.ArraySegment<byte> outBuffer, out uint outBytesWritten)
		{
			ProtectMessageOptionsInternal optionsInternal = new ProtectMessageOptionsInternal();
			optionsInternal.Set(ref options);

			outBytesWritten = 0;
			System.IntPtr outBufferAddress = Helper.AddPinnedBuffer(outBuffer);

			var funcResult = Bindings.EOS_AntiCheatClient_ProtectMessage(InnerHandle, ref optionsInternal, outBufferAddress, ref outBytesWritten);

			Helper.Dispose(ref optionsInternal);

			Helper.Dispose(ref outBufferAddress);

			return funcResult;
		}

		/// <summary>
		/// Call when an anti-cheat message is received from a peer.
		/// Mode: <see cref="AntiCheatClientMode.PeerToPeer" />.
		/// </summary>
		/// <param name="options">Structure containing input data.</param>
		/// <returns>
		/// <see cref="Result.Success" /> - If the message was processed successfully
		/// <see cref="Result.InvalidParameters" /> - If input data was invalid
		/// <see cref="Result.AntiCheatInvalidMode" /> - If the current mode does not support this function
		/// </returns>
		public Result ReceiveMessageFromPeer(ref ReceiveMessageFromPeerOptions options)
		{
			ReceiveMessageFromPeerOptionsInternal optionsInternal = new ReceiveMessageFromPeerOptionsInternal();
			optionsInternal.Set(ref options);

			var funcResult = Bindings.EOS_AntiCheatClient_ReceiveMessageFromPeer(InnerHandle, ref optionsInternal);

			Helper.Dispose(ref optionsInternal);

			return funcResult;
		}

		/// <summary>
		/// Call when an anti-cheat message is received from the game server.
		/// Mode: <see cref="AntiCheatClientMode.ClientServer" />.
		/// </summary>
		/// <param name="options">Structure containing input data.</param>
		/// <returns>
		/// <see cref="Result.Success" /> - If the message was processed successfully
		/// <see cref="Result.InvalidParameters" /> - If input data was invalid
		/// <see cref="Result.InvalidRequest" /> - If message contents were corrupt and could not be processed
		/// <see cref="Result.AntiCheatInvalidMode" /> - If the current mode does not support this function
		/// </returns>
		public Result ReceiveMessageFromServer(ref ReceiveMessageFromServerOptions options)
		{
			ReceiveMessageFromServerOptionsInternal optionsInternal = new ReceiveMessageFromServerOptionsInternal();
			optionsInternal.Set(ref options);

			var funcResult = Bindings.EOS_AntiCheatClient_ReceiveMessageFromServer(InnerHandle, ref optionsInternal);

			Helper.Dispose(ref optionsInternal);

			return funcResult;
		}

		/// <summary>
		/// Registers a connected peer-to-peer client.
		/// Mode: <see cref="AntiCheatClientMode.PeerToPeer" />.
		/// 
		/// Must be paired with a call to <see cref="UnregisterPeer" /> if this user leaves the session
		/// in progress, or <see cref="EndSession" /> if the entire session is ending.
		/// </summary>
		/// <param name="options">Structure containing input data.</param>
		/// <returns>
		/// <see cref="Result.Success" /> - If the player was registered successfully
		/// <see cref="Result.InvalidParameters" /> - If input data was invalid
		/// <see cref="Result.AntiCheatInvalidMode" /> - If the current mode does not support this function
		/// </returns>
		public Result RegisterPeer(ref RegisterPeerOptions options)
		{
			RegisterPeerOptionsInternal optionsInternal = new RegisterPeerOptionsInternal();
			optionsInternal.Set(ref options);

			var funcResult = Bindings.EOS_AntiCheatClient_RegisterPeer(InnerHandle, ref optionsInternal);

			Helper.Dispose(ref optionsInternal);

			return funcResult;
		}

		/// <summary>
		/// Remove a previously bound <see cref="AddNotifyClientIntegrityViolated" /> handler.
		/// Mode: Any.
		/// </summary>
		/// <param name="notificationId">The previously bound notification ID</param>
		public void RemoveNotifyClientIntegrityViolated(ulong notificationId)
		{
			Bindings.EOS_AntiCheatClient_RemoveNotifyClientIntegrityViolated(InnerHandle, notificationId);

			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		/// <summary>
		/// Remove a previously bound <see cref="AddNotifyMessageToPeer" /> handler.
		/// Mode: Any.
		/// </summary>
		/// <param name="notificationId">The previously bound notification ID</param>
		public void RemoveNotifyMessageToPeer(ulong notificationId)
		{
			Bindings.EOS_AntiCheatClient_RemoveNotifyMessageToPeer(InnerHandle, notificationId);

			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		/// <summary>
		/// Remove a previously bound <see cref="AddNotifyMessageToServer" /> handler.
		/// Mode: Any.
		/// </summary>
		/// <param name="notificationId">The previously bound notification ID</param>
		public void RemoveNotifyMessageToServer(ulong notificationId)
		{
			Bindings.EOS_AntiCheatClient_RemoveNotifyMessageToServer(InnerHandle, notificationId);

			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		/// <summary>
		/// Remove a previously bound <see cref="AddNotifyPeerActionRequired" /> handler.
		/// Mode: Any.
		/// </summary>
		/// <param name="notificationId">The previously bound notification ID</param>
		public void RemoveNotifyPeerActionRequired(ulong notificationId)
		{
			Bindings.EOS_AntiCheatClient_RemoveNotifyPeerActionRequired(InnerHandle, notificationId);

			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		/// <summary>
		/// Remove a previously bound <see cref="AddNotifyPeerAuthStatusChanged" /> handler.
		/// Mode: Any.
		/// </summary>
		/// <param name="notificationId">The previously bound notification ID</param>
		public void RemoveNotifyPeerAuthStatusChanged(ulong notificationId)
		{
			Bindings.EOS_AntiCheatClient_RemoveNotifyPeerAuthStatusChanged(InnerHandle, notificationId);

			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		/// <summary>
		/// Optional NetProtect feature for game message encryption.
		/// Decrypts an encrypted message received from the game server.
		/// Mode: <see cref="AntiCheatClientMode.ClientServer" />.
		/// 
		/// Options.Data and OutBuffer may refer to the same buffer to decrypt in place.
		/// </summary>
		/// <param name="options">Structure containing input data.</param>
		/// <param name="outBuffer">On success, buffer where encrypted message data will be written.</param>
		/// <param name="outBytesWritten">On success, the number of bytes that were written to OutBuffer.</param>
		/// <returns>
		/// <see cref="Result.Success" /> - If the message was unprotected successfully
		/// <see cref="Result.InvalidParameters" /> - If input data was invalid
		/// <see cref="Result.AntiCheatInvalidMode" /> - If the current mode does not support this function
		/// </returns>
		public Result UnprotectMessage(ref UnprotectMessageOptions options, System.ArraySegment<byte> outBuffer, out uint outBytesWritten)
		{
			UnprotectMessageOptionsInternal optionsInternal = new UnprotectMessageOptionsInternal();
			optionsInternal.Set(ref options);

			outBytesWritten = 0;
			System.IntPtr outBufferAddress = Helper.AddPinnedBuffer(outBuffer);

			var funcResult = Bindings.EOS_AntiCheatClient_UnprotectMessage(InnerHandle, ref optionsInternal, outBufferAddress, ref outBytesWritten);

			Helper.Dispose(ref optionsInternal);

			Helper.Dispose(ref outBufferAddress);

			return funcResult;
		}

		/// <summary>
		/// Unregisters a disconnected peer-to-peer client.
		/// Mode: <see cref="AntiCheatClientMode.PeerToPeer" />.
		/// 
		/// Must be called when a user leaves a session in progress.
		/// </summary>
		/// <param name="options">Structure containing input data.</param>
		/// <returns>
		/// <see cref="Result.Success" /> - If the player was unregistered successfully
		/// <see cref="Result.InvalidParameters" /> - If input data was invalid
		/// <see cref="Result.AntiCheatInvalidMode" /> - If the current mode does not support this function
		/// </returns>
		public Result UnregisterPeer(ref UnregisterPeerOptions options)
		{
			UnregisterPeerOptionsInternal optionsInternal = new UnregisterPeerOptionsInternal();
			optionsInternal.Set(ref options);

			var funcResult = Bindings.EOS_AntiCheatClient_UnregisterPeer(InnerHandle, ref optionsInternal);

			Helper.Dispose(ref optionsInternal);

			return funcResult;
		}

		[MonoPInvokeCallback(typeof(OnClientIntegrityViolatedCallbackInternal))]
		internal static void OnClientIntegrityViolatedCallbackInternalImplementation(ref OnClientIntegrityViolatedCallbackInfoInternal data)
		{
			OnClientIntegrityViolatedCallback callback;
			OnClientIntegrityViolatedCallbackInfo callbackInfo;
			if (Helper.TryGetCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnMessageToPeerCallbackInternal))]
		internal static void OnMessageToPeerCallbackInternalImplementation(ref AntiCheatCommon.OnMessageToClientCallbackInfoInternal data)
		{
			OnMessageToPeerCallback callback;
			AntiCheatCommon.OnMessageToClientCallbackInfo callbackInfo;
			if (Helper.TryGetCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnMessageToServerCallbackInternal))]
		internal static void OnMessageToServerCallbackInternalImplementation(ref OnMessageToServerCallbackInfoInternal data)
		{
			OnMessageToServerCallback callback;
			OnMessageToServerCallbackInfo callbackInfo;
			if (Helper.TryGetCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnPeerActionRequiredCallbackInternal))]
		internal static void OnPeerActionRequiredCallbackInternalImplementation(ref AntiCheatCommon.OnClientActionRequiredCallbackInfoInternal data)
		{
			OnPeerActionRequiredCallback callback;
			AntiCheatCommon.OnClientActionRequiredCallbackInfo callbackInfo;
			if (Helper.TryGetCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnPeerAuthStatusChangedCallbackInternal))]
		internal static void OnPeerAuthStatusChangedCallbackInternalImplementation(ref AntiCheatCommon.OnClientAuthStatusChangedCallbackInfoInternal data)
		{
			OnPeerAuthStatusChangedCallback callback;
			AntiCheatCommon.OnClientAuthStatusChangedCallbackInfo callbackInfo;
			if (Helper.TryGetCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}
	}
}