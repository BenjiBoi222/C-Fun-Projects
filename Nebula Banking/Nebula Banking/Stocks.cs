using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula_Banking
{
    class Stocks
    {
        public string StockName { get; set;}
        public int AvailableStocks { get; set;}
        public double StockPricePerPiece { get; set;}

        /// <summary>
        /// Displays a specific stocks full information in separated lines
        /// </summary>
        public void DisplaySpecificStockInfo()
        {
            Console.WriteLine($"Stock name: {StockName}");
            Console.WriteLine($"Available stocks: {AvailableStocks}");
            Console.WriteLine($"One stock price: {StockPricePerPiece}");
        }

        /// <summary>
        /// Displays a stock full information in one line
        /// </summary>
        public void DisplayStock()
        {
            Console.WriteLine($"Stock name: {StockName} | Available stock: {AvailableStocks} | Stock price/piece: {StockPricePerPiece}"); 
        }
    }
}