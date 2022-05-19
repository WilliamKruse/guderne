using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using festivalprojekt.Shared.Models;

namespace festivalprojekt.Server.Models
{

    //laver interfacet med metoder til klasserne
    public interface IPersonRepositoryDapper
    {
       List<PersonDTO> HentAllePersoner();

       void OpdaterPerson(PersonDTO NyPerson);

       void OpretPerson(PersonDTO NyPerson);
     

    }
}