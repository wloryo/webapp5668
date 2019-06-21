using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StackExchange.Redis;

namespace webapp5668.Pages
{
    public class redis : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            ConfigurationOptions conopt = new ConfigurationOptions();
            ConnectionMultiplexer mycm = ConnectionMultiplexer.Connect("myjapanredis.redis.cache.windows.net:6380,password=lX4L1f0iaCxc47P6AaV1OG8tDqCwXWTORX47bk9QLOg=,ssl=True,abortConnect=False");
            IDatabase mycache = mycm.GetDatabase();
            if (mycache.StringGet("myindex").IsNullOrEmpty)
            {
                mycache.StringSet("myindex", "1",new TimeSpan(0,1,0));
            }
            else
            {
                string strindex = mycache.StringGet("myindex");
                int index = int.Parse(strindex);
                mycache.StringSet("myindex", index++.ToString());
            }
            Message = "key name: myindex value: ";
            Message += "\n";
            Message += "value: " + mycache.StringGet("myindex");
            Message += "\n";
            Message += "TTL: " + mycache.KeyTimeToLive("myindex");
        }
    }
}
