﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Request
{
    public class AlexaSystem
    {
        [JsonProperty("application")]
        public Application Application { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("device")]
        public Device Device { get; set; }
    }
}
