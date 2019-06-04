using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGLCodingExercise.Domain.Abstractions;
using AGLCodingExercise.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AGLCodingExercise.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPetsService _petsService;
        public IList<PetsGroupingByOwnerGender> Groupings;

        public IndexModel(IPetsService petsService)
        {
            _petsService = petsService;
        }

        public async Task OnGetAsync()
        {
            Groupings = await _petsService.GetCatsGroupedByOwnerGender();
        }
    }
}
