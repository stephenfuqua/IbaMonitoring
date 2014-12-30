using System.Web;

namespace IbaMonitoring.ServiceTests
{
   public class ResponseStub : HttpResponseBase
    {

       public string RedirectedTo { get; set; }
       public bool ResponseWasEnded { get; set; }

       public override void Redirect(string url)
       {
           RedirectedTo = url;
           ResponseWasEnded = false;
       }

       public override void Redirect(string url, bool endResponse)
       {
           RedirectedTo = url;
           ResponseWasEnded = endResponse;
           
       }
    }
}
