using System;
using IbaMonitoring;

public partial class Maintenance : IbaPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.DisableLinks();
    }
}
