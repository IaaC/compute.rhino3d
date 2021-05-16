﻿using System;
using System.Windows.Forms;

namespace Hops
{
    public partial class HopsAppSettingsUserControl : UserControl
    {
        public HopsAppSettingsUserControl()
        {
            InitializeComponent();
            _serversTextBox.Lines = HopsAppSettings.Servers;
            _serversTextBox.TextChanged += ServersTextboxChanged;
            _hideWorkerWindows.Checked = HopsAppSettings.HideWorkerWindows;
            _hideWorkerWindows.CheckedChanged += (s, e) =>
            {
                HopsAppSettings.HideWorkerWindows = _hideWorkerWindows.Checked;
            };
            _launchWorkerAtStart.Checked = HopsAppSettings.LaunchWorkerAtStart;
            _launchWorkerAtStart.CheckedChanged += (s, e) =>
            {
                HopsAppSettings.LaunchWorkerAtStart = _launchWorkerAtStart.Checked;
            };
            _childComputeCount.Value = HopsAppSettings.LocalWorkerCount;
            _childComputeCount.ValueChanged += (s, e) =>
            {
                HopsAppSettings.LocalWorkerCount = (int)_childComputeCount.Value;
            };
            _updateChildCountButton.Click += (s, e) =>
            {
                int numberToLaunch = HopsAppSettings.LocalWorkerCount - Servers.ActiveLocalComputeCount;
                Servers.LaunchChildComputeGeometry(numberToLaunch);
            };
            toolTip1.SetToolTip(_updateChildCountButton, "Click to force Rhino.Compute to update");
            _lblCacheCount.Text = $"({Hops.MemoryCache.EntryCount} items in cache)";
            _btnClearMemCache.Click += (s, e) =>
            {
                Hops.MemoryCache.ClearCache();
                _lblCacheCount.Text = $"({Hops.MemoryCache.EntryCount} items in cache)";
            };
        }

        private void ServersTextboxChanged(object sender, EventArgs e)
        {
            string[] lines = _serversTextBox.Lines;
            HopsAppSettings.Servers = lines;
        }
    }
}
