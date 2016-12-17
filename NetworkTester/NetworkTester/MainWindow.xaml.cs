﻿using System.Windows.Documents;

namespace NetworkTester
{
    public partial class MainWindow
    {
        private PingSender pingSender;
        private bool isSendingPings;

        public MainWindow()
        {
            isSendingPings = false;
            pingSender = new PingSender();

            InitializeComponent();

            pingSender.AllPingsReceived += OnAllPingsReceived;
        }

        private void btn_togglePings_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            isSendingPings = !isSendingPings;

            if (isSendingPings)
            {
                pingSender.Start();
            }
            else
            {
                pingSender.Stop();
            }

            btn_togglePings.Content = isSendingPings ? "Stop Pings" : "Start Pings";
        }

        private void btn_addIp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ip = tb_ip.Text;
            var success = pingSender.AddAddress(ip);

            tb_ip.Text = success ? "" : "INVALID IP ADDRESS";

            dg_ipList.DataContext = typeof(List);
            dg_ipList.DataContext = pingSender.GetAddressList();
        }

        private void btn_showAddFlyout_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            flo_addIp.IsOpen = true;
        }

        public void OnAllPingsReceived(object source, PingEventArgs e)
        {
            // TODO: Figure out how to do this without calling Dispatcher.Invoke
            Dispatcher.Invoke(() =>
            {
                dg_pingResponces.DataContext = typeof(PingEventArgs);
                dg_pingResponces.DataContext = e.PingResults;
            });
        }

    }
}