using projeto_mobile_adm_app.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Requests
{
    public class ClienteStatusRequest
    {
        public int ClienteId { get; set; }
        public StatusEnum Status { get; set; }
    }
}
