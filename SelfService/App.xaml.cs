﻿using SelfService.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfService {
    public partial class App : Application {
        public static String DbName;
        public static String DbPath;

        public App() {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage(true));
        }

        public App(string dbPath, string dbName) {
            InitializeComponent();
            App.DbName = dbName;
            App.DbPath = dbPath;
            MainPage = new NavigationPage(new LoginPage(true));
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
