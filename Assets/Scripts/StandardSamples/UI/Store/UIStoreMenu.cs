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

namespace PlayEveryWare.EpicOnlineServices.Samples
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using Epic.OnlineServices.Ecom;

    public class UIStoreMenu : SampleMenu
    {
        [Header("Store UI")]
        public Button queryOffersButton;

        public Text catalogueItem0;
        public Button checkOutButton0;

        public Text catalogueItem1;
        public Button checkOutButton1;

        private EOSStoreManager StoreManager;

        protected override void InternalAwake()
        {
            StoreManager = EOSManager.Instance.GetOrCreateManager<EOSStoreManager>();
        }

        private void OnDestroy()
        {
            EOSManager.Instance.RemoveManager<EOSStoreManager>();
        }

        protected override void InternalUpdate()
        {
            if (StoreManager.GetCatalogOffers(out List<CatalogOffer> CatalogOffers))
            {
                // Generate UI for offers
                // Hard-code for demo
                if (CatalogOffers.Count > 0)
                {
                    catalogueItem0.text = string.Format("{0}, ${1}", CatalogOffers[0].TitleText, StoreManager.GetCurrentPriceAsString(CatalogOffers[0]));
                }

                if (CatalogOffers.Count > 1)
                {
                    catalogueItem1.text = string.Format("{0}, ${1}", CatalogOffers[1].TitleText, StoreManager.GetCurrentPriceAsString(CatalogOffers[1]));
                }
            }
        }

        // E-Commerce
        public void OnQueryOffersClick()
        {
            print("OnQueryOffersClick: IsValid=" + EOSManager.Instance.GetLocalUserId().IsValid() + ", accountId" + EOSManager.Instance.GetLocalUserId().ToString());

            StoreManager.QueryOffers();
        }

        public void CheckOutButton(int index)
        {
            StoreManager.CheckOutOverlay(index);
        }

        public override void Show()
        {
            EOSManager.Instance.GetOrCreateManager<EOSStoreManager>().OnLoggedIn();

            queryOffersButton.gameObject.SetActive(true);

            catalogueItem0.gameObject.SetActive(true);
            checkOutButton0.gameObject.SetActive(true);

            catalogueItem1.gameObject.SetActive(true);
            checkOutButton1.gameObject.SetActive(true);

            // Controller
            EventSystem.current.SetSelectedGameObject(UIFirstSelected);
        }

        public override void Hide()
        {
            StoreManager?.OnLoggedOut();

            queryOffersButton.gameObject.SetActive(false);
            catalogueItem0.gameObject.SetActive(false);
            checkOutButton0.gameObject.SetActive(false);

            catalogueItem1.gameObject.SetActive(false);
            checkOutButton1.gameObject.SetActive(false);
        }
    }
}