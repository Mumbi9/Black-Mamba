//Youtube : Jacob Amaral
//SPY Swing Trading Algo, works best on 30 min candles
#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.Indicators;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

// This namespace holds strategies in this folder and is required. Do not change it.
namespace NinjaTrader.NinjaScript.Strategies
{
	public class SPYStratYT : Strategy
	{
		
		protected override void OnStateChange()
		{
			base.OnStateChange();

			if (State == State.SetDefaults)
			{
				IncludeTradeHistoryInBacktest             = false;
				IsInstantiatedOnEachOptimizationIteration = true;
				MaximumBarsLookBack                       = MaximumBarsLookBack.TwoHundredFiftySix;
				Name                                      = "SPYStratYT";
				SupportsOptimizationGraph                 = false;
			}
			else if (State == State.Configure)
			{
				SetProfitTarget(CalculationMode.Percent, 0.02);
				SetStopLoss(CalculationMode.Percent, 0.01);
			}
			else if (State == State.DataLoaded)
			{
				AddChartIndicator(RelativeVigorIndex(10));
			}
		}

		protected override void OnBarUpdate()
		{
			base.OnBarUpdate();

			if (CurrentBars[0] < BarsRequiredToTrade)
				return;

			if (CrossAbove(RelativeVigorIndex(10), -0.214, 1))
				EnterLong();
		}
	}
}
