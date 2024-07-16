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

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Epic.OnlineServices.Sessions;

namespace PlayEveryWare.EpicOnlineServices.Samples
{
    public class UISessionEntry : MonoBehaviour
    {
        public Text NameTxt;
        public Text StatusTxt;
        public Text PlayersTxt;
        public Text LevelTxt;
        public Text JIPTxt;
        public Text PermissionTxt;
        public Text InvitesTxt;

        public Button JoinButton;
        [HideInInspector]
        public SessionDetails JoinSessionDetails = null;

        public Session RepresentedSession { get; set; }

        public Button StartButton;
        public Button EndButton;
        public Button ModifyButton;
        public Button LeaveButton;

        // Callbacks
        public Action<string> StartOnClick;
        public Action<string> EndOnClick;
        public Action<string> ModifyOnClick;
        public Action<string> LeaveOnClick;

        public Action<SessionDetails> JoinOnClick;

        public void OnlyEnableSearchResultButtons()
        {
            NameTxt.transform.gameObject.SetActive(false);

            JoinButton.transform.gameObject.SetActive(true);
            StartButton.interactable = true;

            StartButton.interactable = false;
            EndButton.interactable = false;
            ModifyButton.interactable = false;
            LeaveButton.interactable = false;
        }

        /// <summary>
        /// Sets the UI buttons interactivity based on the parameters provided.
        /// </summary>
        /// <param name="updating">
        /// Provide "true" if the Session represented by this object is mid-update.
        /// When it's done, call this function again providing "false".
        /// This helps with trying to perform operations before a Session is finished being created or joined.
        /// </param>
        /// <param name="state">
        /// Status indicating the state of this Session.
        /// <see cref="OnlineSessionState.NoSession"/> indicates that there is no local Session for this object yet,
        /// suggesting that it hasn't been joined.
        /// Otherwise indicates things such as <see cref="OnlineSessionState.InProgress"/> for setting contextual buttons.
        /// </param>
        /// <param name="isOwner">
        /// Is this local user the owner of the represented Session?
        /// If false, state-affecting buttons are disabled.
        /// </param>
        public void EnableButtonsBySessionState(bool updating, OnlineSessionState state, bool isOwner)
        {
            // If we aren't in this Session, then only show the buttons for joining
            // If we are in this Session, enable those buttons
            if (state == OnlineSessionState.NoSession)
            {
                OnlyEnableSearchResultButtons();
            }
            else
            {
                NameTxt.transform.gameObject.SetActive(true);

                if (isOwner)
                {
                    StartButton.interactable = !updating && (state == OnlineSessionState.Pending || state == OnlineSessionState.Ended);
                    EndButton.interactable = !updating && state == OnlineSessionState.InProgress;
                    ModifyButton.interactable = !updating;
                }
                else
                {
                    StartButton.interactable = false;
                    EndButton.interactable = false;
                    ModifyButton.interactable = false;
                }

                JoinButton.transform.gameObject.SetActive(false);
                LeaveButton.interactable = true;
            }
        }

        public void JoinOnClickHandler()
        {
            if (JoinOnClick != null)
            {
                JoinOnClick(JoinSessionDetails);
            }
            else
            {
                Debug.LogError("UISessionEntry: JoinOnClick is not defined!");
            }
        }

        public void StartOnClickHandler()
        {
            if (StartOnClick != null)
            {
                //StartButton.interactable = false;
                //EndButton.interactable = true;
                StartOnClick(NameTxt.text);
            }
            else
            {
                Debug.LogError("UISessionEntry: StartOnClick is not defined!");
            }
        }

        public void EndOnClickHandler()
        {
            if (EndOnClick != null)
            {
                EndOnClick(NameTxt.text);
                //StartButton.interactable = true;
                //EndButton.interactable = false;
            }
            else
            {
                Debug.LogError("UISessionEntry: EndOnClick is not defined!");
            }
        }

        public void ModOnClickHandler()
        {
            if (ModifyOnClick != null)
            {
                ModifyOnClick(NameTxt.text);
            }
            else
            {
                Debug.LogError("UISessionEntry: ModOnClick is not defined!");
            }
        }

        public void LeaveOnClickHandler()
        {
            if (LeaveOnClick != null)
            {
                LeaveOnClick(NameTxt.text);
            }
            else
            {
                Debug.LogError("UISessionEntry: LeaveOnClick is not defined!");
            }
        }

        /// <summary>
        /// Updates all of the labels and the pressable buttons for this UI element.
        /// This version takes in only a <see cref="Session"/>.
        /// It is meant to be called when you don't need <see cref="JoinOnClick"/> to be set,
        /// perhaps because the user has already joined the Session this represents.
        /// Call <see cref="SetUIElementsFromSessionAndDetails(Session, SessionDetails, UISessionsMatchmakingMenu)"/> to set the <see cref="JoinOnClick"/> action.
        /// </summary>
        /// <param name="session">The local Session that this should represent, from the Epic Online Services C# SDK.</param>
        /// <param name="ui">The UI that contains click callback handlers.</param>
        public void SetUIElementsFromSession(Session session, UISessionsMatchmakingMenu ui)
        {
            RepresentedSession = session;

            NameTxt.text = session.Name;

            if (session.UpdateInProgress)
            {
                StatusTxt.text = "*Updating*";
            }
            else
            {
                StatusTxt.text = session.SessionState.ToString();
            }

            EnableButtonsBySessionState(session.UpdateInProgress, session.SessionState, session.IsLocalUserOwnerOfSession);

            PlayersTxt.text = string.Format("{0}/{1}", session.NumConnections, session.MaxPlayers);
            JIPTxt.text = session.AllowJoinInProgress.ToString();
            PermissionTxt.text = session.PermissionLevel.ToString();
            InvitesTxt.text = session.InvitesAllowed.ToString();

            StartOnClick = ui.StartButtonOnClick;
            EndOnClick = ui.EndButtonOnClick;
            ModifyOnClick = ui.ModifyButtonOnClick;
            LeaveOnClick = ui.LeaveButtonOnClick;

            bool levelAttributeFound = false;
            foreach (SessionAttribute sessionAttr in session.Attributes)
            {
                if (sessionAttr.Key.Equals("Level", StringComparison.OrdinalIgnoreCase))
                {
                    LevelTxt.text = sessionAttr.AsString;
                    levelAttributeFound = true;
                    break;
                }
            }

            if (!levelAttributeFound)
            {
                Debug.LogErrorFormat($"UISessionEntry (SetUIElementsFromSession): Attribute 'Level' not found for session '{session.Name}'.");
                LevelTxt.text = "-NA-";
            }
        }

        /// <summary>
        /// Updates all of the labels and the pressable buttons for this UI element.
        /// This calls <see cref="SetUIElementsFromSession(Session, UISessionsMatchmakingMenu)"/>, and additionally sets <see cref="JoinOnClick"/> and <see cref="JoinSessionDetails"/>.
        /// </summary>
        /// <param name="session">The local Session that this should represent, from the Epic Online Services C# SDK.</param>
        /// <param name="details">Additional Session information from the Epic Online Services C SDK. Used to provide additional information needed to join a Session.</param>
        /// <param name="ui">The UI that contains click callback handlers.</param>
        public void SetUIElementsFromSessionAndDetails(Session session, SessionDetails details, UISessionsMatchmakingMenu ui)
        {
            SetUIElementsFromSession(session, ui);
            JoinOnClick = ui.JoinButtonOnClick;
            JoinSessionDetails = details;
        }
    }
}