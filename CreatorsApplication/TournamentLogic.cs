using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CreatorsApplication.TournamentLogic;

namespace CreatorsApplication
{
    public class TournamentLogic : ITournamentLogic
    {
        // ITournamentLogic.cs
        public interface ITournamentLogic
        {
            //List<string> SimulateTournament(List<string> players);
        }

            public List<string> SimulateTournament(List<string> players)
            {
            // Turnauksen logiikka täällä
            return players;
            }
        }
    }