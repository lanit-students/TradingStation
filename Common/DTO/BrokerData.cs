namespace DTO
{
    /// <summary>
    /// Required for create any bank broker
    /// </summary>
    public class BrokerData
    {
        /// <summary>
        /// Authorization token
        /// </summary>
        public string Token { get; set; } = "";

        /// <summary>
        /// Depth by market glass
        /// </summary>
        public int Depth { get; set; } = 5;
    }
}
