// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.RTCData
{
	/// <summary>
	/// This struct is passed in with a call to <see cref="OnUpdateReceivingCallback" />.
	/// </summary>
	public struct UpdateReceivingCallbackInfo : ICallbackInfo
	{
		/// <summary>
		/// This returns:
		/// <see cref="Result.Success" /> if receiving of channels of remote users was successfully enabled/disabled.
		/// <see cref="Result.NotFound" /> if the participant isn't found by ParticipantId.
		/// <see cref="Result.UnexpectedError" /> otherwise.
		/// </summary>
		public Result ResultCode { get; set; }

		/// <summary>
		/// Client-specified data passed into <see cref="RTCDataInterface.UpdateReceiving" />.
		/// </summary>
		public object ClientData { get; set; }

		/// <summary>
		/// The Product User ID of the user who initiated this request.
		/// </summary>
		public ProductUserId LocalUserId { get; set; }

		/// <summary>
		/// The room this settings should be applied on.
		/// </summary>
		public Utf8String RoomName { get; set; }

		/// <summary>
		/// The participant to modify or null to update the global configuration
		/// </summary>
		public ProductUserId ParticipantId { get; set; }

		/// <summary>
		/// Created or destroyed data channel
		/// </summary>
		public bool DataEnabled { get; set; }

		public Result? GetResultCode()
		{
			return ResultCode;
		}

		internal void Set(ref UpdateReceivingCallbackInfoInternal other)
		{
			ResultCode = other.ResultCode;
			ClientData = other.ClientData;
			LocalUserId = other.LocalUserId;
			RoomName = other.RoomName;
			ParticipantId = other.ParticipantId;
			DataEnabled = other.DataEnabled;
		}
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateReceivingCallbackInfoInternal : ICallbackInfoInternal, IGettable<UpdateReceivingCallbackInfo>, ISettable<UpdateReceivingCallbackInfo>, System.IDisposable
	{
		private Result m_ResultCode;
		private System.IntPtr m_ClientData;
		private System.IntPtr m_LocalUserId;
		private System.IntPtr m_RoomName;
		private System.IntPtr m_ParticipantId;
		private int m_DataEnabled;

		public Result ResultCode
		{
			get
			{
				return m_ResultCode;
			}

			set
			{
				m_ResultCode = value;
			}
		}

		public object ClientData
		{
			get
			{
				object value;
				Helper.Get(m_ClientData, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_ClientData);
			}
		}

		public System.IntPtr ClientDataAddress
		{
			get
			{
				return m_ClientData;
			}
		}

		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId value;
				Helper.Get(m_LocalUserId, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_LocalUserId);
			}
		}

		public Utf8String RoomName
		{
			get
			{
				Utf8String value;
				Helper.Get(m_RoomName, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_RoomName);
			}
		}

		public ProductUserId ParticipantId
		{
			get
			{
				ProductUserId value;
				Helper.Get(m_ParticipantId, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_ParticipantId);
			}
		}

		public bool DataEnabled
		{
			get
			{
				bool value;
				Helper.Get(m_DataEnabled, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_DataEnabled);
			}
		}

		public void Set(ref UpdateReceivingCallbackInfo other)
		{
			ResultCode = other.ResultCode;
			ClientData = other.ClientData;
			LocalUserId = other.LocalUserId;
			RoomName = other.RoomName;
			ParticipantId = other.ParticipantId;
			DataEnabled = other.DataEnabled;
		}

		public void Set(ref UpdateReceivingCallbackInfo? other)
		{
			if (other.HasValue)
			{
				ResultCode = other.Value.ResultCode;
				ClientData = other.Value.ClientData;
				LocalUserId = other.Value.LocalUserId;
				RoomName = other.Value.RoomName;
				ParticipantId = other.Value.ParticipantId;
				DataEnabled = other.Value.DataEnabled;
			}
		}

		public void Dispose()
		{
			Helper.Dispose(ref m_ClientData);
			Helper.Dispose(ref m_LocalUserId);
			Helper.Dispose(ref m_RoomName);
			Helper.Dispose(ref m_ParticipantId);
		}

		public void Get(out UpdateReceivingCallbackInfo output)
		{
			output = new UpdateReceivingCallbackInfo();
			output.Set(ref this);
		}
	}
}