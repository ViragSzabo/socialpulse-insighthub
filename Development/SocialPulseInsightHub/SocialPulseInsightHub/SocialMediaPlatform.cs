using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPulseInsightHub
{
    public class SocialMediaPlatform
    {
        public string PlatformName { get; set; }
        public string ApiEndPoints { get; set; }

        public SocialMediaPlatform(string name, string api)
        {
            PlatformName = name;
            ApiEndPoints = api;
        }
    }
}
