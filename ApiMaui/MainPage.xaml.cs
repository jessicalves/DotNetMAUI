﻿using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace ApiMaui;

public partial class MainPage : ContentPage
{
    RestService _restService;

    public MainPage()
    {
        InitializeComponent();
        _restService = new RestService();

        ICommand refreshCommand = new Command(() =>
        {
            refreshView.IsRefreshing = false;
        });
        refreshView.Command = refreshCommand;
    }

    async protected override void OnAppearing()
    {
        base.OnAppearing();

        List<Users> users = await _restService.GetUsersAsync(Constants.UsersEndpoint);
        users.Sort(new UserNameComparer());
        collectionView.ItemsSource = users;
    }

    async void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        Grid grid = sender as Grid;
        if (grid != null)
        {
            Label thirdChild = grid.Children[2] as Label;
            int UserID = Convert.ToInt32(thirdChild.Text.ToString());
            await Navigation.PushAsync(new UserPosts(UserID));
        }
    }
}


