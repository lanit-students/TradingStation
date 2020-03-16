using DTO;
using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IBroker
    {
        /// <summary>
        /// Method returns list with all curencies from bank broker
        /// </summary>
        /// <returns>List with currencies</returns>
        List<IMarketInstrument> GetCurrencies();

        /// <summary>
        /// Method returns specific currency
        /// </summary>
        /// <param name="idCurrency">Id required currency, e.g. figi</param>
        /// <returns>Required currency</returns>
        IMarketInstrument GetCurrency(string idCurrency);

        /// <summary>
        /// Method returns list with all stocks from bank broker
        /// </summary>
        /// <returns>List with stocks</returns>
        List<IMarketInstrument> GetStocks();

        /// <summary>
        /// Method returns specific stock
        /// </summary>
        /// <param name="idStock">Id required stock, e.g. figi</param>
        /// <returns>Required stock</returns>
        IMarketInstrument GetStock(string idStock);

        /// <summary>
        /// Method returns list with all bonds from bank broker
        /// </summary>
        /// <returns>List with bonds</returns>
        List<IMarketInstrument> GetBonds();

        /// <summary>
        /// Method returns specific bond
        /// </summary>
        /// <param name="idBond">Id required bond, e.g. figi</param>
        /// <returns>Required bond</returns>
        IMarketInstrument GetBond(string idBond);

        /*
         * After create broker, if you want receive responce to request
         * with another depth, you must change depth here and make
         * a request again
        */
        /// <summary>
        /// Depth by market glass
        /// </summary>
        int Depth { get; set; }
    }
}
