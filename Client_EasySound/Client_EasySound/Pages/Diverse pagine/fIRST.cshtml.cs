using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client_EasySound.Data;
using Client_EasySound.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client_EasySound.Pages.Diverse_pagine
{
    public class fIRSTModel : PageModel
    {
        private readonly AppDbContext _context;
        public fIRSTModel(AppDbContext context)
        {
            _context = context;
            eleClient = _context.eleclient.ToList();
        }
        public IList<client> eleClient { get; set; }
        public void OnGet()
        {

        }
    }
}
