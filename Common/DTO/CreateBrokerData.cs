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
        public string Token { get; set; } = "t.-lW-yXwRofmPWDrSqm9XpBO2K4WbTmHPkKPgRskRDlMqLCxzbP8efGmM3l6U1HU9ReMg5R0t3QqO86twfG3-vw";

        /// <summary>
        /// Depth by market glass
        /// </summary>
        public int Depth { get; set; } = 5;
    }
}
