using System;
using System.IO;
using System.Text;
using IbaMonitoring;
using Ionic.Zip;
using safnet.iba.Business.AppFacades;

public partial class CreateBMDE : IbaPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fileName = "ibamonitoringorg.xml";
        string serverPath = Server.MapPath(Path.Combine("BMDE", fileName));
        using (StreamWriter writer = new StreamWriter(serverPath, false, Encoding.Unicode))
        {
            writer.Write(ResultsFacade.GetBMDE());
        }

        string zipPath = serverPath + ".zip";

        File.Delete(zipPath);

        using (ZipFile zip = new ZipFile(zipPath))
        {
            zip.AddFile(serverPath, ".\\");
            zip.Save();
        }

        File.Delete(serverPath);
    }
}
