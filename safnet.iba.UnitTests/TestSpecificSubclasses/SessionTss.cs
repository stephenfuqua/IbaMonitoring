using System.Collections.Generic;
using System.Web;

namespace safnet.iba.UnitTests.TestSpecificSubclasses
{
    public class SessionTss : HttpSessionStateBase
    {
        public Dictionary<string, object> KeyValues = new Dictionary<string, object>();


        public override object this[string name]
        {
            get { return KeyValues[name]; }
            set { KeyValues[name] = value; }
        }
    }
}
