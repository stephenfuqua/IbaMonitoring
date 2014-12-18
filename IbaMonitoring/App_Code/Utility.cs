using System;
using System.ComponentModel;
using safnet.iba.Business.Components;

namespace IbaMonitoring
{

    /// <summary>
    /// Summary description for Utility
    /// </summary>
    public static class Utility
    {
        public static void LogSiteError(Exception ex)
        {
            Logger.Error(ex);
        }


        public static void SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            String token = (string) e.UserState;
            if (e.Cancelled)
            {
                LogSiteError(new Exception(token + " Send cancelled"));
            }
            if (e.Error != null)
            {
                LogSiteError(new Exception(token + " " + e.Error.ToString()));
            }
        }
    }
}