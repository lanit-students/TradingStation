using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    /// <summary>
    /// Required for create any bank broker
    /// </summary>
    public class CreateBrokerData
    {
        /// <summary>
        /// Authorization token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Depth by market glass
        /// </summary>
        public int Depth { get; set; }
    }
}
