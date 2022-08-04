﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Field.General;
using Field.Models;
using Field.Textures;
using SharpDX.Toolkit.Graphics;

namespace Charm;

public partial class MainMenuView : UserControl
{
    private static MainWindow _mainWindow = null;
        
    public MainMenuView()
    {
        InitializeComponent();
    }
        
    private void OnControlLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
        _mainWindow = Window.GetWindow(this) as MainWindow;
    }
    
    private void ApiViewButton_OnClick(object sender, RoutedEventArgs e)
    {
        TagListViewerView apiView = new TagListViewerView();
        apiView.LoadContent(ETagListType.ApiList);
        _mainWindow.MakeNewTab("api", apiView);
        _mainWindow.SetNewestTabSelected();
    }
    
    private void NamedEntitiesBagsViewButton_OnClick(object sender, RoutedEventArgs e)
    {
        TagListViewerView tagListView = new TagListViewerView();
        tagListView.LoadContent(ETagListType.DestinationGlobalTagBagList);
        _mainWindow.MakeNewTab("destination global tag bag", tagListView);
        _mainWindow.SetNewestTabSelected();
    }
    
    private void AllEntitiesViewButton_OnClick(object sender, RoutedEventArgs e)
    {
        TagListViewerView tagListView = new TagListViewerView();
        tagListView.LoadContent(ETagListType.EntityList);
        _mainWindow.MakeNewTab("dynamics", tagListView);
        _mainWindow.SetNewestTabSelected();
    }

    private void ActivitiesViewButton_OnClick(object sender, RoutedEventArgs e)
    {
        TagListViewerView tagListView = new TagListViewerView();
        tagListView.LoadContent(ETagListType.ActivityList);
        _mainWindow.MakeNewTab("activities", tagListView);
        _mainWindow.SetNewestTabSelected();
    }

    private void AllStaticsViewButton_OnClick(object sender, RoutedEventArgs e)
    {
        TagListViewerView tagListView = new TagListViewerView();
        tagListView.LoadContent(ETagListType.StaticsList);
        _mainWindow.MakeNewTab("statics", tagListView);
        _mainWindow.SetNewestTabSelected();    
    }
    
    private void WeaponAudioViewButton_Click(object sender, RoutedEventArgs e)
    {
        TagListViewerView tagListView = new TagListViewerView();
        tagListView.LoadContent(ETagListType.WeaponAudioGroupList);
        _mainWindow.MakeNewTab("weapon audio", tagListView);
        _mainWindow.SetNewestTabSelected();    
    }

    private void AllAudioViewButton_OnClick(object sender, RoutedEventArgs e)
    {
        TagListViewerView tagListView = new TagListViewerView();
        tagListView.LoadContent(ETagListType.SoundsPackagesList);
        _mainWindow.MakeNewTab("sounds", tagListView);
        _mainWindow.SetNewestTabSelected();    
    }

    private void AllStringsViewButton_OnClick(object sender, RoutedEventArgs e)
    {
        TagListViewerView tagListView = new TagListViewerView();
        tagListView.LoadContent(ETagListType.StringContainersList);
        _mainWindow.MakeNewTab("strings", tagListView);
        _mainWindow.SetNewestTabSelected();      
    }

    private void AllTexturesViewButton_OnClick(object sender, RoutedEventArgs e)
    {
        TagListViewerView tagListView = new TagListViewerView();
        tagListView.LoadContent(ETagListType.TextureList);
        _mainWindow.MakeNewTab("textures", tagListView);
        _mainWindow.SetNewestTabSelected();
    }

    private void SkeletonButton_OnClick(object sender, RoutedEventArgs e)
    {
        FbxHandler fbxHandler = new FbxHandler();
        // Init skeleton for all these meshes
        fbxHandler.SetGlobalSkeleton(new TagHash("4065a180")); // one of the ones that looks like a player skeleton
        
        // Add model
        uint sunbracers = 1862800747;
        uint contraverse = 1906093346;
        uint astrocyte = 866590993;
        uint chromatic = 3488362706;
        uint transversive = 138282166;
        uint wise_bond = 1016461220;
        var helm = astrocyte;
        var chest = chromatic;
        var arms = contraverse;
        var legs = transversive;
        var classitem = wise_bond;
        List<uint> models = new List<uint>
        {
            helm,
            chest,
            arms,
            legs,
            classitem
        };
        foreach (var model in models)
        {
            var entities = InvestmentHandler.GetEntitiesFromHash(new DestinyHash(model));
            var entity = entities[0];
            var parts = entity.Load(ELOD.MostDetail);
            fbxHandler.AddEntityToScene(entity, parts, ELOD.MostDetail);
        }

        // Add animation
        string animHash = "29e8dc80"; // 38C0A380 clap, 20FCA880 good
        Field.Animation animation = PackageHandler.GetTag(typeof(Field.Animation), new TagHash(animHash)); // idle animation  
        animation.Load();
        animation.SaveToFile($"C:/T/animation_{animHash}.json");
        fbxHandler.AddAnimationToEntity(animation, fbxHandler._globalSkeletonNodes);
        
        // Save
        fbxHandler.ExportScene($"C:/T/skeleton_{animHash}.fbx");
    }

    private void AnimationsButton_OnClick(object sender, RoutedEventArgs e)
    {
        TagListViewerView tagListView = new TagListViewerView();
        tagListView.LoadContent(ETagListType.AnimationPackageList);
        _mainWindow.MakeNewTab("animations", tagListView);
        _mainWindow.SetNewestTabSelected();
    }
}