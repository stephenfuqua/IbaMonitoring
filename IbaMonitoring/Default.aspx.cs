using System;
using IbaMonitoring;

public partial class _Default : IbaPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.SetHomeActive();
    }
}
