namespace ForeignExchangeRate.Service.Options
{
    public abstract class OptionIsEnabled : OptionBase
    {
        public virtual bool IsEnabled { get; set; }
    }
}
