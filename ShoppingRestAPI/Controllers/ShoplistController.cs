using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingRestAPI.ShopModel;

namespace WorkBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoplistController : ControllerBase
    {
        private readonly ShoppingDBContext db = new ShoppingDBContext();
        string correctKey; 
      
        // GET: api/Shoplist
        [HttpGet("{keycode}")]
        public ActionResult GetShoplist(string keycode)
        {
            correctKey = DateTime.UtcNow.ToString("dd") + "abc";

            if (keycode == correctKey)
            {
                var lista = db.Shoplists.ToList();
                return Ok(lista);
            }
            else {
                return Unauthorized();
            }
        }
      

        // POST: api/Shoplist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{keycode}")]
        public ActionResult PostShoplist(string keycode, [FromBody] Shoplist tavara)
        {
            try
            {
                correctKey = DateTime.UtcNow.ToString("dd") + "abc";

                if (keycode != correctKey)
                {
                    return Unauthorized();
                }

                db.Shoplists.Add(tavara);
                db.SaveChanges();

                return Ok("successfully added" + tavara);

            }
            catch (Exception ex) {
                return BadRequest(ex.InnerException);
            }
        }

        // DELETE: api/Shoplist/5
        [HttpDelete("{id}/{keycode}")]
        public ActionResult DeleteShoplist(int id, string keycode)
        {

            correctKey = DateTime.UtcNow.ToString("dd") + "abc";

            if (keycode != correctKey) {

                return Unauthorized();
            }

            var shoplist = db.Shoplists.Find(id);
            if (shoplist == null)
            {
                return NotFound("Annetulla id:llä ei löydy tavaraa");
            }

            db.Shoplists.Remove(shoplist);
            db.SaveChanges();

            return Ok(shoplist.Item + " poistettu");
        }

     
    }
}
