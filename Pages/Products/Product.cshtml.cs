using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M05_UF3_P2_Template.App_Code.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M05_UF3_P2_Template.Pages.Products
{
    public class ProductModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public int Game_Id { get; set; }
        [BindProperty]
        public Product product { get; set; }
        [BindProperty]
        public Game gamee { get; set; }

        public bool isGame { get; set; }

        public void OnGet()
        {
            //if (!isGame) product.Type = Product.TYPE.VIDEO;
            if (Id > 0)
            {
                product = new Product(Id);
                if (product.Type == Product.TYPE.GAME)
                {
                    try
                    {
                        Game_Id = (int)DatabaseManager.Select("Game", new string[] { "Id" }, "Product_Id = " + Id + "").Rows[0][0];
                    } catch
                    {
                        DatabaseManager.DB_Field[] fields = new DatabaseManager.DB_Field[]
                        {
                                new DatabaseManager.DB_Field("Product_Id", Id),
                                new DatabaseManager.DB_Field("Rating", null),
                                new DatabaseManager.DB_Field("Version", null)
                        };
                        Game_Id = (int)DatabaseManager.Insert("Game", fields);
                    }
                }
            }
        }
        public void OnPost()
        {
            product.Id = Id;
            if (Id > 0)
            {
                product.Update();
            }
            else
            {
                product.Add();
                Id = (int)DatabaseManager.Scalar("Product", DatabaseManager.SCALAR_TYPE.MAX, new string[] { "Id" }, "");
                OnGet();
            }
        }
    }
}
