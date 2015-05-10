using System;
using System.Collections.Generic;
using System.Linq;
using Soomla;
using Soomla.Profile;
using UnityEngine;
using UnityEngine.UI;

public class ProfileController : BaseController
{
    #region Private Variables
    private Reward loginReward;
    private Reward coinReward;
    private Reward mysteryReward;
    #endregion

    #region Editor Properties
    [SerializeField]
    protected InputField textInput;
    [SerializeField]
    protected ToggleGroup platformPicker;
    [SerializeField]
    protected Button[] buttons;
    #endregion

    #region Override Properties
    protected override bool IsStatusOK
    {
        get { return SoomlaProfile.IsLoggedIn(SelectedProvider); }
    }

    protected override string StatusName
    {
        get { return "IsLoggedIn"; }
    }
    #endregion

    #region Private Properties
    private string SelectedProviderName
    {
        get { return platformPicker.ActiveToggles().Single().name.ToLower(); }
    }

    private Provider SelectedProvider
    {
        get { return Provider.fromString(SelectedProviderName); }
    }

    private string SocialPageName
    {
        get
        {
            switch (SelectedProviderName)
            {
                case "facebook": return "The.SOOMLA.Project";
                case "twitter": return "Soomla";
                case "google": return "SoomLa";
            }

            return null;
        }
    }

    private bool IsAbleToPost
    {
        get
        {
            if (string.IsNullOrEmpty(textInput.text))
            {
                PopUpMessage.Show("Missing Text", "Please enter some text and try again");

                return false;
            }

            return true;
        }
    }
    #endregion

    #region Override Functions
    protected override void Init()
    {
        CreateRewards();

        SoomlaProfile.Initialize();
    }

    protected override void RegisterEvents()
    {
        #region Login / Logout Events
        ProfileEvents.OnLoginStarted += (provider, userPayload) => MenuView.SetLoadingOverlayVisiblity(true);
        ProfileEvents.OnLoginCancelled += (provider, userPayload) => MenuView.SetLoadingOverlayVisiblity(false);
        ProfileEvents.OnLoginFailed += (provider, message, userPayload) =>
        {
            MenuView.SetLoadingOverlayVisiblity(false);

            PopUpMessage.Show(provider + " - Login Failed!", message);
        };

        ProfileEvents.OnLoginFinished += (profile, userPayload) =>
        {
            MenuView.SetLoadingOverlayVisiblity(false);

            PopUpMessage.Show(SelectedProviderName, string.Format("Logged in as '{0} {1}'", profile.FirstName, profile.LastName));

            UpdateControlsState();
        };

        ProfileEvents.OnLogoutStarted += provider => MenuView.SetLoadingOverlayVisiblity(true);
        ProfileEvents.OnLogoutFailed += (provider, message) =>
        {
            MenuView.SetLoadingOverlayVisiblity(false);

            PopUpMessage.Show(provider + " - Logout Failed!", message);
        };

        ProfileEvents.OnLogoutFinished += provider =>
        {
            MenuView.SetLoadingOverlayVisiblity(false);

            PopUpMessage.Show(SelectedProviderName, "Logged out!");

            UpdateControlsState();
        };
        #endregion

        #region Data Events
        ProfileEvents.OnGetContactsFailed += (provider, pageNumber, message, userPayload) =>
        {
            MenuView.SetLoadingOverlayVisiblity(false);

            PopUpMessage.Show(provider + " - Failed to get contacts!", message);
        };

        ProfileEvents.OnGetContactsStarted += (provider, pageNumber, message) => MenuView.SetLoadingOverlayVisiblity(true);
        ProfileEvents.OnGetContactsFinished += (provider, contactsData, userPayload) =>
        {
            MenuView.SetLoadingOverlayVisiblity(false);

            var firstContact = contactsData.PageData.FirstOrDefault();

            PopUpMessage.Show(string.Format("{0} - Got {1} Contacts", provider, contactsData.PageData.Count),
                                                          firstContact != null
                                                          ? "The first contact is: " + firstContact.FirstName + " " + firstContact.LastName
                                                          : "...but the contact list was empty :(");

        };
        #endregion

        #region Social Events
        ProfileEvents.OnSocialActionFailed += (provider, type, message, userPayload) => MenuView.SetLoadingOverlayVisiblity(false);
        ProfileEvents.OnSocialActionStarted += (provider, type, userPayload) => MenuView.SetLoadingOverlayVisiblity(true);
        ProfileEvents.OnSocialActionFinished += (provider, type, userPayload) => MenuView.SetLoadingOverlayVisiblity(false);
        ProfileEvents.OnSocialActionCancelled += (provider, type, userPayload) => MenuView.SetLoadingOverlayVisiblity(false);
        #endregion
    }

    protected override void UpdateControlsState()
    {
        foreach (var button in buttons)
        {
            UpdateButtonState(button);
        }

        UpdateButtonState(textInput);
    }
    #endregion

    #region Private Functions
    protected void UpdateButtonState(Selectable control)
    {
        control.interactable = "login".Equals(control.name, StringComparison.OrdinalIgnoreCase) ? !IsStatusOK : IsStatusOK ||
                                "back".Equals(control.name, StringComparison.OrdinalIgnoreCase);
    }

    private void CreateRewards()
    {
        var socialReward1 = new BadgeReward(
            "social_reward_1", // ID
            "Social 1" // Name
            );

        var socialReward2 = new BadgeReward(
            "social_reward_2", // ID
            "Social 2" // Name
            );

        var socialReward3 = new BadgeReward(
            "social_reward_3", // ID
            "Social 3" // Name
            );

        loginReward = new SequenceReward(
            "social_reward", // ID
            "Social Reward", // Name
            new List<Reward>
            {
                // Rewards in sequence
                socialReward1,
                socialReward2,
                socialReward3
            }
            );

        coinReward = new VirtualItemReward(
            "coin_reward", // ID
            "Coins!", // Name
            ExampleAssets.Coin.ItemId, // Associated item ID
            7 // Amount
            )
        { Schedule = Schedule.AnyTimeUnLimited() };


        var jackpotReward = new VirtualItemReward(
            "jackpot_reward",
            "JACKPOT!!!!!!!",
            ExampleAssets.Coin.ItemId,
            77
            )
        { Schedule = Schedule.AnyTimeUnLimited() };

        mysteryReward = new RandomReward(
            "mystery_reward", // ID
            "Mystery Box Reward", // Name
            new List<Reward>
            {
                // Rewards to choose from
                coinReward,
                coinReward,
                coinReward,
                coinReward,
                coinReward,
                coinReward,
                jackpotReward
            }
            )
        { Schedule = Schedule.AnyTimeUnLimited() };
    }
    #endregion

    #region Profile Functions
    public void Login()
    {
        SoomlaProfile.Login(SelectedProvider, reward: loginReward);
    }

    public void Logout()
    {
        SoomlaProfile.Logout(SelectedProvider);
    }

    public void UpdateStatus()
    {
        if (IsAbleToPost)
        {
            SoomlaProfile.UpdateStatus(SelectedProvider, textInput.text, reward: coinReward);
        }
    }

    public void UpdateStrory()
    {
        if (IsAbleToPost)
        {
            SoomlaProfile.UpdateStory(SelectedProvider,
                                      textInput.text,
                                      "Story Title",
                                      "A Caption",
                                      "A Description",
                                      "http://spartonix.com",
                                      "http://www.spartonix.com/wp-content/uploads/2014/11/SpartonixLogo1.png",
                                      reward: coinReward);
        }
    }

    public void GetContacts()
    {
        SoomlaProfile.GetContacts(SelectedProvider);
    }

    public void GetFeeds()
    {
        // Not available at the moment of writing this
        //SoomlaProfile.GetFeeds();

        PopUpMessage.Show("GetFeeds", "Coming soon...");
    }

    public void Like()
    {
        SoomlaProfile.Like(SelectedProvider, SocialPageName, mysteryReward);
    }

    public void UploadCurrentScreenShot()
    {
        SoomlaProfile.UploadCurrentScreenShot(this, SelectedProvider, "ScreenShot", textInput.text, reward: mysteryReward);
    }

    public void OpenAppRatingPage()
    {
        SoomlaProfile.OpenAppRatingPage();
    }

    public void Invite()
    {
        if (IsAbleToPost)
        {
            SoomlaProfile.Invite(SelectedProvider, textInput.text, "Invite", reward: coinReward);
        }
    }
    #endregion
}