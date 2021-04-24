using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TruYumOrderApiWithAzure.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using TruYumOrderApiWithAzure.Repository;
using Microsoft.AspNetCore.Authorization;

namespace TruYumOrderApiWithAzure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        public IOrderRepository _orderRepository { get; set; }
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        //[HttpPost]
        //public async Task<IActionResult> Order([FromBody]int MenuId)
        //{
        //    Cart cart = new Cart();
        //    cart.Id = 1;
        //    cart.UserId = 1;
        //    cart.MenuItemId = MenuId;
        //    var token = CreateJwtToken();
        //    using(HttpClient client=new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //        using(var response =await client.GetAsync($"https://localhost:5001/api/menuItem{MenuId}"))
        //        {
        //            var apiResponse=await response.Content.ReadAsStringAsync();
        //            string Name=JsonConvert.DeserializeObject<string>(apiResponse);
        //            cart.MenuItemName = Name;
        //            return CreatedAtRoute("Default", new { Id = cart.Id }, cart);
        //        }
        //    }
            
        //}
        
        [HttpPost]
        public async Task<IActionResult> Order([FromBody] Cart cart)
        {
            var token = CreateJwtToken();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                using (var response = await client.GetAsync("https://localhost:5001/api/MenuItem/" + cart.MenuItemId))
                {
                    if(response.IsSuccessStatusCode)
                    { 
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        cart.MenuItemName = apiResponse;
                        cart = _orderRepository.AddToCart(cart);
                        return Created("default", cart);
                    }
                }
            }
            return NotFound();
        }

        [HttpGet("Cart")]
        public IActionResult Cart()
        {
            List<Cart> li;
            li=_orderRepository.ShowCart();
            return Ok(li);
        }

        private string CreateJwtToken()
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretkeyarespecial"));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                issuer: "mySystem",
                audience: "myUsers",
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: Credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
