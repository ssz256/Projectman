using ProjectmanApi.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectmanApi
{
    public class ApiPing : ApiReturnedObject
    {
        public int PingId { get; private set; }

        public ApiPing(string token) : base(token)
        { 
            
            Random r = new Random();
            PingId = r.Next(0, 100);
        }
    }
}
