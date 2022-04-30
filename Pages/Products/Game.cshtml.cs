using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using M05_UF3_P2_Template.App_Code.Model;

namespace M05_UF3_P2_Template.Pages.Products
{
    public class GameModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        public Game game { get; set; }
        public void OnGet()
        {
            if (Id > 0)
            {
                game = new Game(Id);
            }
        }
        public void OnPost()
        {
            game.Id = Id;
            if (Id > 0)
            {
                game.Update();
            }
            else
            {
                game.Add();
                Id = (int)DatabaseManager.Scalar("Game", DatabaseManager.SCALAR_TYPE.MAX, new string[] { "Id" }, "");
                OnGet();
            }
        }
    }
}
