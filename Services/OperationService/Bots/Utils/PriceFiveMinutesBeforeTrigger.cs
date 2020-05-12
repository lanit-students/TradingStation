namespace OperationService.Bots.Utils
{
    public class PriceFiveMinutesBeforeTrigger : Trigger
    {
        public override bool Check(string figi)
        {
            return true;
        }
    }
}
