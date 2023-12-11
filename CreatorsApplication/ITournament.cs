using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorsApplication
{
    public interface ITournament
    {
        List<string> SimulateTournament(List<string> players);
    
    }
}