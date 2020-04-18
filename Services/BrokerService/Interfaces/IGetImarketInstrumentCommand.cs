﻿using System.Collections.Generic;
using Interfaces;

namespace BrokerService.Interfaces
{
    public interface IGetImarketInstrumentCommand
    {
        List<IMarketInstrument> Execute(CommandsType commandType, IBroker broker);
        IMarketInstrument Execute(CommandsType commandsType, IBroker broker, string Id);
    }
}
