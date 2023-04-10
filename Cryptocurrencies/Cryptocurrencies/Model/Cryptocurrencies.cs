using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrencies.Model
{
    public class Cryptocurrencies : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private int rank;
        public int Rank
        {
            get { return rank; }
            set
            {
                if (rank != value)
                {
                    rank = value;
                    OnPropertyChanged(nameof(Rank));
                }
            }
        }

        private string symbol;
        public string Symbol
        {
            get { return symbol; }
            set
            {
                if (symbol != value)
                {
                    symbol = value;
                    OnPropertyChanged(nameof(Symbol));
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private decimal? supply;
        public decimal? Supply
        {
            get { return supply; }
            set
            {
                if (supply != value)
                {
                    supply = value;
                    OnPropertyChanged(nameof(Supply));
                }
            }
        }

        private decimal? maxSupply;
        public decimal? MaxSupply
        {
            get { return maxSupply; }
            set
            {
                if (maxSupply != value)
                {
                    maxSupply = value;
                    OnPropertyChanged(nameof(MaxSupply));
                }
            }
        }

        private decimal? marketCapUsd;
        public decimal? MarketCapUsd
        {
            get { return marketCapUsd; }
            set
            {
                if (marketCapUsd != value)
                {
                    marketCapUsd = value;
                    OnPropertyChanged(nameof(MarketCapUsd));
                }
            }
        }

        private decimal? volumeUsd24Hr;
        public decimal? VolumeUsd24Hr
        {
            get { return volumeUsd24Hr; }
            set
            {
                if (volumeUsd24Hr != value)
                {
                    volumeUsd24Hr = value;
                    OnPropertyChanged(nameof(VolumeUsd24Hr));
                }
            }
        }

        private decimal priceUsd;
        public decimal PriceUsd
        {
            get { return priceUsd; }
            set
            {
                if (priceUsd != value)
                {
                    priceUsd = value;
                    OnPropertyChanged(nameof(PriceUsd));
                }
            }
        }

        private decimal? changePercent24Hr;
        public decimal? ChangePercent24Hr
        {
            get { return changePercent24Hr; }
            set
            {
                if (changePercent24Hr != value)
                {
                    changePercent24Hr = value;
                    OnPropertyChanged(nameof(ChangePercent24Hr));
                }
            }
        }

        private decimal? vwap24Hr;
        public decimal? Vwap24Hr
        {
            get { return vwap24Hr; }
            set
            {
                if (vwap24Hr != value)
                {
                    vwap24Hr = value;
                    OnPropertyChanged(nameof(Vwap24Hr));
                }
            }
        }

        private string explorer;
        public string Explorer
        {
            get { return explorer; }
            set
            {
                if (explorer != value)
                {
                    explorer = value;

                    OnPropertyChanged(nameof(Explorer));
                }
            }
        }
    }
}
